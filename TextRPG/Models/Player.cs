namespace TextRPG.Models;

public class Player : Character
{
    #region 프로퍼티
    // 직업
    public JobType Job { get; private set; }
    // 골드
    public int Gold { get; private set; }
    
    // TODO: 장착 무기
    // TODO: 장착 방어구
    #endregion
    
    #region 생성자
    public Player(string name, JobType job) : base(
        name, 
        maxHp:100, 
        maxMp:50, 
        attackPower:20,
        defense:10,
        level:1)
    {
        Job = job;
        Gold = 1000;
    }
    #endregion
    
    #region 직업별 초기 스텟 로딩

    // private static int GetInitHp(JobType job)
    // {
    //     switch (job)
    //     {
    //         case JobType.Warrior:
    //     }
    // }
    #endregion
}
