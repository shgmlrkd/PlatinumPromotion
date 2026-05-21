using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject playerObj;

    private PlayerMove playerMove;
    private Transform playerTransform;

    [SerializeField]
    private float height = 2.0f;
    [SerializeField]
    private float camDistance = 6.0f;
    [SerializeField]
    private float sensitivity = 1.5f;
    [SerializeField]
    private float pitchMin = 10.0f;
    [SerializeField]
    private float pitchMax = 70.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private void Awake()
    {
        playerMove = playerObj.GetComponent<PlayerMove>();
        playerTransform = playerObj.transform;
    }

    void Start()
    {
        transform.position = playerTransform.position;
        transform.rotation = Quaternion.Euler(30.0f, 0.0f, 0.0f);
    }

    private void LateUpdate()
    {
        HandleMouse();

        playerMove.HandleRotation(yaw);

        UpdateCameraPosition();
    }

    private void HandleMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yaw += mouseX * sensitivity;
        pitch += -mouseY * sensitivity;

        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
    }

    private void UpdateCameraPosition()
    {
        Quaternion rot = Quaternion.Euler(pitch, yaw, 0.0f);

        Vector3 offset = rot * new Vector3(0.0f, 0.0f, -camDistance);

        transform.position = playerTransform.position + offset;

        transform.LookAt(playerTransform.position + Vector3.up * height);
    }
}