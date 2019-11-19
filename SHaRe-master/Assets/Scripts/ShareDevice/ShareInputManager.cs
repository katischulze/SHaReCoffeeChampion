using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShareInputManager : MonoBehaviour {

    private ShareInputManager _instance;

    [SerializeField]
    private ShareInputType _inputType;

    [SerializeField]
    private KeyCode _inputTypeChangeKey;

    private static IShareInput _shareInput;

    private static Type _shareInputType;

    public static IShareInput ShareInput { get { return _shareInput; } }

    void Awake ()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Initialize();
    }

    private void Initialize()
    {
        Debug.Log("Initializing Share Input Manager");
        SetShareInput();
    }

    void Update()
    {
        if (Input.GetKeyDown(_inputTypeChangeKey))
        {
            int numberOfEnums = Enum.GetNames(typeof(ShareInputType)).Length;
            _inputType = (ShareInputType)(((int)_inputType + 1) % numberOfEnums);
            SetShareInput();
        }
        

    }

    private void SetShareInput()
    {
        if (_shareInput != null)
        {
            Destroy(gameObject.GetComponent(_shareInputType));
        }

        switch (_inputType)
        {
            case ShareInputType.Keyboard:
                _shareInput = gameObject.AddComponent<KeyboardInput>();
                break;

            case ShareInputType.ShareDevice:
                _shareInput = gameObject.AddComponent<ShareInput>();
                break;
        }

        _shareInputType = _shareInput.GetType();
    }

    /*private void OnValidate()
    {
        if(Application.isPlaying)
            SetShareInput();
    }*/

    enum ShareInputType
    {
        Keyboard,
        ShareDevice
    }

}
