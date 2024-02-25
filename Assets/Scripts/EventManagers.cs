using UnityEngine.Events;

public static class EventManager
{
    public static UnityAction OnLevelComplete;
    public static void RaiseOnLevelComplete() => OnLevelComplete?.Invoke();
    public static UnityAction<float, float> OnOxygenChanged;
    public static void RaiseOnOxygenChanged(float currentOxygen, float fullOxygen) => OnOxygenChanged?.Invoke(currentOxygen, fullOxygen);
    public static UnityAction<float> OnOxygenLost;
    public static void RaiseOnOxygenLost(float oxygen) => OnOxygenLost?.Invoke(oxygen);
    public static UnityAction<float> OnOxygenReceived;
    public static void RaiseOnOxygenReceived(float oxygen) => OnOxygenReceived?.Invoke(oxygen);
    public static UnityAction OnLifeLost;
    public static void RaiseOnLifeLost() => OnLifeLost?.Invoke();
    public static UnityAction OnNewLife;
    public static void RaiseOnNewLife() => OnNewLife?.Invoke();

    
    public static UnityAction<int> OnScoreChanged;
    public static void RaiseOnScoreChanged(int score) => OnScoreChanged?.Invoke(score);
    public static UnityAction<int> OnScoreUpdated;
    public static void RaiseOnScoreUpdated(int scoreValue) => OnScoreUpdated?.Invoke(scoreValue);


    // Inventory
    public static UnityAction<ItemBase> OnItemAdded;
    public static void RaiseOnItemAdded(ItemBase item) => OnItemAdded?.Invoke(item);
}