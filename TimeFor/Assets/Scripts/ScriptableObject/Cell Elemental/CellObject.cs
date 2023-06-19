using UnityEngine;

[CreateAssetMenu(fileName = "Cell", menuName = "CellElemental")]
public class CellObject : Item
{
    [Header("Тип стихии")]
    public ElementObject element;

    [Header("Тип стихии")]
    public CellObject nextCell;

    [Header("Тип стихии")]
    public int levelCell;

    public enum CellType { Первая, Вторая, Третья, }
    [Header("Номер ячейки")]
    public CellType cellType;

    [Header("Тип стихии")]
    public bool isOpenCell;

    [Header("Тип стихии")]
    public GameObject totemPrefab;

    [Header("Тип стихии")]
    public float damageTotem;

    [Header("Тип стихии")]
    public float timeTotem;

    public void Upgrade()
    {
        if(cellType == CellType.Первая)
        {
            UpgradeDamage();
        }
        else if(cellType == CellType.Вторая)
        {
            UpgradePercent();
        }
        else if(cellType == CellType.Третья)
        {
            UpgradeTotem();
        }
    }

    public void UpgradeDamage()
    {
        levelCell++;
        element.baseDamage += 5;

        if(levelCell >= 3)
        {
            nextCell.isOpenCell = true; 
        }
    }

    public void UpgradePercent()
    {
        levelCell++;
        element.basePersent += 0.05f;

        if (levelCell >= 3)
        {
            nextCell.isOpenCell = true;
        }
    }

    public void UpgradeTotem()
    {
        levelCell++;
        damageTotem += 5;
        timeTotem += 1;
    }
}
