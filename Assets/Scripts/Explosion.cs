using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{ 
    public void Start()
    {
        Destroy(this.gameObject, 3.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            AI_Movement hitEnemy = other.GetComponent<AI_Movement>();
            hitEnemy._currentState = AI_Movement.AIState.Death;
        }
    }
}
