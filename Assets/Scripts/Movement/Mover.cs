using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour,IAction
    {
        [SerializeField] Transform target;

        NavMeshAgent navMeshAgent;
        ActionSchedular actionSchedular;
        Animator animator;
        private void Start() {
            actionSchedular = GetComponent<ActionSchedular>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            UpdateAnimator();
        }
        public void StartMovementAction(Vector3 destination)
        {
            actionSchedular.StartAction(this);
            MoveTo(destination);
        }
        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }
    }
}