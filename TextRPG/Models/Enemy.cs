namespace TextRPG.Models;

public class Enemy : Character
{
    #region 프로퍼티
    public int GoldReward { get; private set; }
    #endregion

    #region 생성자
    public Enemy(string name, int maxHp, int maxMp, int attackPower, int defense, int level, int goldReward) : 
        base(name, maxHp, maxMp, attackPower, defense, level)
    {
        GoldReward = goldReward;
    }
    #endregion

    #region 메서드
    // 적 생성 메서드 (레벨에 따라서 난이도 설정)
    // 변수 : 카멜케이스 camelCase
    public static Enemy CreateEnemy(int playerLevel)
    {
        // 난수 발생
        Random random = new Random();
        // 적 캐릭터의 레벨 (플레이어 레벨 +-1) -1, 0, +1
        int enemyLevel = Math.Max(1, playerLevel + random.Next(-1, 2)); // -1, 0, +1
        // 적 캐릭터의 종류 
        // 형변환 => (변환할타입)변수
        EnemyType enemyType = (EnemyType)random.Next(0, 3); // 0, 1, 2
        
        // 적 캐릭터 스텟
        int maxHp = 200 + (enemyLevel - 1) * 20;
        int maxMp = 20 + (enemyLevel - 1) * 10;
        int attackPower = 10 + (enemyLevel - 1) * 5;
        int defense = 5 + (enemyLevel - 1) * 3;
        int goldReward = 20 + (enemyLevel - 1) * 10;

        return new Enemy($"LV{enemyLevel} {enemyType.ToString()}", maxHp, maxMp, attackPower, defense, enemyLevel, goldReward);
    }
    
    // 적 캐릭터 정보 출력
    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"리워드: {GoldReward}");
    }

    public override int Attack(Character target)
    {
        // 일반 공격(70%) / 강한 공격(30%)
        Random random = new Random();

        if (random.NextDouble() <= 0.7) // double 타입은 f 접미사 생략
        {
            // 일반 공격
            return target.TakeDamage(AttackPower);
        }
        else
        {
            // 강한 공격
            int damage = (int)(AttackPower * 1.5f);
            Console.WriteLine($"{Name}의 강한 공격! : {damage}");
            return target.TakeDamage(damage);
        }
    }
    
    #endregion
}
