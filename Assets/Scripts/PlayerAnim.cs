using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rigid;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    private void Update() 
    {
        float speed = new Vector3(rigid.linearVelocity.x, 0.0f, rigid.linearVelocity.z).magnitude; 
        
        animator.SetFloat("MoveX", speed / 3); 
    }
}
