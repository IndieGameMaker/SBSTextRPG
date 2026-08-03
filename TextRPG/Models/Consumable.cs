namespace TextRPG.Models;

public class Consumable : Item
{
    #region 프로퍼티
    public int HpAmount { get; private set; }
    // MP 회복량
    public int MpAmount { get; private set; }
    #endregion
    
    // HP 회복량
    public Consumable(
        string name, 
        string description, 
        int price, 
        ItemType type, 
        int hpAmount = 0, 
        int mpAmount = 0) 
        : base(name, description, price, type)
    {
        HpAmount = hpAmount;
        MpAmount = mpAmount;
    }

    #region 메서드

    public override bool Use(Player player)
    {
        // 플레이어의 HP/MP를 회복하는 로직
        bool isUsed = false;

        if (HpAmount > 0)
        {
            int healedHp = player.HealHp(HpAmount);
            if (healedHp > 0)
            {
                Console.WriteLine($"{Name} 아이템 사용! HP +{healedHp} 회복됨!");
                isUsed = true;
            }
            else
            {
                Console.WriteLine("HP가 이미 최대치 입니다.");
            }
        }
        
        // MP 회복
        if (MpAmount > 0)
        {
            int healedMP = player.HealMp(MpAmount);
            if (healedMP > 0)
            {
                Console.WriteLine($"{Name} 아이템 사용! MP +{healedMP} 회복됨!");
                isUsed = true;
            }
            else
            {
                Console.WriteLine("MP가 이미 최대치 입니다.");
            }
        }
        
        return isUsed;
    }
    #endregion
    
    
}
