using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class TractorScript : MonoBehaviour
{
    private Vector3 initialScale;
    public float moveSpeed = 5f;
    private Vector3 target;
    private bool selected;
    private Vector3 targetPosition;
    Vector3 direction;

    private void Start()
    {
        target = transform.position;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            targetPosition = PlayerRaycast.hitPosition;
            targetPosition.y = transform.position.y;

            
            if (Vector3.Distance(transform.position, PlayerRaycast.hitPosition) > 0.1f)
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }
    }

    private void Awake()
    {
        initialScale = transform.localScale;
    }
    private void OnMouseEnter()
    {
        IncreaseScale(true);
    }
    private void OnMouseExit()
    {
        IncreaseScale(false);
    }    
    private void IncreaseScale(bool status)
    {
        Vector3 finalScale = initialScale;

        if (status) finalScale = initialScale * 1.1f;

        transform.localScale = finalScale;
    }
}