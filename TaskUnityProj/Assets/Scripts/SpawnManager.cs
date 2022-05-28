using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private SelectionManager _selectManager;
    
    [SerializeField] Button3D _spawnButton = null;
    private Camera _cam;
    
    [SerializeField] private float _spawnDist = 5f;
    
    [SerializeField] private List<SpawnObjectData> _spawnDataList;
    private int _dataInd = 0;
    
    private GameObject _heldObject = null;
    private bool _cancelSpawn = true;
    
    // Start is called before the first frame update
    void Start() {
        _cam = Camera.main;
        if (_spawnDataList.Count > 0)
            _spawnButton.SetTextureMaterial(_spawnDataList[_dataInd].ButtonImageMat);
    }

    // Update is called once per frame
    void Update() {
        if (_heldObject) {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);
            _heldObject.transform.position = ray.GetPoint(_spawnDist);
        }
    }
    
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

    public void ObjToReturn() { _cancelSpawn = true; }
    
    public void ObjToStay() { _cancelSpawn = false; }

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

    public void SpawnObject() {
        if (_spawnDataList.Count <= 0 || _heldObject) return;

        _heldObject = GameObject.Instantiate(_spawnDataList[_dataInd].PlaceableObjectPrefab, null);
    }
}
