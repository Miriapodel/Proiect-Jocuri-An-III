using UnityEngine;

[RequireComponent(typeof(AudioListener))]
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource; // Reference to the AudioSource component

    private void Awake()
    {
        // Add an AudioSource component if it doesn't already exist
        if (GetComponent<AudioSource>() == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        Debug.Log("AudioManager started.");

        // Ensure no duplicate AudioListeners
        AudioListener[] aL = FindObjectsOfType<AudioListener>();
        for (int i = 0; i < aL.Length; i++)
        {
            if (!aL[i].CompareTag("MainCamera"))
            {
                Debug.Log("Destroying extra AudioListener.");
                DestroyImmediate(aL[i]);
            }
        }

        // Debug GameManager state
        if (GameManager.Instance != null)
        {
            Debug.Log("GameManager.Instance is available.");
            if (GameManager.Instance.currentAudioClip != null)
            {
                Debug.Log("Audio clip found: " + GameManager.Instance.currentAudioClip.name);

                // Play the audio
                audioSource.clip = GameManager.Instance.currentAudioClip;
                audioSource.loop = true;
                audioSource.playOnAwake = false;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("GameManager.Instance.currentAudioClip is null.");
            }
        }
        else
        {
            Debug.LogError("GameManager.Instance is null.");
        }
    }
}
