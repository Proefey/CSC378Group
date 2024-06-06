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
    }
}
