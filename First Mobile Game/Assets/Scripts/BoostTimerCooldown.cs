using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostTimerCooldown : MonoBehaviour
{

    [SerializeField]
    private Image uiFill;

    private int duration;

    public Player playerScript;
    public Boost boostScript;

    private int remainingDuration;

    public void Begin(int seconds)
    {
        duration = seconds;
        remainingDuration = duration;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            uiFill.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration - 1);
            remainingDuration--;
            if (playerScript.isDead)
            {
                uiFill.fillAmount = 0;
                break;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    /*private void OnEnd()
    {
        
    }
    */

}
