using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// Extends DragAndDrop to include snapping onto objects
/// </summary>

public class DragAndDropStack : MonoBehaviour
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
            Destroy(this.gameObject);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(this.gameObject.name + " colliding with " + collision.gameObject.name);

        if (collision.gameObject.tag == "Snapables")
        {
            _isSnapped = true;
            _snappedObject = collision.gameObject;
        }
        else
        {
            _isSnapped = false;
            _snappedObject = null;
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        _isSnapped = false;
        _snappedObject = null;
    }

}
