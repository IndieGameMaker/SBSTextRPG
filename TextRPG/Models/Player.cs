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
        base.DisplayInfo();
        Console.WriteLine($"골드: {Gold}");
        Console.WriteLine("=====================");
    }
    #endregion
}
