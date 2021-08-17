using RPG.Control;
using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControlRemover : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<PlayableDirector>().played += onDisable;
        GetComponent<PlayableDirector>().stopped += onEnable;
    }

    private void onEnable(PlayableDirector obj)
    {
        player.GetComponent<PlayerController>().enabled = true;
    }

    private void onDisable(PlayableDirector obj)
    {
        GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<ActionScheduler>().CancelCurrentAction();
        player.GetComponent<PlayerController>().enabled = false;
    }
}
