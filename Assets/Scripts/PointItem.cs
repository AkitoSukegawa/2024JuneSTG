using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointItem : ItemBase
{
    [SerializeField] int _score = 100;
    public override void OnActivated()
    {
        FindObjectOfType<ScoreManager>().AddScore(_score);
    }
}
