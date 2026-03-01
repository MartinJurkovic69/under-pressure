using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;
    public GameObject[] towerPrefabs;
    public int[] towerCosts;

    public GameObject buildMenuUI;
    public GameObject upgradeMenuUI;

    void Awake() => Instance = this;

    public void OpenBuildMenu()
    {
        buildMenuUI.SetActive(true);
        upgradeMenuUI.SetActive(false);
    }

    public void SelectTower(int index)
    {
        if (CurrencyManager.Uranium >= towerCosts[index])
        {
            CurrencyManager.Uranium -= towerCosts[index];
            Instantiate(towerPrefabs[index], Vector3.zero, Quaternion.identity);
            buildMenuUI.SetActive(false);
        }
    }
}