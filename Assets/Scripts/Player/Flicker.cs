using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flicker : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] UnityEngine.Rendering.Universal.Light2D flashlight;
    private Transform enemy;
    private GameObject[] collectibles;
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        flashlight = flashlight.GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        for (; ; ) //this is while(true)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.position);
            float randomIntensity = Random.Range(1.5f, 3.5f);
            float minCollectibleDistance = 999.9f;
            int minindex = 0;
            collectibles = GameObject.FindGameObjectsWithTag("Collectible");
            for (int i = 0; i < collectibles.Length; i++){
                float dist = Vector3.Distance(transform.position, collectibles[i].transform.position);
                if (minCollectibleDistance > dist){
                    minindex = i;
                    minCollectibleDistance = dist;
                }
            }
            if(distanceToEnemy <= 7.5) randomIntensity = Random.Range(0f, 3.5f);
            if(IsPlayerLookingAtCollectible(collectibles[minindex].transform) && minCollectibleDistance < 20) randomIntensity = Random.Range(0f, 3.5f);
            flashlight.intensity = randomIntensity;


            float randomTime = Random.Range(0f, 0.1f);
            yield return new WaitForSeconds(randomTime);
        }
    }

    private bool IsPlayerLookingAtCollectible(Transform t)
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 collectibleScreenPos = Camera.main.WorldToScreenPoint(t.position);

        Vector2 playerToMouse = (mousePos - playerScreenPos).normalized;
        Vector2 playerToCollectible = (collectibleScreenPos - playerScreenPos).normalized;

        float angle = Vector2.Angle(playerToMouse, playerToCollectible);

        // Assume the player is looking at the enemy if the angle is less than a certain threshold
        float lookAngleThreshold = 30f;
        return angle < lookAngleThreshold;
    }
}
