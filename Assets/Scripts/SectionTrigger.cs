using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject roadSection;

    private bool hasSpawned = false; // Flag pentru a verifica daca am spawnat deja sectiunea

    private void OnTriggerEnter(Collider other)
    {
        // Verifica daca jucatorul a intrat in trigger si daca nu am spawnat deja sectiunea
        if (other.gameObject.CompareTag("Trigger_for_NS") && !hasSpawned)
        {
            Instantiate(roadSection, new Vector3(0, 0, 128), Quaternion.identity);
            hasSpawned = true; // Seteaza flag-ul ca am spawnat sectiunea
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Resetam flag-ul daca jucatorul a iesit din trigger
        if (other.gameObject.CompareTag("Trigger_for_NS"))
        {
            hasSpawned = false;
        }
    }
}
