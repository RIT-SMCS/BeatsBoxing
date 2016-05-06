using UnityEngine;
using System.Collections;

//[RequireComponent(typeof (GameManager))]
public static class ScoreManager {

    private static float speedScale = 1.0f;

    private static float score = 0.0f;
    private static float multiplier = 1.0f;
    private static int combo = 0;

    /// <summary>
    /// The current score of the player. 
    /// Automatically multiples the inputed increment by the current
    /// multiplier.
    /// </summary>
    public static float Score
    {
        get
        {
            return score;
        }
        set
        {
            //float diff = value - score;
            //score += diff * multiplier;
            score = value;
        }
    }

    public static float Multiplier
    {
        get
        {
            return multiplier;
        }
    }

    public static int Combo
    {
        get
        {
            return combo;
        }

        set
        {
            combo = value;
            //score multipler based on Combo
            multiplier = 1.0f + ((combo / 10) * 0.1f);
        }
    }
    /// <summary>
    /// Returns the current acceleration based on the current score. Here is the formula
    /// </summary>
    public static float SpeedScale
    {
        get
        {
            speedScale = 1.0f + 0.50f*(score / 100f);
            //Mathf.Log()
            return speedScale;
        }
    }

    public static void AddScoreWithMultiplier(float scoreAdd)
    {
        score += (scoreAdd * multiplier);
    }
    public static void AddScoreWithoutMultiplier(float scoreAdd)
    {
        score += scoreAdd;
    }

	public static void Reset()
	{
		score = 0;
		speedScale = 0;
		combo = 0;
	}
}
