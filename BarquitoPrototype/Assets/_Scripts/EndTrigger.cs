using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] private UIManager uIManager;

    [SerializeField] private int numberOfIslandsToConnect = 2;
    [SerializeField] private Collider islandCollider;

    private int islandCounter = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            islandCounter++;

            if(islandCounter >= numberOfIslandsToConnect)
            {
                uIManager.ActivateEndMenu();
                islandCollider.enabled = false; 
            }
        }
    }
}
