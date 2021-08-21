using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed=3,damage=0;
        Health target;
        private void Update()
        {
            if (target == null) return;
            Vector3 targetPosition = GetTargetPosition(target.transform);
            transform.LookAt(targetPosition);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        public void SetTarget(Health target,float damage)
        {
            this.target = target;
            this.damage = damage;
        }
        private Vector3 GetTargetPosition(Transform targetTransform)
        {
            CapsuleCollider sphereCollider = targetTransform.GetComponent<CapsuleCollider>();
            return targetTransform.position + Vector3.up * sphereCollider.height/2;
        }
        private void OnTriggerEnter(Collider other)
            {
            if (other.GetComponent<Health>())
                other.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}