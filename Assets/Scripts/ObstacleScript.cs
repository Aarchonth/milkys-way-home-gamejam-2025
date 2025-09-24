using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour
{
    // Variables
    public Obstacles ObstacleType = Obstacles.ImpulseField;
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
        if (ObstacleType == Obstacles.StasisField)
        {
            HandleStasisField(other);
        }
    }

    // Handles collision events for the ImpulseField
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (ObstacleType == Obstacles.ImpulseField)
        {
            HandleImpulseField(collision);
        }
    }

    public void HandleStasisField(Collider2D playerCollider)
    {
        // start Coroutine
        StartCoroutine(StasisTimer(playerCollider, StasisDuration));
    }

    IEnumerator StasisTimer(Collider2D playerCollider, float stasisDuration)
    {
        // Take the PlayerMovement component from the player
        PlayerMovement playerMovement = playerCollider.GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            // Deaktivate the script to freeze the player
            playerMovement.enabled = false;
        }

        // wait for the duration of the stasis
        yield return new WaitForSeconds(stasisDuration);

        if (playerMovement != null)
        {
            // Aktivate the script to unfreeze the player
            playerMovement.enabled = true;
        }
    }

    public void HandleImpulseField(Collision2D collision)
    {
        Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();

        if (playerRB != null)
        {
            Vector2 impulseDirection = playerRB.transform.position - transform.position;
            playerRB.velocity = impulseDirection.normalized * ImpulseForce;

            Debug.Log("Impulse applied to player.");
        }
    }
}