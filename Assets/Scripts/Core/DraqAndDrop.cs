using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// Simple drag and drop script for gameobjects
/// </summary>
public class DraqAndDrop : MonoBehaviour
{

    private Vector3 _dragOffset;
    private Camera _camera;
    private GameObject _snappedObject;
    private bool _isSnapped = false;
    private Vector3 _originalPosition;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnMouseDown()
    {
        _originalPosition = transform.position;
        _dragOffset = transform.position - GetMousePosition();
    }

    private void OnMouseUp()
    {
        if(_isSnapped)
        {
            transform.position = _snappedObject.transform.position;
            transform.localPosition -= new Vector3(0, 0, 1);
        }
        else
        {
            transform.position = _originalPosition;
        }

    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePosition() + _dragOffset;
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

}
