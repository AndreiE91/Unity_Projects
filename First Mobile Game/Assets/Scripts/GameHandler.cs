using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    /*
    public static GameHandler Instance { get; private set; }

    public AudioSource menuMusic;
    public AudioSource lvl1Music;
    public AudioSource lvl2Music;

    public Scene currentScene;
    public Scene previousScene;

    public bool triggeredSceneChange = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        previousScene = currentScene;
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "MainMenu" || currentScene.name == "LevelEnd")
        {
            if (previousScene.name != "LevelEnd")
            {
                menuMusic.Play();
                lvl1Music.Stop();
                lvl2Music.Stop();
            }
        }
        if (currentScene.name == "Gameplay")
        {
            lvl1Music.Play();
            lvl2Music.Stop();
            menuMusic.Stop();
        }
        if (currentScene.name == "GameplayAlt")
        {
            lvl2Music.Play();
            lvl1Music.Stop();
            menuMusic.Stop();
        }
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        previousScene = currentScene;
        currentScene = SceneManager.GetActiveScene();
        Debug.Log(previousScene.name);
        if (currentScene.name == "MainMenu" || currentScene.name == "LevelEnd")
        {
            if (previousScene.name != "LevelEnd")
            {
                menuMusic.Play();
                lvl1Music.Stop();
                lvl2Music.Stop();
            }
        }
        if (currentScene.name == "Gameplay")
        {
            lvl1Music.Play();
            lvl2Music.Stop();
            menuMusic.Stop();
        }
        if (currentScene.name == "GameplayAlt")
        {
            lvl2Music.Play();
            lvl1Music.Stop();
            menuMusic.Stop();
        }
    }

    private void Update()
    {
        if (triggeredSceneChange)
        {
            StartCoroutine(Wait(0.5f));
            triggeredSceneChange = false;
        }
    }
    */
}


