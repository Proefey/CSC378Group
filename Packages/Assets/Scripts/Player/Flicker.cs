using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flicker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] UnityEngine.Rendering.Universal.Light2D flashlight;
    void Start()
    {
        flashlight = flashlight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        for (; ; ) //this is while(true)
        {
            float randomIntensity = Random.Range(1.5f, 3.5f);
            flashlight.intensity = randomIntensity;


            float randomTime = Random.Range(0f, 0.1f);
            yield return new WaitForSeconds(randomTime);
        }
    }
}
