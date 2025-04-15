using UnityEngine;
using UnityEngine.SceneManagement;

public class EndZone : MonoBehaviour
{
    public bool isCombat = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out PlayerMovement _))
        {
            if (isCombat)
            {
                string scene = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene("Combat_" + scene);
            }
            else
            {
                LevelManager.instance.LoadChapter(1);
            }
        }
    }
}
