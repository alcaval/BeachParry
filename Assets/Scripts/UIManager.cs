using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText = null;
    public Text maxScoreText = null;
    public Text gameOverScore = null;

    public void UpdateScore(int score)
    {
        scoreText.text = "SCORE: " + score;
        gameOverScore.text = "SCORE: " + score;
    }

    public void UpdateMaxScore(int score)
    {
        maxScoreText.text = "MAX SCORE: " + score;
    }

}
