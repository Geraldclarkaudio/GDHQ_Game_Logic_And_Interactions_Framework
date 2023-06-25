using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.SceneManagement;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField]
    private GameObject _spawnPoint;
    [SerializeField]
    private List<Transform> _waypoints;
    [SerializeField]
    private int _currentPoint = 0;
    [SerializeField]
    private bool _reverse;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();    
        _agent.destination = _waypoints[_currentPoint].position;

        _waypoints[0] = GameObject.Find("StartPoint").GetComponent<Transform>();
        _waypoints[1] = GameObject.Find("EndPoint").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
       if(_agent.transform.position == new Vector3(_waypoints[_currentPoint].position.x, transform.position.y, _waypoints[_currentPoint].position.z))
        {
            if (_currentPoint == _waypoints.Count - 1)
            {
                _currentPoint--;
                this.gameObject.SetActive(false);
                transform.position = _spawnPoint.transform.position;
            }
            else
            {
                _currentPoint++;
            }
        }
       if(this.gameObject.activeSelf)
        {
            _agent.SetDestination(_waypoints[_currentPoint].position);
        }

    }
}
