using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = 1f;
    public Vector3 startPos;
    public float time = 0f;
    public bool isShaking = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(isShaking)
            {
                return;
            }
            StartCoroutine(ShakeCamera());
        }
    }

    public IEnumerator ShakeCamera()
    {
        isShaking = true;
        startPos = transform.localPosition;
        time = 0f;

        while(time <= duration)
        {
            time += Time.deltaTime;
            float strength = curve.Evaluate(time / duration);
            transform.localPosition = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.localPosition = startPos;
        isShaking = false;
    }
}
