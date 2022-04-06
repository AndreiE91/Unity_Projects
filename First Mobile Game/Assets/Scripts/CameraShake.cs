using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public CameraFollow camFollow;

    [HideInInspector]
    public bool shaking = false;

    public IEnumerator Shake(float duration, float magnitude)
    {
        shaking = true;

        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = camFollow.tempPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
        shaking = false;


    }

}
