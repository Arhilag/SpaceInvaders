using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ScoreCounter
{
    [SerializeField] private Text _text;
    private int _score;
    
    public void PlusScore()
    {
        _score++;
        _text.text = $"{_score}";
    }
}
