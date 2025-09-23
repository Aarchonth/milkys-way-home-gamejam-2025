using UnityEngine;
using System.Collections; 

public class ObstacleScript : MonoBehaviour
{
    // Variables
    public Obstacles ObstacleType;
    public float StasisDuration = 2f;

    // Methods

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
        
    }
}