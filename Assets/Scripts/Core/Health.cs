using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float health = 20;
        bool isDead = false;
        Animator animator;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        public bool IsDead()
        {
            return isDead;
        }
        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);
            if (health == 0)
            {
                TriggerDieAnimation();
            }
            
        }

        private void TriggerDieAnimation()
        {
            if (isDead)
                return;
            isDead = true;
            animator.SetTrigger("die");
            GetComponent<ActionSchedular>().CancelCurrentAction();
        }
    }
}