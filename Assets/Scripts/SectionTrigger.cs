using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject roadSection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger_for_NS"))
        {
            Instantiate(roadSection, new Vector3(0, 0, 128), Quaternion.identity);
        }
    }
}
