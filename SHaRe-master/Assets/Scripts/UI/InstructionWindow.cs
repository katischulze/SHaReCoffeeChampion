using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionWindow : MonoBehaviour {

    private static GameObject _instructionCanvas;

	// Use this for initialization
	void Start () {
        _instructionCanvas = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void Hide()
    {
        if(_instructionCanvas != null)
        {
            for (int i = 0; i < _instructionCanvas.transform.childCount; i++)
            {
                Transform t = _instructionCanvas.transform.GetChild(i);
                t.gameObject.SetActive(false);
            }
        }
    }

    public static void Show(string text)
    {
        if(_instructionCanvas != null)
        {
            for (int i = 0; i < _instructionCanvas.transform.childCount; i++)
            {
                Transform tr = _instructionCanvas.transform.GetChild(i);
                tr.gameObject.SetActive(true);
            }

            Text t = _instructionCanvas.GetComponentInChildren<Text>();
            if(t != null)
            {
                t.text = text;
            }
        }
    }
}
