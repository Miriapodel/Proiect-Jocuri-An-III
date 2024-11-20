using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Light sun; // Reference to the Sun object
    public float sunIntensity = 0; // Default to 0 to check for uninitialized values
    public Quaternion sunRotation = Quaternion.identity; // Default rotation
    public AudioClip currentAudioClip; // Selected audio clip for daytime
    public AudioClip defaultDayAudio; // Default audio clip for Daytime

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make sure GameManager persists across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate GameManager instances
        }
    }

    public void ApplyDaytimeSettings()
    {
        if (sun != null)
        {
            sun.intensity = sunIntensity;
            sun.transform.rotation = sunRotation;

            if (currentAudioClip != null)
            {
                // Play the assigned audio clip (this requires an AudioSource)
                AudioSource audioSource = sun.GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = sun.gameObject.AddComponent<AudioSource>();
                }
                audioSource.clip = currentAudioClip;
                audioSource.loop = true;
                audioSource.playOnAwake = false;
                audioSource.Play();
            }
        }
    }
}
