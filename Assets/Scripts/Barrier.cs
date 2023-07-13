using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField]
    private float _respawnTimer;
    [SerializeField]
    private bool disabled;
    [SerializeField]
    private int barrierHealth;
    [SerializeField]
    private MeshRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        barrierHealth = 5;
        _respawnTimer = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (disabled == false)
        {
            _respawnTimer = 10f;
        }
        if(barrierHealth <= 0)
        {
            disabled = true;
        }

        if(disabled == true)
        {
            _renderer.enabled = false;
            _respawnTimer -= Time.deltaTime;
            
            if (_respawnTimer < 0)
            {
                _renderer.enabled = true;
                barrierHealth = 5;
                disabled = false;
            }
        }
    }

   public void Damage()
    {
        barrierHealth--;
    }
}
