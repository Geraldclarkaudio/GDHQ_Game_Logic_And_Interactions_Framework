using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("Game Manager is Null");
            }
            return _instance;
        }
    }

    public bool youWin { get;set; }
    public bool youLose { get; set; }   


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

    private void Update()
    {
        if(youWin == true)
        {
            YouWin();
        }
        if(youLose == true) 
        {
            YouLose();
        }
    }

    public void YouWin()
    {
        UIManager.Instance.YouWin();
        Cursor.lockState = CursorLockMode.None;
        //you win canvas active restart button
    }

    public void YouLose()
    {
        UIManager.Instance.YouLose();
        Cursor.lockState = CursorLockMode.None;
        //you lose canvas active reestart button. 
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
