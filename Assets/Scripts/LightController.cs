using UnityEngine;

public class LightController : MonoBehaviour
{
    private void Start()
    {
        // Check if GameManager exists and has a sun reference
        if (GameManager.Instance != null && GameManager.Instance.sun != null)
        {
            // Apply the stored intensity and rotation from GameManager
            GameManager.Instance.sun.intensity = GameManager.Instance.sunIntensity;
            GameManager.Instance.sun.transform.rotation = GameManager.Instance.sunRotation;
        }
        else if (GameManager.Instance != null)
        {
            // Find the Sun in MainScene by name or tag and assign it to GameManager
            GameManager.Instance.sun = GameObject.FindWithTag("Sun")?.GetComponent<Light>();

            // Check if GameManager now has a sun reference
            if (GameManager.Instance.sun != null)
            {
                // Apply the stored intensity and rotation
                GameManager.Instance.sun.intensity = GameManager.Instance.sunIntensity;
                GameManager.Instance.sun.transform.rotation = GameManager.Instance.sunRotation;
            }
        }
    }
}
