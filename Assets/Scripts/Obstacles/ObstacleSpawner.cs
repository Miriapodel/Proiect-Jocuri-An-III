using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] puncteSpawn;
    private float sansaAparitie = 50f;

    void Start()
    {
        GenerareObstacole();
    }

    public void GenerareObstacole()
    {
        foreach (Transform punct in puncteSpawn)
        {
            float sansa = Random.Range(0f, 100f);
            if (sansa < sansaAparitie)
            {
                GameObject obstacol = ObstaclePooler.Instance.GetPooledObstacle();
                if (obstacol != null)
                {
                    obstacol.transform.position = punct.position;
                    obstacol.transform.rotation = punct.rotation;
                    obstacol.transform.parent = transform;
                    obstacol.SetActive(true);
                }
            }
        }
    }

    public void ResetObstacole()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Obstacle"))
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
