using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawning : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public bool testMode = false;

    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject[] waveEnemyList;
        public Transform enemySpawnPoint;
        public int count;
        public float rate;
    }



    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;



    [SerializeField]
    private GameObject[] enemyReference;
    [SerializeField]
    private GameObject[] asteroidReference;
    [SerializeField]
    private GameObject[] bossReference;
    [SerializeField]
    private GameObject[] powerUpReference;

    public float difficulty;
    private bool survivalMode;
    public TextMeshProUGUI WaveInfoDisplay;

    private GameObject spawnedEnemy;
    private GameObject spawnedAsteroid;
    private GameObject spawnedBoss;
    private GameObject spawnedPowerUp;

    private CameraFollow camScript;

    public GameObject enemiesHolder;

    public GameObject AsteroidUI;
    public GameObject BossUI;
    public GameObject SurvivalUI;
    public GameObject WaveCompleteUI;
    public GameObject SupplyNotice;
    public GameObject SupplyText;
    public AudioSource AsteroidWarningSound;
    public AudioSource WaveCompleteSound;
    public AudioSource BossWarningSound;
    public AudioSource WaveStartSound;
    public AudioSource BGMusic;
    public AudioSource AltBGMusic;

    [SerializeField]
    private Transform leftPos, rightPos, botPos, topPos;
    [SerializeField]
    private Transform NWPos, NEPos, SEPos, SWPos;

    private int randomIndex;
    private int randomSide;
    private int randomOffset;

    private int randomIndexAsteroid;
    private int randomSideAsteroid;
    private int randomOffsetAsteroid;

    private int randomIndexBoss;
    private int randomSideBoss;
    private int randomOffsetBoss;

    private int randomIndexPowerUp;
    private int randomSidePowerUp;

    private Rigidbody2D powerUpBody;

    void Start()
    {
        survivalMode = false;
        waveCountdown = timeBetweenWaves;
        difficulty = 1f;

        camScript = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        if (camScript.motherShip)
            difficulty *= 1.25f;
        InvokeRepeating("incrementDifficulty",45f,45f);
        InvokeRepeating("spawnCratesForInvoke", 60f, 60f);
    }

    void spawnCratesForInvoke()
    {
        spawnCrates((int)(difficulty));
    }

    private void Update()
    {
        if (!survivalMode)
        {
            if (state == SpawnState.WAITING)
            {
                if (!EnemyIsAlive())
                {
                    WaveCompleted();
                }
                else
                {
                    return;
                }
            }

            if (waveCountdown <= 0)
            {
                if (state != SpawnState.SPAWNING)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
            else
            {
                waveCountdown -= Time.deltaTime;
            }
        }
    }

    void WaveCompleted()
    {
        

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1 || testMode)
        {
            nextWave = waves.Length + 1;
            survivalMode = true;
            Debug.Log("All Waves Complete! Survival mode is now ON. Good luck :)");
            WaveInfoDisplay.text = "Survival Mode";
            StartCoroutine(SurvivalStartEffect());
            StartCoroutine(SpawnEnemies(0,true));
            InvokeRepeating("callAsteroidWave", 60, 120);
            InvokeRepeating("callBossWave", 120, 120);
        }
        else
        {
            Debug.Log("Wave Completed!");
            WaveInfoDisplay.text = "Wave " + (nextWave+1).ToString() + " Completed!";
            WaveCompleteSound.Play();
            //StartCoroutine(WaveCompleteEffect());
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        WaveStartSound.Play();
        WaveInfoDisplay.text = "Wave " + (nextWave+1).ToString() + ": " + _wave.name.ToString();
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            int randomEnemyWaveIndex = Random.Range(0, _wave.waveEnemyList.Length);
            SpawnEnemy(_wave.waveEnemyList[randomEnemyWaveIndex], _wave.enemySpawnPoint);
            yield return new WaitForSeconds(5f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(GameObject _enemy, Transform pos)
    {
        //Spawn enemy
        Debug.Log("Spawning Enemy: " + _enemy.name);
        Instantiate(_enemy, pos.position + new Vector3(Random.Range(-15,15), Random.Range(-15, 15),0), pos.rotation);
    }

    void incrementDifficulty()
    {
        difficulty += 0.1f;
    }


    private void callAsteroidWave()
    {
        StartCoroutine(SpawnAsteroids((int)(25*difficulty), false));
    }

    private void callBossWave()
    {
        StartCoroutine(SpawnBossWave((int)(5 * difficulty), false));
    }

    private void spawnCrates(int amount)
    {
        StartCoroutine(PowerUpEffect());
        while (amount >= 0)
        {
            randomIndexPowerUp = Random.Range(0, powerUpReference.Length);
            randomSidePowerUp = Random.Range(0, 8);
            spawnedPowerUp = Instantiate(powerUpReference[randomIndexPowerUp]);
            powerUpBody = spawnedPowerUp.GetComponent<Rigidbody2D>();

            switch (randomSidePowerUp)
            {
                case 0:
                    {
                        powerUpBody.velocity = Vector2.right * 10;
                        spawnedPowerUp.transform.position = leftPos.position;
                        break;
                    }
                case 1:
                    {
                        powerUpBody.velocity = Vector2.left * 10;
                        spawnedPowerUp.transform.position = rightPos.position;
                        break;
                    }
                case 2:
                    {
                        powerUpBody.velocity = Vector2.down * 10;
                        spawnedPowerUp.transform.position = topPos.position;
                        break;
                    }
                case 3:
                    {
                        powerUpBody.velocity = Vector2.up * 10;
                        spawnedPowerUp.transform.position = botPos.position;
                        break;
                    }
                case 4:
                    {
                        powerUpBody.velocity = (Vector2.right + Vector2.down) * 10;
                        spawnedPowerUp.transform.position = NWPos.position;
                        break;
                    }
                case 5:
                    {
                        powerUpBody.velocity = (Vector2.left + Vector2.down) * 10;
                        spawnedPowerUp.transform.position = NEPos.position;
                        break;
                    }
                case 6:
                    {
                        powerUpBody.velocity = (Vector2.left + Vector2.up) * 10;
                        spawnedPowerUp.transform.position = SEPos.position;
                        break;
                    }
                case 7:
                    {
                        powerUpBody.velocity = (Vector2.right + Vector2.up) * 10;
                        spawnedPowerUp.transform.position = SWPos.position;
                        break;
                    }
            }
            amount--;
        }
    }
    IEnumerator PowerUpEffect()
    {
        SupplyNotice.SetActive(true);
        SupplyText.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        SupplyNotice.SetActive(false);
        SupplyText.SetActive(false);
    }

    IEnumerator SpawnEnemies(int amount, bool forever)
    {
        while (amount >= 0 || forever)
        {
            yield return new WaitForSeconds(Random.Range(3f / difficulty, 10f / difficulty));

            randomIndex = Random.Range(0, enemyReference.Length);
            randomSide = Random.Range(0, 8);
            randomOffset = Random.Range(-10, 10);
          
            spawnedEnemy = Instantiate(enemyReference[randomIndex]);
            spawnedEnemy.transform.parent = enemiesHolder.transform;

            switch (randomSide)
            {
                case 0: //left
                    {
                        spawnedEnemy.transform.position = leftPos.position + new Vector3(0, randomOffset, 0);
                        break;
                    }
                case 1: //right
                    {
                        spawnedEnemy.transform.position = rightPos.position + new Vector3(0, randomOffset, 0);
                        break;
                    }
                case 2: //bot
                    {
                        spawnedEnemy.transform.position = botPos.position + new Vector3(randomOffset, 0, 0);
                        break;
                    }
                case 3: //top
                    {
                        spawnedEnemy.transform.position = topPos.position + new Vector3(randomOffset, 0, 0);
                        break;
                    }
                case 4: //north-west
                    {
                        spawnedEnemy.transform.position = NWPos.position + new Vector3(0, randomOffset, 0);
                    }
                    break;
                case 5: //north-east
                    {
                        spawnedEnemy.transform.position = NEPos.position + new Vector3(0, randomOffset, 0);
                    }
                    break;
                case 6: //south-east
                    {
                        spawnedEnemy.transform.position = SEPos.position + new Vector3(0, randomOffset, 0);
                    }
                    break;
                case 7: //south-west
                    {
                        spawnedEnemy.transform.position = SWPos.position + new Vector3(0, randomOffset, 0);
                    }
                    break;
            }
            amount--;
        }
    }

    IEnumerator SpawnAsteroids(int amount, bool forever)
    {
        StartCoroutine(AsteroidWaveEffect());
        while (amount >= 0)
        {
            yield return new WaitForSeconds(Random.Range(3f / difficulty, 10f / difficulty));

            randomIndexAsteroid = Random.Range(0, asteroidReference.Length);
            randomSideAsteroid = Random.Range(0, 8);
            randomOffsetAsteroid = Random.Range(-10, 10);

            spawnedAsteroid = Instantiate(asteroidReference[randomIndex]);
            spawnedAsteroid.transform.parent = enemiesHolder.transform;

            switch (randomSide)
            {
                case 0: //left
                    {
                        spawnedAsteroid.transform.position = leftPos.position + new Vector3(0, randomOffset, 0);
                        break;
                    }
                case 1: //right
                    {
                        spawnedAsteroid.transform.position = rightPos.position + new Vector3(0, randomOffset, 0);
                        break;
                    }
                case 2: //bot
                    {
                        spawnedAsteroid.transform.position = botPos.position + new Vector3(randomOffset, 0, 0);
                        break;
                    }
                case 3: //top
                    {
                        spawnedAsteroid.transform.position = topPos.position + new Vector3(randomOffset, 0, 0);
                        break;
                    }
                case 4: //north-west
                    {
                        spawnedAsteroid.transform.position = NWPos.position + new Vector3(0, randomOffset, 0);
                    }
                    break;
                case 5: //north-east
                    {
                        spawnedAsteroid.transform.position = NEPos.position + new Vector3(0, randomOffset, 0);
                    }
                    break;
                case 6: //south-east
                    {
                        spawnedAsteroid.transform.position = SEPos.position + new Vector3(0, randomOffset, 0);
                    }
                    break;
                case 7: //south-west
                    {
                        spawnedAsteroid.transform.position = SWPos.position + new Vector3(0, randomOffset, 0);
                    }
                    break;
            }
            amount--;
        }

    }
    IEnumerator AsteroidWaveEffect()
    {
        AsteroidUI.SetActive(true);
        AsteroidWarningSound.Play(0);
        yield return new WaitForSeconds(2.5f);
        AsteroidUI.SetActive(false);
    }

    IEnumerator SpawnBossWave(int amount, bool forever)
    {
        StartCoroutine(BossWaveEffect());
        while (amount >= 0)
        {
            yield return new WaitForSeconds(Random.Range(10f / difficulty, 50f / difficulty));

            randomIndexBoss = Random.Range(0, bossReference.Length);
            randomSideBoss = Random.Range(0, 8);
            randomOffsetBoss = Random.Range(-10, 10);

            spawnedBoss = Instantiate(bossReference[randomIndex]);
            spawnedBoss.transform.parent = enemiesHolder.transform;

            switch (randomSide)
            {
                case 0: //left
                    {
                        spawnedBoss.transform.position = leftPos.position + new Vector3(0, randomOffset, 0);
                        break;
                    }
                case 1: //right
                    {
                        spawnedBoss.transform.position = rightPos.position + new Vector3(0, randomOffset, 0);
                        break;
                    }
                case 2: //bot
                    {
                        spawnedBoss.transform.position = botPos.position + new Vector3(randomOffset, 0, 0);
                        break;
                    }
                case 3: //top
                    {
                        spawnedBoss.transform.position = topPos.position + new Vector3(randomOffset, 0, 0);
                        break;
                    }
                case 4: //north-west
                    {
                        spawnedBoss.transform.position = NWPos.position + new Vector3(0, randomOffset, 0);
                    }
                    break;
                case 5: //north-east
                    {
                        spawnedBoss.transform.position = NEPos.position + new Vector3(0, randomOffset, 0);
                    }
                    break;
                case 6: //south-east
                    {
                        spawnedBoss.transform.position = SEPos.position + new Vector3(0, randomOffset, 0);
                    }
                    break;
                case 7: //south-west
                    {
                        spawnedBoss.transform.position = SWPos.position + new Vector3(0, randomOffset, 0);
                    }
                    break;
            }
            amount--;
        }

    }
    IEnumerator BossWaveEffect()
    {
        BossUI.SetActive(true);
        AsteroidWarningSound.Play(0);
        yield return new WaitForSeconds(2.5f);
        BossUI.SetActive(false);
    }
    IEnumerator SurvivalStartEffect()
    {
        BossWarningSound.Play(0);
        BGMusic.Pause();
        SurvivalUI.SetActive(true);
        yield return new WaitForSeconds(5f);
        SurvivalUI.SetActive(false);
        yield return new WaitForSeconds(7f);
        AltBGMusic.Play();
    }

    IEnumerator WaveCompleteEffect()
    {
        WaveCompleteUI.SetActive(true);
        WaveCompleteSound.Play(0);
        yield return new WaitForSeconds(2.5f);
        WaveCompleteUI.SetActive(false);
    }
}
