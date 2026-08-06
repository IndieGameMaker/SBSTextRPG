using TextRPG.Models;
using TextRPG.Utils;

namespace TextRPG.Systems;

//[상점 시스템]
// 메뉴선택 (구매(buy)/판매(sell)/취소)
public class ShopSystem
{
    #region 프로퍼티
    private List<Item>? ShopItems { get; set; }
    #endregion

    #region 생성자
    public ShopSystem()
    {
        ShopItems = new List<Item>();
        // 상점 아이템 초기화
        InitShop();
    }
    #endregion

    #region 상점 아이템 초기화
    private void InitShop()
    {
        // 무기
        ShopItems?.Add(Equipment.CreateWeapon(WeaponType.WoodSword));
        ShopItems?.Add(Equipment.CreateWeapon(WeaponType.IronSword));
        ShopItems?.Add(Equipment.CreateWeapon(WeaponType.LegendarySword));
        // 방어구
        ShopItems?.Add(Equipment.CreateAmor("천갑옷"));
        ShopItems?.Add(Equipment.CreateAmor("철갑옷"));
        ShopItems?.Add(Equipment.CreateAmor("전설갑옷"));
        // 포션
        ShopItems?.Add(Consumable.CreatePotion("체력포션"));
        ShopItems?.Add(Consumable.CreatePotion("대용량체력포션"));
        ShopItems?.Add(Consumable.CreatePotion("마나포션"));
        ShopItems?.Add(Consumable.CreatePotion("대용량마나포션"));
    }
    #endregion

    #region 상점 메뉴

    public void ShowShopMenu(Player player , InventorySystem<Item> inventory)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════╗");
            Console.WriteLine("║             상  점             ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.WriteLine("\n1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");

            Console.Write("\n선택 > ");
            string? input = Console.ReadLine();

            switch (input)
            {
                case "1": // 구매 메서드
                    BuyItem(player, inventory);
                    break;
                case "2": // 판매 메서드
                    SellItem(player, inventory);
                    break;
                case "0":
                    Console.WriteLine("\n상점을 나갑니다....");
                    ConsoleUI.PressAnyKey();
                    return;
                default:
                    Console.WriteLine("\n잘못된 선택입니다.");
                    ConsoleUI.PressAnyKey();
                    break;
            }
        }
    }
    #endregion

    #region 구매 메서드

    private void BuyItem(Player player, InventorySystem<Item> inventory)
    {
        // 구매 가능한 아이템 목록 (슈더 코드 : Pseudo - code)
        Console.Clear();
        Console.WriteLine("\n[구매 가능한 목록]");

        for (int i = 0; i < ShopItems.Count; i++)
        {
            Console.Write($"[{i + 1}] ");
            ShopItems[i].DisplayInfo();
        }
        
        Console.Write("\n구매할 아이템 번호를 선택하세요. (0:취소)> ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= ShopItems.Count)
        {
            var selectedItem = ShopItems[index - 1];
            
            // 골드가 충분한지 확인
            if (player.Gold >= selectedItem.Price)
            {
                Console.Write($"{selectedItem.Name}을 {selectedItem.Price} 골드로 구매하시겠습니까? (y/n): ");

                if (Console.ReadLine()?.ToLower() == "y")
                {
                    // 골드 차감
                    player.SpendGold(selectedItem.Price);
                    
                    // 구매한 아이템을 복제 (인스턴스)
                    var item = CreateItem(selectedItem);
                    // 아이템 장착 , 인벤토리에 추가
                    if (item is Equipment equipment)
                    {
                        inventory.AddItem(equipment);
                        player.EquipItem(equipment);
                    }
                    else if (item is Consumable consumable)
                    {
                        inventory.AddItem(consumable);
                    }
                    
                    Console.WriteLine($"{selectedItem.Name}을 구매했습니다.");
                    ConsoleUI.PressAnyKey();
                }
            }
            else
            {
                Console.WriteLine("\n골드가 부족합니다.");
                ConsoleUI.PressAnyKey();
            }
        }
        
    }
    #endregion

    #region 아이템 복사 메서드

    private Item CreateItem(Item item)
    {
        // 장착 아이템
        if (item is Equipment equipment)
        {
            var newItem = new Equipment(
                equipment.Name,
                equipment.Description,
                equipment.Price,
                equipment.Slot,
                equipment.AttackBonus,
                equipment.DefenseBonus
                );
            
            return newItem;
        }
        // 소모성 아이템
        else if (item is Consumable consumable)
        {
            return new Consumable(
                consumable.Name,
                consumable.Description,
                consumable.Price,
                consumable.HpAmount,
                consumable.MpAmount
                );
        }

        return null;
    }

    #endregion

    #region 아이템 판매

    private void SellItem(Player player, InventorySystem<Item> inventory)
    {
        if (inventory.Count == 0)
        {
            Console.WriteLine("\n판매할 아이템이 없습니다.");
            return;
        }
        
        Console.Clear();
        Console.WriteLine("\n[보유 아이템 목록]");
        for (int i = 0; i < inventory.Items.Count; i++)
        {
            Console.Write($"[{i + 1}] ");
            inventory.Items[i].DisplayInfo();
        }
        
        Console.Write("\n판매할 아이템 번호를 입력하세요. (0:취소)> ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= inventory.Items.Count)
        {
            // 인벤토리의 아이템 추출
            var selectedItem = inventory.Items[index - 1];
            
            // 판매가격 할인 (50%)
            int sellPrice = (int)(selectedItem.Price * 0.5f);
            
            Console.WriteLine($"{selectedItem.Name}을 {sellPrice} 골드에 판매하시겠습니까? (y/n): ");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                // 아이템을 인벤토리에서 제거
                inventory.RemoveItem(selectedItem);
                player.GainGold(sellPrice);
                
                // 장착해제
                if (selectedItem is Equipment equipment)
                {
                    player.UnEquipItem(equipment.Slot);
                }
                
                Console.WriteLine($"{selectedItem.Name}를 판매완료했습니다.");
                ConsoleUI.PressAnyKey();
            }
        }
    }

    #endregion
}
