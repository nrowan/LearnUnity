using UnityEngine.Events;

public static class LevelEventManager
{
    public static UnityAction OnLevelComplete;
    public static void RaiseOnLevelComplete() => OnLevelComplete?.Invoke();
}