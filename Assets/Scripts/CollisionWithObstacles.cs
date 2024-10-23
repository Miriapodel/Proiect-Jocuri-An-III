using System.Collections;
using UnityEngine;

public class CollisionWithObstacles : MonoBehaviour
{
    public CameraShake cameraShake; // Referință la CameraShake

    void Start()
    {
        // Obține referința la CameraShake
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Pornim efectul de camera shake
            cameraShake.TriggerShake();
        }
    }
}
