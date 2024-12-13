using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField]
    private Transform leaderboardTable; // Referință la tabel
    [SerializeField]
    private GameObject leaderboardRowPrefab; // Prefab-ul pentru un rând
    private List<float> scores = new List<float>();

    void Start()
    {
        float lastScore = PlayerPrefs.GetFloat("LastRoundScore", 0);
        LoadLeaderboard();
        UpdateLeaderboard(lastScore);
        DisplayLeaderboard(lastScore);
    }

    void LoadLeaderboard()
    {
        scores.Clear();
        for (int i = 0; i < 5; i++)
        {
            scores.Add(PlayerPrefs.GetFloat("LeaderboardScore" + i, 0));
        }
    }

    void SaveLeaderboard()
    {
        for (int i = 0; i < scores.Count && i < 5; i++)
        {
            PlayerPrefs.SetFloat("LeaderboardScore" + i, scores[i]);
        }
        PlayerPrefs.Save();
    }

    void UpdateLeaderboard(float lastScore)
    {
        scores.Add(lastScore);
        scores.Sort((a, b) => b.CompareTo(a)); // Sortare descrescătoare
        if (scores.Count > 5)
        {
            scores.RemoveAt(scores.Count - 1); // Păstrează doar top 5
        }
        SaveLeaderboard();
    }

    void DisplayLeaderboard(float lastScore)
    {
        // Șterge rândurile existente din tabel
        foreach (Transform child in leaderboardTable)
        {
            Destroy(child.gameObject);
        }

        // Creează rânduri noi
        for (int i = 0; i < scores.Count; i++)
        {
            GameObject row = Instantiate(leaderboardRowPrefab, leaderboardTable);
            TextMeshProUGUI positionText = row.transform.Find("PositionText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = row.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();

            positionText.text = (i + 1).ToString();
            scoreText.text = $"{scores[i]:F0} ms";

            // Evidențiază scorul curent
            if (Mathf.Approximately(scores[i], lastScore))
            {
                positionText.color = Color.green;
                positionText.fontStyle = FontStyles.Bold;
                scoreText.color = Color.green;
                scoreText.fontStyle = FontStyles.Bold;
            }
        }

        // Dacă scorul nu e în top 5, îl afișează separat
        if (!scores.Contains(lastScore))
        {
            GameObject row = Instantiate(leaderboardRowPrefab, leaderboardTable);
            TextMeshProUGUI positionText = row.transform.Find("PositionText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = row.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();

            positionText.text = "6";
            scoreText.text = $"{lastScore:F0} ms";

            positionText.color = Color.red;
            positionText.fontStyle = FontStyles.Bold;
            scoreText.color = Color.red;
            scoreText.fontStyle = FontStyles.Bold;
        }
    }
}
