using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BackpackItem : MonoBehaviour
{
    public float weight;
    public string title;
    public int id;
    public int type;

    private new Rigidbody rigidbody;
    private new Collider collider;
    private new Camera camera;

    public float forceFactor = 10f;
    public float angularFactor = 0.9f;
    public float zTarget = 6f;

    public LayerMask backpackLayer;

    public BackpackItemUI uiPrefab;

    private static int idCounter;

    private void Awake()
    {
        // generate unique id
        id = idCounter++;

        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        camera = Camera.main;
    }

    private void OnDisable()
    {
        // forbid interaction while disabled
        rigidbody.isKinematic = true;
        collider.enabled = false;
    }

    private void OnEnable()
    {
        rigidbody.isKinematic = false;
        collider.enabled = true;
    }

    // dragging functionality
    private bool dragging;

    private void OnMouseDown()
    {
        if (!enabled)
            return;

        dragging = true;
    }

    private void OnMouseUp()
    {
        if (!enabled)
            return;

        dragging = false;

        // check for backpack under cursor
        RaycastHit hit;
        if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 1000, backpackLayer))
            hit.collider.GetComponent<Backpack>().Put(this);
    }

    private void FixedUpdate()
    {
        if(dragging)
        {
            // push object to cursor and reduce rotation
            var delta = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zTarget)) - transform.position;
            rigidbody.velocity = delta * forceFactor;
            rigidbody.angularVelocity *= angularFactor;
        }
    }

    public void Push(Vector3 impulse)
    {
        rigidbody.AddForce(impulse, ForceMode.Impulse);
    }
}
