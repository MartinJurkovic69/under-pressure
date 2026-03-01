using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static int Uranium;
    public float passiveRate = 1f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            Uranium += (int)passiveRate;
            timer = 0;
        }
    }

    public static int GetUraniumAmount()
    {
        return Uranium;
    }
}