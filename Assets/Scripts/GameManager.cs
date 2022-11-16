using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public string playerName { get; private set; }
    public int score { get; private set; } = 0;
    private int highScore = 0;
    private string highScoreName;

    [SerializeField]
    private TMP_InputField playerNameInputField;

    [SerializeField]
    private TMP_Text scoreUIText;
    [SerializeField]
    private TMP_Text highScoreUIText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            ReadExistingHighScore();
            UpdateScoreText();
            UpdateHighScoreText();
            Destroy(gameObject);
        }

        if (scoreUIText != null)
        {
            UpdateScoreText();
        }

        if (highScoreUIText != null)
        {
            ReadExistingHighScore();
            if (highScore == 0)
            {
                Instance.highScoreUIText.text = "No high score yet!";
            } else
            {
                UpdateHighScoreText();
            }
        }

    }


    private void UpdateScoreText()
    {
        if (Instance.scoreUIText == null)
        {
            Instance.scoreUIText = GameObject.Find("ScoreUIText").GetComponent<TMP_Text>();
        }
        Instance.scoreUIText.text = $"{Instance.playerName} : {Instance.score}";
    }

    private void UpdateHighScoreText()
    {
        if (Instance.highScoreUIText == null)
        {
            Instance.highScoreUIText = GameObject.Find("HighScoreText").GetComponent<TMP_Text>();
        }
        Instance.highScoreUIText.text = $"High Score : {Instance.highScoreName} : {Instance.highScore}";
    }

    public void IncrementScore()
    {
        Instance.score++;
        UpdateScoreText();
        CheckHighScore();
    }

    public void PlayGame()
    {
        if (playerNameInputField != null)
        {
            Instance.playerName = playerNameInputField.text;
        } else
        {
            Instance.playerName = "AAA";
        }
        ReadExistingHighScore();
        SceneManager.LoadScene(1);
    }

    private void CheckHighScore()
    {
        if (Instance.score > Instance.highScore)
        {
            Instance.highScore = Instance.score;
            Instance.highScoreName = Instance.playerName;
            WriteNewHighScore();
            UpdateHighScoreText();
        }
    }

    private void WriteNewHighScore()
    {
        SaveScore save = new SaveScore();
        save.score = Instance.score;
        save.name = Instance.playerName;

        string json = JsonUtility.ToJson(save);
        File.WriteAllText(Application.persistentDataPath + "/highScore.json", json);
    }

    private void ReadExistingHighScore()
    {
        if (File.Exists(Application.persistentDataPath + "/highScore.json"))
        {
            SaveScore save = JsonUtility.FromJson<SaveScore>(File.ReadAllText(Application.persistentDataPath + "/highScore.json"));
            Instance.highScore = save.score;
            Instance.highScoreName = save.name;
        }
    }

    public void RefreshUI()
    {
        ReadExistingHighScore();
        UpdateHighScoreText();
    }

    class SaveScore
    {
        public int score;
        public string name;
    }
}
