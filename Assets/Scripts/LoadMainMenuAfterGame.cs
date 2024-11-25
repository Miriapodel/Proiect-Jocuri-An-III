using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenuAfterGame : MonoBehaviour
{
    public void LoadMainMenu()
    {
        // Apply daytime settings in GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ApplyDaytimeSettings();
        }

        // Load the StartMenu scene
        SceneManager.LoadScene("StartMenu");
    }
}
