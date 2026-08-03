using TextRPG.Utils;
using TextRPG.Models;
using TextRPG.Systems;

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
        // 전투 시스템 초기화(인스턴스화)
        BattleSystem = new BattleSystem();
    }
    #endregion

    #region 프로퍼티
    // 주인공 캐릭터 클래스
    public Player? Player { get; private set; }
    
    // 게임 실행 여부
    public bool IsRunning { get; private set; } = true;

    // 전투 시스템
    public BattleSystem BattleSystem { get; private set; }
    
    // 인벤토리 시스템
    public InventorySystem Inventory { get; private set; }
    #endregion

    #region 게임 시작 / 종료
    // 게임 시작 메서드
    public void StartGame()
    {
        // 게임 타이틀 표시
        ConsoleUI.ShowTitle();
        // 주인공 캐릭터 생성
        CreateCharacter();
        
        // 인벤토리 초기화
        Inventory = new InventorySystem();
        // 초기 아이템 지급
        SetupInitItems();
        
        while (IsRunning)
        {
            DisplayMenu();
        }
        
        // 게임 종료 로직 처리
        if (!IsRunning)
        {
            ConsoleUI.ShowGameOver();
        }
    }

    // 초기 아이템 지급
    private void SetupInitItems()
    {
        // 기본 장비
        Inventory.AddItem(Equipment.CreateWeapon(WeaponType.WoodSword));
        Inventory.AddItem(Equipment.CreateAmor("천갑옷"));
        // 포션 지급
        Inventory.AddItem(Consumable.CreatePotion("체력포션"));
        Inventory.AddItem(Consumable.CreatePotion("체력포션"));
        Inventory.AddItem(Consumable.CreatePotion("마나포션"));
        
        Console.WriteLine("\n초기 장비가 지급되었습니다.");
        ConsoleUI.PressAnyKey();
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
        ConsoleUI.PressAnyKey();
    }
    #endregion

    #region 메인 메뉴

    private void DisplayMenu()
    {
        string? input = ConsoleUI.ShowMainMenu();
        // 메뉴 분기 처리
        switch (input)
        {
            case "1": // 상태보기
                Player.DisplayInfo();
                ConsoleUI.PressAnyKey();
                break;
            
            case "2": // 인벤토리
                Inventory.ShowInventoryMenu();
                break;
            
            case "3": // 상점
                break;
            
            case "4": // 던전입장
                // 던전 입장 기능
                EnterDungeon();
                break;
            
            case "5": // 휴식
                break;
            
            case "6": // 저장
                break;
            
            case "0":
                IsRunning = false;
                break;
            
            default:
                Console.WriteLine("\n잘못된 입력입니다. 다시 선택해주세요.");
                ConsoleUI.PressAnyKey();
                break;
        }
    }
    #endregion

    #region 메뉴 기능 구현부
    // 던전 입장
    public void EnterDungeon()
    {
        Console.Clear();
        Console.WriteLine("\n던전에 입장합니다...");
        
        // 적 캐릭터 생성
        Enemy enemy = Enemy.CreateEnemy(Player.Level);  
        ConsoleUI.PressAnyKey();
        
        // 전투 시작
        BattleSystem.StartBattle(Player, enemy);
        
        Console.WriteLine("\n던전 탐험을 마치고 마을로 돌아갑니다...");
        ConsoleUI.PressAnyKey();        
    }
    #endregion
}
