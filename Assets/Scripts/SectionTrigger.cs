using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject roadSection;

    private bool hasSpawned = false; // Flag to prevent multiple instantiations

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider has the correct tag and if we haven't spawned the section yet
        if (other.gameObject.CompareTag("Trigger_for_NS") && !hasSpawned)
        {
            Instantiate(roadSection, new Vector3(0, 0, 128), Quaternion.identity);
            hasSpawned = true; // Set the flag to prevent further spawns
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset the flag when the player exits the trigger area
        if (other.gameObject.CompareTag("Trigger_for_NS"))
        {
            hasSpawned = false;
        }
    }
}
