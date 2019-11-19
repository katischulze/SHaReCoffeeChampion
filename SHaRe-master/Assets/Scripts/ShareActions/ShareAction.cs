using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShareAction : MonoBehaviour {

    // Is the current Action active?
    protected bool _active;
    // Instruction text which can be shown in the progress list on the UI
    protected string _instruction;
    protected static Text _instructionText;
    // The name of the GameObject which should be used as the Text _instructionText
    protected static string _instructionTextObjectName = "DebugText";
    protected static string _instructionImageObjectName = "InstructionImage";

    protected static RecipeToUI _recipeToUI;

    protected Sprite[] instructionImages;
    protected Image _currentInstructionImage;

    protected int _currentInstructionImageIndex;
    protected readonly float imageChangeStartTime = 2.0f;
    protected float _imageChangeTimer = -1.0f;

    /// <summary>
    /// When overriding this method, call base.Awake();
    /// </summary>
    protected void Awake()
    {
        
        if(_instructionText == null)
        {
            _instructionText = GameObject.Find(_instructionTextObjectName).GetComponent<Text>();
        }

        if(_recipeToUI == null)
        {
            _recipeToUI = FindObjectOfType<RecipeToUI>();
        }

        if(_currentInstructionImage == null)
        {
            GameObject _instructionImageObject = GameObject.Find(_instructionImageObjectName);
            if (_instructionImageObject != null)
            {
                _currentInstructionImage = _instructionImageObject.GetComponent<Image>();
            } else
            {
                Debug.LogWarning("Couldn't find the Instruction Image GameObject with name " + _instructionImageObjectName);
            }
            
        }
    }
    
    protected void Update()
    {
        if (_active)
        {
            ShowInstructionImage();
        }
    }

    public void SetActive(bool active)
    {
        _active = active;
    }

    protected abstract void SetInstructionImages();

    protected void ShowInstructionText(string instruction)
    {
        _instruction = instruction;
        ShowInstructionText();
    }

    protected void ShowInstructionText()
    {
        if(_instructionText != null)
        {
            _instructionText.text = _instruction;
        } else
        {
            Debug.LogError("Text _instructionText is not set in ShareAction.");
        }
    }

    protected void ShowInstructionImage()
    {
        //Debug.Log("ShowInstruction");
        if(_imageChangeTimer <= 0.0f) //Change Image
        {
            if(instructionImages == null || instructionImages.Length == 0||_currentInstructionImage == null)
            {
                Debug.LogWarning("Instruction Image or current Instruction Image is null");
                return;
            }
            _imageChangeTimer = imageChangeStartTime;
            _currentInstructionImageIndex = (_currentInstructionImageIndex + 1) % instructionImages.Length;
            _currentInstructionImage.sprite = instructionImages[_currentInstructionImageIndex];
            //Debug.Log("Switch Picture");
        } else
        {
            _imageChangeTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// If the action is finished, this returns true.
    /// </summary>
    /// <returns>True, if finished</returns>
    public abstract bool Finished();

    /// <summary>
    /// To make the Actions work, they need to be added to a GameObject.
    /// If they are not created using this method, every Child Class needs to instantiate itself by first creating a GameObject g and then return g.AddComponent();
    /// </summary>
    /// <returns></returns>
    public static ShareAction Create<T>() where T : ShareAction {
        GameObject g = new GameObject();
        return g.AddComponent<T>();
    }

    /// <summary>
    /// This is called when an Action is starting. 
    /// If you override this method, make sure to set _active to true, or call the parent method, too.
    /// You can do all the setup you need for the action here.
    /// </summary>
    public virtual void EnterAction() {
        //Debug.Log("Enter Action " + GetType());
        _active = true;
        SetInstructionImages();
        
    }

    /// <summary>
    /// This is called when an Action is finished.
    /// If you override this method, make sure to set _active to false or call the parent method, too.
    /// </summary>
    public virtual void ExitAction()
    {
        //Debug.Log("Exit Action " + GetType());
        _active = false;
    }
    	
}