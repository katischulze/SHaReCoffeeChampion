using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionAddImmediate : ActionAddIngredient {

    protected override void SetInstructionImages()
    {
        instructionImages = null;
    }

    public override void EnterAction()
    {
        base.EnterAction();
        if(!ReferenceEquals(GameData.SelectedIngredient, null))
        {
            UpdateIngredientProgress(1f);
            _finished = true;
        }
    }
}
