using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalty {

    private string _reason;

    private float _deduction;

    public Penalty(string reason, float deduction)
    {
        this.Reason = reason;
        this.Deduction = deduction;
    }

    public string Reason
    {
        get
        {
            return _reason;
        }

        set
        {
            _reason = value;
        }
    }

    public float Deduction
    {
        get
        {
            return _deduction;
        }

        set
        {
            _deduction = value;
        }
    }
}
