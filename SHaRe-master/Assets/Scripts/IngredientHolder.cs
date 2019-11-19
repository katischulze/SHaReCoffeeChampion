using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientHolder : MonoBehaviour {
    private Ingredient _ingredient;

    public Ingredient GetIngredient() {
        return _ingredient;
    }

    public void SetIngredient(Ingredient ingredient) {
        _ingredient = ingredient;
    }
}
