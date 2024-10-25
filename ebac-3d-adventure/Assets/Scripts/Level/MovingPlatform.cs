using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public float height = 3f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + Mathf.PingPong(Time.time * speed, height);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
