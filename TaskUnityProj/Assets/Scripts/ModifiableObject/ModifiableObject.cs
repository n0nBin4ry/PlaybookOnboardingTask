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
        
        _subjectCollider.enabled = false;
        _modifiers.gameObject.SetActive(true);
    }

    public void Deselect() {
        if (!_isSelected) return;
        _isSelected = false;
        
        _subjectCollider.enabled = true;
        _modifiers.gameObject.SetActive(false);
    }
}
