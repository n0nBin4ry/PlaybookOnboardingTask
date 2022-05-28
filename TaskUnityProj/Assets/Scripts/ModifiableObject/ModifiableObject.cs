using UnityEngine;

public class ModifiableObject : MonoBehaviour {
    [SerializeField] Transform _modifiers;
    private Collider _subjectCollider;
    private SelectionManager _selectionManager;
    private bool _isSelected;
    
    // Start is called before the first frame update
    void Start() {
        _subjectCollider = GetComponent<Collider>();
        _isSelected = true;
        Deselect();
    }

    public void SetSelectionManager(SelectionManager manager) {
        _selectionManager = manager;
    }

    private void OnMouseDown() {
        if (_isSelected || !_selectionManager) return;
        _selectionManager.SelectObject(this);
    }
    
    public void Select() {
        if (_isSelected) return;
        _isSelected = true;
        
        // when selected, we no longer need our colliders to click ourselves; this also allows us to click objects behind
        _subjectCollider.enabled = false;
        _modifiers.gameObject.SetActive(true);
    }

    public void Deselect() {
        if (!_isSelected) return;
        _isSelected = false;
        
        // when deselected, we don't need our modifying gimbal controls to clutter up the screen; also need to be clickable again 
        _subjectCollider.enabled = true;
        _modifiers.gameObject.SetActive(false);
    }
}
