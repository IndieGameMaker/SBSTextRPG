namespace TextRPG.Models;

// 캐릭터의 기본 추상 클래스
public abstract class Character
{
    #region 프로퍼티
    public string Name { get; protected set; }
    public int CurrentHp { get; protected set; }
    public int MaxHp { get; protected set; }
    public int CurrentMp { get; protected set; }
    public int MaxMp { get; protected set; }
    public int AttackPower {get; protected set;}
    public int Defense {get; protected set;}
    
    public int Level {get; protected set;}
    
    // 생존 여부
    public bool IsAlive => CurrentHp > 0;
    #endregion

    #region 생성자
    protected Character(string name, int maxHp, int maxMp, int attackPower, int defense, int level)
    {
        Name = name;
        MaxHp = maxHp;
        CurrentHp = maxHp;
        MaxMp = maxMp;
        CurrentMp = maxMp;
        AttackPower = attackPower;
        Defense = defense;
        Level = level;
    }
    #endregion

    #region 메서드
    // 캐릭터의 스텟 출력
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"==== {Name} 정보 ====");
        Console.WriteLine($"레벨: {Level}");
        Console.WriteLine($"체력: {CurrentHp}/{MaxHp}");
        Console.WriteLine($"마나: {CurrentMp}/{MaxMp}");
        Console.WriteLine($"공격력: {AttackPower}");
        Console.WriteLine($"방어력: {Defense}");
    }
    
    // 공격 메서드 정의 (추상 메서드)
    public abstract int Attack(Character target);
    
    // 데미지 처리 메서드 (가상 메서드)
    public virtual int TakeDamage(int damage)
    {
        // 방어력 적용
        int actualDamage = Math.Max(1, damage - Defense);
        CurrentHp = Math.Max(0, CurrentHp - actualDamage);
        return actualDamage;
    }

    #endregion
}
