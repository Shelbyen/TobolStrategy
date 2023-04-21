using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject CameraObject;
    private GameObject CameraRotator;
    private GameObject CameraZoomer;
    private Camera MainCamera;

    public bool BlockCameraControl;
    public float MoveSpeed;
    public float RotationSpeed;
    public float ZoomSpeed;
    public float TiltSpeed;

    void Awake()
    {
        MainCamera = Camera.main;
        CameraZoomer = MainCamera.transform.parent.gameObject;
        CameraRotator = CameraZoomer.transform.parent.gameObject;
        CameraObject = CameraRotator.transform.parent.gameObject;
    }

    void Update()
    {
        if (!BlockCameraControl)
        {
            CameraMovement();
            CameraRotation();
            CameraZooming();
            CameraTilt();
        }
    }

    private void CameraMovement()
    {
        float AxisX = Input.GetAxis("Horizontal");
        float AxisZ = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * AxisZ * MoveSpeed * Time.deltaTime);
        transform.Translate(Vector3.right * AxisX * MoveSpeed * Time.deltaTime);
    }

    private void CameraRotation()
    {
        float Rotation = 0;
        if (InputManager.GetKey("CameraRotationControl")) Rotation = Input.GetAxis("Mouse X") * SettingsManager.Sensitivity;
        else if (InputManager.GetKey("RotateLeft")) Rotation = 1;
        else if (InputManager.GetKey("RotateRight")) Rotation = -1;
        if (InputManager.GetKeyDown("CameraRotationControl")) Cursor.lockState = CursorLockMode.Locked;
        if (InputManager.GetKeyUp("CameraRotationControl")) Cursor.lockState = CursorLockMode.Confined;

        transform.Rotate(Vector3.up * Rotation * RotationSpeed * Time.deltaTime);
    }

    private void CameraZooming()
    {
        float Scroll = Input.GetAxis("Mouse ScrollWheel");
        CameraZoomer.transform.localPosition += new Vector3(0, 0, Scroll * ZoomSpeed * Time.deltaTime);
        CameraZoomer.transform.localPosition = new Vector3(0, 0, Mathf.Clamp(CameraZoomer.transform.localPosition.z, -50, 0));
    }

    private void CameraTilt()
    {
        float Tilt = 0;
        if (InputManager.GetKey("CameraRotationControl")) Tilt = Input.GetAxis("Mouse Y");
        CameraRotator.transform.Rotate(Vector3.left * Tilt * TiltSpeed * SettingsManager.Sensitivity * Time.deltaTime);
        CameraRotator.transform.localEulerAngles = new Vector3(Mathf.Clamp(CameraRotator.transform.localEulerAngles.x, 20f, 60f), 0, 0);
        MainCamera.transform.localPosition = new Vector3(0, 0, -CameraRotator.transform.localEulerAngles.x / 9f);
    }
}