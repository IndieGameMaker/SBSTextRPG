using TextRPG.Models;
using TextRPG.Utils;

namespace TextRPG.Systems;

public class BattleSystem
{
    #region 던전 입장 - 전투 실행
    // 전투 시작 메서드 
    // 반환값 : 전투 승리 여부
    public bool StartBattle(Player player, Enemy enemy)
    {
        ConsoleUI.ShowBattleTitle();
        // TODO : 전투로직 실행
        return player.IsAlive;
    }
    #endregion

    #region 플레이어 턴 (공격)

    #endregion

    #region 적캐럭터 턴 (공격)

    #endregion
}
