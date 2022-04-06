using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Funds : MonoBehaviour
{
    public Text fundsCountText;
    public decimal funds = 0m;
    public decimal deltaFunds = 0m;
    private GameManager gameManager;
    private Color originalColor;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        originalColor = gameObject.GetComponent<Text>().color;
        funds = ImportantVariables.totalFunds;
        UpdateFunds();
        ImportantVariables.tempFunds = 0;

        InvokeRepeating("passiveAddFunds", 1, 1);
        InvokeRepeating("UpdateFunds", 1, 1);
    }

    public void UpdateFunds()
    {
        fundsCountText.text = funds.ToString();
        ImportantVariables.tempFunds = funds - ImportantVariables.totalFunds;
    }

    void passiveAddFunds()
    {
        if (!gameManager.gameEnded)
            funds += 0.1m;
    }

    public IEnumerator blinkFundText()
    {
        gameObject.GetComponent<Text>().color = new Color(0,1,0,0.588f);
        yield return new WaitForSecondsRealtime(0.15f);
        gameObject.GetComponent<Text>().color = originalColor;
    }

    public IEnumerator blinkFundTextRed()
    {
        gameObject.GetComponent<Text>().color = new Color(1, 0, 0, 0.588f);
        yield return new WaitForSecondsRealtime(0.15f);
        gameObject.GetComponent<Text>().color = originalColor;
    }
}
