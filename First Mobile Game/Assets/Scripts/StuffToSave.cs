using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//How much of everything we CURRENTLY have

//Must be initialised with previous data from previous sessions to work properly.

//This script will keep track of all stats and MUST be active at all times in order to function properly

public class StuffToSave : MonoBehaviour
{
    /*
    public Boost boostScript;
    public ScoreCount scoreCountScript;
    public ScoringSystem scoringSystemScript;

    public int boosts;
    public int kills;
    public int deaths;
    public int score;
    public int highScore;
    public decimal funds;


    void Start()
    {
        InvokeRepeating("autoSave",3,1);
    }



    void autoSave()
    {
        boosts = boostScript.boostAmount;
        kills = scoringSystemScript.kills;
        deaths = scoringSystemScript.deaths;
        score = scoreCountScript.score;
        highScore = score;
        funds = scoreCountScript.funds;

        /*
        Debug.Log(boosts);
        Debug.Log(kills);
        Debug.Log(deaths);
        Debug.Log(score);
        Debug.Log(highScore);
        Debug.Log(funds);
        
    }
    */
}
