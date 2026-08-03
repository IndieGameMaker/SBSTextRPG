namespace TextRPG.Models;

public class Equipment : Item
{
    #region 프로퍼티
    // 장착슬롯 타입


    public Equipment(
        string name, 
        string description, 
        int price, 
        ItemType type, 
        EquipmentSlot slot, 
        int attackBonus, 
        int defenseBonus) : base(name, description, price, type)
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
    public static Equipment CreateWeapon()
    // 방어구 생성 메서드
    #endregion
}
