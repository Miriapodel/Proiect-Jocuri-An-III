using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLampController : MonoBehaviour
{
    private List<GameObject> streetLamps;
    private readonly float intensityThreshold = 0.5f;

    private void Start()
    {
        // Gaseste toate obiectele cu tag-ul "StreetLamp"
        streetLamps = new List<GameObject>(GameObject.FindGameObjectsWithTag("StreetLamp"));

        // Update initial ca sa fie sigur ca sunt pornite sau oprite
        UpdateLampStatus();
    }

    private void Update()
    {
        // Verifica daca intensitatea soarelui se schimba si modifica statusul lampilor
        UpdateLampStatus();
    }

    private void UpdateLampStatus()
    {
        // Verifica daca GameManager-ul exista si daca soarele exista
        if (GameManager.Instance != null && GameManager.Instance.sun != null)
        {
            // Luam intensitatea curenta a soarelui
            float sunIntensity = GameManager.Instance.sun.intensity;

            // Porneste sau opreste lampile in functie de intensitatea soarelui
            foreach (var lamp in streetLamps)
            {
                lamp.SetActive(sunIntensity < intensityThreshold);
            }
        }
    }
}
