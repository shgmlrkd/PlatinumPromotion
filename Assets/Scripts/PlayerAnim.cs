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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("JumpStart"); 
        }

        animator.SetFloat("velocityY", move.VelocityY);

        animator.SetBool("IsGround", move.IsGround);
    }
}