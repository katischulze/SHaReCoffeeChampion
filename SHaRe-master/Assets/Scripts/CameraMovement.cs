using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraMovement : MonoBehaviour {

    public List<GameObject> cameraPositions;
    public float speed = 10;

    int positionCounter = 0;
    bool moveToNextPos = false;

    private bool _animationCoroutineRunning;

    public GameObject destination;

	// Update is called once per frame
	void Update () {

        if (moveToNextPos)
        {
            
            transform.position = Vector3.Lerp(transform.position, cameraPositions[positionCounter].transform.position, speed*Time.deltaTime);
            
            if((transform.position.x - cameraPositions[positionCounter].transform.position.x) < 0.1f)
            {
                moveToNextPos = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if(destination != null)
            {
                FocusOnXPosition(destination.transform.position);
            }
        }
	}

    public void switchPosition()
    {
        positionCounter = (positionCounter+1)%cameraPositions.Count;
        moveToNextPos = true;
    }

    public void FocusOnXPosition(Vector3 destination)
    {
        if (!_animationCoroutineRunning)
        {
            StartCoroutine(AnimatePositionTo(destination));
        } else
        {
            StartCoroutine(WaitForAnimation(destination));
        }
    }

    IEnumerator WaitForAnimation(Vector3 destination)
    {
        while (_animationCoroutineRunning)
        {
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(AnimatePositionTo(destination));
    }

    IEnumerator AnimatePositionTo(Vector3 destination)
    {
        _animationCoroutineRunning = true;
        Vector3 startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        float timeRequired = (destination - startPosition).magnitude / 7;
        float startTime = Time.time;
        float progress = 0;
        while(progress < 1)
        {
            float timePassed = Time.time - startTime;
            progress = Mathf.Clamp01(timePassed / timeRequired);
            progress = Mathf.Clamp01((Mathf.Sin((-Mathf.PI / 2) + (progress * Mathf.PI)) + 1) / 2);
            transform.position = new Vector3(Mathf.Lerp(startPosition.x, destination.x, progress), startPosition.y, startPosition.z);
            yield return new WaitForEndOfFrame();
        }
        _animationCoroutineRunning = false;
    }

}
