using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour
{
    // Variables
    public Obstacles ObstacleType = Obstacles.None;
    public bool DestroyOnUse = true;
    public float StasisDuration = 2f;
    public float ImpulseForce = 1000f;
    public GameObject bubbleScreen;

    private Animator anim;

    void Start()
    {
        try { anim = GetComponent<Animator>(); }
        catch { anim = null; }
    }

    // Methods
    public enum Obstacles
    {
        None,
        StasisField,
        ImpulseField,
        Bubble
    }

    // Handles trigger events for the StasisField
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (ObstacleType == Obstacles.StasisField)
            {
                HandleStasisField(other);
            }
        }
    }

    // Handles collision events for the ImpulseField
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (ObstacleType == Obstacles.ImpulseField)
            {
                HandleImpulseField(collision.gameObject);
                if (DestroyOnUse)
                    Destroy(gameObject);
            }
            else if (ObstacleType == Obstacles.Bubble)
            {
                StartCoroutine(BubbleEffect());
            }
        }
    }

    IEnumerator BubbleEffect()
    {
        anim.SetTrigger("Hit");
        yield return new WaitForSeconds(1f);
        if (DestroyOnUse)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        GameObject bub = Instantiate(bubbleScreen, new Vector3(0, 0, -5), Quaternion.identity);
        yield return new WaitForSeconds(5f);
        Destroy(bub);
        if (DestroyOnUse)
            Destroy(gameObject);
    }

    public void HandleStasisField(Collider2D playerCollider)
    {
        // start Coroutine
        StartCoroutine(StasisTimer(playerCollider, StasisDuration));
    }

    IEnumerator StasisTimer(Collider2D playerCollider, float stasisDuration)
    {
        if (DestroyOnUse)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        // Take the PlayerMovement component from the player
        PlayerMovement playerMovement = playerCollider.GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            // Deaktivate the script to freeze the player
            playerMovement.canMove = false;
        }

        // wait for the duration of the stasis
        yield return new WaitForSeconds(stasisDuration);

        if (playerMovement != null)
        {
            // Aktivate the script to unfreeze the player
            playerMovement.canMove = true;
        }

        if (DestroyOnUse)
            Destroy(gameObject);
    }

    public void HandleImpulseField(GameObject player)
    {
        Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();

        if (playerRB != null)
        {
            Vector2 impulseDirection = playerRB.transform.position - transform.position;
            playerRB.linearVelocity = impulseDirection.normalized * ImpulseForce;

            Debug.Log("Impulse applied to player.");
        }
    }
}