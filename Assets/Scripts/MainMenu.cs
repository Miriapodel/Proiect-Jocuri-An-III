using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.sunIntensity == 0 && GameManager.Instance.sunRotation == Quaternion.identity)
            {
                GameManager.Instance.sunIntensity = 1.0f;
                GameManager.Instance.sunRotation = Quaternion.Euler(67.8f, -6.1f, 17.5f);

                if (GameManager.Instance.defaultDayAudio != null)
                {
                    GameManager.Instance.currentAudioClip = GameManager.Instance.defaultDayAudio;
                    Debug.Log("Default daytime audio assigned: " + GameManager.Instance.defaultDayAudio.name);
                }
                else
                {
                    Debug.LogWarning("DefaultDayAudio is not assigned in GameManager.");
                }
            }
        }

        // Load the next scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
