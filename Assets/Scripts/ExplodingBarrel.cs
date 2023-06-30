using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingBarrel : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionPrefab;
    
    public void Shot()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        Destroy(this.gameObject, 1.0f);
    }
}
