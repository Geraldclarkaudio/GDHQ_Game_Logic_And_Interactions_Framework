using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.SceneManagement;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour
{
    public enum AIState
    {
        Running,
        Hiding,
        Death
    }

    [SerializeField]
    public AIState _currentState;

    private NavMeshAgent _agent;
    [SerializeField]
    private GameObject _spawnPoint;
    [SerializeField]
    private List<Transform> _waypoints;
    [SerializeField]
    private int _currentPoint = 0;
    [SerializeField]
    private bool _reverse;

    [SerializeField]
    private float _hideTimer;

    public int killPoint = 50;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();    
        _agent.destination = _waypoints[_currentPoint].position;

        _waypoints[0] = GameObject.Find("StartPoint").GetComponent<Transform>();
        _waypoints[3] = GameObject.Find("EndPoint").GetComponent<Transform>();
        _hideTimer = 3.0f;
    }

    public void OnEnable()
    {
        _currentPoint = 0;
        _currentState = AIState.Running;
    }

    // Update is called once per frame
    void Update()
    {
       switch( _currentState )
        {
            case AIState.Running:
                Run();
                break;
            case AIState.Hiding:
                Hide();
                break;
            case AIState.Death:
                Death();
                break;
        }
    }

    private void Death()
    {
        this.gameObject.SetActive( false );
        transform.position = _spawnPoint.transform.position;
    }

    private void Hide()
    {
        _agent.isStopped = true;
        _hideTimer -= Time.deltaTime;

        if(_hideTimer<= 0)
        {
            _currentState = AIState.Running;
            _hideTimer = 3.0f;
        }
    }

    private void Run()
    {
        _agent.isStopped = false;
        //if at a waypoint
        if (_agent.transform.position == new Vector3(_waypoints[_currentPoint].position.x, transform.position.y, _waypoints[_currentPoint].position.z))
        {
            //if at the end
            if (_currentPoint == _waypoints.Count - 1)
            {
                //start at beginning
                _currentPoint--;
                this.gameObject.SetActive(false);
                transform.position = _spawnPoint.transform.position;
            }
            else // otherwise
            {
                _currentPoint++;
            }

            _currentState = AIState.Hiding;
        }
        if (this.gameObject.activeSelf)
        {
            _agent.SetDestination(_waypoints[_currentPoint].position);
        }
    }
}
