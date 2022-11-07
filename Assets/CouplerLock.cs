using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouplerLock : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public DistanceJoint2D distanceJoint2D;
    Vector3 dir;
    public LineRenderer lineRenderer;
    Vector2 mouseClickPos;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir = mouseClickPos - (Vector2)transform.position;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(mouseClickPos, dir, 100);
            if (raycastHit2D.collider!=null)
            {
                Rigidbody2D rigidbody2D = raycastHit2D.collider.GetComponent<Rigidbody2D>();
                if (rigidbody2D != null)
                {
                    distanceJoint2D.connectedBody = rigidbody2D;
                }
            }
            
        }
        lineRenderer.SetPositions(new Vector3[2] { transform.position, mouseClickPos });
        Debug.DrawRay(transform.position, dir * 100, Color.red);
    }
    private void OnMouseDown()
    {

    }
}
