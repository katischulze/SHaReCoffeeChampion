using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressCoffee : MonoBehaviour {

    int rotationDirection = 1;
    float amtToRotate = 20;

    Vector3 startPos;

    bool firstTimeRed = true;


	// Use this for initialization
	void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward, amtToRotate * rotationDirection * Time.deltaTime);
        if ((transform.rotation.eulerAngles.z >= 10.0f && transform.rotation.eulerAngles.z <= 340.0f) 
            || (transform.rotation.eulerAngles.z <= 350.0f && transform.rotation.eulerAngles.z >= 20.0f))
        {
			rotationDirection *= -1;
        }

        if (ProgressBarScript.isRed())
        {
            this.gameObject.GetComponent<Rigidbody2D>().simulated = true;
            if (firstTimeRed)
            {
                SoundEffectManager.Instance.PlayOpenMenu();
                firstTimeRed = false;
            }
        }

        if (ProgressBarScript.isZero())
        {
            //Debug.Log("isZero");
            this.gameObject.GetComponent<Rigidbody2D>().simulated = false;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            transform.position = startPos;
            firstTimeRed = true;

        }

	}
}
