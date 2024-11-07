using UnityEngine;

public class DayTimeSelector : MonoBehaviour
{
    public void DayTime()
    {
        Debug.Log("Setting Day Sun Intensity and Rotation");
        if (GameManager.Instance != null && GameManager.Instance.sun != null)
        {
            GameManager.Instance.sun.intensity = 1.0f;
            GameManager.Instance.sunIntensity = 1.0f;  // Save intensity value in GameManager

            // Set rotation for daytime
            Quaternion dayRotation = Quaternion.Euler(67.8f, -6.1f, 17.5f);
            GameManager.Instance.sun.transform.rotation = dayRotation;
            GameManager.Instance.sunRotation = dayRotation;  // Save rotation in GameManager
        }
    }

    public void DuskTime()
    {
        Debug.Log("Setting Dusk Sun Intensity and Rotation");
        if (GameManager.Instance != null && GameManager.Instance.sun != null)
        {
            GameManager.Instance.sun.intensity = 0.35f;
            GameManager.Instance.sunIntensity = 0.35f;  // Save intensity value in GameManager

            // Set rotation for dusk
            Quaternion duskRotation = Quaternion.Euler(5.869f, -177.649f, -156.419f);
            GameManager.Instance.sun.transform.rotation = duskRotation;
            GameManager.Instance.sunRotation = duskRotation;  // Save rotation in GameManager
        }
    }

    public void NightTime()
    {
        Debug.Log("Setting Night Sun Intensity and Rotation");
        if (GameManager.Instance != null && GameManager.Instance.sun != null)
        {
            GameManager.Instance.sun.intensity = 0.0001f;
            GameManager.Instance.sunIntensity = 0.0001f;  // Save intensity value in GameManager

            // Set rotation for nighttime
            Quaternion nightRotation = Quaternion.Euler(-10f, 180f, 0f);
            GameManager.Instance.sun.transform.rotation = nightRotation;
            GameManager.Instance.sunRotation = nightRotation;  // Save rotation in GameManager
        }
    }
}
