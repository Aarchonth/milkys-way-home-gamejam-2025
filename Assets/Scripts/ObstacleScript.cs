using UnityEngine;
using System.Collections; 

public class ObstacleScript : MonoBehaviour
{
    // Variables
    public Obstacles ObstacleType = Obstacles.None;
    public float StasisDuration = 2f;
    public float ImpulseForce = 10f;

    // Methods
    public enum Obstacles
    {
        None,
        StasisField,
        ImpulseField
    }

    void OnTriggerEnter(Collider other)
    {
        switch (ObstacleType)
        {
            case Obstacles.StasisField:
                HandleStasisField(other);
                break;
            case Obstacles.ImpulseField:
                HandleImpulseField(other);
                break;
            default:
                debug.log("No obstacle type selected"); 
        }
    }

    

    public void HandleStasisField(Collider playerCollider)
    {
        // start Coroutine
        StartCoroutine(StasisTimer(playerCollider, StasisDuration));
    }

    IEnumerator StasisTimer(Collider playerCollider, float stasisDuration)
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

    public void HandleImpulseField(Collider playerCollider)
    {
        Rigidbody2D playerRB = playerCollider.GetComponent<Rigidbody2D>();

            if (playerRB != null)
            {
                Vector2 impulseDirection = playerCollider.transform.position - this.transform.position;
                Vector2 impulseForce = impulseDirection.normalized * ImpulseForce;
                playerRB.AddForce(impulseForce, ForceMode2D.Impulse);
                

            Debug.Log("Impulse applied to player.");
        }
    }
}