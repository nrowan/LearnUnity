using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class ACTIVEMENU
{
    public static string PAUSE = "Pause";
    public static string GAME_OVER = "GameOver";
    public static string DEAD = "Dead";
}
public class InGameMenus : MonoBehaviour
{
    public GameObject MenuOverlay;
    private GameObject ActiveOverlay;
    private Button ActiveButton;
    private PlayerActions _playerActions;

    public bool Paused = false;

    private void Start()
    {
        HideMenu();
        _playerActions.Player_Map.Pause.performed += _ => Pause();
    }
    void Awake()
    {
        _playerActions = new PlayerActions();
    }
    private void OnEnable()
    {
        _playerActions.Player_Map.Enable();
    }

    private void OnDisable()
    {
        _playerActions.Player_Map.Disable();
    }
    private void HandleMenuClick()
    {
        switch (ActiveButton.gameObject.name)
        {
            case "Resume":
                Resume();
                break;
            case "MainMenu":
                MainMenu();
                break;
            case "RestartLevel":
                RestartLevel();
                break;
            default:
                break;
        }
    }

    private void HideMenu()
    {
        if (MenuOverlay != null && MenuOverlay.activeSelf)
        {
            for (int i = 0; i < MenuOverlay.transform.childCount; i++)
            {
                MenuOverlay.transform.GetChild(i).gameObject.SetActive(false);
            }
            MenuOverlay.SetActive(false);
        }
    }

    private void SetActiveMenu(string menu)
    {
        try
        {
            Debug.Log(menu);
            for (int i = 0; i < MenuOverlay.transform.childCount; i++)
            {
                if (MenuOverlay.transform.GetChild(i).gameObject.name == menu)
                {
                    ActiveOverlay = MenuOverlay.transform.GetChild(i).gameObject;
                }
            }
            if (ActiveOverlay)
            {
                HideMenu();
                MenuOverlay.SetActive(true);
                ActiveOverlay.SetActive(true);
                ActiveButton = ActiveOverlay?.GetComponentInChildren<Button>();
                if (ActiveOverlay != null && ActiveButton != null)
                {
                    ActiveButton.onClick.AddListener(HandleMenuClick);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            Debug.LogError(ex.InnerException);
        }
    }

    public void ShowGameOver()
    {
        SetActiveMenu(ACTIVEMENU.GAME_OVER);
        Time.timeScale = 0;
    }
    private void MainMenu()
    {
        HideMenu();
        SceneManager.LoadScene(0);
    }
    public void Pause()
    {
        if (!Paused)
        {
            Paused = true;
            SetActiveMenu(ACTIVEMENU.PAUSE);
            Time.timeScale = 0;
        }
        else
        {
            Resume();
        }
    }
    private void Resume()
    {
        Paused = false;
        HideMenu();
        Time.timeScale = 1;
    }
    public void ShowDead()
    {
        Debug.Log("hello dead");
        SetActiveMenu(ACTIVEMENU.DEAD);
        Time.timeScale = 0;
    }
    private void RestartLevel()
    {
        HideMenu();
        EventManager.RaiseOnNewLife();
        Time.timeScale = 1;
    }
}
