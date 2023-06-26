using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UI Manager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private TMP_Text _enemyountText;
    [SerializeField]
    private TMP_Text _timeText;
    [SerializeField]
    private int enemyCount;
    [SerializeField]
    private float timeValue;

    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        timeValue = 120f;
    }

    public void UpdateScore(int addScore)
    {
        _player._score += addScore;
        _scoreText.text = _player._score.ToString();
    }

    public void UpdateEnemyCount()
    {
        enemyCount++;
        //enemy count text is equal to how many enemies are set active. 
        _enemyountText.text = enemyCount.ToString();
    }

    public void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float Minute = Mathf.FloorToInt(timeToDisplay / 60f);
        float Second = Mathf.FloorToInt(timeToDisplay % 60); // takes the remainder 

        _timeText.text = string.Format("{0:00}:{1:00}", Minute, Second);

    }

    private void Update()
    {

        if(timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
            //END GAME
        }

        DisplayTime(timeValue);
    }
}
