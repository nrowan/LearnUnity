using UnityEngine.Events;

public static class EventManager
{
    public static UnityAction<int> OnLevelComplete;
    public static void RaiseOnLevelComplete(int level) => OnLevelComplete?.Invoke(level);
    public static UnityAction<float, float> OnHealthChanged;
    public static void RaiseOnHealthChanged(float currentHealth, float fullHealth) => OnHealthChanged?.Invoke(currentHealth, fullHealth);
    public static UnityAction<float> OnDamageTaken;
    public static void RaiseOnDamageTaken(float damage) => OnDamageTaken?.Invoke(damage);
    public static UnityAction<float> OnHealthReceived;
    public static void RaiseOnHealthReceived(float health) => OnHealthReceived?.Invoke(health);
    public static UnityAction OnLifeLost;
    public static void RaiseOnLifeLost() => OnLifeLost?.Invoke();
    public static UnityAction OnNewLife;
    public static void RaiseOnNewLife() => OnNewLife?.Invoke();

    
    public static UnityAction<int> OnScoreChanged;
    public static void RaiseOnScoreChanged(int score) => OnScoreChanged?.Invoke(score);
    public static UnityAction<int> OnScoreUpdated;
    public static void RaiseOnScoreUpdated(int scoreValue) => OnScoreUpdated?.Invoke(scoreValue);
}