using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters; // Array to hold character GameObjects in the StartMenu
    private int selectedCharacter = 0; // Index of the currently selected character

    public void NextCharacter()
    {
        // Deactivate current character
        characters[selectedCharacter].SetActive(false);

        // Increment selectedCharacter index and loop if necessary
        selectedCharacter = (selectedCharacter + 1) % characters.Length;

        // Activate the new selected character
        characters[selectedCharacter].SetActive(true);
    }

    public void PreviousCharacter()
    {
        // Deactivate current character
        characters[selectedCharacter].SetActive(false);

        // Decrement selectedCharacter index and loop if necessary
        selectedCharacter = (selectedCharacter - 1 + characters.Length) % characters.Length;

        // Activate the new selected character
        characters[selectedCharacter].SetActive(true);
    }

    public void SelectCharacter()
    {
        // Save the selected character index
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        Debug.Log("Selected character with the id " + selectedCharacter);
    }
}
