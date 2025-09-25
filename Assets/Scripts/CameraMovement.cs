using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 0.2f;

    [Header("Offsets")]
    public Vector2 baseOffset = new Vector2(0f, 1f);
    public float velocityOffsetFactor = 0.3f;
    public float maxVelocityOffset = 2f;

    private float velocityY = 0.0f;
    private Rigidbody2D playerRb;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null) player = p.transform;
        }

        if (player != null)
            playerRb = player.GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        if (player == null) return;

        float targetY = player.position.y + baseOffset.y;

        if (playerRb != null)
        {
            float velocityOffset = playerRb.linearVelocity.y * velocityOffsetFactor;

            velocityOffset = Mathf.Clamp(velocityOffset, -maxVelocityOffset, maxVelocityOffset);

            targetY += velocityOffset;
        }

        float newY = Mathf.SmoothDamp(transform.position.y, targetY, ref velocityY, smoothTime);

        transform.position = new Vector3(0f, newY, transform.position.z);
    }
}
