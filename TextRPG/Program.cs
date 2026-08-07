using System.Text;
using TextRPG.Core;
using TextRPG.Systems;

namespace TextRPG;

class Program
{
    static void Main(string[] args)
    {
        // 콘솔의 인코딩 설정 (한글 지원)
        Console.OutputEncoding = Encoding.UTF8;

        // 저장된 게임 존재 여부 확인
        if (SaveLoadSystem.IsSaveFileExist())
        {
            // 메뉴 오픈(새게임, 이어서하기, 종료)
            ShowStartMenu();
        }
        else
        {
            GameManager.Instance.StartGame();
        }
    }

    static void ShowStartMenu()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════╗");
        Console.WriteLine("║            게임시작            ║");
        Console.WriteLine("╚════════════════════════════════╝");

        Console.WriteLine("\n1. 새 게임");
        Console.WriteLine("2. 이어서하기");
        Console.WriteLine("0. 종료");

        while (true)
        {
            Console.Write("\n선택> ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1": // 새 게임 시작
                    GameManager.Instance.StartGame();
                    return;
                case "2": // 이어서 하기
                    if (GameManager.Instance.LoadGame())
                    {
                        GameManager.Instance.StartGame(true);
                    }
                    return;
                case "0": // 종료
                    Console.WriteLine("게임을 종료합니다.");
                    return;
                default:
                    break;
            }
        }
    }
}