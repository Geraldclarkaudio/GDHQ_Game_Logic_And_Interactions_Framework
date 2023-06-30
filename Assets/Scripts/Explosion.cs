using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{ 
    public void Start()
    {
        Destroy(this.gameObject, 3.0f);
    }

    public void HitEnemy()
    {
        //check if this collider hit the enemy. if so, kill it. 
    }
}
