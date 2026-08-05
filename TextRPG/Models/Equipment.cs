namespace TextRPG.Models;

public class Equipment : Item
{
    #region 프로퍼티
    // 장착슬롯 타입
    public EquipmentSlot Slot { get; private set; }
    // 공격력 보너스
    public int AttackBonus  { get; private set; }
    // 방어력 보너스
    public int DefenseBonus  { get; private set; }
    // 장착여부
    public bool IsEquipped { get; private set; } = false;
    #endregion

    #region 생성자
    public Equipment(
        string name, 
        string description, 
        int price, 
        EquipmentSlot slot, 
        int attackBonus = 0, 
        int defenseBonus = 0) : base(name, description, price, slot == EquipmentSlot.Weapon ? ItemType.Weapon : ItemType.Armor)
    {
        Slot = slot;
        AttackBonus = attackBonus;
        DefenseBonus = defenseBonus;
    }
    #endregion

    #region 장착 상태변경 메서드
    public void Equip() => IsEquipped = true;
    public void Unequip() => IsEquipped = false;
    #endregion

    #region 

    #endregion
    
    public override bool Use(Player player)
    {
        // 장비 착용 로직 구현
        player.EquipItem(this);
        return true;
    }

    #region 장착아이템 생성 매서드
    // 무기 생성 메서드
    public static Equipment CreateWeapon(WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.WoodSword:
                return new Equipment( WeaponType.WoodSword.ToString(), 
                                        WeaponGrade.Basic.ToString(), 
                                        100, 
                                        EquipmentSlot.Weapon, 
                                        5);
            
            case WeaponType.IronSword:
                return new Equipment( WeaponType.IronSword.ToString(), 
                                        WeaponGrade.Standard.ToString(), 
                                        100, 
                                        EquipmentSlot.Weapon, 
                                        attackBonus: 15);
            
            case WeaponType.LegendarySword:
                return new Equipment( WeaponType.LegendarySword.ToString(), 
                                        WeaponGrade.Elite.ToString(), 
                                        100, 
                                        EquipmentSlot.Weapon, 
                                        attackBonus: 25);
            default:
                return null;
        }
    }
    // 방어구 생성 메서드
    public static Equipment CreateAmor(string armorType)
    {
        switch (armorType)
        {
            case "천갑옷":
                return new Equipment("천갑옷", "기본 방어구", 50, EquipmentSlot.Armor, 0, 0);
            
            case "철갑옷":
                return new Equipment("철갑옷", "일반 방어구", 200, EquipmentSlot.Armor, 0, 20);
            
            case "전설갑옷":
                return new Equipment("전설갑옷", "최고급 방어구", 1000, EquipmentSlot.Armor, 20, 50);
            default:
                return null;
        }
    }
    #endregion
}