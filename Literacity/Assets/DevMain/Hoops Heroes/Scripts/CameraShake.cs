using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = 1f;
    public Vector3 startPos;
    public float time = 0f;
    public float strengthMultiplier = 1f;
    public bool isShaking = false;
    public CameraShakeData testing;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(isShaking)
            {
                return;
            }
            StartCoroutine(ShakeCamera(testing));
        }
    }

    public IEnumerator ShakeCamera(CameraShakeData csdata)
    {
        isShaking = true;
        SetData(csdata);
        startPos = transform.localPosition;
        time = 0f;

        while(time <= duration)
        {
            time += Time.deltaTime;
            float strength = curve.Evaluate(time / duration) * strengthMultiplier;
            transform.localPosition = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.localPosition = startPos;
        isShaking = false;
    }

    public void SetData(CameraShakeData data)
    {
        duration = data.duration;
        strengthMultiplier = data.strengthMultiplier;
        curve = data.curve;
    }
}
