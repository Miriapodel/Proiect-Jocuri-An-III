using System.Collections;
using UnityEngine;

public class CollisionWithObstacles : MonoBehaviour
{
    public CameraShake cameraShake; // Referință la CameraShake
    public PlayerMovement playerMovement;

    public int lives = 3; // Numarul de vieti
    public GameObject[] lifeObjects; // Obiectele de viață în scenă

    private bool recentlyDamaged = false; // Flag pentru a verifica dacă jucătorul a fost recent lovit
    public float damageCooldown = 0.5f; // Durata cooldown-ului pentru damage

    void Start()
    {
        // Obține referința la CameraShake
        cameraShake = Camera.main.GetComponent<CameraShake>();
        playerMovement = GetComponent<PlayerMovement>();

        // Initializează UI-ul de vieți
        UpdateLivesUI();
    }

    void OnTriggerEnter(Collider collision)
    {
        // Verifică dacă jucătorul a intrat în coliziune cu un obstacol și nu a fost recent lovit
        if (collision.gameObject.CompareTag("Obstacle") && !recentlyDamaged)
        {
            // Calculează direcția coliziunii pentru a determina dacă este frontală
            Vector3 directionToObstacle = (collision.transform.position - transform.position).normalized;
            float dotProduct = Vector3.Dot(transform.forward, directionToObstacle);

            if (dotProduct > 0.99f) // Aproximativ frontal (1.0 ar fi direct frontal)
            {
                // Coliziune frontală - pierde toate viețile
                lives = 0;
                UpdateLivesUI();
                Debug.Log("Game Over!");

                // Pornim efectul de camera shake mai intens
                cameraShake.TriggerShake();
            }
            else
            {
                // Coliziune laterală - pierde o viață
                StartCoroutine(HandleDamage()); // Pornește coroutine pentru a aplica cooldown

                lives--;
                UpdateLivesUI();

                // Pornim efectul de camera shake
                cameraShake.TriggerShake();

                // Verifică dacă jucătorul a rămas fără vieți
                if (lives <= 0)
                {
                    Debug.Log("Game Over!");
                }
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

    IEnumerator HandleDamage()
    {
        recentlyDamaged = true; // Setează flag-ul pentru a evita damage-ul dublu
        yield return new WaitForSeconds(damageCooldown); // Așteaptă durata cooldown-ului
        recentlyDamaged = false; // Resetează flag-ul după cooldown
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
