using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private int currentAct;
    private int currentEpisode;

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
        currentEpisode = Convert.ToInt32(currentScene.Substring(currentScene.Length - 1, 1));
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // can do any scene-specific initialization
        Debug.Log("Scene loaded: " + scene.name);
    }

    public void LoadAct(int act)
    {
        currentAct = act;
        SceneManager.LoadScene("Episode" + currentEpisode + "Act" + act);
    }

    public void LoadEpisode(int episode)
    {
        currentEpisode = episode;
        SceneManager.LoadScene("Episode" + episode);
    }

    public void LoadCurrentEpisode()
    {
        SceneManager.LoadScene("Episode" + currentEpisode);
    }
}
