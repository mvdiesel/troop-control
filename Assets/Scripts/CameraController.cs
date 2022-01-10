using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform mainCameraTransform;

    private Vector3 newCameraPosition;

    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float cameraRotateSpeed;
    [SerializeField] private float movementTime;
    
    // Start is called before the first frame update
    void Start()
    {
        newCameraPosition = mainCameraTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        HandleRotationInput();
    }

    void HandleMovementInput()
    {
        //Camera movement
        if(Input.GetKey(KeyCode.W)){
            newCameraPosition += (transform.forward * cameraMoveSpeed);
        }
        if(Input.GetKey(KeyCode.S)){
            newCameraPosition += (transform.forward * -cameraMoveSpeed);
        }
        if(Input.GetKey(KeyCode.D)){
            newCameraPosition += (transform.right * cameraMoveSpeed);
        }
        if(Input.GetKey(KeyCode.A)){
            newCameraPosition += (transform.right * -cameraMoveSpeed);
        }
        
        //Lerp for smooth transitions
        transform.position = Vector3.Lerp(transform.position, newCameraPosition, Time.deltaTime * movementTime);
    }

    void HandleRotationInput()
    {
        if (Input.GetMouseButton(2))
        {
            transform.eulerAngles +=
                cameraRotateSpeed * new Vector3(Mathf.Clamp(-Input.GetAxis("Mouse Y"), -90, 90),
                    Mathf.Clamp(Input.GetAxis("Mouse X"), -90, 90), 0);
        }
    }
}
