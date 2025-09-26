using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject button;

    void Start()
    {
        button.SetActive(false);
    }
    void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(currentPosition, Vector2.zero, Time.deltaTime);
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

        if (currentPosition == Vector2.zero)
        {
            button.SetActive(true);
        }
    }
}
