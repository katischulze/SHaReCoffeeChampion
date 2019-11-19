using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeToUI : MonoBehaviour {
    public Text leftSide, rightSide, headline;
    public GameObject endScreen;
    public Text scoreHead, scoreLeft, scoreRight, scoreText, scoreTotal;

    private bool _endScreenOpen = false;

    private void Update()
    {
        if (RecipeManager._activeRecipe.Finished())
        {
            writeScore(RecipeManager._activeRecipe);
        }
    }

    //Writes the Recipe on the Note
    public void writeRecipe(Recipe recipe)
    {
        //Reset textboxes
        leftSide.text = "";
        rightSide.text = "";
        //Write out the Recipename
        Debug.Log("Recipe Size: " + recipe.GetSize());
        headline.text = Translation.Get(recipe.GetName());
        List<Ingredient> ingredients = recipe.GetIngredientsList();

        //Write out the Ingredients
        foreach(Ingredient i in ingredients)
        {
            string left = "";
            string right = "";
            left =makeAmountNice(i) + " " + i.GetUnit();
            right = right + i.GetName();
            //Sets the checkmark and changes color to green if the ingredient is finished
            if (i.GetProgress() >= 1f &&i.GetProgress()<=ProgressBarScript.greenEnd)
            {
                //Adds the checkmark
                left = '\u2713'+left;
                //Changes color to green
                left = "<color=#008000ff>" + left + "</color>";
                right = "<color=#008000ff>" + right + "</color>";
            }else if(i.GetProgress() >ProgressBarScript.greenEnd && i.GetProgress()<=ProgressBarScript.orangeEnd){
                //Adds the checkmark
                left = '\u2713' + left;
                //Changes color to orange
                left = "<color=#ffa500ff>" + left + "</color>";
                right = "<color=#ffa500ff>" + right + "</color>";
            }
            else if (i.GetProgress()>ProgressBarScript.orangeEnd)
            {
                //Adds the checkmark
                left = '\u2713' + left;
                //Changes color to red
                left = "<color=#ff0000ff>" + left + "</color>";
                right = "<color=#ff0000ff>" + right + "</color>";
            }
            else
            {
                //Text stays black
                left = "    "+left;     //Makes the format prettier, space for the checkmark
            }
            leftSide.text += left+"\n";
            rightSide.text += right+"\n";
        }
        leftSide.text += "    "+ Translation.Get("Serve");
    }

    //Can be used to make strikethrough text
    private string StrikeThrough(string s)
    {
        string strikethrough = "";
        foreach (char c in s)
        {
            strikethrough = strikethrough + c + '\u0336';
        }
        return strikethrough;
    }

    //Changes Cupsizes to fractions, just looks prettier
    private string makeAmountNice(Ingredient i)
    {
        if (i.GetUnit().Equals("Cup"))
        {
            if(i.GetAmount() == 0.5f)
            {
                return "1/2";
            }else if(i.GetAmount() == 0.33f)
            {
                return "1/3";
            }else if(i.GetAmount() == 0.2f)
            {
                return "1/4";
            }
            else
            {
                return "" + i.GetAmount();
            }
        }
        else
        {
            return "" + i.GetAmount();
        }
    }

    public void writeScore(Recipe r)
    {
        if (!endScreen.activeSelf)
        {
            int scoreTotalValue = 0;
            endScreen.SetActive(true);
            scoreHead.text = Translation.Get(r.GetName());
            List<Ingredient> ingredients = r.GetIngredientsList();
            scoreLeft.text = "";
            scoreRight.text = "";
            scoreText.text = "";
            //Write out the Ingredients
            foreach (Ingredient i in ingredients)
            {
                string left = "";
                string right = "";
                string score = "";
                left = makeAmountNice(i) + " " + i.GetUnit();
                right = right + i.GetName();
                score += (int)Mathf.Clamp(200 - i.GetProgress() * 100, 0, 100);
                Debug.Log(Translation.Get("Score") + ": " + scoreTotalValue);
                //Sets the checkmark and changes color to green if the ingredient is finished
                if (i.GetProgress() >= 1f)
                {
                    //Adds the checkmark
                    left = '\u2713' + left;
                    if (i.GetProgress() < ProgressBarScript.greenEnd)
                    {
                        //Changes color to green
                        left = "<color=#008000ff>" + left + "</color>";
                        right = "<color=#008000ff>" + right + "</color>";
                        score = "<color=#008000ff>" + score + "</color>";
                    }
                    else if (i.GetProgress() > ProgressBarScript.orangeEnd)
                    {
                        //Changes color to red
                        left = "<color=#ff0000ff>" + left + "</color>";
                        right = "<color=#ff0000ff>" + right + "</color>";
                        score = "<color=#ff0000ff>" + score + "</color>";
                    }
                    else
                    {
                        //Changes color to orange
                        left = "<color=#ffa500ff>" + left + "</color>";
                        right = "<color=#ffa500ff>" + right + "</color>";
                        score = "<color=#ffaf00ff>" + score + "</color>";
                    }
                }
                else
                {
                    left = "    " + left;     //Makes the format prettier, space for the checkmark
                }
                scoreLeft.text += left + "\n";
                scoreRight.text += right + "\n";
                scoreText.text += score + "\n";
            }
            scoreLeft.text += "    "+Translation.Get("Penalties");
            scoreText.text += "-" + GameData.penalty;
            r.calculateScore();
            scoreTotal.text = Translation.Get("TotalScore") + ": " + GameData.score;
            _endScreenOpen = true;
        }
    }
    public void CloseEndScreen()
    {
        endScreen.SetActive(false);
    }
}


