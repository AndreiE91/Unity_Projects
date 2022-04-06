using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FundsPopup : MonoBehaviour
{
    //Create a Funds Popup
    public static FundsPopup Create(Vector3 position, decimal fundsAmount, bool isCritical)
    {
        Transform fundsPopupTransform = Instantiate(GameAssets.i.pfFundsPopup, position, Quaternion.identity);
        FundsPopup fundsPopup = fundsPopupTransform.GetComponent<FundsPopup>();
        fundsPopup.Setup(fundsAmount, isCritical);

        return fundsPopup;
    }

    private static int sortingOrder;

    private const float DISAPPEAR_TIMER_MAX = 1f;

    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    //private Vector3 moveVector;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(decimal fundsAmount, bool isCritical)
    {
        textMesh.SetText("+$ " + fundsAmount.ToString());
        if (!isCritical) //Normal popup
        {
            //Debug.Log("not critical");
            textMesh.fontSize = 18;
            textColor = new Color(0.89f, 1f, 0.031f, 1f);
        }
        else //Critical popup
        {
            //Debug.Log("critical");
            textMesh.fontSize = 36;
            textColor = new Color(0.22f,1f,0.033f,1f);
        }
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        //moveVector = new Vector3(1, 1) * 30f;
    }

    private void Update()
    {
        transform.position += new Vector3(0,2) * Time.deltaTime;
        //moveVector -= moveVector * 8f * Time.deltaTime;

        /*
        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f) //First half of lifetime
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else //Second half of lifetime
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }
        */
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0) //Start disappearing
        {
            float disappearSpeed = 3f;
            textColor.a = disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;

            if (textColor.a <= 20)
            {
                Destroy(gameObject);
            }
        }
    }

}
