using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionAddIngredient : ShareAction {

    CupMovement cupMovement;

    protected bool _finished = false;

    public override bool Finished()
    {
        return _finished;
    }

    protected void UpdateIngredientProgress(float progress)
    {
        if (!ReferenceEquals(GameData.SelectedIngredient, null))
        {
            GameData.SelectedIngredient.SetProgress(progress);
            if (GameData.SelectedCup != null && GameData.SelectedIngredient.AddLiquidToCup)
            {
                GameData.SelectedCupFilling.SetColor(GameData.SelectedIngredient.Color, GameData.SelectedIngredient.AmountAdded, GameData.SelectedIngredient.Unit);
            }
        } else
        {
            Debug.LogError("Ingredient not selected!");
        }

        ProgressBarScript.value = progress;
    }

    public override void EnterAction()
    {
        base.EnterAction();

        cupMovement = FindObjectOfType<CupMovement>();

        if (cupMovement != null && !ReferenceEquals(GameData.SelectedIngredient, null))
        {
            if (GameData.SelectedIngredient.GetIngredientType() == Ingredients.Coffee)
            {
                cupMovement.LockMovement = true;
            }
            else
            {
                cupMovement.LockMovement = false;

                
                
            }
        }
    }

    public override void ExitAction()
    {
        base.ExitAction();
        if (!ReferenceEquals(GameData.SelectedIngredient, null) && GameData.SelectedIngredient.GetProgress() < 1.0f)
            GameData.SelectedIngredient.SetProgress(2.0f - GameData.SelectedIngredient.GetProgress());

        if(this.GetType() != typeof(ActionAddImmediate))
            cupMovement.PutDownObject();

        ProgressBarScript.value = 0;
    }

}
