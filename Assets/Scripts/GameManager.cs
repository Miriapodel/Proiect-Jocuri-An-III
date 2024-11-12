using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Instanta Singleton
    public Light sun; // Referinta la Soare (Lumina Directionala)
    public float sunIntensity = 1.0f;  // Salveaza intensitatea Soarelui
    public Quaternion sunRotation = Quaternion.identity; // Salveaza rotatia Soarelui

    private void Awake()
    {
        // Asigura ca exista doar o instanta a GameManager-ului
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persista GameManager-ul intre scene
    }
}
