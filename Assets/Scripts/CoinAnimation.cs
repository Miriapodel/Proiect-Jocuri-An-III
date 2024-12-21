using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    public float rotationSpeed = 150f; // Viteza de rotație în grade/secundă

    void Update()
    {
        // Rotește moneda în jurul axei sale verticale
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
