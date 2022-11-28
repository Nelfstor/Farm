using Global;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private State _currentState;
    public State CurrentState => _currentState;

    AnimatorController animatorController;  
 
    NavMeshAgent navMeshAgent;
    Vector3 targetPosition;    
    private Cell _targetCell;

    public enum State
    {
        putting,
        collecting,
        harvesting,
        moving,        
        idle
    }

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        animatorController = GetComponent<AnimatorController>();        
    }

    private void Start()
    {
        SetState(State.idle);
    }

    private void Update()
    {
        if (_currentState == State.moving)
        {
            if (Vector3.Distance(navMeshAgent.destination, transform.position) < navMeshAgent.stoppingDistance)
            {
                SetState(State.idle);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cell cell))
        {
            if (cell.Equals(_targetCell))
            {
                if (cell.State == CellState.collectable)
                {                    
                    if (cell.Plant.name.Contains("Grass"))
                        SetState(State.harvesting);
                    else
                        SetState(State.collecting);
                }
                else 
                {
                    SetState(State.putting);
                }
            }
        }
    }
    public void PuttingFinished() //raised by animation
    {
        _targetCell.PutByPlayer();
        StartCoroutine(WalkAround(3));
    }

    public void CollectionFinished() //raised by animation
    {        
        _targetCell.CollectPlant();
        StartCoroutine(WalkAround(1));
    }

    public void MoveToNextCell(Cell cell)
    {
        if (_currentState == State.idle)
        {
            _targetCell = cell;
            targetPosition = cell.transform.position;
            navMeshAgent.destination = targetPosition;
            SetState(State.moving);
        }
    }

    private IEnumerator WalkAround(float delay)
    {
        yield return new WaitForSeconds(delay);        
        SetState(State.moving);
        navMeshAgent.destination = transform.position + Random.onUnitSphere * 2f; 
    }

    void SetState(State state)
    {
        //Global.Log.MyGreenLog($"Setting State {state}");
        _currentState = state;
        CameraManager.Instance.SwitchToPlayer();
        switch (state)
        {
            case State.moving:
                animatorController.RunAnimation();                
                break;
            case State.idle:
                animatorController.IdleAnimation();                
                break;
            case State.collecting:
                animatorController.RunToCollect();                
                break;
            case State.harvesting:
                animatorController.RunToHarvest();
                break;
            case State.putting:
                animatorController.Put();
                break;
            default:
                throw new System.ArgumentException();
        }  
    }
}

