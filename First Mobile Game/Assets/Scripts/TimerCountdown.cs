using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountdown : MonoBehaviour
{
    public GameObject textDisplay;
    public AudioSource timerClick;
    public int respawnTime = 7;
    public int secondsLeft = 7;


    public IEnumerator TimerTake()
    {
        
        while (secondsLeft > 0)
        {
            textDisplay.GetComponent<Text>().text += secondsLeft;
            yield return new WaitForSeconds(1);
            secondsLeft--;
            textDisplay.GetComponent<Text>().text = "";
            if(secondsLeft <= 3 && secondsLeft > 0)
            timerClick.Play(0);
        }
    }

}
