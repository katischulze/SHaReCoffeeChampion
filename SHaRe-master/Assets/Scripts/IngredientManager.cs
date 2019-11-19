using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IngredientManager : MonoBehaviour {

    private static IngredientManager _ingredientManager;

    [SerializeField]
    private IngredientSetting[] IngredientSettings;

    [SerializeField]
    private GameObject[] IngredientPrefabs;

    public static IngredientManager Instance
    {
        get {
            if (_ingredientManager == null)
            {
                GameObject g = new GameObject("IngredientManager");
                _ingredientManager = g.AddComponent<IngredientManager>();
                GameObject managers = GameObject.Find("Managers");
                if (managers != null)
                {
                    g.transform.parent = managers.transform;
                }
            }
                
            return _ingredientManager;
        }
    }

    private void Awake()
    {
        InitializeIngredients();
    }

    private void InitializeIngredients()
    {
        Ingredients[] allTypes = (Ingredients[])Enum.GetValues(typeof(Ingredients));
        IngredientPrefabs = new GameObject[allTypes.Length];
        foreach (Ingredients type in allTypes)
        {
            GameObject prefab = Resources.Load("Prefabs/Ingredients/" + type.ToString()) as GameObject;
            if (prefab != null)
            {
                IngredientPrefabs[(int)type] = prefab;
            }
            else
            {
                Debug.LogWarning("Didn't find Ingredient Prefab: " + type.ToString());
            }
        }
    }

    public GameObject GetIngredientPrefab(Ingredients type)
    {

        if(IngredientPrefabs.Length == 0)
        {
            InitializeIngredients();
        }

        GameObject prefab = IngredientPrefabs[(int)type];
        if (ReferenceEquals(prefab, null))
        {
            Debug.LogError("No prefab was found for Ingredient Type: " + type.ToString());
        }
        return prefab;
    }

    public IngredientSetting GetIngredientSetting(Ingredients type)
    {
        IEnumerable<IngredientSetting> ingredientSettings = IngredientSettings.Where(set => set.type == type);
        if(ingredientSettings.Count() > 0)
        {
            return ingredientSettings.First();
        }

        return null;
    }
	
}

[Serializable]
public class IngredientSetting
{
    public Ingredients type;

    public Color color;
}
