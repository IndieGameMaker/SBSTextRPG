
using TextRPG.Models;

namespace TextRPG.Core;

public class GameSaveData
{
    // Player Stat 데이터
    public PlayerData Player { get; set; }
    // 인벤토리 데이터
    public List<ItemData> Inventory {get; set;}
}

public class PlayerData
{
    // 기본정보
    public string Name  { get; set; }
    public string Job { get; set; }
    
    // 스텟정보
    public int Level { get; set; }
    public int CurrentHp { get; set; }
    public int MaxHp { get; set; }
    public int CurrentMp { get; set; }
    public int MaxMp { get; set; }
    public int AttackPower  { get; set; }
    public int Defense { get; set; }
    public int Gold { get; set; }
    
    // 장착 아이템
    public string? WeaponName  { get; set; }
    public string? ArmorName  { get; set; }
}

public class ItemData
{
    public string ItemType { get; set; }
    public string Name { get; set; }
    public string Slot { get; set; }
}
