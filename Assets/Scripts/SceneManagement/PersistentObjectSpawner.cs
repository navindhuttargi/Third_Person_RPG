using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject persistentPrefab;
    static bool hasSpawned = false;
    void Start()
    {
        if (hasSpawned) return;
        SpwanObject();
        hasSpawned = true;
    }

    private void SpwanObject()
    {
        DontDestroyOnLoad(Instantiate(persistentPrefab));
    }
}
