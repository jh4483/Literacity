using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraShake : MonoBehaviour
{
    public AnimationCurve curve;
    //public Canvas canvas;
    //public Image background;
    public float duration = 1f;
    public Vector3 startPos;
    public float time = 0f;
    public float strengthMultiplier = 1f;
    public bool isShaking = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
        //canvas.renderMode = RenderMode.WorldSpace;

        while(time <= duration)
        {
            time += Time.deltaTime;
            float strength = curve.Evaluate(time / duration) * strengthMultiplier;
            transform.localPosition = startPos + Random.insideUnitSphere * strength;
            yield return null;
        }

        transform.localPosition = startPos;
        //canvas.renderMode = RenderMode.ScreenSpaceCamera;
        isShaking = false;
    }
}
