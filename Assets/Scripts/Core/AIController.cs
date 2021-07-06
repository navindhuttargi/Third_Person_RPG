using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]Transform player;
    [SerializeField] float chaseDistance = 5;
    private void Update()
    {
        if (IsInRange())
            Debug.Log(transform.name + ": follow player");
    }
    bool IsInRange()
    {
        return Vector3.Distance(player.position, transform.position) < 5;
    }
}
