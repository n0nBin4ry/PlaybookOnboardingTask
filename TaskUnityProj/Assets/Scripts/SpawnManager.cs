using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    // manually assign here so that we don't need to manually add the reference in editor to every object
    // the spawner will efficiently feed the Select Manager into everything spawned w/out a singleton
    [SerializeField] private SelectionManager _selectManager;
    
    // button used for spawning, the reference here is for extra visual feedback as well
    [SerializeField] Button3D _spawnButton;
    // distance from camera that a spawned object should start at when dragged into world space from button
    [SerializeField] private float _spawnDist = 5f;
    private Camera _cam;
    
    // list of data needed for spawning different objects
    [SerializeField] private List<SpawnObjectData> _spawnDataList;
    private int _dataInd = 0;
    
    private GameObject _heldObject = null;
    private bool _cancelSpawn = true;
    
    void Start() {
        _cam = Camera.main;
        if (_spawnDataList.Count > 0)
            _spawnButton.SetTextureMaterial(_spawnDataList[_dataInd].ButtonImageMat);
    }
    
    // on update we drag any held item in the world space
    void Update() {
        if (_heldObject) {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);
            _heldObject.transform.position = ray.GetPoint(_spawnDist);
        }
    }
    
    // rotates spawnable object to next one in list; update the texture of spawn button to show user what type of object is selected
    public void NextObject() {
        if (_spawnDataList.Count > 0) {
            _dataInd = (_dataInd + 1) % _spawnDataList.Count;
            _spawnButton.SetTextureMaterial(_spawnDataList[_dataInd].ButtonImageMat);
        }
    }
    
    public void PrevObject() {
        if (_spawnDataList.Count > 0) {
            _dataInd--;
            if (_dataInd < 0) {
                _dataInd += _spawnDataList.Count;
            }
            _spawnButton.SetTextureMaterial(_spawnDataList[_dataInd].ButtonImageMat);
        }
    }
    
    // these methods are for if we drag the held object back onto the button, cancelling the spawn
    public void ObjToReturn() { _cancelSpawn = true; }
    public void ObjToStay() { _cancelSpawn = false; }
    
    // we release any held object, either canceling or finalizing the spawn
    public void ReleaseObj() {
        if (!_heldObject) return;

        if (_cancelSpawn)
            Destroy(_heldObject);
        else {
            var modObjComp = _heldObject.GetComponentInChildren<ModifiableObject>();
            modObjComp.SetSelectionManager(_selectManager);
            _selectManager.SelectObject(modObjComp);
        }

        _heldObject = null;
    }
    
    // create the object we are currently at on our list, and have it be "held" by user
    public void SpawnObject() {
        if (_spawnDataList.Count <= 0 || _heldObject) return;

        _heldObject = GameObject.Instantiate(_spawnDataList[_dataInd].PlaceableObjectPrefab, null);
    }
}
