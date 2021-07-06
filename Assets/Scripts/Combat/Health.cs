using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 20;
    bool isDead = false;
    public void TakeDamage(float damage)
    {
        health = Mathf.Max(health-damage, 0);
        Debug.Log(health);
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
        GetComponent<Animator>().SetTrigger("die");
    }
}
