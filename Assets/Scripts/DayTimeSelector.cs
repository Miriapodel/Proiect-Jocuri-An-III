using UnityEngine;

public class DayTimeSelector : MonoBehaviour
{
    private Quaternion rotatieZI = Quaternion.Euler(67.8f, -6.1f, 17.5f);
    private Quaternion rotatieAPUS = Quaternion.Euler(5.869f, -177.649f, -156.419f);
    private Quaternion rotatieNOAPTE = Quaternion.Euler(-10f, 180f, 0f);

    public void DayTime()
    {
        Debug.Log("Setting Day Sun Intensity and Rotation");
        if (GameManager.Instance != null && GameManager.Instance.sun != null)
        {
            GameManager.Instance.sun.intensity = 1.0f;
            GameManager.Instance.sunIntensity = 1.0f;  // Salveaza valoarea intensitatii in GameManager

            // Seteaza rotatia pentru day
            Quaternion dayRotation = rotatieZI;
            GameManager.Instance.sun.transform.rotation = dayRotation;
            GameManager.Instance.sunRotation = dayRotation;  // Salveaza rotatia in GameManager
        }
    }

    public void DuskTime()
    {
        Debug.Log("Setting Dusk Sun Intensity and Rotation");
        if (GameManager.Instance != null && GameManager.Instance.sun != null)
        {
            GameManager.Instance.sun.intensity = 0.35f;
            GameManager.Instance.sunIntensity = 0.35f;  // Salveaza valoarea intensitatii in GameManager

            // Seteaza rotatia pentru dusk
            Quaternion duskRotation = rotatieAPUS;
            GameManager.Instance.sun.transform.rotation = duskRotation;
            GameManager.Instance.sunRotation = duskRotation;  // Salveaza rotatia in GameManager
        }
    }

    public void NightTime()
    {
        Debug.Log("Setting Night Sun Intensity and Rotation");
        if (GameManager.Instance != null && GameManager.Instance.sun != null)
        {
            GameManager.Instance.sun.intensity = 0.0001f;
            GameManager.Instance.sunIntensity = 0.0001f;  // Salveaza valoarea intensitatii in GameManager

            // Seteaza rotatia pentru night
            Quaternion nightRotation = rotatieNOAPTE;
            GameManager.Instance.sun.transform.rotation = nightRotation;
            GameManager.Instance.sunRotation = nightRotation;  // Salveaza rotatia in GameManager
        }
    }
}
