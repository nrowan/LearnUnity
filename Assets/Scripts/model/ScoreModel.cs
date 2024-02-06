using UnityEngine;

public class ScoreModel : MonoBehaviour
{
    private int _maxScore = 10000000;

    public int Score
    {
        get => GameInformation.Score;
        set
        {
            GameInformation.Score = Mathf.Clamp(value, 0, _maxScore);
            EventManager.RaiseOnScoreChanged(GameInformation.Score);
        }
    }

    public void UpdateScore(int scoreValue)
    {
        Score += scoreValue;
    }
}
