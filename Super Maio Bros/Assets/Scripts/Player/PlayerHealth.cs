using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public int maxHealth = 3;
    public Sprite[] heartSprites;
    public Image uiHearts;
    public AudioClip hurtSFX;
    public AudioClip gameOverMusic; // 🎵 Música quando morre
    public AudioClip restartSound;  // 🎵 Som quando clica em restart
    public GameObject gameOverPanel;
    public Button restartButton;

    private int currHealth;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private AudioSource musicPlayer; // 🎵 Controla a música do jogo

    void Start() {
        currHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // 🎵 Encontra o MusicPlayer na cena 🎵
        musicPlayer = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();

        // Garante que o painel está escondido
        gameOverPanel.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
    }

    void Update() {
        if (currHealth >= 0 && currHealth < heartSprites.Length) {
            uiHearts.sprite = heartSprites[currHealth];
        }
    }

    public void TakeDamage(int damage) {
        currHealth -= damage;
        ScoreManager.Instance.SubScore(50);
        audioSource.PlayOneShot(hurtSFX, 1f);
        spriteRenderer.color = new Color(255, 0, 0, 255);
        StartCoroutine(ChangeColorBack());

        if (currHealth <= 0) {
            Die();
        }
    }

    public void Heal(int healing) {
        currHealth += healing;
        if (currHealth > maxHealth) {
            currHealth = maxHealth;
        }
    }

    private void Die() {
        Debug.Log("Player Died");

        // 🎵 PAUSA a música do jogo e toca a de Game Over 🎵
        if (musicPlayer != null) {
            musicPlayer.Pause();  // 🔴 PAUSA a música em vez de parar
        }

        if (gameOverMusic != null) {
            audioSource.PlayOneShot(gameOverMusic);  // 🎵 Toca música de Game Over separadamente
        }

        // 🔴 Exibir painel de Game Over
        gameOverPanel.SetActive(true);
        Time.timeScale = 0; // 🔴 Pausa o jogo enquanto o jogador está morto
        ScoreManager.Instance.SaveOnPlayerDeath();
    }

    private void RestartGame() {
        Debug.Log("Restarting Game...");

        // 🔴 Para qualquer música que esteja tocando (Game Over ou outra)
        audioSource.Stop();

        // 🎵 Para tudo e toca o som de restart antes de reiniciar 🎵
        if (musicPlayer != null) {
            musicPlayer.Stop(); // 🔴 Para qualquer música (inclusive Game Over)
        }

        if (restartSound != null) {
            audioSource.PlayOneShot(restartSound); // Toca o som de transição
            StartCoroutine(WaitForRestart(restartSound.length));
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    IEnumerator WaitForRestart(float waitTime) {
        yield return new WaitForSecondsRealtime(waitTime); // Espera o áudio terminar
        Time.timeScale = 1; // 🔴 Volta o tempo ao normal

        // 🎵 Retoma a música original do jogo 🎵
        if (musicPlayer != null) {
            musicPlayer.Play(); // 🔥 Agora reinicia a música do jogo
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ChangeColorBack() {
        yield return new WaitForSeconds(0.75f);
        spriteRenderer.color = new Color(255, 255, 255, 255);
    }
}
