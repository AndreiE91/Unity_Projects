using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public Text scoreCountText;
    
    public int score;
    
    public GameManager gameManager;

    void Start()
    {
        InvokeRepeating("UpdateScore", 1, 1);
        InvokeRepeating("passiveAddScore", 0, 1);
    }

    public void UpdateScore()
    {
        scoreCountText.text = score.ToString();
    }

    void passiveAddScore()
    {
        if(!gameManager.gameEnded)
        score += 100;
    }
    
}
