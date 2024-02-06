using UnityEngine;
using System.Collections;

public class HealthModel : MonoBehaviour
{
    private IEnumerator coroutine;
    private float _fullHealth = 100;
    private int _healthDrain = 1;
    private int _drainSpeed = 1;
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
        DontDestroyOnLoad(gameObject);
        ResetHealth();
    }

    public void ResetHealth()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        CurrentHealth = _fullHealth;
        coroutine = HealthDrain();
        StartCoroutine(coroutine);
    }

    public IEnumerator HealthDrain()
    {
        while (true)
        {
            DamageTaken(_healthDrain);
            yield return new WaitForSeconds(_drainSpeed);
        }
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
