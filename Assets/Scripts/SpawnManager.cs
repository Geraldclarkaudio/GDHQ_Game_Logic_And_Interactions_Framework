using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;
    public static SpawnManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("SpawnManager is null");
            }
            return _instance;
        }
    }

    [SerializeField]
    private Transform _spawnPoint;
    [SerializeField]
    private int amountToPool;
    [SerializeField]
    private List<GameObject> spawnList;
    [SerializeField]
    private GameObject AIPrefab;

    [SerializeField]
    private float _spawnCountDownTimer;

    private void Awake()
    {
        _spawnCountDownTimer = 2.0f;

        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        spawnList = new List<GameObject>();
        GameObject tmp;

        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(AIPrefab);
            tmp.SetActive(false);
            spawnList.Add(tmp);
        }
        spawnList[0].SetActive(true);
    }

    public GameObject GetPooledObject() 
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if (!spawnList[i].gameObject.activeSelf)
            {
                return spawnList[i];
            }
        }
        return null;
    }

    void Update()
    {
        _spawnCountDownTimer -= Time.deltaTime;
        if(_spawnCountDownTimer <= 0)
        {
            GameObject AI = GetPooledObject();
            if(AI != null)
            {
                AI.SetActive(true);
            }
            _spawnCountDownTimer = 2.0f;
        }
    }


}
