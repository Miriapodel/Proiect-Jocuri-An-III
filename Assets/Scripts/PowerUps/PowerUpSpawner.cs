using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] puncteSpawn; // Punctele de spawn definite prin empty GameObjects
    public static float sansaAparitie = 50f; // Șansa ca un power-up să apară la fiecare punct

    void Start()
    {
        GenerarePowerUps();
    }

    public void GenerarePowerUps()
    {
        foreach (Transform punct in puncteSpawn)
        {
            float sansa = Random.Range(0f, 100f);
            if (sansa < sansaAparitie)
            {
                GameObject powerUp = PowerUpPool.Instance.GetPooledPowerUp();
                if (powerUp != null)
                {
                    powerUp.transform.position = punct.position;
                    powerUp.transform.rotation = punct.rotation;
                    powerUp.transform.parent = transform;
                    powerUp.SetActive(true);
                }
            }
        }
    }

    public void ResetPowerUps()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("PowerUp"))
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
