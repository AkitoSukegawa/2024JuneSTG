using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItem : ItemBase
{
    [SerializeField] int _power = 1;
    public override void OnActivated()
    {
        FindObjectOfType<ScoreManager>().AddPower(_power);
    }
}
