using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {
    private Cookbook _cookbook;

    void Start () {
        Ingredient.Translate();
        _cookbook = new Cookbook(new List<Recipe>());
        List<Ingredient> i = new List<Ingredient>();

        //Ingredient temp = new Ingredient(0.5f, Unit.Cup, Ingredients.Milk);
        //i.Add(temp);
        //temp = new Ingredient(0.3f, Unit.Cup, Ingredients.Raspberries);
        //i.Add(temp);
        //temp = new Ingredient(1, Unit.Cl, Ingredients.LemonJuice);
        //i.Add(temp);
        //temp = new Ingredient(2, Unit.Tablespoon, Ingredients.WhippedCream);
        //i.Add(temp);
        //CreateRecipes("Raspberry Cocktail", 0, i);

        //i.Clear();
        //temp = new Ingredient(0.5f, Unit.Cup, Ingredients.Milk);
        //i.Add(temp);
        //temp = new Ingredient(0.3f, Unit.Cup, Ingredients.Cherries);
        //i.Add(temp);
        //temp = new Ingredient(0.3f, Unit.Cup, Ingredients.Oranges);
        //i.Add(temp);
        //temp = new Ingredient(1, Unit.Cl, Ingredients.LemonJuice);
        //i.Add(temp);
        //temp = new Ingredient(2, Unit.Tablespoon, Ingredients.WhippedCream);
        //i.Add(temp);
        //CreateRecipes("Cherry-Orange Cocktail", 1, i);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F3)) {
            _cookbook.PrintRecipes();
        }
    }

    private void CreateRecipes(String name, int id, Size size,List<Ingredient> ingredients) {
        Recipe temp = new Recipe(name, id, size, ingredients);
        _cookbook.AddRecipe(temp);
    }
}
