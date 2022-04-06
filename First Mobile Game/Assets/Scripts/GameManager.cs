using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject pauseButton;
    public GameObject player;
    public Player playerScript;
    private Buttons buttonScript;

    

    public AudioSource barrierDestroy;
    public AudioSource gameOverSound;
    public AudioSource BGMusic;
    public AudioSource gameBGMusic;
    public AudioSource gameBGAltMusic;
    public Text finalScore;
    public Text cashEarned;
    public ScoreCount scoreReference;
    public Funds fundScript;
    public GameObject boostButton;
    public GameObject boostOverlay;
    public GameObject brakeButton;
    public GameObject boardShip;
    public GameObject boardShipVisual;
    public GameObject teleporter;


    public bool hasDied = false;
    [HideInInspector]
    public bool gameEnded = false;
    public Boost boostScript;

    void Start()
    {
        buttonScript = GameObject.Find("UIController").GetComponent<Buttons>();
        InvokeRepeating("checkIfDead", 0.25f, 0.25f);
        InvokeRepeating("SaveFunds",20, 20);
    }

    void checkIfDead()
    {
        if (hasDied)
        {
            boostScript.overlay.SetActive(false);
            boostScript.isBoosted = false;
            boostScript.StopAllCoroutines();
            hasDied = false;
        }

    }

    public void EndGame()
    {
        gameEnded = true;
        finalScore.text = scoreReference.score.ToString();
        Invoke("gameOver", 2);
        
    }

    void SaveFunds()
    {
        ImportantVariables.totalFunds += ImportantVariables.tempFunds;
        //Debug.Log(ImportantVariables.currentLevelFunds);
        //Debug.Log(ImportantVariables.totalFunds);
    }

    public void EndLevel1()
    {
        //Debug.Log("Level 1 Completed!");
        if (boardShip != null && boardShipVisual != null)
        {
            boardShip.SetActive(true);
            boardShipVisual.SetActive(true);
        }

    }
    public void EndLevel2()
    {
        //Debug.Log("Level 2 Completed!");
        teleporter.SetActive(true);

    }



    void gameOver()
    {
        SaveFunds();
        cashEarned.text = "$ " + (fundScript.funds).ToString();
        player.SetActive(false);
        pauseButton.SetActive(false);
        boostButton.SetActive(false);
        boostOverlay.SetActive(false);
        brakeButton.SetActive(false);
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        gameOverSound.Play(0);
        gameBGMusic.Stop();
        gameBGAltMusic.Stop();
        BGMusic.Play(0);
        
    }

}
