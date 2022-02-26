using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int Score { get; private set; }

    public static void AddScorePoints(int points)
    {
        Score += points;
    }
}