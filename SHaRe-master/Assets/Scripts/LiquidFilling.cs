using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidFilling : MonoBehaviour {

    Renderer _renderer;

    [Range(0, 1)][SerializeField]
    private float FillLevel;

    [SerializeField]
    private Color LiquidColor;

    [SerializeField]
    private float _capacity;

    private Vector3 _lastPosition;

    /// <summary>
    /// The capacity of the container in ML
    /// </summary>
    public float Capacity {
        get { return _capacity; }
        set { _capacity = value; }
    }

    private Dictionary<Color, float> _colorAmounts;

    private float _worldLiquidLimit;

    // Use this for initialization
    void Start () {
        _renderer = GetComponent<Renderer>();
        _colorAmounts = new Dictionary<Color, float>();
    }
	
	// Update is called once per frame
	void Update () {
        float newLimit = _renderer.bounds.min.y + (_renderer.bounds.max.y - _renderer.bounds.min.y) * FillLevel;
        if(FillLevel == 0)
        {
            newLimit -= 0.1f;
        }
        if(_worldLiquidLimit != newLimit)
        {
            _worldLiquidLimit = newLimit;
            _renderer.sharedMaterial.SetFloat("_LiquidHeight", _worldLiquidLimit);
        }
    }

    public void SetFillLevel(float level)
    {
        FillLevel = Mathf.Clamp(level, 0, 1);
    }

    public void SetColor(Color color, float amount, Unit unit)
    {

        amount = UnitConversion.ToMl(amount, unit);

        if (_colorAmounts.ContainsKey(color))
        {
            _colorAmounts[color] = amount;
        } else
        {
            _colorAmounts.Add(color, amount);
        }
        

        Color c = Color.black;
        float sum = 0;
        foreach(KeyValuePair<Color, float> colorAndFilling in _colorAmounts)
        {
            if (c == Color.black)
            {
                c = colorAndFilling.Key;
                sum = colorAndFilling.Value;
                continue;
            }
            float weight = colorAndFilling.Value / (sum + colorAndFilling.Value);
            c = Color.Lerp(c, colorAndFilling.Key, weight);
            sum += colorAndFilling.Value;
        }

        LiquidColor = c;
        _renderer.sharedMaterial.SetColor("_LiquidColor", LiquidColor);
        SetFillLevel(sum / Capacity);
    }

    private void OnValidate()
    {
        if(_renderer != null)
        {
            _renderer.sharedMaterial.SetFloat("_LiquidHeight", _worldLiquidLimit);
            _renderer.sharedMaterial.SetColor("_LiquidColor", LiquidColor);
        }
    }
    
}
