using UnityEngine;

public class ScoreModel : MonoBehaviour
{
    private int _maxScore = 10000000;
    private int _score = 0;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public int Score
    {
        get => _score;
        set
        {
            _score = Mathf.Clamp(value, 0, _maxScore);
            EventManager.RaiseOnScoreChanged(_score);
        }
    }

    public void UpdateScore(int scoreValue)
    {
        Score += scoreValue;
    }
}
