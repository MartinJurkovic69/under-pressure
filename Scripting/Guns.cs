using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Weapons/Energy Gun")]
public class Gun : ScriptableObject
{
    [Header("Visuals")]
    public string weaponName;        
    public Sprite upperBodySprite;   // Used by PlayerController
    public Sprite weaponSprite;      // Used by WeaponVisuals (Fixes CS1061)
    public int animID;               

    [Header("Projectile Settings")]
    public GameObject bulletPrefab; 
    public float fireRate = 0.2f;
    public float baseDamage = 10f;
    public int baseProjectiles = 1;
    public float baseSpread = 0f;
    public float range = 5f; 

    [Header("Effects")]
    public AudioClip shootSound;
    public GameObject muzzleFlashPrefab;

    [System.NonSerialized] 
    private float nextFireTime;

    private void OnEnable() => nextFireTime = 0f;

    public bool Shoot(Transform firePoint, float playerDamageMult, GameObject shooter)
    {
        if (Time.time < nextFireTime) return false;

        PerkSystem perks = shooter.GetComponent<PerkSystem>();
        float currentFireInterval = (perks != null) ? (fireRate / perks.GetFireRateMultiplier()) : fireRate;
        nextFireTime = Time.time + currentFireInterval;

        for (int i = 0; i < baseProjectiles; i++)
        {
            Quaternion spreadRotation = firePoint.rotation * Quaternion.Euler(0, Random.Range(-baseSpread, baseSpread), 0);
            
            if (bulletPrefab != null)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, spreadRotation);
                Projectile p = bullet.GetComponent<Projectile>();
                if (p != null)
                {
                    p.damage = baseDamage * playerDamageMult;
                    p.shooter = shooter;
                    p.maxLifetime = range; 
                }
            }
        }

        if (muzzleFlashPrefab != null)
            Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation);

        return true;
    }
}