using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Player : MonoBehaviour
{
    public int maxHealth = 250;
    public int shieldHP = 0;
    public int currentHealth;
    //public float knockbackAmount = 5.0f;

    public bool tookDmg = false;
    public GameObject player;
    public GameObject deathEffect;
    public AudioSource damageSound;
    public GameObject healthBarRedOverlayObj;
    public GameObject playerDmgOverlay;
    public GameObject playerDmgOverlayWeapon;
    public Boost boostScript;
    public GameManager gameManager;
    public AudioSource damageShield;
    private AudioSource damageOrig;
    public GameObject shieldOverlay;
    public CameraFollow camScript;

    public bool shieldActive = false;

    [HideInInspector]
    public bool isDead;

    public ScoringSystem deathCounter;

    public HealthBar healthBar;
    public HealthBar healthBarRedOverlay;
    public HealthBar healthBarGreenOverlay;

    public GameObject playerHealOverlay;
    public GameObject playerHealOverlayWeapon;
    public GameObject healthBarGreenOverlayObj;

    void Start()
    {
        damageOrig = damageSound;
        isDead = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBarRedOverlay.SetMaxHealth(maxHealth);
        InvokeRepeating("RepairHull", 0f, 0.25f);
        InvokeRepeating("resetShieldHP", 5, 5);
    }

    public void UpdateHealthBar()
    {
        healthBar.SetHealth(currentHealth);
        healthBarRedOverlay.SetHealth(currentHealth);
        healthBarGreenOverlay.SetHealth(currentHealth);
        StartCoroutine(healthBarBlinkGreen());
    }

    private void RepairHull()
    {
        if (currentHealth < maxHealth && !isDead)
        {
            currentHealth += 1;
            healthBar.SetHealth(currentHealth);
            healthBarRedOverlay.SetHealth(currentHealth);
            healthBarGreenOverlay.SetHealth(currentHealth);
            if (boostScript.isBoosted)
                currentHealth += 4;
        }
    }

    private void Update()
    {

        if (currentHealth <= 0)
        {
            if (!camScript.motherShip)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                UtilsClass.ShakeCamera(.1f, .075f);
                isDead = true;
                tookDmg = false;
                gameManager.hasDied = true;
                if (!deathCounter.alreadyCountedDeath)
                {
                    deathCounter.AddDeath();
                    deathCounter.showDeaths();
                    deathCounter.alreadyCountedDeath = true;
                }
                player.SetActive(false);
            }
            else
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                UtilsClass.ShakeCamera(.1f, .075f);
                player.SetActive(false);
                gameManager.EndGame();
            }
            
        }
        if (shieldHP > 0)
        {
            shieldActive = true;
        }
        else
        {
            shieldActive = false;
        }
        if (shieldActive)
        {
            shieldOverlay.SetActive(true);
            damageSound = damageShield;
        }
        else
        {
            shieldOverlay.SetActive(false);
            damageSound = damageOrig;
        }

    }
    void LateUpdate()
    {
        if (tookDmg && !isDead)
        {
            UtilsClass.ShakeCamera(.1f, .075f);
            StartCoroutine(healthBarBlink());
            damageSound.Play(0);
            tookDmg = false;
            
        }
    }


    void resetShieldHP()
    {
        if (shieldHP < 0)
            shieldHP = 0;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            GameObject collidedWith = col.gameObject;
            Enemy enemyScript = col.GetComponent<Enemy>();

            if (shieldHP <= 0)
            {
                int tempHealth = currentHealth;
                currentHealth -= (int)(enemyScript.health / 2);
                enemyScript.TakeDamage(tempHealth * 2);
            }
            if (shieldHP > 0)
            {
                int tempShieldHealth = shieldHP;
                shieldHP -= (int)(enemyScript.health / 2);
                enemyScript.TakeDamage(tempShieldHealth * 2);
            }
            //Debug.Log(enemyScript.health);
            if (collidedWith == null)
            Instantiate(deathEffect, collidedWith.transform.position, Quaternion.identity);
            //Debug.Log(enemyScript.health);
            damageSound.Play(0);
            //Vector2 direction = (this.transform.position - collision.transform.position).normalized;
            //this.transform.Translate(direction * knockbackAmount);


            tookDmg = true;
            
            healthBar.SetHealth(currentHealth);
            healthBarRedOverlay.SetHealth(currentHealth);
            healthBarGreenOverlay.SetHealth(currentHealth);

        }
        if (col.CompareTag("PlayerBorder"))
        {
            currentHealth = -500;
        }
    }

    public IEnumerator healthBarBlink()
    {
        playerDmgOverlay.SetActive(true);
        playerDmgOverlayWeapon.SetActive(true);
        healthBarRedOverlayObj.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        healthBarRedOverlayObj.SetActive(false);
        playerDmgOverlay.SetActive(false);
        playerDmgOverlayWeapon.SetActive(false);
    }
    public IEnumerator healthBarBlinkGreen()
    {
        playerHealOverlay.SetActive(true);
        playerHealOverlayWeapon.SetActive(true);
        healthBarGreenOverlayObj.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        healthBarGreenOverlayObj.SetActive(false);
        playerHealOverlay.SetActive(false);
        playerHealOverlayWeapon.SetActive(false);
    }

}
