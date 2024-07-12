using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    /// <summary>スコアを表示するテキスト</summary>
    [SerializeField] TextMeshProUGUI _scoreText = default;
    /// <summary>パワーを表示するテキスト</summary>
    [SerializeField] TextMeshProUGUI _powerText = default;

    int _score = 0;
    public int Power{ get; private set; }
public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = _score.ToString("D5");
    }

    public void AddPower(int power)
    {
        Power += power;
        if (Power >= 100)
        {
            Power = 100;
        }
        _powerText.text = Power.ToString("D3");
    }
}
