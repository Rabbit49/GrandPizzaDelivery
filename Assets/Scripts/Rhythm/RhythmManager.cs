using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 리듬 게임 관련된 데이터를 관리하는 싱글톤 클래스
/// </summary>
public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance             // 싱글톤 인스턴싱
    {
        get { return instance; }
    }
    public string Title;                            // 관리 할 곡 제목
    public AudioClip AudioClip;                     // 재생할 곡 클립
    public decimal CurrentTime;                     // 현재 시간
    public AudioData Data;                          // 곡 데이터
    public float Speed;                             // 속도
    public float MusicSound;
    public float KeySound;
    public bool SceneChange;
    public JudgeStorage Judges;

    private static RhythmManager instance = null;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Judges = new JudgeStorage();
        Data = new AudioData();
        MusicSound = 0.5f;
        KeySound = 0.5f;
    }

    private void Update()
    {
        Judges.SetAttractive();

        if ((float)CurrentTime >= Data.Length && !SceneChange)
        {
            EndScene();
        }
        if (Input.GetKeyDown(KeyCode.F5) && SceneManager.GetActiveScene().name == "RhythmScene" && !SceneChange)
        {
            EndScene();
        }
    }

    /// <summary>
    /// 곡 데이터를 Json 파일로 저장
    /// </summary>
    public void SaveData()
    {
        JsonManager<AudioData>.Save(Data, Title);
    }

    /// <summary>
    /// Json 파일인 곡 데이터 불러오기
    /// </summary>
    public void LoadData()
    {
        Data = new AudioData(Title);
    }

    public void Init()
    {
        LoadData();
        CurrentTime = 0;
        Judges.Init();
        SceneChange = false;
    }

    private void EndScene()
    {
        SceneChange = true;
        Constant.PizzaAttractiveness = Judges.Attractive;
        LoadScene.Instance.LoadPizzaMenu();
    }
}
