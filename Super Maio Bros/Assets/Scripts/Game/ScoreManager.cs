using UnityEngine;
using TMPro;
using System.IO;
using System.Collections;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance { get; private set; }
    public TMP_Text uiScoreText;
    public TMP_Text uiHighscoreText;
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

        highscorePath = Application.persistentDataPath + "/highscore.txt";
        LoadHighScore();
        uiHighscoreText.text = "Highscore = " + highScore.ToString();
        currScore = 0;
        StartCoroutine(IncreaseScoreOverTime());
    }

    IEnumerator IncreaseScoreOverTime() {
        while (true) {
            yield return new WaitForSeconds(5);
            AddScore(10);
        }
    }

    void Update() {
        uiScoreText.text = "POINTS = " + currScore.ToString();
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

    public void SaveOnPlayerDeath() {
        if (currScore > highScore) {
            highScore = currScore;
        }
        SaveScore();
    }

    public void CheckHighScore() {
        if (currScore > highScore) {
            highScore = currScore;
        }
        SaveScore();
    }

    void OnApplicationQuit() {
        SaveScore();
    }

    private void SaveScore() {
        if (!Directory.Exists(Path.GetDirectoryName(highscorePath))) {
            Directory.CreateDirectory(Path.GetDirectoryName(highscorePath));
        }

        File.WriteAllText(highscorePath, highScore + ";" + currScore);
    }

    private void LoadHighScore() {
        if (File.Exists(highscorePath)) {
            string content = File.ReadAllText(highscorePath);
            string[] values = content.Split(';');

            if (values.Length >= 2 && int.TryParse(values[0], out int savedHighScore) && int.TryParse(values[1], out int savedCurrScore)) {
                highScore = savedHighScore;
                currScore = savedCurrScore;
            } else {
                Debug.LogWarning("Erro ao carregar pontuação! Criando novo arquivo...");
                highScore = 0;
                currScore = 0;
                SaveScore();
            }
        } else {
            Debug.LogWarning("Nenhum arquivo de pontuação encontrado! Criando novo arquivo...");
            highScore = 0;
            currScore = 0;
            SaveScore();
        }
    }

}
