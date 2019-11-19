using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour {
    private const Ingredients _Ingredient = Ingredients.Coffee;
    private float _localBaseY;
    private float _localHeight;
    private float _fillHeight;

    private GameObject CoffeeLiquid;

    private IEnumerator StartFillingCoffee() {
        _localBaseY = -_localBaseY;
        _localHeight = -_localHeight;
        while (_fillHeight < 0.99) {
            _fillHeight += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator StopFillingCoffe() {
        _localBaseY = -_localBaseY;
        _localHeight = -_localHeight;
        while (_fillHeight < 0.99) {
            _fillHeight += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

}
