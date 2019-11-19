using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPicker : MonoBehaviour {
    private bool _canPickUp;
    private IEnumerator _rotation;

    [SerializeField]
    private Object[] _meshForIngredient;

    [SerializeField]
    private Object _ingredientMeshDEBUG;
    private List<Ingredient> _ingredientsOnPlate = new List<Ingredient>();
    private int _actualIngredient = 0;

    [SerializeField] private GameObject _playerMesh;

    private const float SPEED = 8;

    private void Awake() {
        _rotation = Rotate(360 / SPEED);
    }

    // used to debug
    private void Start() {
        GameSettings.AddMeshForIngredient(Ingredients.SmallCup, _meshForIngredient[0]);
        GameSettings.AddMeshForIngredient(Ingredients.MediumCup, _meshForIngredient[1]);
        GameSettings.AddMeshForIngredient(Ingredients.LargeCup, _meshForIngredient[2]);
        GameSettings.AddMeshForIngredient(Ingredients.Milk, _meshForIngredient[3]);
        GameSettings.AddMeshForIngredient(Ingredients.Caramel, _meshForIngredient[4]);
        GameSettings.AddMeshForIngredient(Ingredients.Chocolate, _meshForIngredient[5]);
        GameSettings.AddMeshForIngredient(Ingredients.Vanilla, _meshForIngredient[6]);
        GameSettings.AddMeshForIngredient(Ingredients.Cocoa, _meshForIngredient[7]);
        

        List<Ingredient> DEBUG = new List<Ingredient>();
        Ingredient ing2 = new Ingredient(1, Unit.Cup, Ingredients.MediumCup, null);
        DEBUG.Add(ing2);
        Ingredient ing = new Ingredient(1, Unit.Cup, Ingredients.Caramel, null);
        DEBUG.Add(ing);
        Ingredient ing3 = new Ingredient(1, Unit.Cup, Ingredients.LargeCup, null);
        DEBUG.Add(ing3);
        ing3 = new Ingredient(1, Unit.Cup, Ingredients.Chocolate, null);
        DEBUG.Add(ing3);
        ing3 = new Ingredient(1, Unit.Cup, Ingredients.Milk, null);
        DEBUG.Add(ing3);
        ing3 = new Ingredient(1, Unit.Cup, Ingredients.Vanilla, null);
        DEBUG.Add(ing3);
        SetupPlate(DEBUG);
    }

    // UpdateMethod to Debug the plate
    private void Update() {
        if (Input.GetKeyDown(KeyCode.S) && !_canPickUp) {
            _canPickUp = !_canPickUp;
            StartCoroutine(_rotation);
            Debug.Log("start");
        }

        if (Input.GetKeyDown(KeyCode.W) && _canPickUp) {
            _canPickUp = !_canPickUp;
            Select();
            Debug.Log("stop");
        }


    }

    // start the rotation
    private IEnumerator Rotate(float speed) {
        //no ingredients on the plate
        if (transform.parent.childCount == 1)
            yield break;

        // change to next ingredient if this hits 0
        float changeIngredient = SPEED / (transform.parent.childCount - 1);
        while (_canPickUp) {
            transform.parent.Rotate(Vector3.up * speed * Time.deltaTime);
            changeIngredient -= Time.deltaTime;
            if (changeIngredient < 0.0f) {
                changeIngredient = SPEED / (transform.parent.childCount - 1);
                if (_actualIngredient + 1 < _ingredientsOnPlate.Count) {
                    _actualIngredient++;
                } else {
                    _actualIngredient = 0;
                }
                transform.parent.GetComponent<IngredientHolder>().SetIngredient(_ingredientsOnPlate[_actualIngredient]);
                //TODO: Highlighting
            }
            yield return new WaitForEndOfFrame();
        }
    }

    // spawns the ingredients on the plate
    public void SetupPlate(List<Ingredient> ingredients) {
        _ingredientsOnPlate = ingredients;
        for (int i = 0; i < ingredients.Count; i++) {
            // set angle
            Quaternion angle = Quaternion.AngleAxis(360.0f / ingredients.Count * i, transform.forward);
            _ingredientMeshDEBUG = GameSettings.GetMeshForIngredient(ingredients[i].GetIngredientType());
            if(_ingredientMeshDEBUG != null)
            {
                GameObject tmp = (GameObject)_ingredientMeshDEBUG;
                //Debug.Log(_ingredientMeshDEBUG.name);
                //Debug.Log(ingredients[i].GetIngredientType());
                Vector3 pos = new Vector3(transform.position.x, transform.position.y + tmp.transform.localScale.y / 2, transform.position.z);

                //get the correct mesh
                _ingredientMeshDEBUG = ingredients[i].GetMesh();
                //spawn ingredient
                tmp = (GameObject)Instantiate(_ingredientMeshDEBUG, pos, angle, transform.parent);
                //move to the right position
                tmp.transform.position += 1.5f * tmp.transform.forward;
                //reset angle
                tmp.transform.rotation = Quaternion.identity;
                //Debug.Log(pos + " " + angle + " " + tmp.transform.forward);
                transform.parent.GetComponent<IngredientHolder>().SetIngredient(_ingredientsOnPlate[_actualIngredient]);
            }
            

            
        }
    }

    public Ingredient Select() {
        _canPickUp = false;
        StopCoroutine(_rotation);
        var tmp = (GameObject)(transform.parent.GetComponent<IngredientHolder>().GetIngredient().GetMesh());
        //Debug.Log(tmp);
        _playerMesh.GetComponentInChildren<MeshFilter>().mesh = tmp.GetComponent<MeshFilter>().sharedMesh;
        _playerMesh.GetComponentInChildren<Renderer>().material = transform.parent.GetChild(_actualIngredient).GetComponent<Renderer>().material;
        //_playerMesh.GetComponentInChildren<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
        return transform.parent.GetComponent<IngredientHolder>().GetIngredient();
    }

    public void Rotate() {
        if (!_canPickUp)
        _canPickUp = !_canPickUp;
        StartCoroutine(_rotation);
    }
}
