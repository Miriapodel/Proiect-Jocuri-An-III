using UnityEngine;

public class SkyBackgroundController : MonoBehaviour
{
    public Material daySkybox;  // Material Skybox pentru day 
    public Material duskSkybox; // Material Skybox pentru dusk
    public Material nightSkybox; // Material Skybox pentru night

    private Light sun; // Referinta la lumina soarelui
    private readonly float dayThreshold = 0.7f; // Granita de intensitate pentru day
    private readonly float duskThreshold = 0.15f; // Granita de intensitate pentru dusk

    private void Start()
    {
        // Gaseste lumina soarelui in scena
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
        // Schimba skybox-ul in functie de intensitatea soarelui
        if (intensity > dayThreshold)
        {
            // Seteaza skybox-ul pentru day
            RenderSettings.skybox = daySkybox;
        }
        else if (intensity > duskThreshold)
        {
            // Seteaza skybox-ul pentru dusk
            RenderSettings.skybox = duskSkybox;
        }
        else
        {
            // Seteaza skybox-ul pentru night
            RenderSettings.skybox = nightSkybox;
        }

        // Actualizeaza lumina ambientala pentru a se potrivi cu skybox-ul
        DynamicGI.UpdateEnvironment();
    }
}
