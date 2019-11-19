using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarScript : MonoBehaviour {

    public Slider progressVisObj;
    public Image progressFill;
    public static float value, yellowEnd, greenEnd, orangeEnd; //Value given in %;	
    
	// Update is called once per frame
	void Update () {
        if (value < 0) value = 0;
        if (value > orangeEnd) {
            progressVisObj.value = 1;
            progressFill.color = Color.red;
            return;
        }
        if (value > greenEnd) {
            progressVisObj.value = 1;
            progressFill.color = new Color(1, 0.75f, 0);
            return;
        }
        if(value >= yellowEnd) {
            progressFill.color = Color.green;
        }
        if(value < yellowEnd) {
            progressFill.color = Color.yellow;
        }
        progressVisObj.value = value;
	}

    public static bool isRed()
    {
        return value >= orangeEnd;
    }

    public static bool isZero()
    {
        return value <= 0;
    }
}
