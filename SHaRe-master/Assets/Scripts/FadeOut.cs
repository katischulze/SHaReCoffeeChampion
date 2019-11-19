using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {
    [SerializeField]
    private GameObject _helpMenu;
    private bool _isHelpMenu = true;

    // Use this for initialization
    void Start() {
        StartCoroutine(FadingOut(_helpMenu, 3.0f, 1.0f));
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.F1)) {
            if (!_isHelpMenu) {
                StartCoroutine(FadingIn(_helpMenu, 0.3f));
            } else {
                StartCoroutine(FadingOut(_helpMenu, 0.0f, 0.3f));
            }

        }

    }

    private IEnumerator FadingOut(GameObject obj, float timeUntilFade, float howLongFade) {
        if (!_isHelpMenu) {
            yield break;
        }
        yield return new WaitForSeconds(timeUntilFade);
        for (float i = 0; i < howLongFade; i += Time.deltaTime) {
            obj.GetComponent<CanvasGroup>().alpha -= Time.deltaTime / howLongFade;
            yield return new WaitForEndOfFrame();
        }
        _isHelpMenu = false;
    }

    private IEnumerator FadingIn(GameObject obj, float howLongFade) {
        if (_isHelpMenu) {
            yield break;
        }
        for (float i = 0; i < howLongFade; i += Time.deltaTime) {
            obj.GetComponent<CanvasGroup>().alpha += Time.deltaTime / howLongFade;
            yield return new WaitForEndOfFrame();
        }
        _isHelpMenu = true;
    }

}
