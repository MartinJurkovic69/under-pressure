using UnityEngine;

public class PerkItem : MonoBehaviour
{
    public string assignedRarity;

    public void Setup(string rarity)
    {
        assignedRarity = rarity;
        Renderer ren = GetComponent<Renderer>();
        if (ren == null) return;

        if (rarity == "Legendary") ren.material.color = new Color(1f, 0.84f, 0f); 
        else if (rarity == "Epic") ren.material.color = new Color(0.5f, 0f, 0.5f);
        else if (rarity == "Rare") ren.material.color = Color.blue;
        else ren.material.color = Color.white;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PerkSystem system = other.GetComponent<PerkSystem>();
            if (system != null)
            {
                system.ActivateRandomPerk(assignedRarity);
                Destroy(gameObject);
            }
        }
    }
}