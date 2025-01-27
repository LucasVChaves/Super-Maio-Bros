using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
    public int maxHealth = 3;
    private int currHealth;
    void Start() {
        currHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currHealth -= damage;
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
}
