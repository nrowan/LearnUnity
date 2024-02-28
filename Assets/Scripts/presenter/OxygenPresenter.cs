using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OxygenPresenter : MonoBehaviour
{
    [SerializeField]
    private OxygenSO _playerHealthSO;
    private Slider _oxygenSlider;
    private Image _sliderFill;
    private Color _sliderFillOC;

    private void Awake()
    {
        _oxygenSlider = gameObject.GetComponentInChildren<Slider>();
        _sliderFill = _oxygenSlider.GetComponentsInChildren<Image>().FirstOrDefault(t => t.name == "Fill");
        _sliderFillOC = _sliderFill.color;

        if (_playerHealthSO == null)
        {
            Debug.LogWarning(
                "Oxygen Presenter needs a oxygen to present please make sure one is set in The Inspector",
                gameObject);
            enabled = false;
        }
    }

    private void Start()
    {
        OnOxygenChanged(_playerHealthSO.CurrentOxygen, _playerHealthSO.MaxOxygen);
    }

    private void OnEnable()
    {
        OxygenEventManager.OnOxygenChanged += OnOxygenChanged;
        OxygenEventManager.OnOxygenLost += OnOxygenLost;
        OxygenEventManager.OnOxygenReceived += OnOxygenReceived;
    }

    private void OnDisable()
    {
        OxygenEventManager.OnOxygenChanged -= OnOxygenChanged;
        OxygenEventManager.OnOxygenLost -= OnOxygenLost;
        OxygenEventManager.OnOxygenReceived -= OnOxygenReceived;
    }

    private void OnOxygenLost(float damage)
    {
        _playerHealthSO.OxygenLost(damage);
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
        _playerHealthSO.OxygenReceived(oxygen);
    }
}
