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
    private Animator _anim;
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
    [SerializeField]
    private float _deathTimer;

    public int killPoint = 50;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponentInChildren<Animator>();
        transform.position = _spawnPoint.transform.position;
        _agent.destination = _waypoints[_currentPoint].position;

        _waypoints[0] = GameObject.Find("StartPoint").GetComponent<Transform>();
        _waypoints[3] = GameObject.Find("EndPoint").GetComponent<Transform>();
        _hideTimer = Random.Range(3.0f, 7.0f);

    }

    public void OnEnable()
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = true;
        _currentPoint = 0;
        _currentState = AIState.Running;
        _deathTimer = 3.5f;
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
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        _agent.isStopped = true;
        _deathTimer -= Time.deltaTime;
        _anim.SetBool("Hiding", false);
        _anim.SetBool("Walking", false);
        _anim.SetTrigger("Death");
        if( _deathTimer <= 0 ) 
        {
            this.gameObject.SetActive(false);
            transform.position = _spawnPoint.transform.position;
        }
    }


    private void Hide()
    {
        _anim.SetBool("Walking", false);
        _anim.SetBool("Hiding", true);

        _agent.isStopped = true;
        _hideTimer -= Time.deltaTime;

        if(_hideTimer<= 0)
        {
            _currentState = AIState.Running;
            _hideTimer = Random.Range(3.0f, 7.0f);
        }
    }

    private void Run()
    {
        _anim.SetBool("Hiding", false);
        _anim.SetBool("Walking", true);
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
                AudioManager.Instance.AIComplete();
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
