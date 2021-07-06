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

        Health target; 

        private void Update()
        {
            elapsedAttackTime += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;
            if (target != null && !IsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                StartAttack();
                GetComponent<Mover>().Cancel();
            }
        }

        private void StartAttack()
        {
            transform.LookAt(target.transform.position, Vector3.up);
            if (elapsedAttackTime > timeBeteenAttack)
            {
                elapsedAttackTime = 0;
                GetComponent<Animator>().SetTrigger("attack");
            }
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Cancel()
        {
            target = null;
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionSchedular>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }
        private void Hit()
        {
            if(target!=null)
            target.TakeDamage(damage);
        }
    }
}