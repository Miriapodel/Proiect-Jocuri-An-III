using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenuAfterGame : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
