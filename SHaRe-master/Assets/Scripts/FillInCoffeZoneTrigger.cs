using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillInCoffeZoneTrigger : MonoBehaviour {

    bool inFillInCoffeeZone = false;
    bool inFillInMilkZone = false;
    bool inFillInSpiceZone = false;

    public string ingredient;

    void OnTriggerEnter(Collider c)
    {

        if (c.gameObject.tag == "Player"&&ingredient.Equals("coffee"))
        {

            Debug.Log("Coffeemachine is Triggered !!!!!!!");            

            inFillInCoffeeZone = true;
        }else if(c.gameObject.tag == "Player" && ingredient.Equals("milk"))
        {
            Debug.Log("Milk is Triggered !!!!!!!");

            inFillInMilkZone = true;
        }
        else if (c.gameObject.tag == "Player" && ingredient.Equals("spice"))
        {
            Debug.Log("Spice is Triggered !!!!!!!");

            inFillInSpiceZone = true;
        }

    }

    void OnTriggerExit(Collider c)
    {
        
        if (c.gameObject.tag == "Player" && ingredient.Equals("coffee"))
        {
            Debug.Log("Coffeemachine calmed down *sigh* ");
            inFillInCoffeeZone = false;
        }
        else if (c.gameObject.tag == "Player" && ingredient.Equals("milk"))
        {
            Debug.Log("Milk calmed down *sigh* ");
            inFillInMilkZone = false;
        }
        else if (c.gameObject.tag == "Player" && ingredient.Equals("spice"))
        {
            Debug.Log("Spice calmed down *sigh* ");
            inFillInSpiceZone = false;
        }

    }

    public bool getInCoffeeZone()
    {
        return inFillInCoffeeZone;
    }

    public bool getInMilkZone()
    {
        return inFillInMilkZone;
    }

    public bool getInSpiceZone()
    {
        return inFillInSpiceZone;
    }

}
