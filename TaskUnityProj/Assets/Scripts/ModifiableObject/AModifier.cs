using UnityEngine;

/* Modifiers are in charge of manipulating their given "subjects." 
 * They are all interacted with by clicking and dragging, with different behaviors per derived class to match
 * gimbal controls.
 */

public abstract class AModifier : MonoBehaviour {
    // the transform of the subject we will be modifying
    public Transform SubjectTransform;
    
    // change material on interaction to give user feedback
    [SerializeField] Material _unclickedMat;
    [SerializeField] Material _clickedMat;
    protected MeshRenderer _rend;
    
    protected bool _isClicked = false;
    protected Vector3 _startPos = Vector3.zero;
    protected Camera _cam;

    // Start is called before the first frame update
    protected virtual void Start() {
        _cam = Camera.main;
        _rend = GetComponent<MeshRenderer>();
        _rend.material = _unclickedMat;
    }

    // Update is called once per frame
    protected virtual void Update() {
        
        // when clicked, then we are modifying our subject
        if (_isClicked) {
            TickClickHeld(Input.mousePosition);
            if (Input.GetMouseButtonUp(0))
                ClickOff();
        }
    }

    protected virtual void SetClickOn(Vector3 clickPos) {
        
    }

    protected virtual void TickClickHeld(Vector3 clickPos) {
        
    }

    protected virtual void ClickOff() {
        _isClicked = false;
        _rend.material = _unclickedMat;
    }

    public void OnMouseDown() {
        SetClickOn(Input.mousePosition);
    }
    
    // modifiers will "light-up" when hovered over and/or clicked
    private void OnMouseEnter() {
        if (!Input.GetMouseButton(0))
            _rend.material = _clickedMat;
    }

    // and "dim" when not hovered over
    private void OnMouseExit() {
        if (!_isClicked) {
            _rend.material = _unclickedMat;
        }
    }
}
