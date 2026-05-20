using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform playerTransform;

    private Vector3 posOffSet = new Vector3(0, 3.5f, -3.0f);

    private void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
    }

    void Start()
    {
        transform.position = playerTransform.position;
        transform.rotation = Quaternion.Euler(30.0f, 0.0f, 0.0f);
    }


    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = playerTransform.position + posOffSet;
    }
}