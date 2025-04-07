using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int currentLevel;

    public void LoadLevel(int level)
    {
        currentLevel = level;
        SceneManager.LoadScene("Level" + level);
    }
}
