using UnityEngine.Events;

public static class OxygenEventManager
{
    public static UnityAction OnLevelComplete;
    public static void RaiseOnLevelComplete() => OnLevelComplete?.Invoke();
    public static UnityAction<float, float> OnOxygenChanged;
    public static void RaiseOnOxygenChanged(float currentOxygen, float fullOxygen) => OnOxygenChanged?.Invoke(currentOxygen, fullOxygen);
    public static UnityAction<float> OnOxygenLost;
    public static void RaiseOnOxygenLost(float oxygen) => OnOxygenLost?.Invoke(oxygen);
    public static UnityAction<float> OnOxygenReceived;
    public static void RaiseOnOxygenReceived(float oxygen) => OnOxygenReceived?.Invoke(oxygen);
    public static UnityAction OnOxygenDepleted;
    public static void RaiseOnOxygenDepleted() => OnOxygenDepleted?.Invoke();
}