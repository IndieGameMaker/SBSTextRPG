using TextRPG.Utils;

namespace TextRPG.Models;

public class Player : Character
{
    #region 프로퍼티
    // 직업
    public JobType Job { get; private set; }
    // 골드
    public int Gold { get; set; }
    
    // 현재 장착 무기
    public Equipment? EquipmentWeapon { get; private set; }
    // 현재 장착 방어구
    public Equipment? EquipmentArmor { get; private set; }
    #endregion
    
    #region 생성자
    public Player(string name, JobType job) : base(
        name, 
        maxHp:GetInitHp(job), 
        maxMp:GetInitMp(job), 
        attackPower:GetInitAttack(job),
        defense:GetInitDefense(job),
        level:1)
    {
        Job = job;
        Gold = 1000;
    }
    #endregion
    
    #region 직업별 초기 스텟 로딩

    private static int GetInitHp(JobType job)
    {
        switch (job)
        {
            case JobType.Warrior: return 150;
            case JobType.Archer: return 100;
            case JobType.Wizard: return 80;
            default: return 100;
        }
    }

    private static int GetInitMp(JobType job)
    {
        return job switch
        {
            JobType.Warrior => 30,
            JobType.Archer => 50,
            JobType.Wizard => 100,
            _ => 20
        };
    }

    private static int GetInitAttack(JobType job) =>
        job switch
        {
            JobType.Warrior => 50,
            JobType.Archer => 80,
            JobType.Wizard => 30,
            _ => 20
        };
    
    private static int GetInitDefense(JobType job) =>
        job switch
        {
            JobType.Warrior => 20,
            JobType.Archer => 10,
            JobType.Wizard => 0,
            _ => 0
        };

    #endregion

    #region 메소드
    // 플레이어 정보 출력(오버라이드)
    public override void DisplayInfo()
    {
        // base.DisplayInfo();
        Console.Clear();
        Console.WriteLine($"==== {Name} 정보 ====");
        Console.WriteLine($"레벨: {Level}");
        Console.WriteLine($"체력: {CurrentHp}/{MaxHp}");
        Console.WriteLine($"마나: {CurrentMp}/{MaxMp}");

        int attackBonus = EquipmentWeapon != null ? EquipmentWeapon.AttackBonus : 0;
        int defenseBonus = EquipmentArmor != null ? EquipmentArmor.DefenseBonus : 0;
        
        Console.WriteLine($"공격력: {AttackPower} (+{attackBonus})");
        Console.WriteLine($"방어력: {Defense} (+{defenseBonus})");
        Console.WriteLine($"골드: {Gold}");
        Console.WriteLine("===================");
        
        // 장착아이템 목록 Weapon / Armor
        if (EquipmentWeapon != null || EquipmentArmor != null)
        {
            Console.WriteLine("장착중인 장비 목록");
            if (EquipmentWeapon != null)
            {
                Console.WriteLine($"무기: {EquipmentWeapon.Name}");
            }
            if (EquipmentArmor != null)
            {
                Console.WriteLine($"방어구: {EquipmentArmor.Name}");
            }
        }
    }

    public override int Attack(Character target)
    {
        // 장착무기 또는 방어구에 따른 추가 데미지 계산
        // null 병합 연산자(Null-coalescing operator)
        int attackDamage = AttackPower;
        attackDamage += EquipmentWeapon?.AttackBonus ?? 0;

        return target.TakeDamage(attackDamage);
    }

    // 스킬 공격 (MP 소모) : Player 전용 메서드
    public int SkillAttack(Character target)
    {
        int mpCost = 15;
        
        // 스킬 공격 = 기본공격 * 1.5 데미지
        int totalDamage = AttackPower;
        totalDamage += EquipmentWeapon?.AttackBonus ?? 0;
        totalDamage = (int)(totalDamage * 1.5f);
        // MP 소모
        CurrentMp -= mpCost;
        
        // 데미지 전달
        return target.TakeDamage(totalDamage);
    }
    
    // 골드 획득 메서드
    public void GainGold(int amount)
    {
        Gold += amount;
        Console.WriteLine($"골드 +{amount} 획득! 현재 골드: {Gold}");
    }
    
    // 장비 착용
    public void EquipItem(Equipment newEquipment)
    {
        // 이미 장착중인 아이템 재선택 방지
        if (newEquipment.IsEquipped)
        {
            Console.WriteLine($"{newEquipment.Name}은(는) 이미 장착중입니다.");
            ConsoleUI.PressAnyKey();
            return;
        }
        
        Equipment? prevEquipment = null;

        switch (newEquipment.Slot)
        {
            case EquipmentSlot.Weapon:
                prevEquipment = EquipmentWeapon;
                EquipmentWeapon = newEquipment;
                break;
            case EquipmentSlot.Armor:
                prevEquipment = EquipmentArmor;
                EquipmentArmor = newEquipment;
                break;
        }
        
        // 이전 장비 해제 메시지
        if (prevEquipment != null)
        {
            prevEquipment.Unequip(); // 장착 해제
            Console.WriteLine($"{prevEquipment.Name} 장착 해제");
        }
        newEquipment.Equip(); // 장착
        Console.WriteLine($"{newEquipment.Name} 장착 완료");
    }
    // 장비 해제
    public Equipment? UnEquipItem(EquipmentSlot slot)
    {
        // 해제할 아이템을 저장할 변수
        Equipment? equipment = null;

        switch (slot)
        {
            case EquipmentSlot.Weapon:
                equipment = EquipmentWeapon;
                EquipmentWeapon = null;
                break;
            case EquipmentSlot.Armor:
                equipment = EquipmentArmor;
                EquipmentArmor = null;
                break;
        }

        if (equipment != null)
        {
            equipment.Unequip();
            Console.WriteLine($"{equipment.Name} 장착 해제");
            ConsoleUI.PressAnyKey();
        }
        return equipment;
    }
    
    // 골드 차감 메서드
    public void SpendGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
        }
    }
    #endregion
}
