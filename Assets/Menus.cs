using UnityEngine;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    private Button NewLifeButton;
    private Button UnpauseButton;
    private Button QuitButton;
    public GameObject DeadPanel;
    public GameObject PausePanel;
    public GameObject GameOverPanel;
    private void Start()
    {
        NewLifeButton = DeadPanel.GetComponentInChildren<Button>();
        UnpauseButton = PausePanel.GetComponentInChildren<Button>();
        QuitButton = GameOverPanel.GetComponentInChildren<Button>();
        NewLifeButton.onClick.AddListener(ContinueNextLife);
        UnpauseButton.onClick.AddListener(Unpause);
        QuitButton.onClick.AddListener(Quit);
    }

    public void ShowGameOver()
    {
        GameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
    private void Quit()
    {
        GameOverPanel.SetActive(false);
        Debug.Log("Close App");
    }
    public void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    private void Unpause()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void ShowDead()
    {
        DeadPanel.SetActive(true);
        Time.timeScale = 0;
    }
    private void ContinueNextLife()
    {
        DeadPanel.SetActive(false);
        EventManager.RaiseOnNewLife();
        Time.timeScale = 1;
    }
}
