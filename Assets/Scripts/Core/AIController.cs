using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float chaseDistance = 5;
    [SerializeField] float suspecionTime = 3;

    float timeSinceLastSawPlayer = 0;

    Fighter fighter;
    Health health;
    Mover mover;
    ActionSchedular actionSchedular;

    Vector3 guardPosition;
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
            GuardBehaviour();
        }
        timeSinceLastSawPlayer += Time.deltaTime;
    }

    private void GuardBehaviour()
    {
        mover.StartMovementAction(guardPosition);
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
