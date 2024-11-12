using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters; // Vector pentru a stoca caracterele GameObject în StartMenu
    private int selectedCharacter = 0; // Indexul caracterului selectat in acest moment

    public void NextCharacter()
    {
        // Dezactivarea caracterul curent
        characters[selectedCharacter].SetActive(false);

        // Incrementarea indexului selectedCharacter si loop daca este necesar
        selectedCharacter = (selectedCharacter + 1) % characters.Length;

        // Activarea noului caracter selectat
        characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        // Dezactivarea caracterul curent
        characters[selectedCharacter].SetActive(false);

        // Decrementarea indexului selectedCharacter si loop daca este necesar
        selectedCharacter = (selectedCharacter - 1 + characters.Length) % characters.Length;

        // Activarea noului caracter selectat
        characters[selectedCharacter].SetActive(true);
    }

    public void SelectCharacter()
    {
        // Salvam indexul caracterului selectat in PlayerPrefs
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        Debug.Log("Selected character with the id " + selectedCharacter);
    }
}
