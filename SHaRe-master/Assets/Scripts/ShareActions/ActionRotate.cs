using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRotate : ActionAddIngredient {

    public float tiltAngleThreshold;
    
    private float howLong = 0;

    public float Duration = 5;

    IShareInput shareInput;

    private bool _playSound = false;

    public override bool Finished()
    {
        return _finished;
    }

    public override void EnterAction()
    {
        base.EnterAction();
        shareInput = ShareInputManager.ShareInput;
        _active = true;
        ShowInstructionText("Rotate the Share Device to add the ingredient!");
        SetInstructionImages();
    }

    public override void ExitAction()
    {
        base.ExitAction();
        if (!ReferenceEquals(GameData.SelectedIngredient, null) && GameData.SelectedIngredient.GetProgress() < 1.0f)
            GameData.SelectedIngredient.SetProgress(2.0f - GameData.SelectedIngredient.GetProgress());
        ProgressBarScript.value = 0;
    }

    protected override void SetInstructionImages()
    {
        instructionImages = new Sprite[2];
        instructionImages[0] = Resources.Load<Sprite>("Turn1");
        instructionImages[1] = Resources.Load<Sprite>("Turn2");
    }

    // Update is called once per frame
    new void Update () {
        
        if (_active)
        {
            base.Update();
            
            if (shareInput.GetTiltAngle() > GameSettings.TiltThreshold)
            {
                //Debug.Log("Tilt angle: " + shareInput.GetTiltAngle());
                howLong = howLong + Time.deltaTime;
                UpdateIngredientProgress(howLong / Duration);

                //GameObject.FindObjectOfType<ShaderScript>().setFillHeight(howLong / Duration);
                if (_playSound)
                {
                    _playSound = false;
                    SoundEffectManager.Instance.PlaySplash();
                }
            } else
            {
                if (!_playSound)
                {
                    _playSound = true;
                    SoundEffectManager.Instance.StopSplash();
                }
            }

            if (howLong > Duration)
            {
                ShowInstructionText("You have added enough. Put the Share-Device down.");
            }

            if (!shareInput.IsPickedUp())
                _finished = true;

        }


	}
}
