using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData {

    public static int score = 0;
    public static int penalty = 0;
	public static Ingredient SelectedIngredient { get; set; }

    
    private static LiquidFilling _selectedCupFilling;
    public static LiquidFilling SelectedCupFilling { get { return _selectedCupFilling; } }

    private static GameObject _selectedCup;
    public static GameObject SelectedCup {
        get { return _selectedCup; }
        set {
            _selectedCup = value;
            _selectedCupFilling = value.GetComponentInChildren<LiquidFilling>();
        }
    }

    
    private static List<Ingredient> AddedIngredients;

    public static Penalty Penalty;

    public static void AddIngredient(Ingredient i)
    {
        AddedIngredients.Add(i);
    }

    public static void Reset()
    {
        SelectedIngredient = null;
        AddedIngredients.Clear();
        Penalty = null;
        score = 0;
    }

}
