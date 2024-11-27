using System.Collections;
using UnityEngine;

public class GameProgression : MonoBehaviour
{
    private float initialObstacleChance = 50f;
    private float maxObstacleChance = 90f;
    private float obstacleIncreaseRate = 0.1f;

    private float initialPowerUpChance = 50f;
    private float minPowerUpChance = 20f;
    private float powerUpDecreaseRate = 0.1f;

    private float initialRoadSpeed = 20f; 
    private float maxRoadSpeed = 50f;     
    private float roadSpeedIncreaseRate = 0.1f; 

    void Start()
    {
        ObstacleSpawner.sansaAparitie = initialObstacleChance;
        PowerUpSpawner.sansaAparitie = initialPowerUpChance;
        MoveRoadPlatform.roadSpeed = initialRoadSpeed;
        StartCoroutine(AdjustChancesOverTime());
    }

    private IEnumerator AdjustChancesOverTime()
    {
        float interval = 1f;

        while (true) {
        
            if(MoveRoadPlatform.roadSpeed < maxRoadSpeed)
            {
                //Debug.Log("VITEZA: " + MoveRoadPlatform.roadSpeed);
                MoveRoadPlatform.roadSpeed += roadSpeedIncreaseRate;
                MoveRoadPlatform.roadSpeed = Mathf.Clamp(MoveRoadPlatform.roadSpeed, initialRoadSpeed, maxRoadSpeed);
            }

            if (ObstacleSpawner.sansaAparitie < maxObstacleChance)
            {
                //Debug.Log("SANSA APARITIE: " + ObstacleSpawner.sansaAparitie);
                ObstacleSpawner.sansaAparitie += obstacleIncreaseRate;
                ObstacleSpawner.sansaAparitie = Mathf.Clamp(ObstacleSpawner.sansaAparitie, 0, maxObstacleChance);
            }

          
            if (PowerUpSpawner.sansaAparitie > minPowerUpChance)
            {
                //Debug.Log("SANSA APARITIE PU: " + PowerUpSpawner.sansaAparitie);
                PowerUpSpawner.sansaAparitie -= powerUpDecreaseRate;
                PowerUpSpawner.sansaAparitie = Mathf.Clamp(PowerUpSpawner.sansaAparitie, minPowerUpChance, 100);
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
