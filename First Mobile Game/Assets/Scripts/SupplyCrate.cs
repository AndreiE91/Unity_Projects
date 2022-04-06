using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyCrate : MonoBehaviour
{
    private Boost boost;
    public GameObject pickUpAnimation;
    public MineSpawner mineSpawner;
    public Mothership mother;
    public int crateType = 0;
    public Player player;
    public ScoreCount scoring;
    public Funds fundScript;
    public bool preFabbed = false;
    private CameraFollow camScript;

    void Awake()
    {
        fundScript = GameObject.Find("Canvas").GetComponentInChildren<Funds>();
        camScript = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        scoring = GameObject.Find("Canvas").GetComponentInChildren<ScoreCount>();
        boost = GameObject.Find("GameManager").GetComponent<Boost>();
        if(!camScript.motherShip)
        mother = GameObject.Find("Objective").GetComponentInChildren<Mothership>();

        
    }

    void Start()
    {
        player = GameObject.Find("PlayerCollider").GetComponentInChildren<Player>();
    }

  

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            switch (crateType)
            {
                case 0: //boost power
                    {
                        Instantiate(pickUpAnimation, transform.position, Quaternion.identity);

                        boost.boostAmount++;
                        boost.amountText.text = ("x" + boost.boostAmount).ToString();
                        if (preFabbed)
                        {
                            mineSpawner.hasToRechargeNW = true;
                            gameObject.SetActive(false);
                        }
                        else
                            Destroy(gameObject);
                        break;
                    }
                case 1: // mothership heal
                    {
                        if (!camScript.motherShip)
                        {
                            Instantiate(pickUpAnimation, transform.position, Quaternion.identity);

                            if(mother.motherShipHP <= mother.motherShipMaxHP - 250)
                                mother.motherShipHP += 250;
                            else
                                mother.motherShipHP = mother.motherShipMaxHP;

                            mother.hasToUpdateHPPositive = true;
                            if (preFabbed)
                            {
                                mineSpawner.hasToRechargeSW = true;
                                gameObject.SetActive(false);
                            }
                            else
                                Destroy(gameObject);
                        }
                        else
                        {
                            Instantiate(pickUpAnimation, transform.position, Quaternion.identity);

                            player.currentHealth += 500;
                            player.UpdateHealthBar();
                            if (preFabbed)
                            {
                                mineSpawner.hasToRechargeSW = true;
                                gameObject.SetActive(false);
                            }
                            else
                                Destroy(gameObject);
                        }
                        break;
                    }
                case 2: // player shield power
                    {
                        Instantiate(pickUpAnimation, transform.position, Quaternion.identity);

                        player.shieldHP += 200;
                        if (preFabbed)
                        {
                            mineSpawner.hasToRechargeSE = true;
                            gameObject.SetActive(false);
                        }
                        else
                            Destroy(gameObject);
                        break;
                    }
                case 3: // bonus score
                    {
                        Instantiate(pickUpAnimation, transform.position, Quaternion.identity);

                        scoring.score += 50000;
                        scoring.UpdateScore();
                        fundScript.funds += 50;
                        fundScript.UpdateFunds();
                        fundScript.StartCoroutine(fundScript.blinkFundText());
                        if (preFabbed)
                        {
                            mineSpawner.hasToRechargeNE = true;
                            gameObject.SetActive(false);
                        }
                        else
                            Destroy(gameObject);
                        break;
                    }
            }
            
        
        
        }
    }

}
