using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ���� ���� �� ���� ���� ���̴� ȭ��
// ������ ����� BestScore�� Name�� �ҷ��ͼ� ������
// Name �Է¶��� ����
// Start ��ư�� ������ Main Scene���� �Ѿ
// Quit ��ư�� ������ ���� ����
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

    // �̸��� ���ھ �����ϴ� Ŭ����
    [System.Serializable]
    public class SaveData
    {
        public string Name;
        public int BestScore;
    }
    // Json ���Ϸ� BestScore�� Name�� ����
    public void Save()
    {
        SaveData data = new SaveData();
        data.Name = nameSave;
        data.BestScore = BestScore;

        string json = JsonUtility.ToJson(data);

        System.IO.File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    // ����� BestScore�� Name�� �ҷ���
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
