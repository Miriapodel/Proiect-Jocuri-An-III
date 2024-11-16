using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool : MonoBehaviour
{
    public static PowerUpPool Instance;

    [SerializeField]
    private GameObject[] powerUpPrefabs; // Prefabricate pentru tipurile de power-up-uri
    private readonly int poolSize = 15; // Numărul de power-up-uri în pool
    private readonly List<GameObject> powerUpPool = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Adaugă câte un power-up din fiecare tip
        foreach (GameObject prefab in powerUpPrefabs)
        {
            GameObject powerUp = Instantiate(prefab);
            powerUp.SetActive(false);
            powerUpPool.Add(powerUp);
        }

        // Completează restul pool-ului cu power-up-uri aleatorii
        int powerUpsRamase = poolSize - powerUpPrefabs.Length;
        for (int i = 0; i < powerUpsRamase; i++)
        {
            int indexPowerUp = Random.Range(0, powerUpPrefabs.Length);
            GameObject powerUp = Instantiate(powerUpPrefabs[indexPowerUp]);
            powerUp.SetActive(false);
            powerUpPool.Add(powerUp);
        }
    }

    public GameObject GetPooledPowerUp()
    {
        foreach (GameObject powerUp in powerUpPool)
        {
            if (powerUp != null && !powerUp.activeInHierarchy)
            {
                return powerUp;
            }
        }

        // Extinde pool-ul dacă este necesar
        int indexPowerUp = Random.Range(0, powerUpPrefabs.Length);
        GameObject powerUpNou = Instantiate(powerUpPrefabs[indexPowerUp]);
        powerUpNou.SetActive(false);
        powerUpPool.Add(powerUpNou);
        return powerUpNou;
    }
}
