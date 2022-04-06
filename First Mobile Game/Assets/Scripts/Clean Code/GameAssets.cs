using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = (Instantiate(GameObject.Find("GameAssets"))).GetComponent<GameAssets>();
            return _i;
        }
    }

    public Transform pfFundsPopup;
}
