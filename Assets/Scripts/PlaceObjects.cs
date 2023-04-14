using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjects : MonoBehaviour
{
    public LayerMask layer;

    private void Start() {
        PositionObject();
    }

    void Update()
    {
        PositionObject();

        if(Input.GetMouseButtonDown(0)) {
            Destroy(gameObject.GetComponent<PlaceObjects>());
        }

        if(Input.GetKeyUp(KeyCode.R)) {
            transform.Rotate(Vector3.up * 45);
        }
    }

    private void PositionObject() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 1000f, layer)) {
            transform.position = hit.point;
        }
    }
}
