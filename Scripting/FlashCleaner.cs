using UnityEngine;

public class FlashCleaner : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.2f);
    }
}