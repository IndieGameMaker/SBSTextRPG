using TextRPG.Models;

namespace TextRPG.Systems;

public class InventorySystem
{
    #region 프로퍼티
    // 아이템 목록
    public List<Item> Items { get; set; }
    // 아이템 갯수
    public int Count => Items.Count;
    #endregion

    #region 생성자
    public InventorySystem()
    {
        Items = new List<Item>();
    }
    #endregion

    #region 아이템 관리 메서드
    // 아이템 추가
    public void AddItem(Item item)
    {
        Items.Add(item);
        Console.WriteLine($"{item.Name}을 인벤토리에 추가했습니다.");
    }
    // 아이템 삭제
    public bool RemoveItem(Item item)
    {
        if (Items.Remove(item))
        {
            Console.WriteLine($"{item.Name}을 인벤토리에서 제거했습니다.");
            return true;
        }
        
        return false;
    }
    #endregion
}
