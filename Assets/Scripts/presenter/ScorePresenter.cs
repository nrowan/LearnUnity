using System.Linq;
using TMPro;
using UnityEngine;

public class ScorePresenter : MonoBehaviour
{
    [SerializeField]
    private ScoreSO ScoreModel;
    private TextMeshProUGUI _text;
    void Start()
    {
        _text = gameObject.GetComponentsInChildren<TextMeshProUGUI>().FirstOrDefault(t => t.name == "ScoreText");
        OnScoreChanged(ScoreModel.Score);
        if (ScoreModel == null)
        {
            Debug.LogWarning(
                "Score Presenter needs a Score Model to present please make sure to create a empty object and assign ScoreModel script",
                gameObject);
            enabled = false;
        }
    }

    private void OnEnable()
    {
        EventManager.OnScoreChanged += OnScoreChanged;
        EventManager.OnScoreUpdated += OnScoreUpdated;
    }

    private void OnDisable()
    {
        EventManager.OnScoreChanged -= OnScoreChanged;
        EventManager.OnScoreUpdated -= OnScoreUpdated;
    }

    private void OnScoreChanged(int score)
    {
        _text.text = score.ToString();
    }
    private void OnScoreUpdated(int scoreValue)
    {
        ScoreModel.UpdateScore(scoreValue);
    }
}
