using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] puncteSpawn;
    public static float sansaAparitie = 50f;
    private float obstacleOffset = 1.8f;

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
                    obstacol.transform.parent = transform;
                    obstacol.transform.position = punct.position;
                    obstacol.SetActive(true);

                    if (obstacol.name.Contains("Metal bar"))
                        obstacol.transform.position += new Vector3(obstacleOffset, 0, 0);
                 
                    if(!(obstacol.name.Contains("Metal bar") || obstacol.name.Contains("Warning Sign")))
                        obstacol.transform.rotation = punct.rotation;
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
