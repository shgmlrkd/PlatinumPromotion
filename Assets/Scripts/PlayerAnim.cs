using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private PlayerMove move;

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
        move = GetComponent<PlayerMove>();
    }

    private void Update() 
    {
        // 대기, 걷기, 뛰기
        animator.SetFloat("MoveX", move.Direction.x, 0.1f, Time.deltaTime);
        animator.SetFloat("MoveZ", move.Direction.z, 0.1f, Time.deltaTime);
        animator.SetFloat("Speed", move.SpeedRatio);

        // 점프
        animator.SetFloat("velocityY", move.VelocityY);
        animator.SetBool("IsGround", move.IsGround);

        if (!move.IsGround && move.PrevGround)
        {
            animator.SetBool("IsFalling", true);
        }
        else if (move.IsGround)
        {
            animator.SetBool("IsFalling", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("JumpStart"); 
        }

        if (move.IsGround)
        {
            animator.SetTrigger("JumpEnd");
        }
    }
}