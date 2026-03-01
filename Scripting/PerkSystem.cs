using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PerkSystem : MonoBehaviour
{
    public bool absoluteZero, singularityCore, entropyEngine;
    public bool roundSplit, adrenaline, hazardousTempo, feedbackLoop;
    public bool perk4, bloodlust, greedyMan, penetratingBeam, cardinalBoost, glassSkin, martyrSignal;
    public bool targetPainter, criticalUnstability, bioSynthesizer, deathAndTaxes, gatling, dualWield, slasher;
    public int commandNetworkCount;
    public float scarsOfWarBonus;
    
    private PlayerController player;
    private float lastPos;
    public float hazardousBonus;
    public float bloodlustTimer;
    public int shotCounter;

    void Start()
    {
        player = GetComponent<PlayerController>();
        lastPos = transform.position.magnitude;
    }

    void Update()
    {
        if (bioSynthesizer)
            player.health = Mathf.Min(player.health + (player.maxHealth * 0.01f * Time.deltaTime), player.maxHealth);

        if (bloodlustTimer > 0)
            bloodlustTimer -= Time.deltaTime;

        if (hazardousTempo)
        {
            float dist = Vector3.Distance(transform.position, transform.position); // Placeholder for movement delta
            float currentPos = transform.position.magnitude;
            hazardousBonus += Mathf.Abs(currentPos - lastPos) * 0.01f;
            lastPos = currentPos;
        }
    }

    public void ActivateRandomPerk(string rarity)
    {
        if (rarity == "Legendary")
        {
            int r = Random.Range(0, 3);
            if (r == 0) absoluteZero = true;
            else if (r == 1) singularityCore = true;
            else entropyEngine = true;
        }
        else if (rarity == "Epic")
        {
            int r = Random.Range(0, 5);
            if (r == 0) roundSplit = true;
            else if (r == 1) adrenaline = true;
            else if (r == 2) hazardousTempo = true;
            else if (r == 3) feedbackLoop = true;
            else scarsOfWarBonus += 0; 
        }
        else if (rarity == "Rare")
        {
            int r = Random.Range(0, 7);
            if (r == 0) perk4 = true;
            else if (r == 1) bloodlust = true;
            else if (r == 2) greedyMan = true;
            else if (r == 3) penetratingBeam = true;
            else if (r == 4) cardinalBoost = true;
            else if (r == 5) glassSkin = true;
            else martyrSignal = true;
        }
        else if (rarity == "Common")
        {
            int r = Random.Range(0, 8);
            if (r == 0) commandNetworkCount++;
            else if (r == 1) targetPainter = true;
            else if (r == 2) criticalUnstability = true;
            else if (r == 3) bioSynthesizer = true;
            else if (r == 4) deathAndTaxes = true;
            else if (r == 5) gatling = true;
            else if (r == 6) dualWield = true;
            else ApplySlasher();
        }
    }

    void ApplySlasher()
    {
        slasher = true;
        player.maxHealth *= 1.5f;
        player.health *= 1.5f;
    }
    public float GetTotalDamageMultiplier()
    {
        float mult = 1f;
        if (greedyMan && GetUraniumCount() > 100) mult += 1.0f;
        if (glassSkin) mult += 1.0f;
        if (bloodlust && bloodlustTimer > 0) mult += 0.1f;
        if (hazardousTempo) { mult += hazardousBonus; hazardousBonus = 0; }
        mult += (commandNetworkCount * 0.01f);
        return mult;
    }

    public float GetFireRateMultiplier()
    {
        float mult = 1f;
        if (gatling) mult *= 0.5f;
        if (slasher) mult *= 0.5f;
        if (adrenaline) mult *= (player.health / player.maxHealth);
        return mult;
    }

    public void OnPlayerHit()
    {
        if (scarsOfWarBonus >= 0) scarsOfWarBonus += 1f;
        if (glassSkin) player.health -= 10f; // Placeholder for extra damage taken
    }

    public void OnEnemyKilled()
    {
        if (bloodlust) bloodlustTimer = 3f;
    }

    private int GetUraniumCount()
    {
        // Placeholder: Hook this to your actual Uranium variable
        return 0; 
    }

    public void ApplyShotEffects(Projectile p)
    {
        shotCounter++;
        if (perk4 && shotCounter % 4 == 0) p.damage *= 3f;
        if (absoluteZero && shotCounter % 10 == 0) p.isFreezing = true;
        if (entropyEngine) p.damage *= Random.Range(0f, 2f);
        if (penetratingBeam) p.pierceCount = 99;
        if (singularityCore) p.isGravity = true;
    }
}
