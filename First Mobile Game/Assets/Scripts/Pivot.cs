using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pivot : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
    }
}
