using UnityEngine;

/* Used to keep only one object "selected" and showing gimble controls at a time.
 * May seem like an extra step, but it is to prevent having every object check for every click that
 * doesn't involve them.
 */
public class SelectionManager : MonoBehaviour {
    private ModifiableObject _currobject;
    
    // selects current object and deselects any previously selected
    public void SelectObject(ModifiableObject obj) {
        if (_currobject)
            _currobject.Deselect();
        
        if (!obj) return;
        _currobject = obj;
        _currobject.Select();
    }
}
