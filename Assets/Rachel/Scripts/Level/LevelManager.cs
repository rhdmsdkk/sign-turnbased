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
        currentEpisode = 1;
        
        string currentScene = SceneManager.GetActiveScene().name;

        // index after episode
        int startIndex = currentScene.IndexOf("Episode") + "Episode".Length;

        // index of act
        int endIndex = currentScene.IndexOf("Act");

        if (endIndex > startIndex)
        {
            // get episode number
            string episodeString = currentScene.Substring(startIndex, endIndex - startIndex);
            
            if (int.TryParse(episodeString, out int episodeNumber))
            {
                currentEpisode = episodeNumber;
            }
        }
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
