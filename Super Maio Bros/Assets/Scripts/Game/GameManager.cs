using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    public ScoreManager scoreManager; 
    public PlayerHealth playerHealth; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return; // Se já existir, saímos do método
        }
    }

    void Start()
    {
        // Tenta encontrar os objetos ScoreManager e PlayerHealth na cena
        if (scoreManager == null)
            scoreManager = FindObjectOfType<ScoreManager>();

        if (playerHealth == null)
            playerHealth = FindObjectOfType<PlayerHealth>();
    }
}
