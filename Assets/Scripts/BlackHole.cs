using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public bool isActive = true;
    private float minSpeed = 2f;
    private float speed;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (!isActive) return;
        transform.position += Vector3.down * Time.deltaTime * speed;

        float dir = transform.position.y - player.position.y;
        if (GameManager.instance.endless)
        {
            int score = GameObject.Find("UIManager").GetComponent<UIManager>().score;
            if (score % 100 == 0 && score > 0)
            {
                minSpeed += 0.5f;
            }
        }

        if (dir > 30)
            speed += minSpeed;
        else if (dir < 20)
            speed = minSpeed;
    }
}
