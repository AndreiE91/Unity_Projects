using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boost : MonoBehaviour
{
    [HideInInspector]
    public bool isBoosted = false;
    public AudioSource boostSound;

    public TextMeshProUGUI amountText;

    public int boostAmount = 3;

    public bool previouslyDied = false;
    public bool alreadyStartedCount = false;
    public Weapon weaponScript;

    public BoostTimerCooldown timerCool;
    public GameObject timerCoolObj;

    public GameObject buttonGreyed;
    public GameObject ring;
    public GameObject button;
    public Player player;
    public GameObject overlay;

    public void boostButton()
    {
        StartCoroutine(activateBoost());
    }

    
    void Update()
    {
        /*
        if (player.isDead && !alreadyStartedCount)
        {
            StartCoroutine(countTimeSinceRespawned());
            alreadyStartedCount = true;
        }
        */
        if (boostAmount <= 0)
        {
            buttonGreyed.SetActive(true);
            ring.SetActive(false);
            timerCoolObj.SetActive(false);
        }
        else
        {
            buttonGreyed.SetActive(false);
            ring.SetActive(true);
            timerCoolObj.SetActive(true);
        }
        

    }
    

    void Start()
    {
        amountText.text = ("x" + boostAmount).ToString();
    }

    /*
    public IEnumerator countTimeSinceRespawned()
    {
        previouslyDied = true;
        yield return new WaitForSeconds(29.9f);
        previouslyDied = false;
        alreadyStartedCount = false;
    }
    */

    IEnumerator activateBoost()
    { 
        overlay.SetActive(true);
        grayOutButton();
        
        boostSound.Play(0);
        isBoosted = true;
        boostAmount--;
        amountText.text = ("x" + boostAmount).ToString();
        timerCool.Begin(30);
        yield return new WaitForSeconds(5);
        
        isBoosted = false;
        
        overlay.SetActive(false);
        yield return new WaitForSeconds(25);
        activateBackButton(); 
      }

    public void grayOutButton()
    {
        button.GetComponent<Button>().interactable = false;
        buttonGreyed.SetActive(true);
        ring.SetActive(false);
        timerCoolObj.SetActive(true);

    }

    public void activateBackButton()
    {
        buttonGreyed.SetActive(false);
        ring.SetActive(true);
        button.GetComponent<Button>().interactable = true;
        timerCoolObj.SetActive(false);
    }
}
