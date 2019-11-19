using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour, IShareInput {

    public static KeyCode squeezeKey = KeyCode.F;

    public static KeyCode pickUpPutDownKey = KeyCode.V;

    private float _force = 0;

    private float _maxForce = 3986;

    private float _maxAppliedForce;

    private Quaternion _rotation = Quaternion.identity;

    private bool _isPickedUp = false;

    public Vector3 GetAccelerationRaw()
    {
        return Vector3.zero;
    }

    public float GetForce()
    {
        return _force;
    }

    public Quaternion GetRotation()
    {
        return _rotation;
    }

    public float GetTiltAngle()
    {
        return Quaternion.Angle(Quaternion.identity, _rotation);
    }

    public bool IsPickedUp()
    {
        return _isPickedUp;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckForce();
        CheckRotation();
        CheckPickUp();
	}

    void CheckForce()
    {
        if (Input.GetKey(squeezeKey))
        {
            _force = Mathf.Clamp(_force + Time.deltaTime * 500, -5, MaxForce());
            if (_force > _maxAppliedForce)
                _maxAppliedForce = _force;
        }
        else
        {
            _force = Mathf.Clamp(_force - Time.deltaTime * 2000, -5, MaxForce());
        }
    }

    void CheckRotation()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if(horizontal != 0)
        {
            _rotation = Quaternion.RotateTowards(_rotation, Quaternion.Euler(0, 0, -(horizontal / Mathf.Abs(horizontal)) * 70), Mathf.Abs(horizontal) * Time.deltaTime * 20);
        } else
        {
            _rotation = Quaternion.RotateTowards(_rotation, Quaternion.identity, Time.deltaTime * 40);
        }
    }

    void CheckPickUp()
    {
        if (Input.GetKeyDown(pickUpPutDownKey))
        {
            _isPickedUp = !_isPickedUp;
        }
    }

    public float MaxForce()
    {
        return _maxForce;
    }

    public float MaxAppliedForce()
    {
        return _maxAppliedForce;
    }

    public void ResetMaxAppliedForce()
    {
        _maxAppliedForce = 0;
    }
}
