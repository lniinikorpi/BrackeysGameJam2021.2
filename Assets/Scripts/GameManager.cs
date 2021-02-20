using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public bool isMuted = false;
    public bool isPlayerAlive = true;
    public bool isPaused;
    public int score;
    public string path;
    public ScoreData data;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        path = Application.persistentDataPath + "/score.unicorn";
    }
    // Start is called before the first frame update
    void Start()
    {
        if(File.Exists(path))
        {
            data = SaveSystem.LoadData();
        }
        else
        {
            data = new ScoreData(0);
            SaveSystem.SaveScore();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
