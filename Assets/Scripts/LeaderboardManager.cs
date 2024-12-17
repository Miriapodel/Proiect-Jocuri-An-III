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
	private string indexLastScore;

	private const int MaxScores = 100;

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
		for (int i = 0; i < MaxScores; i++)
		{
			if (PlayerPrefs.HasKey("LeaderboardScore" + i))
			{
				scores.Add(PlayerPrefs.GetFloat("LeaderboardScore" + i, 0));
			}
			else
			{
				break;
			}
		}
	}

	void SaveLeaderboard()
	{
		for (int i = 0; i < scores.Count && i < MaxScores; i++)
		{
			PlayerPrefs.SetFloat("LeaderboardScore" + i, scores[i]);
		}
		PlayerPrefs.Save();
	}

	void UpdateLeaderboard(float lastScore)
	{
		scores.Add(lastScore);
		scores.Sort((a, b) => b.CompareTo(a)); // Sortare descrescătoare

		// Preluam pozitia ultimului scor in clasament
		indexLastScore = (scores.IndexOf(lastScore) + 1).ToString();

		if (scores.Count > MaxScores)
		{
			scores.RemoveAt(scores.Count - 1); // Păstrează doar top 100
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
		for (int i = 0; i < scores.Count && i < 5; i++)
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
		if (!scores.GetRange(0, Mathf.Min(5, scores.Count)).Contains(lastScore))
		{
			GameObject row = Instantiate(leaderboardRowPrefab, leaderboardTable);
			TextMeshProUGUI positionText = row.transform.Find("PositionText").GetComponent<TextMeshProUGUI>();
			TextMeshProUGUI scoreText = row.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();

			positionText.text = indexLastScore;
			scoreText.text = $"{lastScore:F0} ms";

			positionText.color = Color.red;
			positionText.fontStyle = FontStyles.Bold;
			scoreText.color = Color.red;
			scoreText.fontStyle = FontStyles.Bold;
		}
	}
}