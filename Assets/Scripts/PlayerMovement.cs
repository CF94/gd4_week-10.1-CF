using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera playerCamera;
    private Camera debugCamera;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float runMultiplier = 1.5f;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float gravity = 10f;

    public bool sprinting = false;

    public Vector3 originalPos;
    public GameObject player;

    public float mouseSensitivity = 100f;
    [SerializeField] float lookXLimit = 60f;
    float rotationX = 0;



    Vector3 moveDirection;
    CharacterController controller;

    void Start()
    {        
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        debugCamera = Camera.main;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        originalPos = new Vector3(0, 1, 0);
    }

    void Update()
    {
        Movement();
        PlayerCamera();
    }
    public void Movement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        float movementDirectionY = moveDirection.y;
        moveDirection = (horizontalInput * transform.right) + (verticalInput * transform.forward);

        if (controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
            else
            {
                moveDirection.y = -0.1f;
            }

        }
        else
        {
            moveDirection.y = movementDirectionY - (gravity * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) moveSpeed *= runMultiplier;
        if (Input.GetKeyUp(KeyCode.LeftShift)) moveSpeed /= runMultiplier;

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void PlayerCamera()
    {
        transform.Rotate(Vector3.up * mouseSensitivity * Time.deltaTime * Input.GetAxisRaw("Mouse X"));

        rotationX += -Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    public void DebugCamera()
    {
        transform.Rotate(Vector3.up * mouseSensitivity * Time.deltaTime * Input.GetAxisRaw("Mouse X"));

        rotationX += -Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }
}
