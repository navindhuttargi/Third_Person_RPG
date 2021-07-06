using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float attackRange = 5;
    Fighter fighter;
    private void Start()
    {
        fighter = GetComponent<Fighter>();
    }
    private void Update()
    {
        if (InAttackRangeOFPlayer() && fighter.CanAttack(player))
        {
            fighter.Attack(player);
        }
        else
        {
            fighter.Cancel();
        }
    }
    bool InAttackRangeOFPlayer()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        return distance < 5;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}
