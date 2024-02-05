using UnityEngine.Events;

public static class EventManager
{
    public static UnityAction<float, float> OnHealthChanged;
    public static void RaiseOnHealthChanged(float m_currentHealth, float m_fullHealth) => OnHealthChanged?.Invoke(m_currentHealth, m_fullHealth);
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