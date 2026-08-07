using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using TextRPG.Models;
using TextRPG.Core;

namespace TextRPG.Systems;

public class SaveLoadSystem
{
    // 저장 파일 명
    private const string SaveFileName = "gamedata.json";
    
    // JSON 옵션 설정
    private static readonly JsonSerializerOptions jsonOptions = new()
    {
        WriteIndented = true, // 줄바꿈, 들여쓰기 , 가독성 높지, 파일사이즈 증가
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // 한글 지원  
    };

    #region 저장 기능

    public static bool SaveGame(Player player, InventorySystem<Item> inventory)
    {
        try
        {
            // 게임 데이터 객체(GameSaveData) => JSON 저장 (Serialize 직렬화)
            var saveData = new GameSaveData()
            {
                // Player 데이터 변환
                // Inventory 데이터 변환
            };
        }
        catch (Exception e)
        {
            // 예외 처리 로직
        }
    }

    #endregion

    #region Player -> PlayerData

    private static PlayerData ConvertToPlayerData(Player player)
    {
        // PlayerData playerData = new PlayerData();
        // playerData.Name = player.Name;
        // playerData.Level = player.Level;
        // playerData.CurrentHp = player.CurrentHp;
        
        return new PlayerData()
        {
            Name = player.Name,
            Job = player.Job.ToString(),
            Level = player.Level,
            CurrentHp = player.CurrentHp,
            MaxHp = player.MaxHp,
            CurrentMp = player.CurrentMp,
            MaxMp = player.MaxMp,
            AttackPower = player.AttackPower,
            Defense = player.Defense,
            Gold = player.Gold,
            WeaponName = player.EquipmentWeapon?.Name,
            ArmorName = player.EquipmentArmor?.Name,
        };
    }
    #endregion
}
