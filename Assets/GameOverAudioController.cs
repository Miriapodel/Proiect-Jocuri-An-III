using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameOverAudioController : MonoBehaviour
{
    public AudioClip gameOverClip; // Reference to the Game Over audio clip
    private AudioSource audioSource; // AudioSource component reference

    private void Awake()
    {
        // Ensure an AudioSource component exists on the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start()
    {
        // Check if the GameOverClip is assigned
        if (gameOverClip != null)
        {
            audioSource.clip = gameOverClip; // Assign the audio clip
            audioSource.loop = false; // Play once (adjust if you want it to loop)
            audioSource.playOnAwake = false; // Prevent auto-play before GameOver logic
            audioSource.Play(); // Play the audio clip
        }
        else
        {
            Debug.LogWarning("No Game Over audio clip assigned to GameOverAudioController.");
        }
    }
}
