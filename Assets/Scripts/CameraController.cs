using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotateSpeed = 16.0f;
    public float movmentSpeed = 10.0f;
    public float zoomSpeed = 30.0f;
    public float sensitivity=1f;

    private float _zoomLevel;
    private float _zoomPosition;
    private float _mult = 1f;

    private void Update() 
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");


        float rotate = 0f;
        if (Input.GetKey(KeyCode.Q)) {
            rotate = -1f;
        }
        if (Input.GetKey(KeyCode.E)) {
            rotate = 1f;
        }

        _mult = Input.GetKey(KeyCode.LeftShift) ? 2f : 1f;

        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * rotate * _mult, Space.World);

        transform.Translate(new Vector3(hor, 0, ver) * Time.deltaTime * _mult * movmentSpeed, Space.Self);

        _zoomLevel = Mathf.Clamp(Input.GetAxis("Mouse ScrollWheel") * sensitivity, -5, 5);
        _zoomPosition = Mathf.MoveTowards(_zoomPosition, _zoomLevel, zoomSpeed * Time.deltaTime);
        transform.position = transform.position + (Camera.main.transform.forward * _zoomPosition);

        if (Input.GetMouseButton(2)) {
            transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed * 30, 0);
        }
        if (Input.GetMouseButtonDown(2)) {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetMouseButtonUp(2)) {
            Cursor.lockState = CursorLockMode.Confined;
        }

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, -6, 60),
            transform.position.z
        );
    }
}