using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OxygenPresenter : MonoBehaviour
{
    private InGameMenus _menus;
    private TextMeshProUGUI _text;
    private PlayerOxygenModel _oxygenModel;
    private Slider _oxygenSlider;
    private Image _sliderFill;
    private Color _sliderFillOC;

    private void Awake()
    {
        _oxygenSlider = gameObject.GetComponentInChildren<Slider>();
        _oxygenModel = FindObjectOfType<PlayerOxygenModel>();
        _sliderFill = _oxygenSlider.GetComponentsInChildren<Image>().FirstOrDefault(t => t.name == "Fill");
        _sliderFillOC = _sliderFill.color;

        if (_oxygenModel == null)
        {
            Debug.LogWarning(
                "Oxygen Presenter needs a oxygen to present please make sure one is set in The Inspector",
                gameObject);
            enabled = false;
        }
    }

    private void Start()
    {
        _menus = FindObjectOfType<InGameMenus>();
        _text = FindObjectsOfType<TextMeshProUGUI>().FirstOrDefault(t => t.name == "LivesText");
        _text.text = GameInformation.Lives.ToString();

        OnOxygenChanged(_oxygenModel.CurrentOxygen, _oxygenModel.MaxOxygen);
    }

    private void OnEnable()
    {
        EventManager.OnOxygenChanged += OnOxygenChanged;
        EventManager.OnOxygenLost += OnOxygenLost;
        EventManager.OnOxygenReceived += OnOxygenReceived;
        EventManager.OnNewLife += OnNewLife;
        EventManager.OnLifeLost += OnLifeLost;
    }

    private void OnDisable()
    {
        EventManager.OnOxygenChanged -= OnOxygenChanged;
        EventManager.OnOxygenLost -= OnOxygenLost;
        EventManager.OnOxygenReceived -= OnOxygenReceived;
        EventManager.OnNewLife -= OnNewLife;
        EventManager.OnLifeLost -= OnLifeLost;
    }

    private void OnOxygenLost(float damage)
    {
        _oxygenModel.OxygenLost(damage);
    }

    private void OnOxygenChanged(float currentOxygen, float maxHOxygen)
    {
        var percentOxygen = currentOxygen / maxHOxygen;
        _oxygenSlider.value = percentOxygen;
        
        if (percentOxygen < .5)
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
    private void OnOxygenReceived(float oxygen)
    {
        _oxygenModel.OxygenReceived(oxygen);
    }
    private void OnNewLife()
    {
        _oxygenModel.ResetOxygen();
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
