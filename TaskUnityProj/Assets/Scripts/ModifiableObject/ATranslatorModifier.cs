using UnityEngine;

public class ATranslatorModifier : AModifier {
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
        // project current mouse position onto same plane as our start position
        var ray = _cam.ScreenPointToRay(clickPos);
        float dist;
        if (_plane.Raycast(ray, out dist)) {
            Vector3 projClickPos = ray.GetPoint(dist);
            
            // project change vector along our up-normal to get the vector that we will be translating with, then translate
            var ourUp = transform.up;
            var transVec = Vector3.Dot((projClickPos - _startPos), ourUp) * ourUp;
            SubjectTransform.position = SubjectTransform.position + transVec;
            
            // update the start position to keep from continuous moving
            _startPos = projClickPos;
        }
    }
}
