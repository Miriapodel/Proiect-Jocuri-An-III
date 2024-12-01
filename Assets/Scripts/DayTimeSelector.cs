using UnityEngine;

public class DayTimeSelector : MonoBehaviour
{
    private Quaternion rotatieZI = Quaternion.Euler(67.8f, -6.1f, 17.5f);
    private Quaternion rotatieAPUS = Quaternion.Euler(5.869f, -177.649f, -156.419f);
    private Quaternion rotatieNOAPTE = Quaternion.Euler(-10f, 180f, 0f);

    public AudioClip dayAudio; // Referință la clipul audio pentru zi
    public AudioClip duskAudio; // Referință la clipul audio pentru apus
    public AudioClip nightAudio; // Referință la clipul audio pentru noapte

    public void DayTime()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.sunIntensity = 1.0f;
            GameManager.Instance.sunRotation = rotatieZI;
            GameManager.Instance.currentAudioClip = dayAudio;
            GameManager.Instance.currentFogColor = GameManager.Instance.fogColorDay;
            Debug.Log("Setare intensitate de zi, rotatie de zi si clip audio pentru zi");
            Debug.Log("Setare culoare ceata pentru zi");
        }
    }

    public void DuskTime()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.sunIntensity = 0.35f;
            GameManager.Instance.sunRotation = rotatieAPUS;
            GameManager.Instance.currentAudioClip = duskAudio;
            GameManager.Instance.currentFogColor = GameManager.Instance.fogColorDusk;
            Debug.Log("Setare intensitate de apus, rotatie de apus si clip audio pentru apus");
            Debug.Log("Setare culoare ceata pentru apus");
        }
    }

    public void NightTime()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.sunIntensity = 0.0001f;
            GameManager.Instance.sunRotation = rotatieNOAPTE;
            GameManager.Instance.currentAudioClip = nightAudio;
            GameManager.Instance.currentFogColor = GameManager.Instance.fogColorNight;
            Debug.Log("Setare intensitate de noapte, rotatie de noapte si clip audio pentru noapte");
            Debug.Log("Setare culoare ceata pentru noapte");
        }
    }
}