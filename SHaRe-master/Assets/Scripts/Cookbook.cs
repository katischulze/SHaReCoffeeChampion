using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Unit {
    Tablespoon,
    Cup,
    Cl,
    Ml
};

public enum Size {
    Small,
    Medium,
    Big
};

public enum Ingredients {
    Raspberry,
    Caramel,
    Cherry,
    Orange,
    Apple,
    LemonJuice,
    WhippedCream,
    Milk,
    Sugar,
    Coffee,
    Cocoa,
    Chocolate,
    Vanilla,
    // Testing
    SmallCup,
    MediumCup,
    LargeCup
};

public class Cookbook {

    private List<Recipe> _recipes;

    public Cookbook(List<Recipe> recipes) {
        _recipes = recipes;
    }

    public void AddRecipe(Recipe recipe) {
        _recipes.Add(recipe);
    }

    public void RemoveRecipe(Recipe recipe) {
        _recipes.Remove(recipe);
    }

    public List<Recipe> GetRecipes() {
        return _recipes;
    }

    public void PrintRecipes() {
        foreach (Recipe r in _recipes) {
            Debug.Log(r.GetName() + "\n\n" + r.GetIngredients());
        }
    }
}
