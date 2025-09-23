using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rbody;
    private float force = 1f;
    private float rotationSpeed = 550f;
    private float currentRotationInput = 0f;
    private float approachRotation = 2f;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float targetRotationInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            targetRotationInput = 1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetRotationInput = -1f;
        }

        currentRotationInput = Mathf.Lerp(currentRotationInput, targetRotationInput, approachRotation * Time.deltaTime);

        if (Mathf.Abs(currentRotationInput) > 0.01f)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * currentRotationInput * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector2 moveDirection = transform.up;
            rbody.AddForce(moveDirection * force);
        }
    }
}
