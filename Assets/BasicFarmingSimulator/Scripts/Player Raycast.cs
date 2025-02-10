using UnityEngine;
using System.Collections;
using Mono.Cecil.Cil;

public class PlayerRaycast : MonoBehaviour
{
    private Camera overheadCamera;
    public Transform rayOrigin;
    public float lineRange = 50f;

    public static Vector3 hitPosition;

    void Start()
    {
        overheadCamera = GetComponentInParent<Camera>();
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = overheadCamera.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit, lineRange))
            {
                hitPosition = hit.point;
                Debug.Log("Hit position " + hit.point);
                Debug.Log("Hit object: " + hit.collider.name);
            }
        }
    }
}