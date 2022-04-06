using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mothership : MonoBehaviour
{
    public int motherShipHP;
    public int motherShipMaxHP = 1000;
    [HideInInspector]
    public bool isMotherAlive;
    [HideInInspector]
    public bool respawning;
    [HideInInspector]
    public bool hasRespawned;
    public Collider2D motherShip;
    public GameObject deathEffectM;
    public AudioSource damageSoundM;
    public AudioSource playerRespawn;
    public GameObject motherShipObj;
    public GameObject healthBarRedOverlayObj;
    public GameObject motherShipDmgOverlay;
    public GameObject screenRedOverlay;

    public GameObject screenGreenOverlay;
    public GameObject motherShipGreenOverlay;
    public GameObject healthBarGreenOverlayObj;

    public Player player;
    public GameObject playerObj;
    public GameObject playerCollider;
    public GameObject playerHealthBarRedOverlayObj;
    public GameObject playerDmgOverlay;
    public GameObject playerRespawnOverlay;
    public GameObject playerDmgOverlayWeapon;
    public GameObject playerRespawnOverlayWeapon;
    public GameObject respawnCounter;
    public TimerCountdown timer;

    public bool hasToUpdateHPPositive;

    public Rigidbody2D playerRb;

    public ScoringSystem deathCounter;

    public HealthBar playerHealthBar;
    public HealthBar playerHealthBarRedOverlay;
    public HealthBar playerHealthBarGreen;
    public GameObject playerHealthBarGreenOverlay;

    public Boost boostScriptRef;
    public BoostTimerCooldown boostTimer;

    public GameObject motherGlow;


    public HealthBar motherShipHealthBar;
    public HealthBar motherShipHealthBarRedOverlay;
    public HealthBar motherShipHealthBarGreenOverlay;

    void Start()
    {
        hasRespawned = false;
        respawning = false;
        isMotherAlive = true;
        motherShipHP = motherShipMaxHP;
        motherShipHealthBar.SetMaxHealth(motherShipMaxHP);
        motherShipHealthBarRedOverlay.SetMaxHealth(motherShipMaxHP);
        motherShipHealthBarGreenOverlay.SetHealth(motherShipHP);
        InvokeRepeating("RepairMotherHull", 0f, 0.25f);
    }

    private void RepairMotherHull()
    {
        if (motherShipHP < motherShipMaxHP && motherShipHP > 0 && boostScriptRef.isBoosted)
        {
            motherShipHP += 5;
            motherShipHealthBar.SetHealth(motherShipHP);
            motherShipHealthBarRedOverlay.SetHealth(motherShipHP);
            motherShipHealthBarGreenOverlay.SetHealth(motherShipHP);
        }
    }

    private void Update()
    {

        if (motherShipHP <= 0)
        {
            Instantiate(deathEffectM, transform.position, Quaternion.identity);
            Destroy(motherShipObj);
            isMotherAlive = false;
            FindObjectOfType<GameManager>().EndGame();
        }
        if (isMotherAlive && player.isDead && !respawning)
        {
            hasRespawned = false;
            respawning = true;

            playerHealthBar.SetHealth(0);
            playerHealthBarRedOverlay.SetHealth(0);

            StartCoroutine(Respawn());
        }
        if (hasToUpdateHPPositive)
        {
            motherShipHealthBar.SetHealth(motherShipHP);
            motherShipHealthBarRedOverlay.SetHealth(motherShipHP);
            motherShipHealthBarGreenOverlay.SetHealth(motherShipHP);
            StartCoroutine(motherShipHealthBarBlinkGreen());
            hasToUpdateHPPositive = false;
        }

    }

    IEnumerator Respawn()
    {
        boostScriptRef.grayOutButton();
        respawning = true;
        timer.secondsLeft = timer.respawnTime;
        respawnCounter.SetActive(true);
        StartCoroutine(timer.TimerTake());
        yield return new WaitForSeconds(timer.respawnTime);
        respawnCounter.SetActive(false);

        playerCollider.transform.position = new Vector3(0, -12, 0);

        playerObj.SetActive(true);
        playerRb.velocity = new Vector2(0, 0);


        playerHealthBarRedOverlayObj.SetActive(false);
        playerDmgOverlay.SetActive(false);
        playerDmgOverlayWeapon.SetActive(false);


        player.currentHealth = player.maxHealth / 2;

        playerHealthBar.SetMaxHealth(player.maxHealth);
        playerHealthBarRedOverlay.SetMaxHealth(player.maxHealth);

        playerHealthBar.SetHealth(player.maxHealth);
        playerHealthBarRedOverlay.SetHealth(player.maxHealth);


        player.isDead = false;
        hasRespawned = true;
        StartCoroutine(respawnBoostCooldown());
        respawning = false;

        deathCounter.alreadyCountedDeath = false;
        playerRespawn.Play(0);

        playerHealthBarGreen.SetMaxHealth(player.maxHealth);
        playerHealthBarGreen.SetHealth(player.maxHealth / 2);

        playerRespawnOverlay.SetActive(true);
        playerRespawnOverlayWeapon.SetActive(true);
        playerHealthBarGreenOverlay.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        playerRespawnOverlay.SetActive(false);
        playerRespawnOverlayWeapon.SetActive(false);
        playerHealthBarGreenOverlay.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        playerRespawnOverlay.SetActive(true);
        playerRespawnOverlayWeapon.SetActive(true);
        playerHealthBarGreenOverlay.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        playerRespawnOverlay.SetActive(false);
        playerRespawnOverlayWeapon.SetActive(false);
        playerHealthBarGreenOverlay.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        playerHealthBarGreenOverlay.SetActive(true);
        playerRespawnOverlay.SetActive(true);
        playerRespawnOverlayWeapon.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        playerRespawnOverlay.SetActive(false);
        playerRespawnOverlayWeapon.SetActive(false);
        playerHealthBarGreenOverlay.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        playerHealthBarGreenOverlay.SetActive(true);
        playerRespawnOverlay.SetActive(true);
        playerRespawnOverlayWeapon.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        playerRespawnOverlay.SetActive(false);
        playerRespawnOverlayWeapon.SetActive(false);
        playerHealthBarGreenOverlay.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        playerHealthBarGreenOverlay.SetActive(true);
        playerRespawnOverlay.SetActive(true);
        playerRespawnOverlayWeapon.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        playerRespawnOverlay.SetActive(false);
        playerRespawnOverlayWeapon.SetActive(false);
        playerHealthBarGreenOverlay.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        playerHealthBarGreenOverlay.SetActive(true);
        playerRespawnOverlay.SetActive(true);
        playerRespawnOverlayWeapon.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        playerRespawnOverlay.SetActive(false);
        playerRespawnOverlayWeapon.SetActive(false);
        playerHealthBarGreenOverlay.SetActive(false);
    }

    IEnumerator respawnBoostCooldown()
    {
        boostScriptRef.grayOutButton();
        boostTimer.Begin(30);
        yield return new WaitForSeconds(30);
        boostScriptRef.activateBackButton();
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Enemy"))
        {
            GameObject collidedWith = col.gameObject;
            Enemy enemyScript = col.GetComponent<Enemy>();

            int tempHealth = motherShipHP;
            motherShipHP -= (int)(enemyScript.health / 2);
       
            enemyScript.TakeDamage(tempHealth * 2);
            if (collidedWith == null)
                Instantiate(deathEffectM, collidedWith.transform.position, Quaternion.identity);
         
            damageSoundM.Play(0);

            motherShipHealthBar.SetHealth(motherShipHP);
            motherShipHealthBarRedOverlay.SetHealth(motherShipHP);
            motherShipHealthBarGreenOverlay.SetHealth(motherShipHP);
            StartCoroutine(motherShipHealthBarBlink());
        }
    }

    IEnumerator motherShipHealthBarBlink()
    {
        motherGlow.SetActive(false);
        screenRedOverlay.SetActive(true);
        motherShipDmgOverlay.SetActive(true);
        healthBarRedOverlayObj.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        healthBarRedOverlayObj.SetActive(false);
        motherShipDmgOverlay.SetActive(false);
        screenRedOverlay.SetActive(false);
        motherGlow.SetActive(true);

    }

    IEnumerator motherShipHealthBarBlinkGreen()
    {
        motherGlow.SetActive(false);
        screenGreenOverlay.SetActive(true);
        motherShipGreenOverlay.SetActive(true);
        healthBarGreenOverlayObj.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        healthBarGreenOverlayObj.SetActive(false);
        motherShipGreenOverlay.SetActive(false);
        screenGreenOverlay.SetActive(false);
        motherGlow.SetActive(true);

    }

}
