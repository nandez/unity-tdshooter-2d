using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int ScorePoints { get; private set; }

    public TMP_Text txtScore;

    public InGameMenuController inGameMenuController;

    public void AddScorePoints(int points)
    {
        ScorePoints += points;
        txtScore.SetText($"Score: {ScorePoints}");

        // TODO: currently hardcoded for sake of simplicity..
        // to refactor to precalculate winning conditions..
        if(ScorePoints == 150)
            inGameMenuController.ShowStageCleared();
    }
}