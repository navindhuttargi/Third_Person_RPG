using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] GameObject targetObject;
        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (targetObject != null)
                    Destroy(targetObject);
                else
                    Destroy(gameObject);
            }
        }
    }
}