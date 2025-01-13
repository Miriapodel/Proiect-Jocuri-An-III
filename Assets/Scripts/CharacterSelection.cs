using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters; // Vector pentru a stoca caracterele GameObject în StartMenu
    private int selectedCharacter = 0; // Indexul caracterului selectat in acest moment

    [Header("Shop Settings")]
    public int[] characterPrices; // Prețurile pentru fiecare caracter
    public Button[] buyButtons; // Butoanele de cumpărare pentru fiecare caracter
    public TextMeshProUGUI coinText; // Text pentru afișarea banilor
    public Color unavailableColor = Color.gray; // Culoarea pentru când nu sunt destui bani
    public Color availableColor = Color.white; // Culoarea normală a butonului

    private int playerCoins; // Banii jucătorului

	private void Start()
	{
		InitializePlayerPrefs();

		playerCoins = PlayerPrefs.GetInt("TotalCoins", 0);
		UpdateCoinsUI();

		UpdateButtonStates();

		selectedCharacter = PlayerPrefs.GetInt("selectedCharacter", 0);
		for (int i = 0; i < characters.Length; i++)
		{
			characters[i].SetActive(i == selectedCharacter);
			buyButtons[i].gameObject.SetActive(i == selectedCharacter);
		}
	}

	void InitializePlayerPrefs()
	{
		string currentVersion = Application.version;
		string savedVersion = PlayerPrefs.GetString("AppVersion", "");

		if (savedVersion != currentVersion)
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.SetString("AppVersion", currentVersion);
			PlayerPrefs.SetInt("Character_0", 1);
			PlayerPrefs.SetInt("selectedCharacter", 0);
			PlayerPrefs.Save();
		}
	}

	private void UpdateCoinsUI()
    {
        coinText.text = "Coins: " + playerCoins.ToString();
    }

    private void UpdateButtonStates()
    {
        // Actualizăm playerCoins cu valoarea reală din joc
        playerCoins = PlayerPrefs.GetInt("TotalCoins", 0);

        // Actualizăm textul pentru coins
        if (coinText != null)
        {
            coinText.text = playerCoins.ToString() + " Coins";
        }

        for (int i = 0; i < characters.Length; i++)
        {
            bool isUnlocked = PlayerPrefs.GetInt("Character_" + i, 0) == 1;
            if (buyButtons[i] != null)
            {
                TextMeshProUGUI buttonText = buyButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                Image buttonImage = buyButtons[i].GetComponent<Image>();

                Debug.Log($"Character {i} - Price: {characterPrices[i]}"); // Pentru debugging

                if (isUnlocked)
                {
                    buttonText.text = "Select";
                    buttonText.color = Color.white; // Text alb pentru caractere deblocate
                    buttonImage.color = availableColor;
                    buyButtons[i].interactable = true;
                }
                else
                {
                    // Folosim prețul specific pentru acest caracter
                    int price = characterPrices[i];
                    buttonText.text = price.ToString() + " Coins";
                    bool canAfford = playerCoins >= price;
                    buttonImage.color = canAfford ? availableColor : unavailableColor;
                    buttonText.color = canAfford ? Color.white : new Color(0.5f, 0.5f, 0.5f); // Text gri când nu sunt destui bani
                    buyButtons[i].interactable = canAfford;
                }
            }
        }
    }
    public void NextCharacter()
    {
        // Dezactivarea caracterul curent
        characters[selectedCharacter].SetActive(false);
        buyButtons[selectedCharacter].gameObject.SetActive(false);

        // Incrementarea indexului selectedCharacter si loop daca este necesar
        selectedCharacter = (selectedCharacter + 1) % characters.Length;

        // Activarea noului caracter selectat
        characters[selectedCharacter].SetActive(true);
        buyButtons[selectedCharacter].gameObject.SetActive(true);
    }

    public void TryPurchaseOrSelect()
    {
        // Verificăm dacă caracterul e deja deblokat
        bool isUnlocked = PlayerPrefs.GetInt("Character_" + selectedCharacter, 0) == 1;

        if (isUnlocked)
        {
            SelectCharacter();
        }
        else if (playerCoins >= characterPrices[selectedCharacter])
        {
            PurchaseCharacter();
        }
    }

    private void PurchaseCharacter()
    {
        // Scădem prețul din banii jucătorului
        playerCoins -= characterPrices[selectedCharacter];
        PlayerPrefs.SetInt("TotalCoins", playerCoins);

        // Marcăm caracterul ca deblocat
        PlayerPrefs.SetInt("Character_" + selectedCharacter, 1);
        PlayerPrefs.Save();

        // Actualizăm UI-ul
        UpdateCoinsUI();
        UpdateButtonStates();

        // Feedback pentru jucător
        Debug.Log("Purchased character with id " + selectedCharacter);

        // Actualizăm textul butonului imediat
        if (buyButtons[selectedCharacter] != null)
        {
            TextMeshProUGUI buttonText = buyButtons[selectedCharacter].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = "Select";
            buyButtons[selectedCharacter].interactable = true;
            buyButtons[selectedCharacter].GetComponent<Image>().color = availableColor;
        }
        SelectCharacter();
	}

    public void PreviousCharacter()
    {
        // Dezactivarea caracterul curent
        characters[selectedCharacter].SetActive(false);
        buyButtons[selectedCharacter].gameObject.SetActive(false);

        // Decrementarea indexului selectedCharacter si loop daca este necesar
        selectedCharacter = (selectedCharacter - 1 + characters.Length) % characters.Length;

        // Activarea noului caracter selectat
        characters[selectedCharacter].SetActive(true);
        buyButtons[selectedCharacter].gameObject.SetActive(true);
    }

    public void SelectCharacter()
    {
        // Salvam indexul caracterului selectat in PlayerPrefs
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
        Debug.Log("Selected character with the id " + selectedCharacter);
    }
}
