using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public Text counterText;
    public Text deathCounterText;

    private int deathPenalty = 15000;

    public ScoreCount scoreScript;
    public GameObject scoreCounter;
    private Color originalColor;

    public Funds fundScript;

    [HideInInspector]
    public bool alreadyCountedDeath = false;
    public int kills = 0;
    public int deaths = 0;

    void Start()
    {
        originalColor = scoreCounter.GetComponent<Text>().color;
    }

    public void showKills()
    {

        counterText.text = kills.ToString();

    }

    public void showDeaths()
    {
        deathCounterText.text = deaths.ToString();
    }

    public void AddKill(decimal killReward)
    {
        kills++;
        scoreScript.score += (int)(killReward);
        scoreScript.UpdateScore();
        fundScript.funds += killReward / 1000;
        fundScript.UpdateFunds();
        fundScript.StartCoroutine(fundScript.blinkFundText());
        StartCoroutine(greenBlink());
    }

    public void AddDeath()
    {
        deaths++;
        scoreScript.score -= deathPenalty;
        scoreScript.UpdateScore();
        fundScript.funds -= deathPenalty / 1000;
        fundScript.UpdateFunds();
        fundScript.StartCoroutine(fundScript.blinkFundTextRed());
        StartCoroutine(redBlink());

    }

    IEnumerator greenBlink()
    {
        scoreCounter.GetComponent<Text>().color = new Color(0,1,0,0.5882f);
        yield return new WaitForSeconds(0.5f);
        scoreCounter.GetComponent<Text>().color = originalColor;
    }

    IEnumerator redBlink()
    {
        scoreCounter.GetComponent<Text>().color = new Color(1, 0, 0, 0.5882f);
        yield return new WaitForSeconds(0.5f);
        scoreCounter.GetComponent<Text>().color = originalColor;
    }

}
