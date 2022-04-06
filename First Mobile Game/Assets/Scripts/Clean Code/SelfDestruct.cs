using System.Collections;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float duration;

    void Start()
    {
        Destroy(gameObject, duration);
    }
}
