﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float redius = .3f;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(transform.GetChild(i).position, redius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(GetNextPoint(i)));
            }
        }
        public int GetNextPoint(int i)
        {
            if (i + 1 == transform.childCount)
                return 0;
            return i + 1;
        }
        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}