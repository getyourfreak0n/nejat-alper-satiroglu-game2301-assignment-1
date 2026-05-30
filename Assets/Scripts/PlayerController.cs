using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    NewInputActions inputActions;
    Vector2 playerInput;
    Vector3 movement;
    Rigidbody rb;
    [SerializeField] float MovementSpeed = 10f;
    [SerializeField] float RotationSpeed = 250f;


    void Awake()
    {
        inputActions = new NewInputActions();
        rb = GetComponent<Rigidbody>();
    }


    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += handleMovement;
        inputActions.Player.Move.canceled += handleMovement;
    }

    void OnDisable()
    {
        inputActions.Disable();
        inputActions.Player.Move.performed -= handleMovement;
        inputActions.Player.Move.canceled -= handleMovement;
    }

    void FixedUpdate()
    {
        movement = new Vector3(playerInput.x * MovementSpeed, rb.linearVelocity.y, playerInput.y * MovementSpeed);
        
        rb.linearVelocity = movement;
        
        HandleRotation();
    }


    void handleMovement(InputAction.CallbackContext context)
    {
        playerInput = context.ReadValue<Vector2>();

    }

    void HandleRotation()
    {
        Vector3 lookDirection = new Vector3(playerInput.x, 0f, playerInput.y);
        
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, RotationSpeed * Time.deltaTime);
        }
        
        
    }
}
