using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour
{
    // Variables
    public Obstacles ObstacleType = Obstacles.None;
    public bool DestroyOnUse = true;
    public float StasisDuration = 2f;
    public float ImpulseForce = 1000f;

    // Methods
    public enum Obstacles
    {
        None,
        StasisField,
        ImpulseField
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
        }
    }

    public void HandleStasisField(Collider2D playerCollider)
    {
        // start Coroutine
        StartCoroutine(StasisTimer(playerCollider, StasisDuration));
    }

    IEnumerator StasisTimer(Collider2D playerCollider, float stasisDuration)
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
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
        else
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
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