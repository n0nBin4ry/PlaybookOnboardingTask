using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Button3D : MonoBehaviour {
    // texture of a quad used as a user-facing display of the button
    [SerializeField] private Renderer _textureRend;
    // change material on interaction to give user feedback
    [SerializeField] private Material _clickedMaterial;
    [SerializeField] private Material _unclickedMaterial;
    // distance the button will be pressed into the screen, for user feedback as well
    [SerializeField] private float _clickDist = .5f;
    private Renderer _rend;
    
    public UnityEvent ButtonDownInteraction;
    public UnityEvent ButtonUpInteraction;
    public UnityEvent ButtonEnteredInteraction;
    public UnityEvent BUttonExitedInteraction;
    
    void Start() {
        _rend = gameObject.GetComponent<Renderer>();
    }

    public void SetTextureMaterial(Material mat) {
        if (_textureRend)
            _textureRend.material = mat;
    }

    private void OnMouseDown() {
        // click visual-feedback before triggering events
        if (_textureRend)
            _textureRend.enabled = false;
        transform.Translate(transform.forward * _clickDist);
        _rend.material = _clickedMaterial;
        
        ButtonDownInteraction?.Invoke();
    }

    private void OnMouseUp() {
        // click visual-feedback before triggering events
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
