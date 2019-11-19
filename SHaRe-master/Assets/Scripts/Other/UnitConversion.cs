using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitConversion {

    static Dictionary<Unit, float> toMlFactors;

    static UnitConversion()
    {
        toMlFactors = new Dictionary<Unit, float>();
        toMlFactors.Add(Unit.Ml, 1);
        toMlFactors.Add(Unit.Cup, 236.588f);
        toMlFactors.Add(Unit.Cl, 10);
        toMlFactors.Add(Unit.Tablespoon, 14.7868f);

        foreach(Unit unit in System.Enum.GetValues(typeof(Unit)))
        {
            if (!toMlFactors.ContainsKey(unit))
            {
                Debug.LogWarning("No information on how to convert " + unit.ToString() + " to " + Unit.Ml.ToString());
            }
        }
    }

	public static float ToMl(float amount, Unit unit)
    {
        float factor = 1;
        if(toMlFactors.TryGetValue(unit, out factor))
        {
            amount *= factor;
        }
        return amount;
    }

}
