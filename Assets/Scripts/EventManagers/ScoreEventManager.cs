using UnityEngine.Events;

public static class ScoreEventManager
{
    public static UnityAction<int> OnScoreChanged;
    public static void RaiseOnScoreChanged(int score) => OnScoreChanged?.Invoke(score);
    public static UnityAction<int> OnScoreUpdated;
    public static void RaiseOnScoreUpdated(int scoreValue) => OnScoreUpdated?.Invoke(scoreValue);
}