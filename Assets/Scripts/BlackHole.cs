using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public bool isActive = true;
    public float speed = 0.5f;

    void Update()
    {
        if (!isActive) return;
        transform.position += Vector3.down * Time.deltaTime * speed;
    }
}
