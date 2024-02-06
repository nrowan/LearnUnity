using UnityEngine;
using System.Collections;
public class OutsideLevel : MonoBehaviour
{
    private IEnumerator coroutine;
    private int _healthDrain = 10;
    private int _drainSpeed = 1;

    void Start()
    {
        coroutine = HealthDrain();
        StartCoroutine(coroutine);
    }
    public IEnumerator HealthDrain()
    {
        while (true)
        {
            EventManager.RaiseOnDamageTaken(_healthDrain);
            yield return new WaitForSeconds(_drainSpeed);
        }
    }
}
