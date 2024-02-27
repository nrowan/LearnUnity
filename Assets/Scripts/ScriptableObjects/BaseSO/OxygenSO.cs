using UnityEngine;

[CreateAssetMenu(fileName = "Oxygen", menuName = "Oxygen", order = 0)]
public class OxygenSO : ScriptableObject
{
    [SerializeField]
    private float _fullOxygen = 100.0f;
    [SerializeField]
    private float _currentOxygen = 100;

    public float CurrentOxygen
    {
        get => _currentOxygen;
        set
        {
            if (value < 1)
            {
                EventManager.RaiseOnOxygenDepleted();
            }
            _currentOxygen = Mathf.Clamp(value, 0, _fullOxygen);
            EventManager.RaiseOnOxygenChanged(_currentOxygen, _fullOxygen);
        }
    }

    public float MaxOxygen
    {
        get => _fullOxygen;
        set
        {
            float max = Mathf.Max(1, value);
            _currentOxygen = Mathf.Max(Mathf.Clamp(_currentOxygen + max - _fullOxygen, 1, value),
                                        _currentOxygen);
            _fullOxygen = max;
            EventManager.RaiseOnOxygenChanged(_currentOxygen, _fullOxygen);
        }
    }

    public void ResetOxygen()
    {
        CurrentOxygen = _fullOxygen;
    }
    public void OxygenLost(float oxygen)
    {
        CurrentOxygen -= oxygen;
    }
    public void OxygenReceived(float oxygen)
    {
        CurrentOxygen += oxygen;
    }
}