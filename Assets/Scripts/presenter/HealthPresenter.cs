using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HealthPresenter : MonoBehaviour
{
    private HealthModel _healthModel;
    private Slider _healthSlider;

    private void Awake()
    {
        _healthSlider = gameObject.GetComponentInChildren<Slider>();
        _healthModel = FindObjectOfType<HealthModel>();

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
        OnHealthChanged(_healthModel.CurrentHealth, _healthModel.MaxHealth);
    }

    private void OnEnable()
    {
        EventManager.OnHealthChanged += OnHealthChanged;
        EventManager.OnDamageTaken += DamageTaken;
        EventManager.OnHealthReceived += HealthReceived;
        EventManager.OnNewLife += OnNewLife;
    }

    private void OnDisable()
    {
        EventManager.OnHealthChanged -= OnHealthChanged;
        EventManager.OnDamageTaken -= DamageTaken;
        EventManager.OnHealthReceived -= HealthReceived;
        EventManager.OnNewLife += OnNewLife;
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
            var fill = _healthSlider.GetComponentsInChildren<Image>().FirstOrDefault(t => t.name == "Fill");
            if (fill != null)
            {
                fill.color = Color.red;
            }
        }
    }
    private void HealthReceived(float health) 
    {
        _healthModel.HealthReceived(health);
    }
    private void OnNewLife() {
        Debug.Log("New Life");
        _healthModel.ResetHealth();
    }
}
