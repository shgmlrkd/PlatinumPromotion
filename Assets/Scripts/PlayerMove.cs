using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float jump = 6.0f;

    private float walkSpeed = 3.0f;
    private float runSpeed = 5.0f;
    private float finalSpeed = 0.0f;

    private float rotateSpeed = 10.0f;

    private Rigidbody rigid;

    private Vector3 direction = Vector3.zero;

    private bool prevGround = false;
    private bool isGround = false;
    private bool isMovePlayer = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // 입력된 방향 벡터에 이동 속도를 곱해서
        // 실제 이동 속도 벡터를 계산한다
        Vector3 move = direction * finalSpeed;

        // Rigidbody의 현재 Y속도는 유지하고
        // 좌우/앞뒤 속도만 우리가 직접 설정해서 이동시킨다
        rigid.linearVelocity = new Vector3(move.x, rigid.linearVelocity.y, move.z);
    }

    void Update()
    {
        isMovePlayer = false;
        // 방향 벡터 초기화
        direction = Vector3.zero;

        // W, A, S, D, Space 로 움직임 (각 방향 벡터를 더함)
        if(Keyboard.current.wKey.isPressed)
        {
            direction += Vector3.forward;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            direction += Vector3.back;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            direction += Vector3.left;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            direction += Vector3.right;
        }

        if (isMovePlayer && Keyboard.current.shiftKey.isPressed)
        {
            finalSpeed = runSpeed;
        }
        else
        {
            finalSpeed = walkSpeed;
        }

        if (Keyboard.current.spaceKey.isPressed && isGround)    // 땅일 때만 점프 가능 (Rigidbody 물리 기반 점프)
        {
            rigid.linearVelocity = new Vector3(rigid.linearVelocity.x, jump, rigid.linearVelocity.z);
        }

        // 방향이 존재할 경우만 회전
        if (direction != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            
            rigid.MoveRotation(Quaternion.Slerp(rigid.rotation, targetRot, Time.deltaTime * rotateSpeed));
        }

        // 땅인지 아래 방향으로 레이를 쏴서 체크
        isGround = Physics.Raycast(transform.position, Vector3.down, 0.15f);

        // 땅이었는지 공중이었는지 체크해서 이전과 다를 때만 로그를 보여줌
        if(prevGround != isGround)
        {
            print($"isGround : {isGround}");
        }

        prevGround = isGround;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.15f);
    }
}