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
        
        Animator animator;

        Mover mover;
        ActionSchedular actionSchedular;

        float elapsedAttackTime = 0;

        Health target;
        private void Start()
        {
            animator = GetComponent<Animator>();
            mover = GetComponent<Mover>();
            actionSchedular = GetComponent<ActionSchedular>();
        }
        private void Update()
        {
            elapsedAttackTime += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;
            if (target != null && !IsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                StartAttack();
                mover.Cancel();
            }
        }

        private void StartAttack()
        {
            transform.LookAt(target.transform.position, Vector3.up);
            if (elapsedAttackTime > timeBeteenAttack)
            {
                elapsedAttackTime = 0;
                TriggerAttack();
            }
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null)
                return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Cancel()
        {
            target = null;
            TriggerStopAttack();
        }

        private void TriggerStopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        public void Attack(CombatTarget combatTarget)
        {
            actionSchedular.StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }
        private void Hit()
        {
            if(target!=null)
            target.TakeDamage(damage);
        }
    }
}