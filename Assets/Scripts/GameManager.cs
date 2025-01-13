using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Light sun; // Reference to the Sun object
    public float sunIntensity = 0; // Default to 0 to check for uninitialized values
    public Quaternion sunRotation = Quaternion.identity; // Default rotation
    public AudioClip currentAudioClip; // Selected audio clip for daytime
    public AudioClip defaultDayAudio; // Default audio clip for Daytime

    // pentru ceata
    public Color fogColorDay = new Color(0.76f, 0.76f, 0.76f); // Alb-gri pentru zi
    public Color fogColorDusk = new Color(0.85f, 0.6f, 0.5f);  // Roșiatic pentru apus
    public Color fogColorNight = new Color(0.1f, 0.1f, 0.15f); // Albastru închis pentru noapte

    public Color currentFogColor; // Culoarea curentă a ceții

	void Start()
	{
		InitializePlayerPrefs();
		// La primul run resetam PlayerPrefs
	}

	void InitializePlayerPrefs()
	{
		if (!PlayerPrefs.HasKey("FirstLaunch"))
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.SetInt("FirstLaunch", 1);
			PlayerPrefs.SetInt("TotalCoins", 10);
			PlayerPrefs.Save();
		}
	}

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
