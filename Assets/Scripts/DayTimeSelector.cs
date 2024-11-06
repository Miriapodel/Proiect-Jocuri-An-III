using UnityEngine;

public class DayTimeSelector : MonoBehaviour
{
    public void DayTime()
    {
        Debug.Log("Setting Day Sun Intensity");
        if (GameManager.Instance != null && GameManager.Instance.sun != null)
        {
            GameManager.Instance.sun.intensity = 1.0f;
            GameManager.Instance.sunIntensity = 1.0f;  // Save intensity value in GameManager
        }
    }

    public void DuskTime()
    {
        Debug.Log("Setting Dusk Sun Intensity");
        if (GameManager.Instance != null && GameManager.Instance.sun != null)
        {
            GameManager.Instance.sun.intensity = 0.25f;
            GameManager.Instance.sunIntensity = 0.25f;  // Save intensity value in GameManager
        }
    }

    public void NightTime()
    {
        Debug.Log("Setting Night Sun Intensity");
        if (GameManager.Instance != null && GameManager.Instance.sun != null)
        {
            GameManager.Instance.sun.intensity = 0.0f;
            GameManager.Instance.sunIntensity = 0.0f;  // Save intensity value in GameManager
        }
    }
}
