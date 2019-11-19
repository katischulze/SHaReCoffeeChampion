using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareInput : MonoBehaviour, IShareInput {

    private Communication _communication;

    private CircularBuffer<float> _activityBuffer;

    private int _activitiesPerSecond = 10;

    private float _activityMeasure;

    private float _forceIdleTime;

    private bool _initialCalibrationDone = false;

    private bool _pickedUp = false;

    private float _maxForce = 3986;

    private float _maxAppliedForce;

    public Vector3 GetAccelerationRaw()
    {
        return _communication.RawAcceleration;
    }

    public float GetForce()
    {
        return _communication.force;
    }

    public Quaternion GetRotation()
    {
        return Quaternion.Euler(0, 270, 0) * _communication.rotQuat;
    }

    public float GetTiltAngle()
    {
        return Vector3.Angle(Vector3.up, _communication.RawAcceleration);
    }

    public bool IsPickedUp()
    {
        if (!_pickedUp)
        {
            
            _pickedUp = _initialCalibrationDone && RecentActivitySum() > 1250 && GetForce() > 50;
        } else
        {
            _pickedUp = ((RecentActivitySum() > 1250) || !(_forceIdleTime > 0.1));
            if (!_pickedUp && GetTiltAngle() > 20)
                _pickedUp = true;
        }

        return _pickedUp;
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
        Communication c = GetComponent<Communication>();
        if (c != null)
            _communication = c;
        else
        {
            c = FindObjectOfType<Communication>();
            if (c != null)
                _communication = c;
            else
                _communication = gameObject.AddComponent<Communication>();
        }
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(ActivityLogger(_activitiesPerSecond));
        StartCoroutine(WaitForInitialization());
	}
	
	// Update is called once per frame
	void Update () {
        if (GetForce() < 50)
        {
            _forceIdleTime += Time.deltaTime;
        } else
        {
            _forceIdleTime = 0;
        }

        float force = GetForce();
        if (force > _maxAppliedForce)
            _maxAppliedForce = force;
	}

    IEnumerator WaitForInitialization()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            if(RecentActivitySum() < 2000)
            {
                _initialCalibrationDone = true;
                Debug.Log("Initialized device");
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator ActivityLogger(int timesPerSecond)
    {
        _activityBuffer = new CircularBuffer<float>(timesPerSecond);
        float waitTime = 0.5f / timesPerSecond;
        Quaternion lastRotation = GetRotation();
        Vector3 lastAcceleratin = GetAccelerationRaw();
        while (true)
        {
            yield return new WaitForSecondsRealtime(waitTime);
            float activity = Quaternion.Angle(lastRotation, GetRotation()) + (lastAcceleratin - GetAccelerationRaw()).magnitude;
            _activityBuffer.Push(activity);
            lastRotation = GetRotation();
            lastAcceleratin = GetAccelerationRaw();
        }
    }

    private float RecentActivitySum()
    {
        float sum = 0;
        foreach(float a in _activityBuffer)
        {
            sum += a;
        }
        return sum;
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
