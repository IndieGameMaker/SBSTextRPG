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
    }
    #endregion

    #region 상점 아이템 초기화
    private void InitShop()
    {
        
    }
    #endregion
}
