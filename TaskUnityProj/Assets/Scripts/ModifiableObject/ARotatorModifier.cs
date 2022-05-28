using UnityEngine;

public class ARotatorModifier : AModifier {
    private Plane _plane;

    protected override void SetClickOn(Vector3 clickPos) {
        // since we will be rotating around our up-norm, we will be projecting our change vector onto a plane at our
        // origin that's normal is parallel to our up-norm
        _plane = new Plane(transform.up, transform.position);
        
        // project mouse click onto the plane we rotate on to get the start of our change vector
        var ray = _cam.ScreenPointToRay(clickPos);
        float dist;
        if (_plane.Raycast(ray, out dist)) {
            _startPos = ray.GetPoint(dist);
            _isClicked = true;
        }
    }

    protected override void TickClickHeld(Vector3 clickPos) {
        // project mouse click onto the plane we rotate on to get the end position of our change vec
        var ray = _cam.ScreenPointToRay(clickPos);
        float dist;
        Vector3 projClickPos;
        if (_plane.Raycast(ray, out dist)) {
            projClickPos = ray.GetPoint(dist);
            
            // rotate subject around our up axis, based around the angle between the start and end points of the change
            var ourPos = transform.position;
            Vector3 startVec = _startPos - ourPos;
            Vector3 endVec = projClickPos - ourPos;
            var ourUp = transform.up;
            float angle = Vector3.SignedAngle(startVec, endVec, ourUp);
            SubjectTransform.Rotate(ourUp, angle,Space.World);
            
            // update the start position to keep from continuous spinning
            _startPos = projClickPos;
        }
    }
}
