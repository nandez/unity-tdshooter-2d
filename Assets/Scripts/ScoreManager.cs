using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int ScorePoints { get; private set; }

    public TMP_Text txtScore;

    public void AddScorePoints(int points)
    {
        ScorePoints += points;
        txtScore.SetText($"Score: {ScorePoints}");
    }
}