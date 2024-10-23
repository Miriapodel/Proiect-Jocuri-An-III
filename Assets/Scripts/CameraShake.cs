using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Parametrii pentru shake
    public float shakeDuration = 0.5f; // Durata shake-ului
    public float shakeMagnitude = 0.1f; // Magnitudinea shake-ului

    public IEnumerator Shake()
    {
        // Salvează poziția inițială a camerei
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        // Efectul de shake
        while (elapsed < shakeDuration)
        {
            // Generăm o mișcare mai fină
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // Actualizăm poziția camerei
            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            // Incrementăm timpul scurs
            elapsed += Time.deltaTime;

            // Așteptăm următorul frame
            yield return null;
        }

        // Revenim la poziția originală
        transform.localPosition = originalPosition;
    }

    // Funcție pentru a începe shake-ul
    public void TriggerShake()
    {
        StartCoroutine(Shake());
    }
}
