using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Button3D : MonoBehaviour {
    [SerializeField] private Renderer _textureRend;
    [SerializeField] private Material _clickedMaterial;
    [SerializeField] private Material _unclickedMaterial;
    [SerializeField] private float _clickDist = .5f;
    private Renderer _rend;
    
    public UnityEvent ButtonDownInteraction;
    public UnityEvent ButtonUpInteraction;
    public UnityEvent ButtonEnteredInteraction;
    public UnityEvent BUttonExitedInteraction;
    
    // Start is called before the first frame update
    void Start() {
        _rend = gameObject.GetComponent<Renderer>();
    }

    public void SetTextureMaterial(Material mat) {
        if (_textureRend)
            _textureRend.material = mat;
    }

    private void OnMouseDown() {
        if (_textureRend)
            _textureRend.enabled = false;
        transform.Translate(transform.forward * _clickDist);
        _rend.material = _clickedMaterial;
        
        ButtonDownInteraction?.Invoke();
    }

    private void OnMouseUp() {
        if (_textureRend)
            _textureRend.enabled = true;
        transform.Translate(transform.forward * -_clickDist);
        _rend.material = _unclickedMaterial;
        
        ButtonUpInteraction?.Invoke();
    }

    private void OnMouseEnter() {
        ButtonEnteredInteraction?.Invoke();
    }

    private void OnMouseExit() {
        BUttonExitedInteraction?.Invoke();
    }
}
