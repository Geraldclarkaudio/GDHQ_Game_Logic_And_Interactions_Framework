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
    public int enemyScore;
    [SerializeField]
    private TMP_Text _ammoText;

    [SerializeField]
    private TMP_Text _timeText;
    [SerializeField]
    public int enemyCount;
    [SerializeField]
    private float timeValue;

    private Player _player;
    [SerializeField]
    private GameObject[] _killStrikes;

    public GameObject youWinPanel;
    public GameObject youLosePanel;

    private void Start()
    {
        enemyScore = 0;
        _player = FindObjectOfType<Player>();
        timeValue = 180f;
    }

    public void UpdateEnemyScore()
    {
        enemyScore++;
    }

    public void UpdateScore(int addScore)
    {
        _player._score += addScore;
        _scoreText.text = _player._score.ToString();
    }

    public void UpdateEnemyCount()
    {
        enemyCount++;
         
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

    public void UpdateAmmoAmount()
    {
        _ammoText.text = _player._ammoCount.ToString();
    }

    public void UpdateKills()
    {
        _killStrikes[_player.kills - 1].SetActive(true);
    }

    public void YouWin()
    {
        youWinPanel.SetActive(true);
    }

    public void YouLose()
    {
        youLosePanel.SetActive(true);
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
