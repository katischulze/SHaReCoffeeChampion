using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CupMovement : MonoBehaviour {

    IShareInput _shareInput;

    public Transform _activeChild;

    public bool LockMovement;

    private Vector3 _activeChildLocalPositionBeforePickUp;
    private Quaternion _activeChildLocalRotationBeforePickup;
    private Transform _activeChildParentBeforePickUp;
    

    private void Awake()
    {
        _shareInput = ShareInputManager.ShareInput;
        if(transform.childCount > 0)
            _activeChild = transform.GetChild(0);
    }

    void Update()
    {
        if (_activeChild != null)
        {
            if (!LockMovement)
            {
                _activeChild.localRotation = Quaternion.Lerp(_activeChild.localRotation, _shareInput.GetRotation(), 10 * Time.deltaTime);
            }
            else
            {
                _activeChild.localRotation = Quaternion.Lerp(_activeChild.localRotation, Quaternion.identity, 10 * Time.deltaTime);
            }
        } else if(transform.childCount > 0)
        {
            PickUpObject(transform.GetChild(0).gameObject);
            Debug.Log("No active child");
        }
    }

    public void PickUpObject(GameObject objectToHold, Action onFinish = null)
    {
        Vector3 lastPosition = transform.position;
        if (_activeChild != null)
            lastPosition = _activeChild.position;

        PutDownObject();

        _activeChildParentBeforePickUp = objectToHold.transform.parent;
        _activeChildLocalPositionBeforePickUp = objectToHold.transform.position;
        _activeChildLocalRotationBeforePickup = objectToHold.transform.rotation;
        _activeChild = objectToHold.transform;
        _activeChild.transform.parent = transform;

        LockMovement = false;

        Coroutines.AnimatePosition(_activeChild.gameObject, lastPosition, this, true, onFinish);
    }

    public void PutDownObject(Action onFinish = null)
    {
        if (_activeChild != null)
        {
            _activeChild.parent = _activeChildParentBeforePickUp;
            Coroutines.AnimatePosition(_activeChild.gameObject, _activeChildLocalPositionBeforePickUp, this, false, onFinish);
            Coroutines.AnimateRotation(_activeChild.gameObject, _activeChildLocalRotationBeforePickup, this);
            _activeChild = null;
        }
    }

    public void PutAway(Vector3 destination, Action onFinish = null)
    {
        if(_activeChild != null)
        {
            Coroutines.AnimatePosition(_activeChild.gameObject, destination, this, true, onFinish);
            Coroutines.AnimateRotation(_activeChild.gameObject, Quaternion.identity, this);
            _activeChild.transform.parent = null;
            _activeChild = null;
        }
    }
}
