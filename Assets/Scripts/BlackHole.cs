using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public bool isActive = true;

    void Update()
    {
        if (!isActive) return;
        transform.position += Vector3.down * Time.deltaTime * 0.5f;
    }
}
