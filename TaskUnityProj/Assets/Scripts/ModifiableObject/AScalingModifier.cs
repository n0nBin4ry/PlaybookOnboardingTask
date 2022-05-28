using UnityEngine;

public class AScalingModifier : AModifier {
    private Plane _plane;

    protected override void SetClickOn(Vector3 clickPos) {
        // we will project our change vector on a plane at our origin with the camera's normal
        _plane = new Plane(_cam.transform.forward, transform.position);
        
        // get our starting position of the change vector
        var ray = _cam.ScreenPointToRay(clickPos);
        float dist;
        if (_plane.Raycast(ray, out dist)) {
            _startPos = ray.GetPoint(dist);
            _isClicked = true;
        }
    }

    protected override void TickClickHeld(Vector3 clickPos) {
        // project current mouse position onto plane to get the end of the change vector
        var ray = _cam.ScreenPointToRay(clickPos);
        float dist;
        
        if (_plane.Raycast(ray, out dist)) {
            Vector3 projClickPos = ray.GetPoint(dist);
            
            // project change vector along our up-normal to get the vector that we will be scaling with, then scale
            var ourUp = transform.up;
            float projMag = Vector3.Dot((projClickPos - _startPos), ourUp);
            var scaleVec = projMag * ourUp;
            
            // adjusting local scale needs us to take scale vec from world space to local space of subject
            var scaleVecToParentLocal = SubjectTransform.worldToLocalMatrix * scaleVec;
            var newScale = SubjectTransform.localScale +
                           new Vector3(scaleVecToParentLocal.x, scaleVecToParentLocal.y, scaleVecToParentLocal.z);

            SubjectTransform.localScale = newScale;

            // update the start position to keep from continuous scaling
            _startPos = projClickPos;
        }
    }
}
