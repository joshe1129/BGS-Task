using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    private Vector2 moveInput;
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Get movement input from the player and update Animator parameters
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        playerAnimator.SetFloat("Horizontal", moveX);
        playerAnimator.SetFloat("Vertical", moveY);
        playerAnimator.SetFloat("Speed", moveInput.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        // Move the player based on moveInput and speed using Rigidbody2D 
        if (playerRB != null)
        {
            playerRB.MovePosition(playerRB.position + moveInput * speed * Time.fixedDeltaTime);
        }
    }
}
