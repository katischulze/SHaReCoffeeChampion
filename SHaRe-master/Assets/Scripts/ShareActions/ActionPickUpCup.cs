using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPickUpCup : ShareAction {

    private bool pickedUp = false;

    public override bool Finished()
    {
        return pickedUp;
    }

    public override void EnterAction()
    {
        base.EnterAction();
        _active = true;
        _instructionText.text = "Pick up the Share Device or press V";
    }

    protected override void SetInstructionImages()
    {
        instructionImages = new Sprite[2];
        instructionImages[0] = Resources.Load<Sprite>("Take1");
        instructionImages[1] = Resources.Load<Sprite>("Take2");
    }

    public override void ExitAction()
    {
        base.ExitAction();

        SoundEffectManager.Instance.PlayLiftCup();

        CupMovement cupMovement = FindObjectOfType<CupMovement>();
        if (cupMovement != null)
        {
            cupMovement.LockMovement = false;
        }
    }

    // Update is called once per frame
    new void Update () {
        if (_active)
        {
            base.Update();
            if (ShareInputManager.ShareInput.IsPickedUp())
            {
                pickedUp = true;
            }
        }
	}
}
