using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text gameTimerText;
    public Slider levelProgress;
    private float levelDuration = 450f;
    private GameManager gManager;
    float gameTimer = 0f;
    private CameraFollow camScript;


    void Awake()
    {
        camScript = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        gManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (!camScript.motherShip)
        {
            levelProgress.maxValue = levelDuration;
            levelProgress.value = 0;
        }
        else
        {
            levelProgress.maxValue = levelDuration -150f;
            levelProgress.value = 0;
        }
    }

    void Update()
    {
        gameTimer += Time.deltaTime;
        //if(!camScript.motherShip)
        levelProgress.value = gameTimer;

        int seconds = (int)(gameTimer % 60);
        int minutes = (int)(gameTimer / 60) % 60;
        int hours = (int)(gameTimer / 3600);

        string timerString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

        gameTimerText.text = timerString;
        if (!camScript.motherShip)
        {
            if (gameTimer >= 450)
            {
                gManager.EndLevel1();
            }
        }
        else
        {
            if (gameTimer >= 300)
            {
                gManager.EndLevel2();
            }
        }
    }
}
