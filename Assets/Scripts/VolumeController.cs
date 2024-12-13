using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VolumeController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Slider volumeSlider;
    private AudioSource audioSource;
    private bool isDragging = false;

    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();

        // Disable keyboard navigation on the slider
        if (volumeSlider != null)
        {
            var noNavigation = new Navigation { mode = Navigation.Mode.None };
            volumeSlider.navigation = noNavigation;
        }

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void ChangeVolume()
    {
        if (audioSource != null && isDragging)
        {
            audioSource.volume = volumeSlider.value;
            Save();
        }
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        if (audioSource != null)
        {
            audioSource.volume = volumeSlider.value;
        }
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
