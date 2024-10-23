using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Parametrii pentru shake
    public float shakeDuration = 0.5f; // Durata shake-ului
    public float shakeMagnitude = 0.1f; // Magnitudinea shake-ului

    public IEnumerator Shake()
    {
        // Salveaza pozitia initiala a camerei
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0.0f;

        // Efectul de shake
        while (elapsed < shakeDuration)
        {
            // Generam o mișcare mai fina
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            // Actualizam poziția camerei
            transform.localPosition = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            // Incrementam timpul scurs
            elapsed += Time.deltaTime;

            // Asteptam urmatorul frame
            yield return null;
        }

        // Revenim la pozitia originala
        transform.localPosition = originalPosition;
    }

    // Functie pentru a incepe shake-ul
    public void TriggerShake()
    {
        StartCoroutine(Shake());
    }
}
