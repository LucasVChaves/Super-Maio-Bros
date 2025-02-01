using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    public int maxHealth = 3;
    public Sprite[] heartSprites;
    public Image uiHearts;
    public AudioClip hurtSFX;
    private int currHealth;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    void Start() {
        currHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update() {
        uiHearts.sprite = heartSprites[currHealth];
    }

    public void TakeDamage(int damage) {
        currHealth -= damage;
        audioSource.PlayOneShot(hurtSFX, 1f);
        spriteRenderer.color = new Color(255, 0, 0, 255);
        StartCoroutine(changeColorBack());

        Debug.Log("Player took damage. Current Health = " + currHealth);

        if (currHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        Debug.Log("Player Died");
        // Recarrega a cena, eu acho
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator changeColorBack() {
        yield return new WaitForSeconds(0.75f);
        spriteRenderer.color = new Color(255, 255, 255, 255);
    }
}
