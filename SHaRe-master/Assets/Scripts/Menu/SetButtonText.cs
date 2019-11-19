using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetButtonText : MonoBehaviour {

    [SerializeField]
    private Text quit, start, lang;
    
    public void RefreshButtonText()
    {
        Debug.Log("Refresh");
        quit.text = Translation.Get("Quit");
        Debug.Log(Translation.Get("Quit"));
        start.text = Translation.Get("Start");
        lang.text = Translation.Get("Languages");
    }
}
