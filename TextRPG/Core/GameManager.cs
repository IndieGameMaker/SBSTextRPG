namespace TextRPG.Core;

// 싱글턴 (Singleton) 디자인 패턴
// 하나의 인스턴스만 관리하는 패턴
public class GameManager
{
    #region 싱글턴 패턴
    // 싱글턴 인스턴스 (내부 접근용 필드)
    private static GameManager _instance;
    
    // 싱글턴 접근을 위한 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없으면 새로 생성 (new 클래스)
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    private GameManager()
    {
        
    }
    #endregion

    #region

    #endregion
}
