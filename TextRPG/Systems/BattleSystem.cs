using System;
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
        // 등장한 적 캐릭터 스텟 출력
        enemy.DisplayInfo();
        
        // 턴 변수 정의
        int turn = 1;
        
        // 전투로직 실행
        // while (player.IsAlive && enemy.IsAlive)
        {
            Console.WriteLine($"\n======= {turn} 턴 =======");
            // 플레이어 턴 실행
            // 적 캐릭터 사망 여부 판단
            // 적 공격 턴
            turn++;
        }
        
        return player.IsAlive;
    }
    #endregion

    #region 플레이어 턴 (공격)
    // 플레이어 턴 (1.일반공격, 2.스킬공격, 3.도망)
    private void PlayerTurn(Player player, Enemy enemy)
    {
        Console.WriteLine($"\n{player.Name}의 턴!");
        Console.WriteLine($"HP: {player.CurrentHp}/{player.MaxHp} | MP: {player.CurrentMp}/{player.MaxMp}");
        Console.WriteLine("\n행동을 선택하세요.");
        Console.WriteLine("1. 공격");
        Console.WriteLine("2. 스킬");
        Console.WriteLine("3. 도망");
    }
    #endregion

    #region 적캐럭터 턴 (공격)

    #endregion
}
