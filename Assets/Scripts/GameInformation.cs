using TMPro;
using UnityEngine;
using System.Linq;
public class GameInformation : MonoBehaviour
{
    public static int CurrentLevel { get; set; } = 0;
    public static int Lives { get; set; } = 3;
    public static bool GameOver { get; set; } = false;

    private InGameMenus _menus;

    private TextMeshProUGUI _text;

    void Start()
    {
        _menus = FindObjectOfType<InGameMenus>();
        _text = FindObjectsOfType<TextMeshProUGUI>().FirstOrDefault(t => t.name == "LivesText");
        _text.text = Lives.ToString();
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        EventManager.OnLifeLost += OnLifeLost;
    }

    private void OnDisable()
    {
        EventManager.OnLifeLost -= OnLifeLost;
    }

    private void OnLifeLost()
    {
        Debug.Log("Life Lost");
        if (!GameOver)
        {
            Lives--;
            if (Lives == 0)
            {
                GameOver = true;
                _menus.ShowGameOver();
            }
            else
            {
                _text.text = Lives.ToString();
                _menus.ShowDead();
            }
        }
    }
}
