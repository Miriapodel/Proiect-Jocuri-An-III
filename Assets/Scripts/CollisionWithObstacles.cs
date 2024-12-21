using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CollisionWithObstacles : MonoBehaviour
{
    public CameraShake cameraShake; // Referință la CameraShake
    public PlayerMovement playerMovement;
    public int lives = 3; // Numarul de vieți
    public Image[] lifeIcons; // Imaginile de overlay pentru vieți

    private bool recentlyDamaged = false; // Flag pentru a verifica dacă jucătorul a fost recent lovit
    public float damageCooldown = 0.5f; // Durata cooldown-ului pentru damage

    private bool hasDisableObstaclesPowerUp = false; // Flag pentru activarea puterii
    private Coroutine powerUpCoroutine; // Referință la corutina activă
    public float powerUpDuration = 5f; // Durata efectului power-up-ului
    [SerializeField]
    private Image bombImage;   // imaginea pt puterea cu bomba
    private bool doubleCoinActive = false; // Flag pentru efectul de dublare
    private Coroutine doubleCoinCoroutine; // Referință la corutina activă pentru dublare
    public float doubleCoinDuration = 5f; // Durata efectului de dublare
    [SerializeField]
    private Image coinImage; // Imagine pentru power-up-ul de dublare

    private int nrCoin = 0;
    private float roundStartTime; // Timpul de start al rundei
    private float roundScore; // Scorul rundei

    [SerializeField]
    private TextMeshProUGUI coinsText; 


    void Start()
    {
        // Obține referința la CameraShake
        cameraShake = Camera.main.GetComponent<CameraShake>();
        playerMovement = GetComponent<PlayerMovement>();

        // Initializează UI-ul de vieți
        UpdateLivesUI();

        // Încarcă numărul total de monede colectate
        nrCoin = PlayerPrefs.GetInt("TotalCoins", 0);
        UpdateCoinsUI();

        roundStartTime = Time.time; // Marchează timpul de start al rundei

        bombImage = GameObject.FindGameObjectsWithTag("bombImage")[0].GetComponent<Image>();


        if (bombImage != null)
        {
            bombImage.gameObject.SetActive(false); // ascunde imaginea la început
        }

        coinImage = GameObject.FindGameObjectsWithTag("coinImage")[0].GetComponent<Image>();

        if (coinImage != null)
        {
            coinImage.gameObject.SetActive(false); // ascunde imaginea la început
        }

    }

    void Update()
    {
        // Calculează scorul curent bazat pe timpul scurs
        roundScore = (Time.time - roundStartTime) * 1000f; // Milisecunde
    }

    void OnTriggerEnter(Collider collision)
    {
        // Verifică dacă jucătorul a intrat în coliziune cu un obstacol și nu a fost recent lovit
        if (collision.gameObject.CompareTag("Obstacle") && !recentlyDamaged && !hasDisableObstaclesPowerUp)
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
                EndGame();

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
                    EndGame();
                }
            }
        }
        if ((collision.gameObject.CompareTag("Obstacle") || collision.CompareTag("frontSide")) && !recentlyDamaged && hasDisableObstaclesPowerUp)
        {
            // Dezactivează obstacolul părinte
            Transform parent = collision.transform.parent;

            if (parent != null && parent.CompareTag("ObstacleMainComponent")) // Verifică tag-ul părintelui
            {
                parent.gameObject.SetActive(false); // Dezactivează întregul părinte
            }
            else
            {
                collision.gameObject.SetActive(false);
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
        if(collision.CompareTag("frontSide") && !recentlyDamaged && !hasDisableObstaclesPowerUp)
        {
            lives = 0;
            UpdateLivesUI();
            Debug.Log("Game Over!");
            EndGame();

            // Pornim efectul de camera shake mai intens
            cameraShake.TriggerShake();
        }
        if (collision.CompareTag("Bomb"))
        {
            if (!hasDisableObstaclesPowerUp) // Verifică dacă efectul nu este deja activ
            {
                collision.gameObject.SetActive(false); // Dezactivează bomba
                hasDisableObstaclesPowerUp = true; // Activează efectul power-up-ului
                powerUpCoroutine = StartCoroutine(DisablePowerUpAfterTime()); // Pornește durata power-up-ului
            }
            else
            {
                Debug.Log("Power-up-ul este deja activ!");
            }
        }

        // if (collision.CompareTag("Coin"))
        // {
        //     collision.gameObject.SetActive(false);
        //     nrCoin += 1;

        //     // Actualizează numărul total de monede colectate
        //     int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0); // Citește valoarea curentă
        //     totalCoins += 1; // Adaugă moneda colectată
        //     PlayerPrefs.SetInt("TotalCoins", totalCoins); // Salvează în PlayerPrefs
        //     PlayerPrefs.Save(); // Salvează imediat pe disc

        //     // Actualizează UI-ul monedelor
        //     UpdateCoinsUI();
        // }


        if (collision.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
            int coinsToAdd = doubleCoinActive ? 2 : 1; // Dacă efectul este activ, dublează moneda

            nrCoin += coinsToAdd;

            // Actualizează numărul total de monede colectate
            int totalCoins = PlayerPrefs.GetInt("TotalCoins", 0); // Citește valoarea curentă
            totalCoins += coinsToAdd; // Adaugă monedele colectate
            PlayerPrefs.SetInt("TotalCoins", totalCoins); // Salvează în PlayerPrefs
            PlayerPrefs.Save(); // Salvează imediat pe disc

            // Actualizează UI-ul monedelor
            UpdateCoinsUI();
        }


        if (collision.CompareTag("arrow"))
        {
            if (!doubleCoinActive)                                          // Verifică dacă efectul nu este deja activ
            {
                collision.gameObject.SetActive(false);                      // Dezactivează săgeata
                doubleCoinActive = true;                                    // Activează efectul power-up-ului
                doubleCoinCoroutine = StartCoroutine(DoubleCoinPowerUp());  // Pornește durata power-up-ului
            }
            else
            {
                Debug.Log("Power-up-ul de dublare este deja activ!");
            }
        }

    }

    void EndGame()
    {
        // Salvează scorul actual
        PlayerPrefs.SetFloat("LastRoundScore", roundScore);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOver");
    }

    private IEnumerator DisablePowerUpAfterTime()
    {
        if (bombImage != null)
        {
            bombImage.gameObject.SetActive(true);       // activeaza imaginea
        }
        Debug.Log("Efectul power-up-ului activat!");
        yield return new WaitForSeconds(powerUpDuration); // Așteaptă durata efectului
        hasDisableObstaclesPowerUp = false; // Dezactivează efectul
        powerUpCoroutine = null; // Resetează referința corutinei
        Debug.Log("Efectul power-up-ului a expirat!");
        //ascunde imaginea de pe ecran
        if (bombImage != null)
        {
            bombImage.gameObject.SetActive(false);      // dezactiveaza imaginea
        }
    }

    private IEnumerator DoubleCoinPowerUp()
    {
        if (coinImage != null)
        {
            coinImage.gameObject.SetActive(true);               // Activează imaginea
        }
        Debug.Log("Efectul de dublare activat!");
        yield return new WaitForSeconds(doubleCoinDuration);    // Așteaptă durata efectului
        doubleCoinActive = false;                               // Dezactivează efectul
        doubleCoinCoroutine = null;                             // Resetează referința corutinei
        Debug.Log("Efectul de dublare a expirat!");
        if (coinImage != null)
        {
            coinImage.gameObject.SetActive(false);              // Dezactivează imaginea
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
        // Activează sau dezactivează imaginile de viață în funcție de numărul de vieți rămase
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].enabled = (i < lives); // Activează imaginea dacă jucătorul are viața respectivă
        }
    }

    private void UpdateCoinsUI()
    {
        coinsText.text = "Coins: " + nrCoin; // actualizare text cu nr monede
    } 
}

