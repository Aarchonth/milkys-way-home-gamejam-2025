using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GravityField2D : MonoBehaviour
{
    [Tooltip("Beschleunigung in m/s², die auf Objekte wirkt.")]
    public float gravityStrength = 9.81f;

    private void OnTriggerStay2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null)
        {
            Vector2 dir = (Vector2)(transform.position - rb.transform.position);
            float dist = dir.magnitude;
            if (dist < 0.01f) return;

            dir /= dist; // Normalisieren

            // Newtonsche Gravitation: a = G * M / r²
            float acceleration = gravityStrength / (dist * dist);

            // ForceMode2D.Acceleration existiert nicht, stattdessen ForceMode2D.Force verwenden
            rb.AddForce(dir * acceleration, ForceMode2D.Force);
        }
    }

}
