using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System.Web;

public class TitleName : MonoBehaviour
{
    public static TitleName Instance;

    public TextMeshProUGUI bestScoreTitel;
    public string myNameSaved;
    public int highScoreSaved;

    public string myName;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();

        bestScoreTitel.text = "Highest Score: " + myNameSaved + " = " + highScoreSaved;
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }

    public void SetName(string enterName)
    {
        myName = enterName;
        Debug.Log(myName);
        SaveData();
    }

    [Serializable]
    class PlayerData
    {
        public string myNameSave;
        public int highScoreSave;
    }

    public void SaveData()
    {
        PlayerData data = new PlayerData();
        data.myNameSave = myNameSaved;
        data.highScoreSave = highScoreSaved;
        
       string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            myNameSaved = data.myNameSave;
            highScoreSaved = data.highScoreSave;
        }
    }
}
