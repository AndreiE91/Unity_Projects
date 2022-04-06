using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSpawner : MonoBehaviour
{
    private GameObject spawnedMine;
    public GameObject minesHolder;
    public GameObject minePrefab;
    public GameObject pointerNW;
    public GameObject pointerNE;
    public GameObject pointerSE;
    public GameObject pointerSW;
    [HideInInspector]
    public bool hasToRechargeNW = false;
    [HideInInspector]
    public bool hasToRechargeNE = false;
    [HideInInspector]
    public bool hasToRechargeSE = false;
    [HideInInspector]
    public bool hasToRechargeSW = false;
    public GameObject NW;
    public GameObject NE;
    public GameObject SW;
    public GameObject SE;
    private Vector3 Winner;
    private int winnerIndex = 0;

    public GameObject supplyCrateNW;
    public GameObject supplyCrateNE;
    public GameObject supplyCrateSE;
    public GameObject supplyCrateSW;

    public Buttons buttonScript;

    [HideInInspector]
    public int mineCount = 0;

    private int randomOffsetX;
    private int randomOffsetY;

    // Start is called before the first frame update
    void Start()
    {
        if (buttonScript.pointersOff)
        {
            pointerNW.SetActive(false);
            pointerNE.SetActive(false);
            pointerSW.SetActive(false);
            pointerSE.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mineCount < 24)
        {
            spawnedMine = Instantiate(minePrefab);
            spawnedMine.transform.parent = minesHolder.transform;
            randomOffsetX = Random.Range(-15, 15);
            randomOffsetY = Random.Range(-15, 15);
            winnerIndex = Random.Range(0, 4);
            switch (winnerIndex)
            {
                case 0:
                    {
                        Winner = NW.transform.position;
                        break;
                    }
                case 1:
                    {
                        Winner = NE.transform.position;
                        break;
                    }
                case 2:
                    {
                        Winner = SW.transform.position;
                        break;
                    }
                case 3:
                    {
                        Winner = SE.transform.position;
                        break;
                    }
            }    
            spawnedMine.transform.position = Winner + new Vector3(randomOffsetX, randomOffsetY, 0);
            mineCount++;
        }
        if (hasToRechargeNW)
        {
            if(!buttonScript.pointersOff)
            pointerNW.SetActive(false);

            StartCoroutine(rechargeCrateNW());
            hasToRechargeNW = false;
        }
        if (hasToRechargeNE)
        {
            if (!buttonScript.pointersOff)
                pointerNE.SetActive(false);

            StartCoroutine(rechargeCrateNE());
            hasToRechargeNE = false;
        }
        if (hasToRechargeSE)
        {
            if (!buttonScript.pointersOff)
                pointerSE.SetActive(false);

            StartCoroutine(rechargeCrateSE());
            hasToRechargeSE = false;
        }
        if (hasToRechargeSW)
        {
            if (!buttonScript.pointersOff)
                pointerSW.SetActive(false);

            StartCoroutine(rechargeCrateSW());
            hasToRechargeSW = false;
        }
    }

    public IEnumerator rechargeCrateNW()
    {
        yield return new WaitForSeconds(60);
        supplyCrateNW.SetActive(true);
        if (!buttonScript.pointersOff)
            pointerNW.SetActive(true);
    }
    public IEnumerator rechargeCrateNE()
    {
        yield return new WaitForSeconds(60);
        supplyCrateNE.SetActive(true);
        if (!buttonScript.pointersOff)
            pointerNE.SetActive(true);
    }
    public IEnumerator rechargeCrateSE()
    {
        yield return new WaitForSeconds(60);
        supplyCrateSE.SetActive(true);
        if (!buttonScript.pointersOff)
            pointerSE.SetActive(true);
    }
    public IEnumerator rechargeCrateSW()
    {
        yield return new WaitForSeconds(60);
        supplyCrateSW.SetActive(true);
        if (!buttonScript.pointersOff)
            pointerSW.SetActive(true);
    }
}
