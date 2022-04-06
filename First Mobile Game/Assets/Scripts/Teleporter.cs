using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    private Collider2D rb;
    public AudioSource levelEnd;
    public GameObject levelEndAnimation;
    public PlayerControls playerControls;

    void Start()
    {
        rb = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerControls.lockMovement = true;
            StartCoroutine(endLevel());
        }
    }

    IEnumerator endLevel()
    {
        levelEnd.Play(0);
        levelEndAnimation.SetActive(true);
        ImportantVariables.totalFunds += ImportantVariables.tempFunds;
        ImportantVariables.tempFunds = 0;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("LevelEnd");

    }
}
