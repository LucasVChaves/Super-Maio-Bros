using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance {get; private set;}
    public TMP_Text uiScoreText;
    private int currScore = 0;
    private int highScore = 0;
    private string highscorePath;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        highscorePath = Application.dataPath + "/Data/highscore.txt";
        LoadHighScore();
        StartCoroutine(IncreaseScoreOverTime());
    }

    IEnumerator IncreaseScoreOverTime() {
        while (true) {
            yield return new WaitForSeconds(5);
            AddScore(10);
        }
    }

    void Update() {
        uiScoreText.text = "POINTS = " + Convert.ToString(currScore);
    }

    public void AddScore(int val) {
        currScore += val;
        if (currScore > highScore) {
            highScore = currScore;
        }
    }

    public void SubScore(int val) {
        currScore -= val;
    }

    public void OnPlayerDeath() {
        if (currScore > highScore) {
            highScore = currScore;
            SaveHighScore();
        }
    }

    public int GetScore() {
        return currScore;
    }

    public int GetHighScore() {
        return highScore;
    }

    public void CheckHighScore() {
        if (currScore > highScore) {
            highScore = currScore;
            SaveHighScore();
        }
    }

    private void SaveHighScore() {
        if (!Directory.Exists(Path.GetDirectoryName(highscorePath))) {
            Directory.CreateDirectory(Path.GetDirectoryName(highscorePath));
        }
        File.WriteAllText(highscorePath, highScore.ToString());
    }

    private void LoadHighScore() {
        if (File.Exists(highscorePath)) {
            string content = File.ReadAllText(highscorePath);
            if (int.TryParse(content, out int savedHighScore)) {
                highScore = savedHighScore;
            } else {
                Debug.LogWarning("ERROR AT: " + highscorePath + " - CREATING NEW FILE");
                highScore = 0;
                SaveHighScore();
            }
        } else {
            Debug.LogWarning("NO " + highscorePath + " FOUND: CREATING NEW FILE");
            highScore = 0;
            SaveHighScore();
        }
    }
}
