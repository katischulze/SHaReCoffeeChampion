using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutines : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void AnimatePosition(GameObject target, Vector3 destination, MonoBehaviour caller, bool applyBottomOffset = false, Action onFinish = null)
    {
        if (applyBottomOffset)
        {
            destination += Utilities.GetBottomOffset(target);
        }
        caller.StartCoroutine(AnimatePositionCoroutine(target, destination, onFinish));
    }

    private static IEnumerator AnimatePositionCoroutine(GameObject target, Vector3 destination, Action onFinish = null, float speed = 5)
    {
        Vector3 startPos = target.transform.position;
        float startTime = Time.time;
        float interpolationValue = 0.0f;
        float timeNeeded = (startPos - destination).magnitude / speed;
        float timePassed = Time.time - startTime;
        while (timePassed < timeNeeded)
        {
            if (target == null)
                break;
            timePassed = (Time.time - startTime);
            interpolationValue = Mathf.Clamp01(timePassed / timeNeeded);
            interpolationValue = Mathf.Clamp01((Mathf.Sin((-Mathf.PI / 2) + (interpolationValue * Mathf.PI)) + 1) / 2);
            target.transform.position = Vector3.Lerp(startPos, destination, interpolationValue);
            yield return new WaitForEndOfFrame();
        }

        if(onFinish != null)
        {
            onFinish.Invoke();
        }
    }

    public static void AnimateRotation(GameObject target, Quaternion destination, MonoBehaviour caller, Action onFinish = null)
    {
        caller.StartCoroutine(AnimateRotationCoroutine(target, destination, onFinish));
    }

    private static IEnumerator AnimateRotationCoroutine(GameObject target, Quaternion destination, Action onFinish)
    {
        Quaternion startRotation = target.transform.rotation;
        float timeNeeded = 2f;
        float startTime = Time.time;
        float timePassed = 0;
        float progress = Mathf.Clamp01(timePassed / timeNeeded);
        while(progress < 1)
        {
            if (target == null)
                break;
            timePassed = Time.time - startTime;
            progress = Mathf.Clamp01(timePassed / timeNeeded);
            target.transform.rotation = Quaternion.Lerp(startRotation, destination, progress);
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Rotated!");

        if(onFinish != null)
        {
            onFinish.Invoke();
        }
    }
}
