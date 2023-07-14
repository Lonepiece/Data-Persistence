using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임 시작 시 제일 먼저 보이는 화면
// 이전에 저장된 BestScore와 Name을 불러와서 보여줌
// Name 입력란이 있음
// Start 버튼을 누르면 Main Scene으로 넘어감
// Quit 버튼을 누르면 게임 종료
public class MenuManager : MonoBehaviour
{
    public Text bestScoreText;
    public InputField nameInput;
    public Button startButton;
    public Button quitButton;

    public static MenuManager Instance;
    public int BestScore;

    public string nameSave;
    public string scoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);

        Load();
        bestScoreText.text = scoreText;

        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        nameSave = nameInput.text;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        Application.Quit();
#endif
    }

    // 이름과 스코어를 저장하는 클래스
    [System.Serializable]
    public class SaveData
    {
        public string Name;
        public int BestScore;
    }
    // Json 파일로 BestScore와 Name을 저장
    public void Save()
    {
        SaveData data = new SaveData();
        data.Name = nameSave;
        data.BestScore = BestScore;

        string json = JsonUtility.ToJson(data);

        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    // 저장된 BestScore와 Name을 불러옴
    public void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            scoreText = "Best Score : " + data.Name + " : " + data.BestScore;
        }
    }
}
