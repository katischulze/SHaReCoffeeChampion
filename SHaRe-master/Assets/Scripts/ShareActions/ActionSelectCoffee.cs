using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelectCoffee : ShareAction {

    bool _finished = false;

    bool _cupPutDown = false;

    bool _initialAnimationDone = false;

    CupMovement _cupMovement;

    Vector3 _animateCupTo = Vector3.zero;

    IShareInput _shareInput;

    public override bool Finished()
    {
        return _finished;
    }

    protected override void SetInstructionImages()
    {
        instructionImages = new Sprite[2];
        instructionImages[0] = Resources.Load<Sprite>("Lay1");
        instructionImages[1] = Resources.Load<Sprite>("Lay2");
    }

    public override void EnterAction()
    {
        base.EnterAction();
        _cupMovement = FindObjectOfType<CupMovement>();
        if(_cupMovement == null)
        {
            Debug.LogError("Couldn't find the CupMovement Object in the Scene!");
        }
        
        GameObject fillInCoffePosition = GameObject.FindGameObjectWithTag("FillInCoffeeZone");
        if(fillInCoffePosition != null)
        {
            Vector3 cameraPosition = Camera.main.transform.position;
            cameraPosition.x = fillInCoffePosition.transform.position.x;
            _animateCupTo = cameraPosition + Camera.main.transform.forward * 4 + Vector3.down + Vector3.right;
            Coroutines.AnimatePosition(_cupMovement._activeChild.gameObject, _animateCupTo, this, true, OnAnimationFinish);
            Coroutines.AnimatePosition(Camera.main.gameObject, cameraPosition, this);
            _animateCupTo = fillInCoffePosition.transform.position;
            Debug.Log(_animateCupTo);
        } else
        {
            Debug.LogError("Couldn't find the CoffeeMachine Position to animate the coffee cup to.");
        }
        
    }

    // Use this for initialization
    void Start () {
        _shareInput = ShareInputManager.ShareInput;
    }
	
	// Update is called once per frame
	new void Update () {
        if (_active)
        {
            base.Update();
            if (!_shareInput.IsPickedUp() && !_cupPutDown && _initialAnimationDone)
            {
                _cupPutDown = true;
                _cupMovement.LockMovement = true;
                Debug.Log(_animateCupTo);
                _cupMovement.PutAway(_animateCupTo, OnAnimationFinish);
                List<Ingredient> ingredients = RecipeManager._activeRecipe.GetIngredientsList();
                bool found = false;
                foreach(Ingredient ingredient in ingredients)
                {
                    if(ingredient.GetIngredientType() == Ingredients.Coffee)
                    {
                        GameData.SelectedIngredient = ingredient;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Debug.LogError("Couldn't find the Coffe Ingredient in the Recipe!");
                }
            }
        }
	}

    void OnAnimationFinish()
    {
        if (!_initialAnimationDone)
            _initialAnimationDone = true;
        else
            _finished = true;
    }
}
