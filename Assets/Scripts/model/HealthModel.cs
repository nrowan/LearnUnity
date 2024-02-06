using UnityEngine;
public class HealthModel : MonoBehaviour
{
    private float _fullHealth = 100;
    private float _currentHealth;

    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            if (value < 1)
            {
                EventManager.RaiseOnLifeLost();
            }
            _currentHealth = Mathf.Clamp(value, 0, _fullHealth);
            EventManager.RaiseOnHealthChanged(_currentHealth, _fullHealth);
        }
    }

    public float MaxHealth
    {
        get => _fullHealth;
        set
        {
            float max = Mathf.Max(1, value);
            _currentHealth = Mathf.Max(Mathf.Clamp(_currentHealth + max - _fullHealth, 1, value),
                                        _currentHealth);
            _fullHealth = max;
            EventManager.RaiseOnHealthChanged(_currentHealth, _fullHealth);
        }
    }

    private void Awake()
    {
        ResetHealth();
    }

    public void ResetHealth()
    {
        CurrentHealth = _fullHealth;
    }
    public void DamageTaken(float damage)
    {
        CurrentHealth -= damage;
    }
    public void HealthReceived(float health)
    {
        CurrentHealth += health;
    }
}
