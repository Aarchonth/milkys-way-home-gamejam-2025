using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<BlackHole>().isActive = false;
            Debug.Log("Player has died!");
            // For example, you could trigger an animation or reload the scene

            collision.gameObject.GetComponent<Animator>().SetTrigger("Die");
        }
    }
}
