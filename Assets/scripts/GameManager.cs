using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    public static int charecterPicked = 0;//0 - city man, 1 - farming man, 2 - woman
    public static int mapPicked = 1; //1 - city, 2 - town , 3 - nature
    public static float mapIndPos;
    public static Vector3 charIndPos;
    public static string[] leaderBoardText = {"0:0:0 - 0", "0:0:0 - 0", "0:0:0 - 0", "0:0:0 - 0", "0:0:0 - 0", "0:0:0 - 0", "0:0:0 - 0", "0:0:0 - 0", "0:0:0 - 0", "0:0:0 - 0", "0:0:0 - 0" };
    public static float[] scores = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    public static float volume = 0.5f;

    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void mapPick()
    {
        switch (mapPicked) {
            case 1:
                mapIndPos = 35;
                break;
            case 2:
                mapIndPos = 0;
                break;
            case 3:
                mapIndPos = -35;
                break;
        }
    }

    public static void scoreUpdate(string lastScoreText, float lastScore)
    {
        for(int i = 9; i >= 0; i--)
        {
            if(lastScore > scores[i])
            {
                scores[i+1] = scores[i];
                scores[i] = lastScore;
                leaderBoardText[i+1] = leaderBoardText[i];
                leaderBoardText[i] = lastScoreText;
            }
        }
    }

    [System.Serializable]
    class SaveData
    {
        public int charecterPicked;
        public int mapPicked;
        public string[] boardText;
        public float[] score;
        public float volume;
    }

    public static void save()
    {
        SaveData data = new SaveData();
        data.charecterPicked = charecterPicked;
        data.mapPicked = mapPicked;
        data.boardText = leaderBoardText;
        data.score = scores;
        data.volume = volume;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public static void Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            charecterPicked = data.charecterPicked;
            mapPicked = data.mapPicked;
            leaderBoardText = data.boardText;
            scores = data.score;
            volume = data.volume;
        }
    }
}
