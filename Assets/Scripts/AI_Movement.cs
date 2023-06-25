using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour
{
    private NavMeshAgent _agent;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(_agent.remainingDistance < 0.25f)
        {
            _currentPoint++;
            if(_currentPoint == _waypoints.Count)
            {
                _currentPoint--;
            }
        }

        _agent.SetDestination(_waypoints[_currentPoint].position);
    }
}
