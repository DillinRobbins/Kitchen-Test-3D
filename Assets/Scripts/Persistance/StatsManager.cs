using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StatsManager : MonoBehaviour
{
    private HighScoresData highScoresData;
    private string highScoresDataKey = "GetHSD";

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI endScoreText;
    [SerializeField] TextMeshProUGUI lastScoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    [SerializeField] private GameObject highScorePrefab;
    [SerializeField] private GameObject highScoreContainer;

    private int score;

    public int LastScore { get { return highScoresData.LastScore; } set { highScoresData.LastScore = score; } }

    public int HighScore { get { return highScoresData.HighScores.GetHighScore(); } }

    public static StatsManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);

        LoadData();
        scoreText.text = 0.ToString();
        lastScoreText.text = LastScore.ToString();
        highScoreText.text = HighScore.ToString();
    }

    private void OnEnable()
    {
        GameManager.OnEndGame += ConsolidateScoresOnEndGame;
    }

    private void OnDisable()
    {
        GameManager.OnEndGame -= ConsolidateScoresOnEndGame;
    }

    public void IncrementScore(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString();
    }

    private void UpdateHighScores(int score)
    {
        highScoresData.HighScores.Insert(score);
        LastScore = score;
        SaveData(highScoresData);
    }

    public int GetHighScore()
    {
        return HighScore;
    }

    public int GetLastScore()
    {
        return LastScore;
    }

    void ConsolidateScoresOnEndGame()
    {
        UpdateHighScores(score);
        endScoreText.text = score.ToString();
        SaveData(highScoresData);

        var highScores = highScoresData.HighScores.GetScores();
        var index = 1;

        foreach(var score in highScores)
        {
            var textObj = Instantiate(highScorePrefab, highScoreContainer.transform);
            textObj.GetComponent<TextMeshProUGUI>().text = $"{index}: {score}";
            index++;
        }
    }

    private void SaveData(HighScoresData highScoresData)
    {
        string serializedNewData = JsonUtility.ToJson(highScoresData);
        PlayerPrefs.SetString(highScoresDataKey, serializedNewData);
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(highScoresDataKey))
        {
            var serializedData = PlayerPrefs.GetString(highScoresDataKey);
            highScoresData = JsonUtility.FromJson<HighScoresData>(serializedData);
        }
        else
        {
            highScoresData = new HighScoresData();
        }
    }
}
