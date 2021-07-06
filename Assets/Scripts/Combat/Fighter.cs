using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBeteenAttack=1;
        [SerializeField] float damage = 5;

        float elapsedAttackTime = 0;

        Transform target; 

        private void Update()
        {
            elapsedAttackTime += Time.deltaTime;
            if (target == null) return;
            if (target != null && !IsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                StartAttack();
                GetComponent<Mover>().Cancel();
            }
        }

        private void StartAttack()
        {
            if (elapsedAttackTime > timeBeteenAttack)
            {
                elapsedAttackTime = 0;
                GetComponent<Animator>().SetTrigger("attack");
            }
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Cancel()
        {
            target = null;
        }
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            target = combatTarget.transform;
        }
        private void Hit()
        {
            target.GetComponent<Health>().TakeDamage(damage);
        }
    }
}