using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectController : MonoBehaviour
{
    public GameObject cube;
    public LayerMask layer, layerMask;
    public List<GameObject> humans;
    private Camera _cam;
    private GameObject _cubeSelection;
    private RaycastHit _hit;

    private void Awake() { 
        _cam = Camera.main;
    }

    private void Update()
    {
        if (InputManager.GetKeyDown("Select"))
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hit, 1000f, layer))
            {
                _cubeSelection = Instantiate(cube, new Vector3(_hit.point.x, 1, _hit.point.z), Quaternion.identity);
            }
        }

        if (_cubeSelection)
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitDrag, 1000f, layer))
            {
                _cubeSelection.transform.localScale = new Vector3((_hit.point.x - hitDrag.point.x) * -1, (_hit.point.y - hitDrag.point.y), (_hit.point.z - hitDrag.point.z) * -1);
            }
        }

        if (Input.GetMouseButtonUp(0) && _cubeSelection) {

            RaycastHit[] hits = Physics.BoxCastAll(
                _cubeSelection.transform.position,
                _cubeSelection.transform.localScale,
                Vector3.up,
                Quaternion.identity,
                0,
                layerMask);

            foreach (var el in hits)
            {
                humans.Add(el.transform.gameObject);
            }

            Destroy(_cubeSelection);
        }
    }
}
