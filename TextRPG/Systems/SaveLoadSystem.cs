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
}
