using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //������� ������
    public Camera MainCamera;
    public GameObject CameraRotator;
    public GameObject CameraObject;

    //�����������
    public bool BlockCameraController;
    public float CameraSpeed;
    public float MovementZ;
    public float MovementX;
    public float AxisZ;
    public float AxisX;
    public float BorderMovementDistance;

    //�������� (��� Y)
    public bool BlockCameraRotation;
    public float CameraRotationSpeed;

    //���������
    public bool BlockRaycast;
    public GameObject SelectedObject;
    public RaycastHit SelectingHit;


    void Awake()
    {
        MainCamera = Camera.main;
        CameraRotator = MainCamera.gameObject.transform.parent.gameObject;
        CameraObject = CameraRotator.gameObject.transform.parent.gameObject;
    }

    void Update()
    {
        // if (!BlockCameraController) CameraMovement();
        // if (!BlockCameraRotation) CameraRotation();
        if (!BlockRaycast) SelectObject();

    }

    public void CameraMovement()
    {
        MovementZ = Input.GetAxis("Vertical") / Time.timeScale;
        MovementX = Input.GetAxis("Horizontal") / Time.timeScale;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - Screen.height * BorderMovementDistance) AxisZ += 2.2f * Time.deltaTime;
        else if (Input.GetKey("s") || Input.mousePosition.y <= Screen.height - Screen.height * (1 - BorderMovementDistance)) AxisZ -= 2.2f * Time.deltaTime;
        else if (AxisZ < 0.05 && AxisZ > -0.05) AxisZ = 0;
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - Screen.width * BorderMovementDistance) AxisX += 2.2f * Time.deltaTime;
        else if (Input.GetKey("a") || Input.mousePosition.x <= Screen.width - Screen.width * (1 - BorderMovementDistance)) AxisX -= 2.2f * Time.deltaTime;
        else if (AxisX < 0.05 && AxisX > -0.05) AxisX = 0;

        if (!Input.GetKey("w") && Input.mousePosition.y < Screen.height - Screen.height * BorderMovementDistance && AxisZ > 0) AxisZ -= 2.2f * Time.deltaTime;
        if (!Input.GetKey("s") && Input.mousePosition.y > Screen.height - Screen.height * (1 - BorderMovementDistance) && AxisZ < 0) AxisZ += 2.2f * Time.deltaTime;
        if (!Input.GetKey("d") && Input.mousePosition.x < Screen.width - Screen.width * BorderMovementDistance && AxisX > 0) AxisX -= 2.2f * Time.deltaTime;
        if (!Input.GetKey("a") && Input.mousePosition.x > Screen.width - Screen.width * (1 - BorderMovementDistance) && AxisX < 0) AxisX += 2.2f * Time.deltaTime;

        AxisZ = Mathf.Clamp(AxisZ, -1f / Time.timeScale, 1f / Time.timeScale);
        AxisX = Mathf.Clamp(AxisX, -1f / Time.timeScale, 1f / Time.timeScale);

        if (AxisZ != 0) MovementZ = AxisZ;
        if (AxisX != 0) MovementX = AxisX;

        if (MovementX != 0 || MovementZ != 0)
        {
            CameraObject.transform.Translate(Vector3.forward * MovementZ * CameraSpeed * Time.deltaTime);
            CameraObject.transform.Translate(Vector3.right * MovementX * CameraSpeed * Time.deltaTime);
        }
    }

    public void CameraRotation()
    {
        if (Input.GetMouseButton(2))
        {
            CameraObject.transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * CameraRotationSpeed, 0);
        }
        if (Input.GetMouseButtonDown(2))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetMouseButtonUp(2))
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void SelectObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(MainCamera.ScreenPointToRay(Input.mousePosition), out SelectingHit) && SelectingHit.transform.gameObject.GetComponent<Selectable>())
            {
                if (SelectedObject != null) DeselectObject();
                SelectedObject = SelectingHit.transform.gameObject;
                SelectedObject.GetComponent<Selectable>().SelectThis();
                Debug.Log(SelectedObject.GetComponent<Selectable>().Name + " is selected");
            }
        }
        if (Input.GetMouseButtonDown(1) && SelectedObject != null)
        {
            DeselectObject();
        }
    }

    public void DeselectObject()
    {
        Debug.Log(SelectedObject.GetComponent<Selectable>().Name + " is deselected");
        SelectedObject.GetComponent<Selectable>().DeselectThis();
        SelectedObject = null;
    }
}
