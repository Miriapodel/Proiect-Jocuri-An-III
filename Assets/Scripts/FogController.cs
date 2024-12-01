using UnityEngine;

public class FogController : MonoBehaviour
{
    void Start()
    {
        // Activăm ceața dacă nu e deja activată
        RenderSettings.fog = true;

        // Aplicăm culoarea salvată în GameManager
        if (GameManager.Instance != null)
        {
            RenderSettings.fogColor = GameManager.Instance.currentFogColor;
        }
    }
}