using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed;
    [SerializeField]
    private float cameraSensitivity;
    [SerializeField]
    private float minimumFov;
    [SerializeField]
    private float maximunFov;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Camera zooming controls
        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxisRaw("Mouse ScrollWheel") * cameraSensitivity;
        fov = Mathf.Clamp(fov, minimumFov, maximunFov);
        Camera.main.fieldOfView = fov;
    }

    private void FixedUpdate()
    {
        //Movement controls
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.Translate(movement.normalized * cameraSpeed * Time.fixedDeltaTime, Space.World);
    }
}