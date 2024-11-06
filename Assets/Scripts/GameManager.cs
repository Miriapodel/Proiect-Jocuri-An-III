using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Singleton instance
    public Light sun; // Reference to the Sun (Directional Light)
    public float sunIntensity = 1.0f;  // Stored intensity of the Sun
    public Quaternion sunRotation = Quaternion.identity; // Stored rotation of the Sun

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persist GameManager across scenes
    }
}
