using UnityEngine;

public class LightController : MonoBehaviour
{
    private void Start()
    {
        // Check if GameManager exists and has a sun reference
        if (GameManager.Instance != null && GameManager.Instance.sun != null)
        {
            // Apply the stored intensity from GameManager to the sun in MainScene
            GameManager.Instance.sun.intensity = GameManager.Instance.sunIntensity;
        }
        else if (GameManager.Instance != null)
        {
            // Find the Sun in MainScene by name or tag and assign it to GameManager
            GameManager.Instance.sun = GameObject.FindWithTag("Sun")?.GetComponent<Light>();

            if (GameManager.Instance.sun != null)
            {
                // Apply the stored intensity to the newly found sun
                GameManager.Instance.sun.intensity = GameManager.Instance.sunIntensity;
            }
        }
    }
}
