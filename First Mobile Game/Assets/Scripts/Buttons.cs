using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public GameObject pausePopUp;
    public GameObject muteOffOverlay;
    public GameObject cam;
    public AudioSource buttonTap;
    public GameObject boostButton;
    public GameObject brakeIcon;
    public AudioSource brakeToggle;
    public bool isMute = false;
    public bool brake = false;
    public Player player;
    public bool pointersOff;
    public int pointersOffint = 0;
    public GameObject pointerOffOverlay;

    void Awake()
    {
        pointersOffint = PlayerPrefs.GetInt("pointersOff");
        if (pointersOffint == 0)
            pointersOff = false;
        else
            pointersOff = true;
        if (pointersOff && SceneManager.GetActiveScene().name == "MainMenu")
        {
            pointerOffOverlay.SetActive(true); ;
        }
    }

    void Start()
    {
        if (AudioListener.volume == 0)
        {
            muteOffOverlay.SetActive(true);
            isMute = !isMute;
        }
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        pausePopUp.SetActive(true);
        boostButton.SetActive(false);
        buttonTap.Play(0);
    }

    public void resumeGame()
    {

        Time.timeScale = 1;
        boostButton.SetActive(true);
        buttonTap.Play(0);
        pausePopUp.SetActive(false);
    }

    public void restartGame()
    {
        SceneManager.LoadScene("Gameplay");
        Time.timeScale = 1;
        buttonTap.Play(0);
    }

    public void exitGame()
    {

        //Debug.Log("Game closed");
        Application.Quit();
        buttonTap.Play(0);

    }

    public void interPlay()
    {

        cam.transform.position = new Vector3(-60, 0, -10);
        buttonTap.Play(0);
    }

    public void backToMenu()
    {

        cam.transform.position = new Vector3(0, 0, -10);
        buttonTap.Play(0);
    }

    public void playGame()
    {
        SceneManager.LoadScene("Gameplay");
        buttonTap.Play(0);
    }

    public void menuFromGplay()
    {

        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        buttonTap.Play(0);
    }

    public void Mute()
    {
        isMute = !isMute;
        AudioListener.volume = isMute ? 0 : 1;
        muteOffOverlay.SetActive(isMute);
        buttonTap.Play(0);
    }

    public void togglePointers()
    {
        pointersOff = !pointersOff;
        pointerOffOverlay.SetActive(pointersOff);

        if (pointersOff)
            pointersOffint = 1;
        else
            pointersOffint = 0;

        PlayerPrefs.SetInt("pointersOff", pointersOffint);
        PlayerPrefs.Save();

        buttonTap.Play(0);
        //Debug.Log(pointersOff);
    }

    public void brakeButton()
    {
        if (!player.isDead)
        {
            brake = !brake;
            brakeIcon.SetActive(brake);
            brakeToggle.Play(0);
        }
    }

}
