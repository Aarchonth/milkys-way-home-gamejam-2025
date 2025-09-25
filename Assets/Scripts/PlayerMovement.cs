using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Animator anim;
    [HideInInspector]
    public bool canMove = true;

    [Header("Movement Settings")]
    public float force = 5f;
    public float rotationSpeed = 200f;
    public float approachRotation = 5f;

    private float currentRotationInput = 0f;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canMove) return;

        bool right = Input.GetKey(KeyCode.D);
        bool left = Input.GetKey(KeyCode.A);
        bool up = Input.GetKey(KeyCode.S);

        anim.SetBool("isLeft", left);
        anim.SetBool("isRight", right);
        anim.SetBool("isUp", up);

        float targetRotationInput = 0f;
        if (!left && right) targetRotationInput = 1f;
        else if (!right && left) targetRotationInput = -1f;

        currentRotationInput = Mathf.Lerp(
            currentRotationInput,
            targetRotationInput,
            approachRotation * Time.deltaTime
        );
    }

    void FixedUpdate()
    {
        if (Mathf.Abs(currentRotationInput) > 0.01f)
        {
            float rotation = rotationSpeed * currentRotationInput * Time.fixedDeltaTime;
            rbody.MoveRotation(rbody.rotation + rotation);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector2 moveDirection = transform.up;
            rbody.AddForce(moveDirection * force);
        }
    }
}
