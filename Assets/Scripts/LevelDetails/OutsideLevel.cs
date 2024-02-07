using UnityEngine;
using System.Collections;
public class OutsideLevel : MonoBehaviour
{
    private IEnumerator coroutine;
    int _healthDrain { get; set; } = 1;
    int _drainSpeed { get; set; } = 1;
    public OutsideLevel() { }
    public OutsideLevel(int? drainSpeed, int? healthDrain)
    {
        _drainSpeed = drainSpeed ?? _drainSpeed;
        _healthDrain = healthDrain ?? _healthDrain;
    }

    void Start()
    {
        coroutine = HealthDrain();
        StartCoroutine(coroutine);
    }
    IEnumerator HealthDrain()
    {
        while (true)
        {
            EventManager.RaiseOnDamageTaken(_healthDrain);
            yield return new WaitForSeconds(_drainSpeed);
        }
    }
}