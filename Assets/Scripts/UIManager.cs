using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;

    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    public void UpdateScore(int addScore)
    {
        _player._score += addScore;
        _scoreText.text = _player._score.ToString();
    }
}
