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
            // Luam toate obiectele cu tag-ul "Road" si alegem pe ultima
            GameObject[] roads = GameObject.FindGameObjectsWithTag("Road");
            GameObject lastRoad = roads[roads.Length - 1];

            // Calculam coordonatele sectiunii curente a drumului
            Vector3 currentSectionPosition = lastRoad.transform.position;
            // Instantiem o noua sectiune de drum la coordonatele sectiunii curente
            Instantiate(roadSection, new Vector3(currentSectionPosition.x, currentSectionPosition.y, currentSectionPosition.z + 99.9f), Quaternion.identity);

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
