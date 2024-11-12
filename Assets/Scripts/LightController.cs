using UnityEngine;

public class LightController : MonoBehaviour
{
    private void Start()
    {
        // Verifica daca GameManager exista si daca GameManager are o referinta la soare
        if (GameManager.Instance != null && GameManager.Instance.sun != null)
        {
            // Aplica intensitatea si rotatia stocate
            GameManager.Instance.sun.intensity = GameManager.Instance.sunIntensity;
            GameManager.Instance.sun.transform.rotation = GameManager.Instance.sunRotation;
        }
        else if (GameManager.Instance != null)
        {
            // Gaseste soarele si seteaza referinta GameManager-ului
            GameManager.Instance.sun = GameObject.FindWithTag("Sun")?.GetComponent<Light>();

            // Verifica daca GameManager are o referinta la soare
            if (GameManager.Instance.sun != null && GameManager.Instance.sunIntensity != 0 && GameManager.Instance.sunRotation != Quaternion.Euler(0, 0, 0))
            {
                // Aplica intensitatea si rotatia stocate
                GameManager.Instance.sun.intensity = GameManager.Instance.sunIntensity;
                GameManager.Instance.sun.transform.rotation = GameManager.Instance.sunRotation;
            }
        }
    }
}
