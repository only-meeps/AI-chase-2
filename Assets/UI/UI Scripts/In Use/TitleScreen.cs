using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Dan.Main;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor;
using System;

public class TitleScreen : MonoBehaviour
{
    public TMP_InputField MaxPlats;
    public TMP_InputField MinPlats;
    public TMP_InputField Seed;
    public TMP_InputField MinDepth;
    public TMP_InputField MaxDepth;
    public TMP_InputField MaxWidth;
    public TMP_InputField MinWidth;
    public TMP_InputField MaxHeight;
    public TMP_InputField MinHeight;
    public TMP_InputField MinGameWidth;
    public TMP_InputField MaxGameWidth;
    public TMP_InputField MinGameLength;
    public TMP_InputField MaxGameLength;
    public TMP_InputField AIMin;
    public TMP_InputField AIMax;
    public TMP_InputField UsernameInput;
    public TMP_InputField ShadowDrawDistanceInput;
    public Slider ShadowQualitySettingsInput;
    public Slider QualitySettingsInput;
    public Slider AntiAliasingInput;
    public Slider ShadowCascadeInput;
    public TMP_Text DifficultyDisplay;
    public TMP_Dropdown DifficultyDropdown;
    public TMP_Text EndlessHighScoreDisplay;
    public TMP_Text EndlessHighScoreSeedDisplay;
    public TMP_Text ObjectiveHighScoreDisplay;
    public TMP_Text ObjectiveHighScoreSeedDisplay;
    public TMP_Text UsernameDisplay;
    public TMP_Text VsyncDisplay;
    public TMP_Text AntiAliasingDisplay;
    public TMP_Text QualityDisplay;
    public TMP_Text ShadowQualityDisplay;
    public TMP_Text ShadowCascadeDisplay;
    public GameObject UsernameInputter;
    public GameObject SettingsOBJ;
    public GameObject GenerationSettingsOBJPG1;
    public GameObject GenerationSettingsOBJ;
    public GameObject GenerationSettingsOBJPG2;
    public GameObject PlayerSettingsOBJ;
    public GameObject LeaderBoards;
    public GameObject GraphicsSettings;
    public bool Vsync;
    public List<GameObject> LeaderBoardPages;
    public bool LeaderboardsOpen;
    public int LeaderboardsPage;
    public bool GenerationSettingsOpen;
    public bool PlayerSettingsOpen;
    public bool SettingsOpen;
    public int GenerationSettingsPage;
    public int ScrollDistanceEndlessLeaderboard;
    public int ScrollDistanceObjectiveLeaderboard;
    public int AntiAliasing;
    public List<string> Alphabet;
    public List<int> Numbers;
    public GameObject LoadingScreen;
    [SerializeField]
    private List<TextMeshProUGUI> EndlessLeaderboardNames;
    [SerializeField]
    private List<TextMeshProUGUI> EndlessLeaderboardSeeds;
    [SerializeField]
    private List<TextMeshProUGUI> EndlessLeaderboardScores;
    [SerializeField]
    private List<TextMeshProUGUI> EndlessLeaderboardRanks;
    private string EndlessLeaderboardPublicKey = "2bc68de9e205583465c6f12daaedf2030daa1992240b40246c0dad3d6b3bea87";
    [SerializeField]
    private List<TextMeshProUGUI> ObjectiveLeaderboardNames;
    [SerializeField]
    private List<TextMeshProUGUI> ObjectiveLeaderboardSeeds;
    [SerializeField]
    private List<TextMeshProUGUI> ObjectiveLeaderboardScores;
    [SerializeField]
    private List<TextMeshProUGUI> ObjectiveLeaderboardRanks;
    private string ObjectiveLeaderboardPublicKey = "4d5e243fcdafbb6cff40d92b60acb5b5bb754b7c6ec6158a014a406888f0550c";
    // Start is called before the first frame update
    void Start()
    {
        Numbers.Add(0);
        for (int i = 0; i < 9; i++)
        {
            Numbers.Add(i);
        }
        AddAphabet();
        if(PlayerPrefs.GetInt("FirstGame") == 0)
        {
            PlayerPrefs.SetFloat("MinGameWidth", -50);
            PlayerPrefs.SetFloat("MaxGameWidth", 50);
            PlayerPrefs.SetFloat("MinGameLength", -50);
            PlayerPrefs.SetFloat("MaxGameLength", 50);
            PlayerPrefs.SetFloat("MaxWidth", 20);
            PlayerPrefs.SetFloat("MinWidth", 5);
            PlayerPrefs.SetFloat("MaxHeight", 30);
            PlayerPrefs.SetFloat("MinHeight", 5);
            PlayerPrefs.SetFloat("MaxDepth", 20);
            PlayerPrefs.SetFloat("MinDepth", 5);
            PlayerPrefs.SetInt("MaxPlats", 100);
            PlayerPrefs.SetInt("MinPlats", 5);
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("Seed", 5832);
            PlayerPrefs.SetString("SeedString", "5832");
            PlayerPrefs.SetInt("AIMin", 5);
            PlayerPrefs.SetInt("AIMax", 20);
            PlayerPrefs.SetInt("Difficulty", 2);
            PlayerPrefs.SetInt("FirstGame", 1);
            PlayerPrefs.SetFloat("AntiAliasing", 0);
            PlayerPrefs.SetFloat("Quality", 2);
            PlayerPrefs.SetFloat("ShadowQuality", 2);
            PlayerPrefs.SetString("ShadowDrawDistance", "10");
            PlayerPrefs.SetFloat("ShadowCascades", 0);
            SceneManager.LoadScene("TutorialScene");
        }
        
        DifficultyDropdown.value = PlayerPrefs.GetInt("Difficulty");
        MinGameLength.text = PlayerPrefs.GetFloat("MinGameLength").ToString();
        MaxGameLength.text = PlayerPrefs.GetFloat("MaxGameLength").ToString();
        MinGameWidth.text = PlayerPrefs.GetFloat("MinGameWidth").ToString();
        MaxGameWidth.text = PlayerPrefs.GetFloat("MaxGameWidth").ToString();
        MaxPlats.text = PlayerPrefs.GetInt("MaxPlats").ToString();
        MinPlats.text = PlayerPrefs.GetInt("MinPlats").ToString();
        Seed.text = PlayerPrefs.GetString("SeedString");
        MinDepth.text = PlayerPrefs.GetFloat("MinDepth").ToString();
        MaxDepth.text = PlayerPrefs.GetFloat("MaxDepth").ToString();
        MaxWidth.text = PlayerPrefs.GetFloat("MaxWidth").ToString();
        MinWidth.text = PlayerPrefs.GetFloat("MinWidth").ToString();
        MaxHeight.text = PlayerPrefs.GetFloat("MaxHeight").ToString();
        MinHeight.text = PlayerPrefs.GetFloat("MinHeight").ToString();
        AIMin.text = PlayerPrefs.GetInt("AIMin").ToString();
        AIMax.text = PlayerPrefs.GetInt("AIMax").ToString();
        EndlessHighScoreDisplay.text = PlayerPrefs.GetFloat("EndlessHighScore").ToString();
        EndlessHighScoreSeedDisplay.text = PlayerPrefs.GetString("EndlessHighScoreSeed");
        ObjectiveHighScoreSeedDisplay.text = PlayerPrefs.GetString("ObjectiveHighScoreSeed");
        ObjectiveHighScoreDisplay.text = PlayerPrefs.GetFloat("HighScore").ToString();
        AntiAliasingInput.value = PlayerPrefs.GetFloat("AntiAliasing");
        QualitySettingsInput.value = PlayerPrefs.GetFloat("Quality");
        ShadowQualitySettingsInput.value = PlayerPrefs.GetFloat("ShadowQuality");
        ShadowDrawDistanceInput.text = PlayerPrefs.GetString("ShadowDrawDistance");
        ShadowCascadeInput.value = PlayerPrefs.GetFloat("ShadowCascades");
        if(PlayerPrefs.GetInt("Vsync") == 0)
        {
            Vsync = false;
            VsyncDisplay.text = "Off";
        }
        else
        {
            Vsync = true;
            VsyncDisplay.text = "On";
        }
        QualitySettingChanged();
        UpdateQualitySettings();
        if(PlayerPrefs.GetString("Username") == null || PlayerPrefs.GetString("Username") == "")
        {
            UsernameInputter.SetActive(true);
        }
        GenerationSettingsOpen = true;
        GenerationSettingsPage = 0;
        LeaderboardsPage = 0;
        Debug.Log("FirstGame " + PlayerPrefs.GetInt("FirstGame"));
        SetEndlessLeaderboard();
        SetObjectiveLeaderboard();
        UsernameDisplay.text = PlayerPrefs.GetString("Username");
        if(DifficultyDropdown.value == 0)
        {
            DifficultyDisplay.text = "Very Easy";
        }
        else if (DifficultyDropdown.value == 1)
        {
            DifficultyDisplay.text = "Easy";
        }
        else if (DifficultyDropdown.value == 2)
        {
            DifficultyDisplay.text = "Normal";
        }
        else if (DifficultyDropdown.value == 3)
        {
            DifficultyDisplay.text = "Hard";
        }
        else if (DifficultyDropdown.value == 4)
        {
            DifficultyDisplay.text = "Very Hard";
        }
        else if (DifficultyDropdown.value == 5)
        {
            DifficultyDisplay.text = "Impossible";
        }
        else if (DifficultyDropdown.value == 6)
        {
            DifficultyDisplay.text = "Insanity";
        }
        else if (DifficultyDropdown.value == 7)
        {
            DifficultyDisplay.text = "Custom";
        }
        
    }
    void AddAphabet()
    {
        Alphabet.Add("0");
        Alphabet.Add("1");
        Alphabet.Add("2");
        Alphabet.Add("3");
        Alphabet.Add("4");
        Alphabet.Add("5");
        Alphabet.Add("6");
        Alphabet.Add("7");
        Alphabet.Add("8");
        Alphabet.Add("9");
        for (char c = 'A'; c <= 'Z'; c++)
        {
            Alphabet.Add(c.ToString());
        }
        for (char c = 'a'; c <= 'z'; c++)
        {
            Alphabet.Add(c.ToString());
        }
    }
    public void ChangeVsync()
    {
        if (Vsync == true)
        {
            Vsync = false;
            VsyncDisplay.text = "On";
        }
        else if (!Vsync)
        {
            Vsync = true;
            VsyncDisplay.text = "Off";
        }
    }
    public void QualitySettingChanged()
    {
        if (Vsync == true)
        {
            VsyncDisplay.text = "On";
        }
        else if (!Vsync)
        {
            VsyncDisplay.text = "Off";
        }
        if(ShadowCascadeInput.value == 0)
        {
            ShadowCascadeDisplay.text = "None";
        }
        else if(ShadowCascadeInput.value == 1)
        {
            ShadowCascadeDisplay.text = "Two";
        }
        else if(ShadowCascadeInput.value == 2)
        {
            ShadowCascadeDisplay.text = "Four";
        }
        if (QualitySettingsInput.value == 0)
        {
            QualityDisplay.text = "256";
        }
        else if (QualitySettingsInput.value == 1)
        {
            QualityDisplay.text = "512";
        }
        else if (QualitySettingsInput.value == 2)
        {
            QualityDisplay.text = "1024";
        }
        else if (QualitySettingsInput.value == 3)
        {
            QualityDisplay.text = "2048";
        }
        else if(QualitySettingsInput.value == 4)
        {
            QualityDisplay.text = "4096";
        }
        if(AntiAliasingInput.value == 0)
        {
            AntiAliasingDisplay.text = "Off";
        }
        else if(AntiAliasingInput.value == 1)
        {
            AntiAliasingDisplay.text = "2x MultiSampling";
        }
        else if (AntiAliasingInput.value == 2)
        {
            AntiAliasingDisplay.text = "4x MultiSampling";
        }
        else if (AntiAliasingInput.value == 3)
        {
            AntiAliasingDisplay.text = "8x MultiSampling";
        }
        if(ShadowQualitySettingsInput.value == 0)
        {
            ShadowQualityDisplay.text = "Low";
        }
        else if (ShadowQualitySettingsInput.value == 1)
        {
            ShadowQualityDisplay.text = "Medium";
        }
        else if (ShadowQualitySettingsInput.value == 2)
        {
            ShadowQualityDisplay.text = "High";
        }
        else if (ShadowQualitySettingsInput.value == 3)
        {
            ShadowQualityDisplay.text = "Very high";
        }
    }
    public void UpdateQualitySettings()
    {
        PlayerPrefs.SetFloat("ShadowCascades", ShadowCascadeInput.value);
        PlayerPrefs.SetFloat("AntiAliasing", AntiAliasingInput.value);
        PlayerPrefs.SetFloat("Quality", QualitySettingsInput.value);
        PlayerPrefs.SetFloat("ShadowQuality", ShadowQualitySettingsInput.value);
        PlayerPrefs.SetString("ShadowDrawDistance", ShadowDrawDistanceInput.text);
        QualitySettings.antiAliasing = int.Parse(AntiAliasingInput.value.ToString());
        QualitySettings.shadowDistance = int.Parse(ShadowDrawDistanceInput.text);
        QualitySettings.shadowCascades = int.Parse(ShadowCascadeInput.value.ToString());
        if(Vsync == true)
        {
            QualitySettings.vSyncCount = 1;
            PlayerPrefs.SetInt("Vsync", 1);
        }
        else if(Vsync == false)
        {
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt("Vsync", 0);
        }
        if (QualitySettingsInput.value == 0)
        {
            QualitySettings.SetQualityLevel(256);
        }
        else if (QualitySettingsInput.value == 1)
        {
            QualitySettings.SetQualityLevel(512);
        }
        else if (QualitySettingsInput.value == 2)
        {
            QualitySettings.SetQualityLevel(1024);
        }
        else if (QualitySettingsInput.value == 3)
        {
            QualitySettings.SetQualityLevel(2048);
        }
        else if (QualitySettingsInput.value == 4)
        {
            QualitySettings.SetQualityLevel(4096);
        }
        if (ShadowQualitySettingsInput.value == 0)
        {
            QualitySettings.shadowResolution = ShadowResolution.Low;
        }
        else if (ShadowQualitySettingsInput.value == 1)
        {
            QualitySettings.shadowResolution = ShadowResolution.Medium;
        }
        else if (ShadowQualitySettingsInput.value == 2)
        {
            QualitySettings.shadowResolution = ShadowResolution.High;
        }
        else if (ShadowQualitySettingsInput.value == 3)
        {
            QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(LeaderboardsPage == 1)
        {
            ScrollDistanceEndlessLeaderboard -= Mathf.RoundToInt(Input.mouseScrollDelta.y);
            if(Input.mouseScrollDelta.y != 0)
            {
                Debug.Log(ScrollDistanceEndlessLeaderboard);
                GetEndlessLeaderboard();
            }
        }
        else if(LeaderboardsPage == 2)
        {
            ScrollDistanceObjectiveLeaderboard -= Mathf.RoundToInt(Input.mouseScrollDelta.y);
            if (Input.mouseScrollDelta.y != 0)
            {
                Debug.Log(ScrollDistanceObjectiveLeaderboard);
                GetObjectiveLeaderboard();
            }
        }
    }
    public void ChangeDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", DifficultyDropdown.value);
        if(DifficultyDropdown.value == 0)
        {
            PlayerPrefs.SetFloat("MinGameWidth", -115);
            PlayerPrefs.SetFloat("MaxGameWidth", 115);
            PlayerPrefs.SetFloat("MinGameLength", -115);
            PlayerPrefs.SetFloat("MaxGameLength", 115);
            PlayerPrefs.SetFloat("MaxWidth", 25);
            PlayerPrefs.SetFloat("MinWidth", 10);
            PlayerPrefs.SetFloat("MaxHeight", 10);
            PlayerPrefs.SetFloat("MinHeight", 5);
            PlayerPrefs.SetFloat("MaxDepth", 25);
            PlayerPrefs.SetFloat("MinDepth", 10);
            PlayerPrefs.SetInt("MaxPlats", 25);
            PlayerPrefs.SetInt("MinPlats", 15);
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("AIMin", 1);
            PlayerPrefs.SetInt("AIMax", 5);
        }
        else if(DifficultyDropdown.value == 1)
        {
            PlayerPrefs.SetFloat("MinGameWidth", -100);
            PlayerPrefs.SetFloat("MaxGameWidth", 100);
            PlayerPrefs.SetFloat("MinGameLength", -100);
            PlayerPrefs.SetFloat("MaxGameLength", 100);
            PlayerPrefs.SetFloat("MaxWidth", 22);
            PlayerPrefs.SetFloat("MinWidth", 8);
            PlayerPrefs.SetFloat("MaxHeight", 17);
            PlayerPrefs.SetFloat("MinHeight", 8);
            PlayerPrefs.SetFloat("MaxDepth", 22);
            PlayerPrefs.SetFloat("MinDepth", 18);
            PlayerPrefs.SetInt("MaxPlats", 40);
            PlayerPrefs.SetInt("MinPlats", 25);
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("AIMin", 3);
            PlayerPrefs.SetInt("AIMax", 7);
        }
        else if(DifficultyDropdown.value == 2)
        {
            PlayerPrefs.SetFloat("MinGameWidth", -50);
            PlayerPrefs.SetFloat("MaxGameWidth", 50);
            PlayerPrefs.SetFloat("MinGameLength", -50);
            PlayerPrefs.SetFloat("MaxGameLength", 50);
            PlayerPrefs.SetFloat("MaxWidth", 20);
            PlayerPrefs.SetFloat("MinWidth", 10);
            PlayerPrefs.SetFloat("MaxHeight", 30);
            PlayerPrefs.SetFloat("MinHeight", 5);
            PlayerPrefs.SetFloat("MaxDepth", 20);
            PlayerPrefs.SetFloat("MinDepth", 10);
            PlayerPrefs.SetInt("MaxPlats", 100);
            PlayerPrefs.SetInt("MinPlats", 15);
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("AIMin", 6);
            PlayerPrefs.SetInt("AIMax", 10);
        }
        else if(DifficultyDropdown.value == 3)
        {
            PlayerPrefs.SetFloat("MinGameWidth", -40);
            PlayerPrefs.SetFloat("MaxGameWidth", 40);
            PlayerPrefs.SetFloat("MinGameLength", -40);
            PlayerPrefs.SetFloat("MaxGameLength", 40);
            PlayerPrefs.SetFloat("MaxWidth", 20);
            PlayerPrefs.SetFloat("MinWidth", 10);
            PlayerPrefs.SetFloat("MaxHeight", 40);
            PlayerPrefs.SetFloat("MinHeight", 5);
            PlayerPrefs.SetFloat("MaxDepth", 20);
            PlayerPrefs.SetFloat("MinDepth", 10);
            PlayerPrefs.SetInt("MaxPlats", 125);
            PlayerPrefs.SetInt("MinPlats", 15);
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("AIMin", 15);
            PlayerPrefs.SetInt("AIMax", 20);
        }
        else if(DifficultyDropdown.value == 4)
        {
            PlayerPrefs.SetFloat("MinGameWidth", -30);
            PlayerPrefs.SetFloat("MaxGameWidth", 30);
            PlayerPrefs.SetFloat("MinGameLength", -30);
            PlayerPrefs.SetFloat("MaxGameLength", 30);
            PlayerPrefs.SetFloat("MaxWidth", 15);
            PlayerPrefs.SetFloat("MinWidth", 10);
            PlayerPrefs.SetFloat("MaxHeight", 35);
            PlayerPrefs.SetFloat("MinHeight", 5);
            PlayerPrefs.SetFloat("MaxDepth", 25);
            PlayerPrefs.SetFloat("MinDepth", 10);
            PlayerPrefs.SetInt("MaxPlats", 50);
            PlayerPrefs.SetInt("MinPlats", 5);
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("AIMin", 15);
            PlayerPrefs.SetInt("AIMax", 25);
        }
        else if(DifficultyDropdown.value == 5)
        {
            PlayerPrefs.SetFloat("MinGameWidth", -25);
            PlayerPrefs.SetFloat("MaxGameWidth", 25);
            PlayerPrefs.SetFloat("MinGameLength", -25);
            PlayerPrefs.SetFloat("MaxGameLength", 25);
            PlayerPrefs.SetFloat("MaxWidth", 15);
            PlayerPrefs.SetFloat("MinWidth", 10);
            PlayerPrefs.SetFloat("MaxHeight", 30);
            PlayerPrefs.SetFloat("MinHeight", 5);
            PlayerPrefs.SetFloat("MaxDepth", 25);
            PlayerPrefs.SetFloat("MinDepth", 10);
            PlayerPrefs.SetInt("MaxPlats", 40);
            PlayerPrefs.SetInt("MinPlats", 10);
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("AIMin", 25);
            PlayerPrefs.SetInt("AIMax", 50);
        }
        else if (DifficultyDropdown.value == 6)
        {
            PlayerPrefs.SetFloat("MinGameWidth", -50);
            PlayerPrefs.SetFloat("MaxGameWidth", 50);
            PlayerPrefs.SetFloat("MinGameLength", -50);
            PlayerPrefs.SetFloat("MaxGameLength", 50);
            PlayerPrefs.SetFloat("MaxWidth", 20);
            PlayerPrefs.SetFloat("MinWidth", 10);
            PlayerPrefs.SetFloat("MaxHeight", 30);
            PlayerPrefs.SetFloat("MinHeight", 5);
            PlayerPrefs.SetFloat("MaxDepth", 20);
            PlayerPrefs.SetFloat("MinDepth", 10);
            PlayerPrefs.SetInt("MaxPlats", 100);
            PlayerPrefs.SetInt("MinPlats", 15);
            PlayerPrefs.SetInt("Score", 0);
            PlayerPrefs.SetInt("AIMin", 50);
            PlayerPrefs.SetInt("AIMax", 140);
        }
        MinGameLength.text = PlayerPrefs.GetFloat("MinGameLength").ToString();
        MaxGameLength.text = PlayerPrefs.GetFloat("MaxGameLength").ToString();
        MinGameWidth.text = PlayerPrefs.GetFloat("MinGameWidth").ToString();
        MaxGameWidth.text = PlayerPrefs.GetFloat("MaxGameWidth").ToString();
        MaxPlats.text = PlayerPrefs.GetInt("MaxPlats").ToString();
        MinPlats.text = PlayerPrefs.GetInt("MinPlats").ToString();
        Seed.text = PlayerPrefs.GetString("SeedString");
        MinDepth.text = PlayerPrefs.GetFloat("MinDepth").ToString();
        MaxDepth.text = PlayerPrefs.GetFloat("MaxDepth").ToString();
        MaxWidth.text = PlayerPrefs.GetFloat("MaxWidth").ToString();
        MinWidth.text = PlayerPrefs.GetFloat("MinWidth").ToString();
        MaxHeight.text = PlayerPrefs.GetFloat("MaxHeight").ToString();
        MinHeight.text = PlayerPrefs.GetFloat("MinHeight").ToString();
        AIMin.text = PlayerPrefs.GetInt("AIMin").ToString();
        AIMax.text = PlayerPrefs.GetInt("AIMax").ToString();
        if (DifficultyDropdown.value == 0)
        {
            DifficultyDisplay.text = "Very Easy";
        }
        else if (DifficultyDropdown.value == 1)
        {
            DifficultyDisplay.text = "Easy";
        }
        else if (DifficultyDropdown.value == 2)
        {
            DifficultyDisplay.text = "Normal";
        }
        else if (DifficultyDropdown.value == 3)
        {
            DifficultyDisplay.text = "Hard";
        }
        else if (DifficultyDropdown.value == 4)
        {
            DifficultyDisplay.text = "Very Hard";
        }
        else if (DifficultyDropdown.value == 5)
        {
            DifficultyDisplay.text = "Impossible";
        }
        else if (DifficultyDropdown.value == 6)
        {
            DifficultyDisplay.text = "Insanity";
        }
        else if (DifficultyDropdown.value == 7)
        {
            DifficultyDisplay.text = "Custom";
        }
        
    }
    public void Quit()
    {
        #if UNITY_STANDALONE || UNITY_WEBGL
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #endif
    }
    public void SetEndlessLeaderboard()
    {
        Debug.Log(PlayerPrefs.GetString("EndlessHighScoreSeed"));
        Debug.Log(PlayerPrefs.GetString("Username"));
        if(PlayerPrefs.GetString("EndlessHighScoreSeed") != "" && PlayerPrefs.GetString("Username") != "")
        {
            LeaderboardCreator.UploadNewEntry(EndlessLeaderboardPublicKey, PlayerPrefs.GetString("Username"), Mathf.RoundToInt(PlayerPrefs.GetFloat("EndlessHighScore")), PlayerPrefs.GetString("EndlessHighScoreSeed"));

        }
        GetEndlessLeaderboard(); 

    }
    public int ClampValue(int Main, int Min, int Max)
    {
        if(Main > Max)
        {
            Main = Max;
            Debug.Log("Clamped Main to " + Max);
        }
        if(Main < Min)
        {
            Main = Min;
        }
        return Main;
    }
    public void GetEndlessLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(EndlessLeaderboardPublicKey, ((msg) =>
        {
            int LoopLength = (msg.Length < EndlessLeaderboardNames.Count) ? msg.Length : EndlessLeaderboardNames.Count;
            for (int i = 0; i < LoopLength; i++)
            {
                Debug.Log(msg[i].Extra);
                ScrollDistanceEndlessLeaderboard = ClampValue(ScrollDistanceEndlessLeaderboard, 0, msg.Length - EndlessLeaderboardNames.Count);
                if (msg[ScrollDistanceEndlessLeaderboard + i].IsMine())
                {
                    EndlessLeaderboardNames[i].color = Color.green;
                    EndlessLeaderboardScores[i].color = Color.green;
                    EndlessLeaderboardSeeds[i].color = Color.green;
                    EndlessLeaderboardRanks[i].color = Color.green;
                }
                else
                {
                    EndlessLeaderboardNames[i].color = Color.white;
                    EndlessLeaderboardScores[i].color = Color.white;
                    EndlessLeaderboardSeeds[i].color = Color.white;
                    EndlessLeaderboardRanks[i].color = Color.white;
                }
                if(msg[ScrollDistanceEndlessLeaderboard + i].Username == null)
                {
                    EndlessLeaderboardNames[i].text = "ERROR";
                }
                else
                {
                    EndlessLeaderboardNames[i].text = msg[ScrollDistanceEndlessLeaderboard + i].Username;
                }
                EndlessLeaderboardScores[i].text = msg[ScrollDistanceEndlessLeaderboard + i].Score.ToString();
                if(msg[ScrollDistanceEndlessLeaderboard + i].Extra == null)
                {
                    EndlessLeaderboardSeeds[i].text = "ERROR";
                }
                else
                {
                    EndlessLeaderboardSeeds[i].text = msg[ScrollDistanceEndlessLeaderboard + i].Extra;
                }

                EndlessLeaderboardRanks[i].text = msg[ScrollDistanceEndlessLeaderboard + i].Rank.ToString();
            }
        }));    
    }
    public void LoadTutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }
    public void SetObjectiveLeaderboard()
    {
        if (PlayerPrefs.GetString("ObjectiveHighScoreSeed") != "" && PlayerPrefs.GetString("Username") != "")
        {
            LeaderboardCreator.UploadNewEntry(ObjectiveLeaderboardPublicKey, PlayerPrefs.GetString("Username"), PlayerPrefs.GetInt("ObjectiveHighScore"), PlayerPrefs.GetString("ObjectiveHighScoreSeed"));

        }
        GetObjectiveLeaderboard();
    }
    public void GetObjectiveLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(ObjectiveLeaderboardPublicKey, ((msg) =>
        {
            int LoopLength = (msg.Length < ObjectiveLeaderboardNames.Count) ? msg.Length : ObjectiveLeaderboardNames.Count;
            for (int i = 0; i < LoopLength; i++)
            {
                ScrollDistanceObjectiveLeaderboard = ClampValue(ScrollDistanceObjectiveLeaderboard, 0, msg.Length - ObjectiveLeaderboardNames.Count);
                //Debug.Log(msg[i].Extra);
                if (msg[ScrollDistanceObjectiveLeaderboard + i].IsMine())
                {
                    ObjectiveLeaderboardNames[i].color = Color.green;
                    ObjectiveLeaderboardScores[i].color = Color.green;
                    ObjectiveLeaderboardSeeds[i].color = Color.green;
                    ObjectiveLeaderboardRanks[i].color = Color.green;
                }
                else
                {
                    ObjectiveLeaderboardNames[i].color = Color.white;
                    ObjectiveLeaderboardScores[i].color = Color.white;
                    ObjectiveLeaderboardSeeds[i].color = Color.white;
                    ObjectiveLeaderboardRanks[i].color = Color.white;
                }
                if(msg[ScrollDistanceObjectiveLeaderboard + i].Username == null)
                {
                    ObjectiveLeaderboardNames[i].text = "ERROR";
                }
                else
                {
                    ObjectiveLeaderboardNames[i].text = msg[ScrollDistanceObjectiveLeaderboard + i].Username;
                }
                ObjectiveLeaderboardScores[i].text = msg[ScrollDistanceObjectiveLeaderboard + i].Score.ToString();
                if(msg[ScrollDistanceObjectiveLeaderboard + i].Extra == null)
                {
                    ObjectiveLeaderboardScores[i].text = "ERROR";
                }
                else
                {
                    ObjectiveLeaderboardSeeds[i].text = msg[ScrollDistanceObjectiveLeaderboard + i].Extra;
                }

                ObjectiveLeaderboardRanks[i].text = msg[ScrollDistanceEndlessLeaderboard + i].Rank.ToString();
            }
        }));
    } 
    public void Settings()
    {
        if (SettingsOpen)
        {
            SettingsOpen = false;
            SettingsOBJ.SetActive(false);
        }
        else
        {
            SettingsOBJ.SetActive(true);
            SettingsOpen = true;
        }
    }
    public void SubmitUsername()
    {
        PlayerPrefs.SetString("Username", UsernameInput.text);
        UsernameInputter.SetActive(false);
    }
    public void SkipUsername()
    {
        UsernameInputter.SetActive(false);
    }
    public void GenerationSettings()
    {
        if(!GenerationSettingsOBJ.activeSelf)
        {
            GenerationSettingsOBJ.SetActive(true);
            GraphicsSettings.SetActive(false);
            PlayerSettingsOBJ.SetActive(false);
        }
    }
    public void NextLeaderBoardPage()
    {
        LeaderboardsPage++;
        if(LeaderboardsPage > LeaderBoardPages.Count-1)
        {
            LeaderboardsPage = 0;
        }
        for (int i = 0; i < LeaderBoardPages.Count; i++)
        {
            LeaderBoardPages[i].SetActive(false);
        }
        if(LeaderboardsPage == 1)
        {
            SetEndlessLeaderboard();
        }
        LeaderBoardPages[LeaderboardsPage].SetActive(true);
    }
    public void PreviousLeaderBoardPage()
    {
        LeaderboardsPage--;
        if (LeaderboardsPage < 0)
        {
            LeaderboardsPage = LeaderBoardPages.Count-1;
        }
        for (int i = 0; i < LeaderBoardPages.Count; i++)
        {
            LeaderBoardPages[i].SetActive(false);
        }
        if (LeaderboardsPage == 1)
        {
            SetEndlessLeaderboard();
        }
        LeaderBoardPages[LeaderboardsPage].SetActive(true);
    }
    public void OpenLeaderBoards()
    {
        if (LeaderboardsOpen)
        {
            LeaderboardsOpen = false;
        }
        else
        {
            LeaderboardsOpen = true;
        }
        LeaderBoards.SetActive(LeaderboardsOpen);
    }
    public void OpenGraphicsSettings()
    {
        if(!GraphicsSettings.activeSelf)
        {
            GraphicsSettings.SetActive(true);
            GenerationSettingsOBJ.SetActive(false);
            PlayerSettingsOBJ.SetActive(false);
        }
    }
    public void PlayerSettings()
    {
        if (!PlayerSettingsOBJ.activeSelf)
        {
            PlayerSettingsOBJ.SetActive(true);
            GenerationSettingsOBJ.SetActive(false);
            GraphicsSettings.SetActive(false);
        }
    }
    public void Play()
    {
        if (PlayerPrefs.GetString("SeedString") == "OG" || PlayerPrefs.GetString("SeedString") == "og" || PlayerPrefs.GetString("SeedString") == "num1")
        {
            LoadingScreen.SetActive(true);
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            LoadingScreen.SetActive(true);
            SceneManager.LoadScene("OutdoorsScene");
        }

    }
    public void PlayEndless()
    {
        if (PlayerPrefs.GetString("SeedString") == "OG" || PlayerPrefs.GetString("SeedString") == "og" || PlayerPrefs.GetString("SeedString") == "num1")
        {
            LoadingScreen.SetActive(true);
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            LoadingScreen.SetActive(true);
            SceneManager.LoadScene("Endless");
        }
    }
    public void ClearPlayerPrefs()
    {
        Debug.Log("ClearedPlayerPrefs");
        PlayerPrefs.DeleteAll();
    }
    public void ChangeMaxPlats()
    {
        
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("MaxPlats", int.Parse(MaxPlats.text));
    }
    public void ChangeMinPlats()
    {
        
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("MinPlats", int.Parse(MinPlats.text));
    }
    public void ChangeSeed()
    {
        string Seedstring = "";

        if(Seed.text == "SEED !> 4")
        {
            Seed.text = "";
        }
        else if(Seed.text.Length > 4 & Seed.text != "SEED !> 4")
        {
            Seed.text = "SEED !> 4";
        }
        else if(Seed.text.Length <= 4)
        {
            for (int i = 0; i < Seed.text.Length; i++)
            {
                for(int j = 0; j < Alphabet.Count; j++)
                {
                    if (Seed.text[i].ToString() == Alphabet[j])
                    {
                        Seedstring = Seedstring + j.ToString();
                    }
                }
                Debug.Log(Seedstring);
                PlayerPrefs.SetInt("Score", 0);
                PlayerPrefs.SetInt("Seed", int.Parse(Seedstring));
                PlayerPrefs.SetString("SeedString", Seed.text);
        }
        }

    }
    public void ChangeMinDepth()
    {
        
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetFloat("MinDepth", float.Parse(MinDepth.text));
    }
    public void ChangeMaxDepth()
    {
        
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetFloat("MaxDepth", float.Parse(MaxDepth.text));
    }
    public void ChangeMinWidth()
    {
        
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetFloat("MinWidth", float.Parse(MinWidth.text));
    }
    public void ChangeMaxWidth()
    {
        
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetFloat("MaxWidth", float.Parse(MaxWidth.text));
    }
    public void ChangeMinHeight()
    {
        
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetFloat("MinHeight", float.Parse(MinHeight.text));
    }
    public void ChangeMaxHeight()
    {
        
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetFloat("MaxHeight", float.Parse(MaxHeight.text));
    }
    public void ChangeMaxGameWidth()
    {
        
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetFloat("MaxGameWidth", float.Parse(MaxGameWidth.text));
    }
    public void ChangeMinGameWidth()
    {
        
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetFloat("MinGameWidth", float.Parse(MinGameWidth.text));
    }
    public void ChangeMaxGameLength()
    {
        
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetFloat("MaxGameLength", float.Parse(MaxGameLength.text));
    }
    public void ChangeMinGameLength()
    {
        
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetFloat("MinGameLength", float.Parse(MinGameLength.text));
    }
    public void ChangeAIMin()
    {
        
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("AIMin", int.Parse(AIMin.text));
    }
    public void ChangeAIMax()
    {
        
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("AIMax", int.Parse(AIMax.text));
    }
    public void SetDifficultyCustom()
    {
        PlayerPrefs.SetInt("Difficulty", 7);
        DifficultyDisplay.text = "Custom";
        DifficultyDropdown.value = 7;
    }
    public void ChangePage()
    {
        if(GenerationSettingsPage == 1)
        {
            GenerationSettingsPage = 0;
            GenerationSettingsOBJPG1.SetActive(true);
            GenerationSettingsOBJPG2.SetActive(false);
        }
        else if(GenerationSettingsPage == 0)
        {
            GenerationSettingsPage = 1;
            GenerationSettingsOBJPG1.SetActive(false);
            GenerationSettingsOBJPG2.SetActive(true);
        }
    }
}
