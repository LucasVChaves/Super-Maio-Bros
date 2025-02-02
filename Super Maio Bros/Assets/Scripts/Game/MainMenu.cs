using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("aaaa");
        SceneManager.LoadScene("SampleScene"); // Troque pelo nome da sua cena principal
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo..."); // Apenas para testes, funciona no editor
        Application.Quit(); // Fecha o jogo (somente na build)
    }
}
