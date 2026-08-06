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

    public void ShowShopMenu()
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
                    break;
                case "2": // 판매 메서드
                    break;
                case "0":
                    Console.WriteLine("\n상점을 나갑니다....");
                    ConsoleUI.PressAnyKey();
                    break;
                default:
                    Console.WriteLine("\n잘못된 선택입니다.");
                    ConsoleUI.PressAnyKey();
                    break;
            }
        }
    }

    #endregion
}
