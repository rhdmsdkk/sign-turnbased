using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    public int currentChapter;

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
