using System.Collections;
using UnityEngine;

public class CollisionWithObstacles : MonoBehaviour
{
    public CameraShake cameraShake; // Referință la CameraShake
    public PlayerMovement playerMovement;

    public int lives = 3; // Numarul de vieti
    public GameObject[] lifeObjects; // Obiectele de viață în scenă

    void Start()
    {
        // Obține referința la CameraShake
        cameraShake = Camera.main.GetComponent<CameraShake>();
        playerMovement = GetComponent<PlayerMovement>();

        // Initializează UI - ul de vieți
        UpdateLivesUI();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Reduce o viață
            lives--;
            UpdateLivesUI();

            // Pornim efectul de camera shake
            cameraShake.TriggerShake();

            // Verifică dacă jucătorul a rămas fără vieți
            if (lives <= 0)
            {
                //Game Over
                Debug.Log("Game Over!");
            }
        }
        if (collision.CompareTag("leftSide"))
        {
            playerMovement.ChangeLane(-1);
        }
        if (collision.CompareTag("rightSide"))
        {
            playerMovement.ChangeLane(1);
        }
    }

    void UpdateLivesUI()
    {
        for (int i = 0; i < lifeObjects.Length; i++)
        {
            // Activează sau dezactivează obiectele de viață în funcție de numărul de vieți rămase
            lifeObjects[i].SetActive(i < lives);
        }
    }
}
