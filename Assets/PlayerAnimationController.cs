using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float inputDeadzone = 0.1f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool wasWalking; 

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.Update(0);
    }

    private void Update()
    {
        Vector2 input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        bool movingRight = input.x > inputDeadzone;
        bool movingLeft = input.x < -inputDeadzone;
        bool movingUp = input.y > inputDeadzone;
        bool movingDown = input.y < -inputDeadzone;

        bool isWalkingHorizontal = movingRight || movingLeft;
        animator.SetBool("IsWalking", isWalkingHorizontal);

        if (isWalkingHorizontal)
        {
            spriteRenderer.flipX = movingLeft;
            animator.SetBool("IsWalkingBack", false);
        }

        if (!isWalkingHorizontal)
        {
            animator.SetBool("IsWalkingBack", movingUp);
           // animator.SetBool("IsWalkingForward", movingDown);
        }

        if (wasWalking != isWalkingHorizontal)
        {
            animator.Update(0);
            wasWalking = isWalkingHorizontal;
        }
    }
}