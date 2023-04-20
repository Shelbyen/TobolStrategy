using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    public bool isSelect;
    public LayerMask layer, layerMask;
    public List<GameObject> humans;
    public Texture2D cubeTexture;
    
    // public GameObject cube;
    // private GameObject _test;

    private Camera _cam;
    private float selX;
    private float selY;
    public Rect _cubeSelection;
    private RaycastHit _hit;
    private RaycastHit _hitDrag;

    private void Awake() { 
        _cam = Camera.main;
    }

    private void OnGUI()
    {
        if (isSelect)
        {
            GUI.DrawTexture(_cubeSelection, cubeTexture);
        }
    }

    private void Update()
    {
        if (InputManager.GetKeyDown("Select"))
        {
            foreach (var el in humans)
            {
                el.transform.GetChild(0).gameObject.SetActive(false);
            }

            humans.Clear();

            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hit, 1000f, layer))
            {
                selX = Input.mousePosition.x;
                selY = Input.mousePosition.y;
                _cubeSelection = new Rect(selX, Screen.height - selY, 1, 1);
                isSelect = true;

            }
        }
        
        if (_cubeSelection != null)
        {
            selX = Input.mousePosition.x;
            selY = Screen.height - Input.mousePosition.y;
            _cubeSelection.width = selX - _cubeSelection.x;
            _cubeSelection.height = selY - _cubeSelection.y;
        }

        if (Input.GetMouseButtonUp(0) && isSelect) {

            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hitDrag, 1000f, layer))
            {
                float xScale = (_hit.point.x - _hitDrag.point.x) * -1;
                float zScale = _hit.point.z - _hitDrag.point.z;

                if (xScale < 0.0f && zScale < 0.0f)
                {
                    xScale = -xScale;
                    zScale = -zScale;
                } else if (xScale < 0.0f)
                {
                    xScale = -xScale;
                } else if (zScale < 0.0f)
                {
                    zScale = -zScale;
                }

                RaycastHit[] hits = Physics.BoxCastAll(
                    _hit.point,
                    new Vector3(xScale, 0, zScale),
                    new Vector3(0, 100, 0),
                    Quaternion.identity,
                    0,
                    layerMask);

                foreach (var el in hits)
                {
                    humans.Add(el.transform.gameObject);
                    el.transform.GetChild(0).gameObject.SetActive(true);
                }
            }

            isSelect = false;
        }
    }
}
