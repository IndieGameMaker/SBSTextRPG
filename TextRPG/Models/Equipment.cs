namespace TextRPG.Models;

public class Equipment : Item
{
    #region 프로퍼티
    // 장착슬롯 타입


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

    public EquipmentSlot Slot { get; private set; }
    // 공격력 보너스
    public int AttackBonus  { get; private set; }
    // 방어력 보너스
    public int DefenseBonus  { get; private set; }
    #endregion

    public override bool Use(Player player)
    {
        // 장비 착용 로직 구현
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
                                        attackBonus: 15);
            default:
                return null;
        }
    }
    // 방어구 생성 메서드
    #endregion
}
