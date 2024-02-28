using UnityEngine;
using System.Collections;
public class OutsideLevel : MonoBehaviour
{
    private IEnumerator coroutine;
    int _oxygenDrain { get; set; } = 1;
    int _drainSpeed { get; set; } = 1;
    public OutsideLevel() { }
    public OutsideLevel(int? drainSpeed, int? oxygenLost)
    {
        _drainSpeed = drainSpeed ?? _drainSpeed;
        _oxygenDrain = oxygenLost ?? _oxygenDrain;
    }

    void Start()
    {
        coroutine = OxygenDrain();
        StartCoroutine(coroutine);
    }
    IEnumerator OxygenDrain()
    {
        while (true)
        {
            OxygenEventManager.OnOxygenLost(_oxygenDrain);
            yield return new WaitForSeconds(_drainSpeed);
        }
    }
}