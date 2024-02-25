using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthPresenter : MonoBehaviour
{
    private InGameMenus _menus;
    private TextMeshProUGUI _text;
    private PlayerHealthModel _healthModel;
    private Slider _healthSlider;
    private Image _sliderFill;
    private Color _sliderFillOC;

    private void Awake()
    {
        _healthSlider = gameObject.GetComponentInChildren<Slider>();
        _healthModel = FindObjectOfType<PlayerHealthModel>();
        _sliderFill = _healthSlider.GetComponentsInChildren<Image>().FirstOrDefault(t => t.name == "Fill");
        _sliderFillOC = _sliderFill.color;

        if (_healthModel == null)
        {
            Debug.LogWarning(
                "Health Presenter needs a Health to present please make sure one is set in The Inspector",
                gameObject);
            enabled = false;
        }
    }

    private void Start()
    {
        _menus = FindObjectOfType<InGameMenus>();
        _text = FindObjectsOfType<TextMeshProUGUI>().FirstOrDefault(t => t.name == "LivesText");
        _text.text = GameInformation.Lives.ToString();

        OnHealthChanged(_healthModel.CurrentHealth, _healthModel.MaxHealth);
    }

    private void OnEnable()
    {
        EventManager.OnHealthChanged += OnHealthChanged;
        EventManager.OnDamageTaken += DamageTaken;
        EventManager.OnHealthReceived += HealthReceived;
        EventManager.OnNewLife += OnNewLife;
        EventManager.OnLifeLost += OnLifeLost;
    }

    private void OnDisable()
    {
        EventManager.OnHealthChanged -= OnHealthChanged;
        EventManager.OnDamageTaken -= DamageTaken;
        EventManager.OnHealthReceived -= HealthReceived;
        EventManager.OnNewLife -= OnNewLife;
        EventManager.OnLifeLost -= OnLifeLost;
    }

    private void DamageTaken(float damage)
    {
        _healthModel.DamageTaken(damage);
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        var percentHealth = currentHealth / maxHealth;
        _healthSlider.value = percentHealth;
        
        if (percentHealth < .5)
        {
            if (_sliderFill != null)
            {
                _sliderFill.color = Color.red;
            }
        }
        else
        {
            _sliderFill.color = _sliderFillOC;
        }
    }
    private void HealthReceived(float health)
    {
        _healthModel.HealthReceived(health);
    }
    private void OnNewLife()
    {
        _healthModel.ResetHealth();
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
