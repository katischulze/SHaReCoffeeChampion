using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRotateSqueeze : ActionAddIngredient
{
    
    private bool playSound = true;
    private float howLong = 0;
    public float Duration = 5;
    IShareInput inS;

    protected override void SetInstructionImages()
    {
        
        instructionImages = new Sprite[2];
        instructionImages[0] = Resources.Load<Sprite>("TurnSqueeze2");
        instructionImages[1] = Resources.Load<Sprite>("TurnSqueeze1");
        
    }


    public override void EnterAction()
    {
        base.EnterAction();
        inS = ShareInputManager.ShareInput;
        _active = true;
        UpdateIngredientProgress(0);
        ShowInstructionText("Rotate the Share-Device and squeeze it.");
        SetInstructionImages();
    }

    // Update is called once per frame
    new void Update()
    {
        if (_active)
        {
            base.Update();
            if ((inS.GetForce() > GameSettings.ForceThreshold && inS.GetTiltAngle() > GameSettings.TiltThreshold))
            {
                howLong = howLong + Time.deltaTime;
                UpdateIngredientProgress(howLong / Duration);

                //GameObject.FindObjectOfType<ShaderScript>().setFillHeight(howLong / Duration);
                if (playSound)
                {
                    SoundEffectManager.Instance.PlaySauceSqueezing();
                    playSound = false;
                }
                    
            }
            else
            {
                if (!playSound)
                {
                    SoundEffectManager.Instance.StopSauceSqueezing();
                    playSound = true;
                }
            }
            
            if (howLong > Duration)
            {
                ShowInstructionText("You have added enough. Put the Share-Device down.");
            }

            if (!inS.IsPickedUp())
            {
                //Return the time value
                _finished = true;
            }

        }
    }
}