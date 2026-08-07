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
            // TODO: 게임 로딩 및 새 게임 시작
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
    }
}