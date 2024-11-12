using System.Collections.Generic;
using UnityEngine;

public class ObstaclePooler : MonoBehaviour
{
    public static ObstaclePooler Instance;

    [SerializeField]
    private GameObject[] obstacolePrefabs;
    private readonly int poolSize = 15;
    private readonly List<GameObject> obstacolePool = new List<GameObject>();

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

        // Adaugă câte un obstacol din fiecare tip
        foreach (GameObject prefab in obstacolePrefabs)
        {
            GameObject obstacol = Instantiate(prefab);
            obstacol.SetActive(false);
            obstacolePool.Add(obstacol);
        }

        // Completează restul pool-ului cu obstacole aleatorii
        int obstacoleRamase = poolSize - obstacolePrefabs.Length;
        for (int i = 0; i < obstacoleRamase; i++)
        {
            int indexObstacol = Random.Range(0, obstacolePrefabs.Length);
            GameObject obstacol = Instantiate(obstacolePrefabs[indexObstacol]);
            obstacol.SetActive(false);
            obstacolePool.Add(obstacol);
        }
    }

    public GameObject GetPooledObstacle()
    {
        foreach (GameObject obstacol in obstacolePool)
        {
            if (obstacol != null && !obstacol.activeInHierarchy)
            {
                return obstacol;
            }
        }

        // Extinde pool-ul dacă este necesar
        int indexObstacol = Random.Range(0, obstacolePrefabs.Length);
        GameObject obstacolNou = Instantiate(obstacolePrefabs[indexObstacol]);
        obstacolNou.SetActive(false);
        obstacolePool.Add(obstacolNou);
        return obstacolNou;
    }
}
