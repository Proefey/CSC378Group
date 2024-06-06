using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private int itemCount = 0;
    public HUDController hudController; // Ensure this is public or [SerializeField]
    public CameraBehav camerabehav;

    void Start()
    {
        if (hudController != null)
        {
            Debug.Log("HUDController assigned successfully");
        }
        else
        {
            Debug.LogError("HUDController not assigned in the Inspector!");
        }
    }

    public void CollectItem()
    {
        itemCount++;
        if (hudController != null)
        {
            hudController.UpdateItemCounter(itemCount);
            if(itemCount >= 4) camerabehav.setbegin();
            if(itemCount >= 5) SceneManager.LoadScene(0);
        }
        else
        {
            Debug.LogError("HUDController is not assigned in the Inspector!");
        }
        Debug.Log("Item collected! Total items: " + itemCount);
    }
}
