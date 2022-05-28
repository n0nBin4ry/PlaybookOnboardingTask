using UnityEngine;

public class ARotatorModifier : AModifier {
    public string DEBUG_DIR;

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
            //Debug.Log("Starting rotation of " + DEBUG_DIR);
        }
    }

    protected override void TickClickHeld(Vector3 clickPos) {
        // update plane before raycast
        //_plane = new Plane(transform.up, transform.parent.position);
        
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

    // private void CreateWireMesh() {
    //     var line = gameObject.AddComponent<LineRenderer>();
    //     // create circle
    //     var segments = 360;
    //     float lineWidth = .1f;
    //     float radius = .5f;
    //     line.useWorldSpace = false;
    //     line.startWidth = lineWidth;
    //     line.endWidth = lineWidth;
    //     line.positionCount = segments + 1;
    //
    //     var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
    //     var points = new Vector3[pointCount];
    //
    //     for (int i = 0; i < pointCount; i++)
    //     {
    //         var rad = Mathf.Deg2Rad * (i * 360f / segments);
    //         points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
    //     }
    //
    //     line.SetPositions(points);
    //     
    //     // create a mesh out of the circle
    //     var newMesh = new Mesh();
    //     line.BakeMesh(newMesh, false);
    //     gameObject.GetComponent<MeshFilter>().mesh = newMesh;
    //     
    //     Destroy(line);
    // }
}
