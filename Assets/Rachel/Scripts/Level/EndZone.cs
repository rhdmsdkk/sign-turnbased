using UnityEngine;

public class EndZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovement>(out PlayerMovement _))
        {
            LevelManager.instance.LoadChapter(1);
        }
    }
}
