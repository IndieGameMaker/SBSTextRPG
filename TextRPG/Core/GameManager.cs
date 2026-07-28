using TextRPG.Utils;
using TextRPG.Models;

namespace TextRPG.Core;

// 싱글턴 (Singleton) 디자인 패턴
// 하나의 인스턴스만 관리하는 패턴
public class GameManager
{
    #region 싱글턴 패턴
    // 싱글턴 인스턴스 (내부 접근용 필드)
    private static GameManager _instance;
    
    // 싱글턴 접근을 위한 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없으면 새로 생성 (new 클래스)
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    private GameManager()
    {
        
    }
    #endregion

    #region 프로퍼티
    public Player? Player { get; private set; }
    #endregion

    #region 게임 시작 / 종료
    // 게임 시작 메서드
    public void StartGame()
    {
        // 게임 타이틀 표시
        ConsoleUI.ShowTitle();
        // 캐릭터 생성
        CreateCharacter();
        // TODO : 인벤토리 초기화
        // TODO : 초기 아이템 지급
    }
    #endregion
    
    #region 캐릭터 생성

    private void CreateCharacter()
    {
        // 이름 입력
        Console.Write("캐릭터의 이름을 입력하세요: ");
        string? name = Console.ReadLine(); // ? : null 값을 허용한다. nullable 변수

        if (string.IsNullOrWhiteSpace(name))
        {
            name = "무명용사";
        }
        Console.WriteLine($"{name}님, 모험을 시작하겠습니다.");
        
        // 직업 선택
        Console.WriteLine("직업을 선택하세요 :");
        Console.WriteLine("1: 전사");
        Console.WriteLine("2: 궁수");
        Console.WriteLine("3: 법사");

        JobType job = JobType.Warrior; // 기본값 설정

        while (true)
        {
            Console.Write("선택 (1-3): ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    job = JobType.Warrior;
                    break;
                case "2":
                    job = JobType.Archer;
                    break;
                case "3":
                    job = JobType.Wizard;
                    break;
                default:
                    Console.WriteLine("잘못 입력했습니다. 다시 입력하세요.");
                    continue;
            }

            break; // while 루프의 break
        }
        
        // 입력한 이름과 선택한 직업으로 플레이어 캐릭터 생성
        Player = new Player(name, job);
        Console.WriteLine($"{name}님, {job} 직업으로 캐릭터가 생성되었습니다.");
    }
    #endregion
}
