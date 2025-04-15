using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 0.1f;

    private Material mat;
    private Vector2 offset;
    private PlayerMovement player;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
        player = FindAnyObjectByType<PlayerMovement>();
    }

    void Update()
    {
        float playerSpeed = player.GetComponent<Rigidbody2D>().linearVelocityX;

        if (Mathf.Abs(playerSpeed) > 0f)
        {
            Scroll(playerSpeed * scrollSpeed);
        }
    }

    private void Scroll(float direction)
    {
        offset.x += Time.deltaTime * direction;
        mat.mainTextureOffset = offset;
    }
}
