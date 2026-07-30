namespace TextRPG.Utils;

// 콘솔 출력 UI 메소드를 담당하는 클래스
public class ConsoleUI
{
    // 타이틀 표시 메서드
    public static void ShowTitle()
    {
        Console.Clear();
        Console.WriteLine(@"
╔═══════════════════════════════════════════════════════════════════════╗
║                                                                       ║
║  ████████╗███████╗██╗  ██╗████████╗    ██████╗ ██████╗  ██████╗       ║
║  ╚══██╔══╝██╔════╝╚██╗██╔╝╚══██╔══╝    ██╔══██╗██╔══██╗██╔════╝       ║
║     ██║   █████╗   ╚███╔╝    ██║       ██████╔╝██████╔╝██║  ███╗      ║
║     ██║   ██╔══╝   ██╔██╗    ██║       ██╔══██╗██╔═══╝ ██║   ██║      ║
║     ██║   ███████╗██╔╝ ██╗   ██║       ██║  ██║██║     ╚██████╔╝      ║
║     ╚═╝   ╚══════╝╚═╝  ╚═╝   ╚═╝       ╚═╝  ╚═╝╚═╝      ╚═════╝       ║
║                                                                       ║
║                    턴제 전투 텍스트 RPG 게임                          ║
║                                                                       ║
╚═══════════════════════════════════════════════════════════════════════╝
");
    }
    
    // 메인 메뉴 출력
    public static string ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════╗");
        Console.WriteLine("║            메인 메뉴           ║");
        Console.WriteLine("╚════════════════════════════════╝");
        Console.WriteLine("\n1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 상점");
        Console.WriteLine("4. 던전입장(전투)");
        Console.WriteLine("5. 휴식(HP/MP 회복)");
        Console.WriteLine("6. 저장");
        Console.WriteLine("0. 종료");

        Console.Write("\n선택 (1-6): ");
        string? input = Console.ReadLine();
        
        return input;
    }
    
    // Press Any Key
    public static void PressAnyKey()
    {
        Console.Write("\n아무 키나 누르면 계속합니다...");
        Console.ReadKey(true);
    }
    
    // 게임 오버 메시지 출력
    public static void ShowGameOver()
    {
        Console.Clear();
        Console.WriteLine("\n게임을 종료합니다.");
        Console.WriteLine("╔════════════════════════════════╗");
        Console.WriteLine("║           GAME OVER            ║");
        Console.WriteLine("╚════════════════════════════════╝");        
    }
    
    // 전투시작 메시지 출력
    public static void ShowBattleTitle()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════╗");
        Console.WriteLine("║           전투 시작 !          ║");
        Console.WriteLine("╚════════════════════════════════╝");        
    }
}


