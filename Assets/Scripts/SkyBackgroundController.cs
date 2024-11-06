using UnityEngine;

public class SkyBackgroundController : MonoBehaviour
{
    public Material daySkybox;  // Skybox material for daytime
    public Material duskSkybox; // Skybox material for dusk
    public Material nightSkybox; // Skybox material for nighttime

    private Light sun; // Reference to the Sun light component
    private float dayThreshold = 0.7f; // Intensity threshold for daytime
    private float duskThreshold = 0.15f; // Intensity threshold for dusk

    private void Start()
    {
        // Find the Sun object by tag
        GameObject sunObject = GameObject.FindGameObjectWithTag("Sun");
        if (sunObject != null)
        {
            sun = sunObject.GetComponent<Light>();
        }
        else
        {
            Debug.LogError("Sun object with tag 'Sun' not found in the scene.");
        }
    }

    private void Update()
    {
        if (sun != null)
        {
            UpdateSkyboxBasedOnIntensity(sun.intensity);
        }
    }

    private void UpdateSkyboxBasedOnIntensity(float intensity)
    {
        // Change the skybox material based on the sun's intensity
        if (intensity > dayThreshold)
        {
            // Set daytime skybox
            RenderSettings.skybox = daySkybox;
        }
        else if (intensity > duskThreshold)
        {
            // Set dusk skybox
            RenderSettings.skybox = duskSkybox;
        }
        else
        {
            // Set nighttime skybox
            RenderSettings.skybox = nightSkybox;
        }

        // Optional: Update lighting when the skybox changes
        DynamicGI.UpdateEnvironment();
    }
}
