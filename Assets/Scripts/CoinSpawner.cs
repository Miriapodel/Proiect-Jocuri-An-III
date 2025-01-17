using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject coinPrefab;  // Prefab-ul pentru moneda

    private List<Transform> coinSpawnPoints;  // Lista cu punctele de spawn

    private void Start()
    {
        // Inițializează lista cu punctele de spawn
        coinSpawnPoints = new List<Transform>();

        // Adaugă toate obiectele Empty GameObjects din copii acestui obiect care sunt puncte de spawn
        foreach (Transform child in transform)
        {
            if (child.CompareTag("CoinSpawn"))
            {
                coinSpawnPoints.Add(child);
            }
        }

        // Generează monede la punctele de spawn
        SpawnCoins();
    }

    private void SpawnCoins()
    {
        foreach (Transform spawnPoint in coinSpawnPoints)
        {
            // Instanțiază moneda la fiecare punct de spawn
            Instantiate(coinPrefab, spawnPoint.position, Quaternion.identity, transform);
        }
    }
}
