using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI itemCounterText;
    

    public void UpdateItemCounter(int count)
    {
        
        itemCounterText.text = "Collected: " + count.ToString();
        Debug.Log("HUD Updated: Items = " + count);
        if(count >= 5){
            Camera.main.backgroundColor = new Color(0.3f, 0f, 0f);
        }
    }
}
