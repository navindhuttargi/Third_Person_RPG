using RPG.Combat;
using RPG.Control;
using RPG.Core;
using RPG.Movement;
using System;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float chaseDistance = 5;
    [SerializeField] float suspecionTime = 3;
    [SerializeField] float patrolTolerence = 1;
    [SerializeField] PatrolPath patrolPath;
    
    float timeSinceLastSawPlayer = 0;

    int currentWaypointIndex = 0;

    Fighter fighter;
    Health health;
    Mover mover;
    ActionSchedular actionSchedular;

    Vector3 guardPosition;
    Vector3 nextPosition;
    private void Start()
    {
        mover = GetComponent<Mover>();
        health = GetComponent<Health>();
        fighter = GetComponent<Fighter>();
        guardPosition = transform.position;
        actionSchedular = GetComponent<ActionSchedular>();
    }
    private void Update()
    {
        if (health.IsDead()) return;
        if (InAttackRangeOFPlayer() && fighter.CanAttack(player))
        {
            timeSinceLastSawPlayer = 0;
            AttackBehaviour();
        }
        else if (timeSinceLastSawPlayer < suspecionTime)
        {
            SuspicionBehaviour();
        }
        else
        {
            PatrollingBehaviour();
        }
        timeSinceLastSawPlayer += Time.deltaTime;
    }

    private void PatrollingBehaviour()
    {
        nextPosition = guardPosition;
        if (patrolPath != null)
        {
            if (AtWayPoint())
            {
                CycleWaypoint();
            }
            nextPosition = GetCurrentWayPoint();
        }
        mover.StartMovementAction(nextPosition);
    }

    private Vector3 GetCurrentWayPoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    private void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextPoint(currentWaypointIndex);
    }

    private bool AtWayPoint()
    {
        float distance = Vector3.Distance(transform.position, GetCurrentWayPoint());
        return distance < patrolTolerence;
    }

    private void SuspicionBehaviour()
    {
        actionSchedular.CancelCurrentAction();
    }

    private void AttackBehaviour()
    {
        fighter.Attack(player);
    }

    bool InAttackRangeOFPlayer()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        return distance < 5;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
