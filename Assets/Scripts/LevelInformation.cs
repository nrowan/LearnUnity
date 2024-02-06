using TMPro;
using UnityEngine;
using System.Linq;
public class LevelInformation : MonoBehaviour
{
    private InGameMenus _menus;

    private TextMeshProUGUI _text;

    void Start()
    {
        _menus = FindObjectOfType<InGameMenus>();
        _text = FindObjectsOfType<TextMeshProUGUI>().FirstOrDefault(t => t.name == "LivesText");
        _text.text = GameInformation.Lives.ToString();
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
        if (!GameInformation.GameOver)
        {
            GameInformation.Lives--;
            if (GameInformation.Lives == 0)
            {
                GameInformation.GameOver = true;
                _menus.ShowGameOver();
            }
            else
            {
                _text.text = GameInformation.Lives.ToString();
                _menus.ShowDead();
            }
        }
    }
}
