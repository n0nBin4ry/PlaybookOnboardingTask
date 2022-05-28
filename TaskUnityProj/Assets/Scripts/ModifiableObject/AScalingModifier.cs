using UnityEngine;

public class AScalingModifier : AModifier
{
    public string DEBUG_DIR;

    private Plane _plane;
    // private float _reverseWeight;
    // private bool _flipped;

    protected override void SetClickOn(Vector3 clickPos) {
        // we will project our change vector on a plane at our origin with the camera's normal
        _plane = new Plane(_cam.transform.forward, transform.position);
        
        // get our starting position of the change vector
        var ray = _cam.ScreenPointToRay(clickPos);
        float dist;
        if (_plane.Raycast(ray, out dist)) {
            _startPos = ray.GetPoint(dist);
            _isClicked = true;
            // _reverseWeight = 1f;
            // _flipped = false;
            //Debug.Log("Starting  of " + DEBUG_DIR);
        }
    }

    protected override void TickClickHeld(Vector3 clickPos) {
        // project current mouse position onto plane to get the end of the change vector
        var ray = _cam.ScreenPointToRay(clickPos);
        float dist;
        Vector3 projClickPos;
        if (_plane.Raycast(ray, out dist)) {
            projClickPos = ray.GetPoint(dist);
            
            // project change vector along our up-normal to get the vector that we will be scaling with, then scale
            var ourUp = transform.up;
            float projMag = Vector3.Dot((projClickPos - _startPos), ourUp);
            var scaleVec = projMag * ourUp;
            
            // adjusting local scale needs us to take scale vec from world space to local space of subject
            var scaleVecToParentLocal = SubjectTransform.worldToLocalMatrix * scaleVec;
            var newScale = SubjectTransform.localScale +
                           new Vector3(scaleVecToParentLocal.x, scaleVecToParentLocal.y, scaleVecToParentLocal.z);
            
            // if (projMag < 0) {
            //     var diffScaleVec = newScale - SubjectTransform.localScale;
            //     var temp = SubjectTransform.worldToLocalMatrix * ourUp;
            //     var ourLocalUp = new Vector3(temp.x, temp.y, temp.z);
            //     if (Vector3.Dot(diffScaleVec, ourLocalUp) < 0) {
            //         _reverseWeight *= -1;
            //         _flipped = true;
            //
            //         projMag *= _reverseWeight;
            //         scaleVec = projMag * ourUp;
            //
            //         // adjusting local scale needs us to take scale vec from world space to local space of subject
            //         scaleVecToParentLocal = SubjectTransform.worldToLocalMatrix * scaleVec;
            //         newScale = SubjectTransform.localScale +
            //                        new Vector3(scaleVecToParentLocal.x, scaleVecToParentLocal.y, scaleVecToParentLocal.z);
            //     }
            // }

            SubjectTransform.localScale = newScale;

            // update the start position to keep from continuous scaling
            _startPos = projClickPos;
        }
    }
}
