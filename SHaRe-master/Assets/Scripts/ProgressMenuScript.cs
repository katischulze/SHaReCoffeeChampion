using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressMenuScript : MonoBehaviour {
    
    //public Text recepy;

    public float val;

    public static void setProgressVal(float value) {
        ProgressBarScript.value = value;
    }

    public static void setColorKeys(float yellowEnd, float greenEnd, float orangeEnd) {
        ProgressBarScript.yellowEnd = yellowEnd;
        ProgressBarScript.greenEnd = greenEnd;
        ProgressBarScript.orangeEnd = orangeEnd;
    }

    //public void setRecepyText(string text) {
    //    recepy.text = text;
    //}

	// Use this for initialization
	void Start () {
        setColorKeys(0.8f, 1.0f, 1.1f);
	}
	
	// Update is called once per frame
	void Update () {
        setProgressVal(val);
	}
}
