using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float jump = 6.0f;

    [SerializeField]
    private float walkSpeed = 3.0f;
    [SerializeField]
    private float runSpeed = 5.0f;
    private float finalSpeed = 0.0f;

    private float runRatio = 1.0f;
    private float walkRatio = 0.5f;

    private float rotSpeed = 12.0f;

    private Rigidbody rigid;
    public float VelocityY
    {
        get { return rigid.linearVelocity.y; }
    }

    private Vector3 direction = Vector3.zero;

    public Vector3 Direction { get; private set; }
    public float SpeedRatio { get; private set; }
    public bool IsRunning { get; private set; }

    private bool prevGround = false;
    public bool IsGround { get; private set; }
    public bool IsFalling { get; private set; }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckGround();
        HandleInput();
        HandleMovementState();
        HandleJump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HandleInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        direction = new Vector3(horizontal, 0.0f, vertical).normalized;

        Direction = direction;
    }

    private void HandleMovementState()
    {
        bool isMoving = direction.magnitude > 0.1f;

        IsRunning = isMoving && Input.GetKey(KeyCode.LeftShift);

        finalSpeed = IsRunning ? runSpeed : walkSpeed;

        if (!isMoving)
        {
            SpeedRatio = 0.0f;
        }
        else
        {
            SpeedRatio = IsRunning ? runRatio : walkRatio;
        }
    }

    private void Move()
    {
        Vector3 move = transform.TransformDirection(direction) * finalSpeed;

        rigid.linearVelocity = new Vector3(move.x, rigid.linearVelocity.y, move.z);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGround)
        {
            rigid.linearVelocity = new Vector3(rigid.linearVelocity.x, jump, rigid.linearVelocity.z);
        }
    }

    private void CheckGround()
    {
        prevGround = IsGround;

        IsGround = Physics.Raycast(transform.position, Vector3.down, 0.15f);

        if (prevGround != IsGround)
        {
            Debug.Log($"isGround : {IsGround}");
        }
    }

    private void CheckFalling()
    {
        IsFalling = rigid.linearVelocity.y < -0.5f;
    }

    public void HandleRotation(float yaw)
    {
        Quaternion target = Quaternion.Euler(0.0f, yaw, 0.0f);

        transform.rotation = Quaternion.Slerp(transform.rotation, target, rotSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.purple;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.15f);
    }
}