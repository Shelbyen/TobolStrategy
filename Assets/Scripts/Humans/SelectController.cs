using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    public bool isSelect;
    public LayerMask layer, layerMask;
    public List<GameObject> humans;
    public Texture2D cubeTexture;
    
    //public GameObject cube;
    //private GameObject _test;

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
                el.transform.GetComponent<Selectable>().DeselectThis();
            }

            humans.Clear();

            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hit, 1000f, layer))
            {
                // _test = Instantiate(cube, new Vector3(_hit.point.x, 1, _hit.point.z), Quaternion.identity);
                selX = Input.mousePosition.x;
                selY = Input.mousePosition.y;
                _cubeSelection = new Rect(selX, Screen.height - selY, 1, 1);
                isSelect = true;

            }
        }

        if (isSelect)
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
                float xScale = (_hit.point.x - _hitDrag.point.x);
                float zScale = _hit.point.z - _hitDrag.point.z;
                var rotate = Quaternion.identity;

                if (xScale < 0.0f && zScale < 0.0f)
                {
                    rotate.Equals(new Vector3(0, 180, 0));
                } else if (xScale < 0.0f) {
                    rotate.Equals(new Vector3(0, 0, 180));
                } else if (zScale < 0.0f) {
                    rotate.Equals(new Vector3(180, 0, 0));
                }

                //cube.Equals(rotate);
                //cube.transform.localScale = new Vector3(xScale, 2, zScale);

                RaycastHit[] hits = Physics.BoxCastAll(
                    new Vector3(_hit.point.x - xScale / 2, -50, _hit.point.z - zScale / 2), 
                    new Vector3(Mathf.Abs(xScale) / 2, 100, Mathf.Abs(zScale) / 2),
                    new Vector3(0, 100, 0),
                    rotate,
                    0,
                    layerMask);

                foreach (var el in hits)
                {
                    humans.Add(el.transform.gameObject);
                    el.transform.GetChild(0).gameObject.SetActive(true);
                    el.transform.GetComponent<Selectable>().SelectThis();
                }
            }

            isSelect = false;
        }
    }
}
