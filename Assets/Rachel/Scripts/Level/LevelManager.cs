using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private int currentLevel;
    private int currentChapter;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        } 
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        currentChapter = Convert.ToInt32(currentScene.Substring(currentScene.Length - 1, 1));
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // can do any scene-specific initialization
        Debug.Log("Scene loaded: " + scene.name);
    }

    public void LoadLevel(int level)
    {
        currentLevel = level;
        SceneManager.LoadScene("Chapter" + currentChapter + "Level" + level);
    }

    public void LoadChapter(int chapter)
    {
        currentChapter = chapter;
        SceneManager.LoadScene("Chapter" + chapter);
    }
}
