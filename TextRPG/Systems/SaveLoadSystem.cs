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
            // 1. 게임 데이터 객체(GameSaveData) => JSON 저장 (Serialize 직렬화)
            // DTO(Data Transfer Object) 변환
            var saveData = new GameSaveData()
            {
                // Player 데이터 변환
                Player = ConvertToPlayerData(player),
                // Inventory 데이터 변환
                Inventory = ConvertToInventoryData(inventory),
            };
            
            // 2. DTO 객체 => JSON 문자열로 변환
            string jsonString = JsonSerializer.Serialize(saveData, jsonOptions);
            
            // 3. JSON 문자열 => 파일로 저장
            File.WriteAllText(SaveFileName, jsonString);
            
            return true;
        }
        catch (Exception e)
        {
            // 예외 처리 로직
            Console.WriteLine(e.Message);
            return false;
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
    
    #region InventorySystem -> Inventory

    private static List<ItemData> ConvertToInventoryData(InventorySystem<Item> inventory)
    {
        var itemDataList = new List<ItemData>();

        for (int i = 0; i < inventory.Count; i++)
        {
            var item = inventory.Items[i];
            if (item == null) continue;

            var itemData = new ItemData()
            {
                Name = item.Name,
            };

            if (item is Equipment equipment)
            {
                itemData.ItemType = "Equipment";
                itemData.Slot = equipment.Slot.ToString();
            }
            else if (item is Consumable consumable)
            {
                itemData.ItemType = "Consumable";
            }
            
            itemDataList.Add(itemData);
        }
        
        return itemDataList;
    }
    #endregion

    #region 불러오기 기능
    // 저장 파일 유무
    public static bool IsSaveFileExist()
    {
        return File.Exists(SaveFileName);
    }

    public static GameSaveData? LoadGameData()
    {
        try
        {
            // 1. JSON 파일에서 문자열 가져오기
            string jsonString = File.ReadAllText(SaveFileName);
            Console.WriteLine(jsonString);
            
            // 2. JSON 문자열 => DTO 클래스로 변환 (Deserialize 역직렬화)
            var saveData = JsonSerializer.Deserialize<GameSaveData>(jsonString, jsonOptions);
            Console.WriteLine("\n게임 데이터 로드가 완료되었습니다.");
            return saveData;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    #endregion

    #region Player 데이터 복원

    public static Player LoadPlayer(PlayerData data)
    {
        var job = Enum.Parse<JobType>(data.Job);
        // Player객체 생성
        var player = new Player(data.Name, job);
        
        // 스텟 설정
        player.Level = data.Level;
        player.CurrentHp = data.CurrentHp;
        player.MaxHp = data.MaxHp;
        player.CurrentMp = data.CurrentMp;
        player.MaxMp = data.MaxMp;
        player.AttackPower = data.AttackPower;
        player.Defense = data.Defense;
        player.Gold = data.Gold;
        
        return player;
    }

    #endregion
    
    #region 인벤토리 데이터 복원 (수업 종료 후)
    // ItemData DTO를 Inventory 클래스로 변환 메서드
    public static InventorySystem<Item> LoadInventory(List<ItemData> itemDataList, Player player)
    {
        var inventory = new InventorySystem<Item>();
        foreach (var itemData in itemDataList)
        {
            Item? item = null;

            if (itemData.ItemType == "Equipment")
            {
                // 장착 슬롯 확인
                var slot = Enum.Parse<EquipmentSlot>(itemData.Slot);
                if (slot == EquipmentSlot.Weapon)
                {
                    item = Equipment.CreateWeapon(Enum.Parse<WeaponType>(itemData.Name));
                }
                else if (slot == EquipmentSlot.Armor)
                {
                    item = Equipment.CreateAmor(itemData.Name);
                }
            }
            else if (itemData.ItemType == "Consumable")
            {
                item = Consumable.CreatePotion(itemData.Name);
            }

            if (item != null)
            {
                inventory.AddItem(item);
            }
        }
        return inventory;
    }
    #endregion

    #region 장착 아이템 복원(무기/방어구) (수업 종료 후)
    public static void LoadEquippedItems(Player player, PlayerData data, InventorySystem<Item> inventory)
    {
        // 무기 장착 복원
        if (!string.IsNullOrEmpty(data.WeaponName))
        {
            // 인벤토리에서 같은 무기를 찾기
            for (int i = 0; i < inventory.Count; ++i)
            {
                var item = inventory.Items[i];
                if (item is Equipment equipment && equipment.Slot == EquipmentSlot.Weapon &&
                    equipment.Name == data.WeaponName)
                {
                    player.EquipItem(equipment);
                    break;
                }
            }
        }
        
        // 방어구 장착 복원
        if (!string.IsNullOrEmpty(data.ArmorName))
        {
            for (int i=0; i < inventory.Count; ++i)
            {
                var item = inventory.Items[i];
                if (item is Equipment equipment && equipment.Slot == EquipmentSlot.Armor &&
                    equipment.Name == data.ArmorName)
                {
                    player.EquipItem(equipment);
                }
            }
        }
    }
    #endregion
}
