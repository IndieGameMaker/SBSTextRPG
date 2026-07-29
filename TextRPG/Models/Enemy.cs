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

    // 변수 : 카멜케이스 camelCase
    public static Enemy CreateEnemy(int playerLevel)
    {
        // 난수 발생
        Random random = new Random();
        // 적 캐릭터의 레벨 (플레이어 레벨 +-1) -1, 0, +1
        int enemyLevel = Math.Max(1, playerLevel + random.Next(-1, 2)); // -1, 0, +1
        // 적 캐릭터의 종류
        EnemyType enemyType = (EnemyType)random.Next(0, 3); // 0, 1, 2
        
        // 적 캐릭터 스텟
        int maxHp = 50 + (enemyLevel - 1) * 20;
        int maxMp = 20 + (enemyLevel - 1) * 10;
        int attackPower = 10 + (enemyLevel - 1) * 5;
        int defense = 5 + (enemyLevel - 1) * 3;
        int goldReward = 20 + (enemyLevel - 1) * 10;
    }

    #endregion
}
