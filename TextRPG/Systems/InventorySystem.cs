using TextRPG.Core;
using TextRPG.Models;
using TextRPG.Utils;

namespace TextRPG.Systems;

public class InventorySystem<T> where T : Item
{
    #region 프로퍼티
    // 아이템 목록
    public List<T> Items { get; set; }
    // 아이템 갯수
    public int Count => Items.Count;
    #endregion

    #region 생성자
    public InventorySystem()
    {
        Items = new List<T>();
    }
    #endregion
    
    private Player? _player => GameManager.Instance.Player;

    #region 아이템 관리 메서드
    // 아이템 추가
    public void AddItem(T item)
    {
        Items.Add(item);
        Console.WriteLine($"{item.Name}을 인벤토리에 추가했습니다.");
    }
    // 아이템 삭제
    public bool RemoveItem(T item)
    {
        if (Items.Remove(item))
        {
            Console.WriteLine($"{item.Name}을 인벤토리에서 제거했습니다.");
            return true;
        }
        
        return false;
    }
    #endregion

    #region 인벤토리 표시

    public void ShowInventoryMenu()
    {
        while (true)
        {
            ConsoleUI.DisplayInventory();

            if (Items.Count == 0)
            {
                Console.WriteLine("인벤토리가 비어있습니다.");
            }
            
            Console.WriteLine("\n[보유 아이템 목록]");
            for (int i = 0; i < Items.Count; i++)
            {
                Console.Write($"[{i + 1}] ");
                Items[i].DisplayInfo();
            }
            
            Console.WriteLine("\n선택하세요.");
            Console.WriteLine("1. 아이템 사용");
            Console.WriteLine("2. 아이템 버리기");
            Console.WriteLine("0. 나가기");
            Console.Write("선택: ");
            
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    // 아이템 사용로직
                    UseItem();
                    break;
                case "2":
                    // 아이템 버리기 로직
                    DropItem();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("잘못된 선택입니다. 다시 선택해주세요.");
                    ConsoleUI.PressAnyKey();
                    break;
            }
        }
    }
    #endregion

    #region  아이템 사용

    private void UseItem()
    {
        if (Items.Count == 0)
        {
            Console.WriteLine("인벤토리가 비어있습니다.");
            return;
        }

        Console.Write($"\n사용할 아이템 번호 (0:취소)> ");

        // 0,1,2,3,4  => 1,2,3,4,5
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Items.Count)
        {
            T item = Items[index-1];
            if (item.Use(_player))
            {
                // 소모성 아이템의 경우 사용후 리스트에서 제거
                if (item is Consumable)
                {
                    // 아이템 삭제 메소드
                    RemoveItem(item);
                }
            }
        }
        else if (index != 0)
        {
            Console.WriteLine("잘못된 선택입니다.");
        }
    }
    #endregion

    #region 아이템 버리기

    private void DropItem()
    {
        if (Items.Count == 0) return;
        
        Console.Write("\n버릴 아이템 번호 (0:취소)> ");

        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Items.Count)
        {
            var item = Items[index - 1]; // 선택한 아이템 번호 - 1 => 배열의 첨자
            
            Console.Write($"정말 {item.Name}을 버리겠습니까? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                RemoveItem(item);
            }
        }
        else if (index != 0)
        {
            Console.WriteLine("잘못된 선택입니다.");
            ConsoleUI.PressAnyKey();
        }
    }

    #endregion
}
