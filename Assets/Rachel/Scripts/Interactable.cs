using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public Image interactIndicator;

    private void Start()
    {
        interactIndicator.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactIndicator.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactIndicator.enabled = false;
        }
    }
}
