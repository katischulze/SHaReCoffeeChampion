using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchBackgroundImage : MonoBehaviour {

    public static Object[] backgrounds;
    private static GameObject _background;

    // Use this for initialization
    void Start() {
        _background = GameObject.FindGameObjectsWithTag("Background")[0];
        backgrounds = Resources.LoadAll("Backgrounds", typeof(Sprite));
    }

    public static void changeBackground(int backgroundID) {
        _background.GetComponent<SpriteRenderer>().sprite = (Sprite)backgrounds[backgroundID];
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.M)) {
            changeBackground(1);

        }
    }

}
