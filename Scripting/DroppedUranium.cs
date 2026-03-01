using UnityEngine;

public class UraniumDrop : MonoBehaviour
{
    public int amount;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CurrencyManager.Uranium += amount;
            Destroy(gameObject);
        }
    }
}