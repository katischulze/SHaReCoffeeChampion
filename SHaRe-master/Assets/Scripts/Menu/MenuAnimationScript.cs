using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuAnimationScript : MonoBehaviour {

    public GameObject Main;


    public Vector3 offscreenPos;
    Vector3 mainPos, startPos;

    private IShareInput _shareInput;

    [SerializeField]
    private Button startGameBtn, quitBtn, languageBtn, portBtn, engBtn, gerBtn;
    private Button leftBtn, midBtn, rightBtn;

    [SerializeField]
    SetButtonText btnText;

    private readonly float timeOut = 1.0f;
    private float timer = 0.0f;


    private int selectedButton = 0;

    private readonly float leftToMidBorder = 40, midToRightBorder = 320, leftRightBorder = 180;

	// Use this for initialization
	void Start () {
        _shareInput = ShareInputManager.ShareInput;
        startPos = Main.transform.position;
        mainPos = startPos;
        leftBtn = quitBtn;
        midBtn = startGameBtn;
        rightBtn = languageBtn;
        btnText.RefreshButtonText();

        if (!GameSettings.ShareDeviceCalibrated)
        {
            ShareAction calibrateForce = ShareAction.Create<ActionCalibrateForce>();
            calibrateForce.EnterAction();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (GameSettings.ShareDeviceCalibrated)
        {
            Debug.Log(GameSettings.Language);

            Main.transform.position = Vector3.Lerp(Main.transform.position, mainPos, 0.1f);

            float zTilt = _shareInput.GetRotation().eulerAngles.z;

            if (zTilt > leftToMidBorder && zTilt < leftRightBorder) //left
            {
                Debug.Log("left");
                //selectedButton = -1;
                leftBtn.Select();
            }
            else if (zTilt < midToRightBorder && zTilt > leftToMidBorder) //right
            {
                Debug.Log("right");
                //selectedButton = 1;
                rightBtn.Select();
            }
            else //mid
            {
                Debug.Log("mid");
                //selectedButton = 0;
                midBtn.Select();
            }

            if (_shareInput.GetForce() > GameSettings.ForceThreshold && timer <= 0.0f)
            {
                EventSystem.current.GetComponent<EventSystem>().currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
                timer = timeOut;
            }

            timer -= Time.deltaTime;

            #region zeug
            //if (_shareInput.GetForce() > pressedThreshold)
            //{
            //    if (selectedButton < 0)
            //    {
            //        leftBtn.onClick.Invoke();
            //        quit();
            //    }
            //    else if (selectedButton > 0)
            //    {
            //        chooseLanguage();
            //    }
            //    else
            //    {
            //        startGame();
            //    }
            //}
            #endregion
        }
    }

    public void startGame() {
        //TODO: link to aktual starting script
        SceneManager.LoadScene("DemoLevel");
    }

    public void chooseLanguage() {
        mainPos = offscreenPos;

        leftBtn = portBtn;
        midBtn = engBtn;
        rightBtn = gerBtn;

        //Main.transform.FindChild("Canvas").GetComponent<>
    }

    public void quit()
    {
        Application.Quit();
    }

    public void languageChosen(int index) {
        Debug.Log(index);
        //TODO: make language stuff in cases
        switch (index) {
            case 0: //english
                //GameSettings.Language = Translation.AvailableLanguage.EN;
                Translation.LoadLanguage(Translation.AvailableLanguage.EN);
                break;
            case 1: //german
                //GameSettings.Language = Translation.AvailableLanguage.DE;
                Translation.LoadLanguage(Translation.AvailableLanguage.DE);
                break;
            case 2: //portuguese
                //GameSettings.Language = Translation.AvailableLanguage.PT;
                Translation.LoadLanguage(Translation.AvailableLanguage.PT);
                break;
            default:
                GameSettings.Language = Translation.AvailableLanguage.EN;
                break;
        }

        leftBtn = quitBtn;
        midBtn = startGameBtn;
        rightBtn = languageBtn;

        btnText.RefreshButtonText();

        mainPos = startPos;
    }
}
