using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button StartGameButton;
    public Button QuitGameButton;
    private void Start()
    {
        StartGameButton.onClick.AddListener(StartGame);
        QuitGameButton.onClick.AddListener(QuitGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    private void QuitGame()
    {
        Application.Quit();
    }
}
