using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLampController : MonoBehaviour
{
    private List<GameObject> streetLamps;
    private readonly float intensityThreshold = 0.5f;

    private void Start()
    {
        // Find all objects tagged as "StreetLamp" and store them in a list
        streetLamps = new List<GameObject>(GameObject.FindGameObjectsWithTag("StreetLamp"));

        // Initial update to ensure lamps are set correctly at start
        UpdateLampStatus();
    }

    private void Update()
    {
        // Check if the sun intensity changes and update lamps accordingly
        UpdateLampStatus();
    }

    private void UpdateLampStatus()
    {
        // Check if GameManager and sun reference are available
        if (GameManager.Instance != null && GameManager.Instance.sun != null)
        {
            // Get the current intensity of the sun
            float sunIntensity = GameManager.Instance.sun.intensity;

            // Enable or disable the street lamps based on sun intensity
            foreach (var lamp in streetLamps)
            {
                lamp.SetActive(sunIntensity < intensityThreshold);
            }
        }
    }
}
