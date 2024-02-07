using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                EventManager.RaiseOnLevelComplete();
                break;
            default:
                break;
        }
    }
}
