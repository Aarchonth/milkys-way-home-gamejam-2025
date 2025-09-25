using UnityEngine;

public class NextLevelCollider : MonoBehaviour
{
    public LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelManager.NewLevel();
            Destroy(gameObject);
        }
    }
}
