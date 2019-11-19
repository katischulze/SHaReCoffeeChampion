using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPicker : MonoBehaviour {

    [SerializeField]
    private float _secondsPerIngredient;

    [SerializeField]
    private GameObject[] _ingredients;

    [SerializeField]
    private GameObject[] _cups;

    private GameObject[] _currentIngredients;

    [SerializeField]
    private Light _light;

    [SerializeField]
    private bool _doRotate;

    private int _currentIndex;

    private float _lastTimeOffset = 0;

    private bool _cupSelected = false;

    private GameObject _selectedCup = null;

    private Quaternion initialRotation;

	// Use this for initialization
	void Start () {
        initialRotation = transform.rotation;
        SetupIngredients();
        Rotate();
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Rotate()
    {
        if (!_doRotate)
        {
            _doRotate = true;
            float degreesPerIngredient = 360f / _currentIngredients.Length;
            float degreesPerSecond = degreesPerIngredient / _secondsPerIngredient;
            StartCoroutine(Rotate(degreesPerSecond));
        }
    }

    public void StopRotate()
    {
        _doRotate = false;
    }

    IEnumerator Rotate(float degreesPerSecond)
    {
        HighlightObject(_currentIngredients[_currentIndex]);
        float lastChange = Time.time;
        if(_lastTimeOffset != 0)
        {
            lastChange -= _lastTimeOffset;
            _lastTimeOffset = 0;
        }
        float lastLoop = Time.time - Time.deltaTime;
        while (_doRotate)
        {
            if(Time.time - lastChange > _secondsPerIngredient)
            {
                lastChange = Time.time;
                _currentIndex = (_currentIndex + 1) % _currentIngredients.Length;
                HighlightObject(_currentIngredients[_currentIndex]);
            }

            float deltaTime = Time.time - lastLoop;
            transform.Rotate(Vector3.up * degreesPerSecond * deltaTime);
            lastLoop = Time.time;
            yield return new WaitForEndOfFrame();
        }
        _lastTimeOffset = Time.time - lastChange;
    }

    public void SetupIngredients()
    {

        _lastTimeOffset = 0;
        _currentIndex = 0;
        transform.rotation = initialRotation;

        if(_currentIngredients != null)
        {
            foreach(GameObject go in _currentIngredients)
            {
                if(go != _selectedCup)
                    Destroy(go);
            }
        }

        int length = _cupSelected ? _ingredients.Length : _cups.Length;
        GameObject[] copy = new GameObject[length];
        GameObject[] toCopy = _cupSelected ? _ingredients : _cups;
        for(int i = 0; i < length; i++)
        {
            copy[i] = toCopy[i];
        }

        _currentIngredients = copy;
        

        if (_currentIngredients.Length > 0)
        {
            float radius = 1.3f;
            float degreesPerIngredient = 360 / _currentIngredients.Length;
            for (int i = 0; i < _currentIngredients.Length; i++)
            {
                GameObject current = _currentIngredients[i];
                current = Instantiate(current, transform);
                current.transform.position = transform.position + Utilities.GetBottomOffset(current);
                current.transform.Rotate(Vector3.up, -i * degreesPerIngredient, Space.Self);
                current.transform.Translate(Vector3.forward * radius, Space.Self);
                _currentIngredients[i] = current;
            }
        }
    }

    void HighlightObject(GameObject toHighlight)
    {
        if (_light != null)
        {
            Vector3 lightPosition = toHighlight.transform.position + toHighlight.transform.up * 3;
            lightPosition += toHighlight.transform.forward * 3;
            _light.transform.position = lightPosition;
            _light.transform.forward = (toHighlight.transform.position - _light.transform.position).normalized;
        }
    }

    public Ingredient Select()
    {
        Debug.Log("Selected ingredient!");
        if (_currentIngredients[_currentIndex].GetComponentInChildren<LiquidFilling>() != null)
        {
            _cupSelected = true;
            _selectedCup = _currentIngredients[_currentIndex];
        }
        StopRotate();
        return _currentIngredients[_currentIndex].GetComponent<Ingredient>();
    }
}
