using UnityEngine;

public class OxygenPresenter : MonoBehaviour
{
    private int _value = 100;
    private float _healthBoost = 10.0f;
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                EventManager.RaiseOnScoreUpdated(_value);
                EventManager.OnHealthReceived(_healthBoost);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
