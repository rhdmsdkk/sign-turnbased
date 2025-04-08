using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public float speed = 12f;
    public float scale = 0.2f;

    private float time;

    private void Start()
    {
        time = 0f;
    }

    private void Update()
    {
        time += Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Oscillate(time, speed, scale), transform.position.z);
    }

    private float Oscillate (float time, float _speed, float _scale)
    {
        return Mathf.Cos(time * _speed / Mathf.PI) * _scale;
    }
}
