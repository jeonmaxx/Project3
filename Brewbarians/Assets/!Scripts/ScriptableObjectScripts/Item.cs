using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public GameObject obj;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;
    public string itemName;

    [Header("ItemType Seed")]
    public Seed seed;
}

public enum ItemType { BuildingBlock, Tool, Seed, HarvestProd, Drink }

public enum ActionType { None, Dig, Water, Plant, Harvest }