using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RecipeManager : MonoBehaviour {
    // Collection of all available recipes
    private List<Recipe> _recipes;

    // The recipe that is currently selected and made
    public static Recipe _activeRecipe;

    private static RecipeToUI _recipeToUI;

    void Awake() {

        if (_recipeToUI == null)
        {
            _recipeToUI = FindObjectOfType<RecipeToUI>();
        }

        LanguageFile.Load("en").Save();
        //Set Progress Bar Values
        ProgressBarScript.yellowEnd = 1.0f;
        ProgressBarScript.greenEnd = 1.5f;
        ProgressBarScript.orangeEnd = 2.0f;


        _recipes = new List<Recipe>();

        List<ShareAction> listForCup = new List<ShareAction>(
            new ShareAction[]{
                ShareAction.Create<ActionPutDownCup>(),
                ShareAction.Create<ActionSelectIngredient>(),
                ShareAction.Create<ActionAddImmediate>(),
            });

        List<ShareAction> listForCoffee = new List<ShareAction>(
            new ShareAction[]{
                ShareAction.Create<ActionSelectCoffee>(),
                ShareAction.Create<ActionPutDownCup>(),
                ShareAction.Create<ActionPickUpCup>(),
                ShareAction.Create<ActionSqueeze>(),
                ShareAction.Create<ActionPutDownCup>(),
            });

        List<ShareAction> listForMilk = new List<ShareAction>(
            new ShareAction[]{
                ShareAction.Create<ActionSelectIngredient>(),
                ShareAction.Create<ActionRotate>(), 
                ShareAction.Create<ActionPutDownCup>(), 
            });

        List<ShareAction> listForCaramel = new List<ShareAction>(
            new ShareAction[]{
                ShareAction.Create<ActionSelectIngredient>(),
                ShareAction.Create<ActionRotateSqueeze>(), 
                ShareAction.Create<ActionPutDownCup>(),
            });

        _recipes.Add(new Recipe("DemoRecipe",
            0,Size.Big,
            new List<Ingredient>(
                new Ingredient[] {
                    new Ingredient(1, Unit.Cup, Ingredients.LargeCup, listForCup),
                    new Ingredient(100, Unit.Ml, Ingredients.Coffee, listForCoffee),
                    new Ingredient(50, Unit.Ml, Ingredients.Milk, listForMilk),
                    new Ingredient(2, Unit.Tablespoon,Ingredients.Caramel, listForCaramel)})));



        startRecipe(_recipes[0]);

        if (_activeRecipe == null)
        {
            Debug.LogError("No recipe");
        } else
        {
            _activeRecipe.Begin();
        }
    }

    void Update() {
        if (_activeRecipe != null) {
            _activeRecipe.Update();
        }
    }

    private void startRecipe(Recipe r)
    {
        _activeRecipe = r;
        UpdateRecipeUI();        
    }

    public static void UpdateRecipeUI()
    {
        if(_recipeToUI != null)
        {
            _recipeToUI.writeRecipe(_activeRecipe);
        } else
        {
            Debug.LogError("RecipeToUI Script is not set! Is the Script added to the Scene?");
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _recipeToUI = FindObjectOfType<RecipeToUI>();
        if (_recipeToUI != null)
        {
            UpdateRecipeUI();
            _recipeToUI.CloseEndScreen();
        }
    }

}
