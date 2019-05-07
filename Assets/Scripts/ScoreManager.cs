using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject highScoreText;
    public GameObject livesText;
    

    private int score = 0, highScore = 0, lives = 3;

    public void SetLives(int _lives)
    {
        lives = _lives;
        UpdateScoreUI(score, highScore, lives);
    }


    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreUI(score, highScore, lives);
        CheckHighScore();
    }

    public void CheckHighScore()
    {
        
        if (score > highScore)
        {
            highScore = score;
        }

        
    }

    private void UpdateScoreUI(int score, int highScore, int lives)
    {
        scoreText.GetComponent<Text>().text = "Score : " + score;
        highScoreText.GetComponent<Text>().text = "HighScore : " + score;
        livesText.GetComponent<Text>().text = "Lives  : " + lives;
       
    }
    public int GetScore()
    {
        return score;
    }
}
