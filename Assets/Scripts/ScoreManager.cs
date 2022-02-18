using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public UIManager uiManager = null;
    public int scoreMeteorito = 10;
    public int scoreDiagonal = 5;
    public int scoreZigZag = 20;

    public int maxScore = 0;
    public int score = 0;
    public float scoreIncreaseInterval = 1.0f;
    private Coroutine coroutine;

    void Start()
    {
        //coroutine = StartCoroutine(IncreaseScoreCoroutine());
    }

    private void OnDisable() 
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);    
        }
    }

    private void OnDestroy()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);    
        }
    }

    private IEnumerator IncreaseScoreCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(scoreIncreaseInterval);
            UpdateScore(+1);
        }
    }

    public void UpdateScore(int update)
    {
        score += update;
        uiManager.UpdateScore(score);
        if (score > maxScore)
        {
            maxScore = score;
            uiManager.UpdateMaxScore(maxScore);
        }
    }

    public void ResetScore()
    {
        score = 0;
        uiManager.UpdateScore(score);
    }

    public void stopCoroutines()
	{
        StopCoroutine(coroutine);
    }

    public void startCoroutines()
	{
        coroutine = StartCoroutine(IncreaseScoreCoroutine());
    }
}
