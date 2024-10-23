using System.Collections;
using UnityEngine;

public class CollisionWithObstacles : MonoBehaviour
{
    public CameraShake cameraShake; // Referinta la CameraShake


    void Start()
    {
        // Obtine referinta la CameraShake
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica daca s-a lovit de un obstacol
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Pornim efectul de camera shake
            cameraShake.TriggerShake();
        }
    }
}
