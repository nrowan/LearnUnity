using UnityEngine;

[CreateAssetMenu(fileName = "ScoreModel", menuName = "Score Model")]
public class ScoreSO : ScriptableObject
{
    [field: SerializeField]
    private int _score = 0;
    [SerializeField]
    private int _maxScore = 10000000;

    public int Score
    {
        get => _score;
        set
        {
            _score = Mathf.Clamp(value, 0, _maxScore);
            ScoreEventManager.RaiseOnScoreChanged(_score);
        }
    }

    public void UpdateScore(int scoreValue)
    {
        Score += scoreValue;
    }
}