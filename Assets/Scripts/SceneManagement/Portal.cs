using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum PortalDestinations
        {
            a,b,c,d
        }
        [SerializeField]PortalDestinations destinationPortal;
        [SerializeField]
        Transform spawnPoint;
        [SerializeField]
        int sceneIndex;
        [SerializeField]
        float fadeInTime=1, fadeOutTime=.5f, fadeWaitTime=.5f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }
        }
        IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            Fader fader = FindObjectOfType<Fader>();
            yield return fader.FadeIn(fadeInTime);
            yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
            Portal portal = GetOtherPortal();
            UpdatePlayer(portal);
                yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeOut(fadeOutTime);
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            Portal[] portals = FindObjectsOfType<Portal>();
            foreach (var item in portals)
            {
                if (item != this && item.destinationPortal==destinationPortal) return item;
            }
            return null;
        }
    }
}