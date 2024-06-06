using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBehav : MonoBehaviour
{
    // Start is called before the first frame update
    float timer = 0.0f;
    float shakeTimer= 2.0f;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    bool begin = true;
    void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update(){
        if(begin) return;
        if(shakeTimer > 0){
            shakeTimer -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 2.0f;
        }
        else{
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0.0f;
            shakeTimer = 2.0f + Random.Range(-0.5f, 0.5f);
            timer = 0.0f + Random.Range(-1.0f, 1.0f);
        }
    }

    public void setbegin(){
        Debug.Log("Shaking Beginning");
        begin = false;
    }
    
}
