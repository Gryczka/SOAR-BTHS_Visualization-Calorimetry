using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chimney : MonoBehaviour {
    public GameObject stateManagerContainer;

    private bool dragging = false;
    private float distance;
    private bool movedForward = false;
    private int step;
    private bool inPosition;
    private bool droppedInPlace;
    static private Quaternion origin = new Quaternion(0, 0, 0, 0);
    void OnMouseDown()
    {
        if (!inPosition && step == 5)
        {
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            dragging = true;
        }
    }

    void OnMouseUp()
    {
        dragging = false;
        if (inPosition && step == 5)
        {
            gameObject.transform.position = stateManagerContainer.GetComponent<StateManager>().chimneyPosition6;
            gameObject.transform.rotation = origin;
            droppedInPlace = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnMouseEnter()
    {
        if (step == 5 && !inPosition && !movedForward)
        {
            gameObject.transform.Translate(-0.1f, 0, -0.5f);
            movedForward = true;
        }
    }

    private void OnMouseExit()
    {
        if (step == 5 && !inPosition && movedForward)
        {
            gameObject.transform.Translate(0.1f, 0, 0.5f);
            movedForward = false;
        }
    }

    private void Start()
    {
        step = 1;
        inPosition = false;
        droppedInPlace = false;
    }

    private void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            rayPoint.z = -3.53f;
            transform.position = rayPoint;
        }
        step = stateManagerContainer.GetComponent<StateManager>().getCurrentStep();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BTHS_GhostChimney" && step == 5)
        {
            inPosition = true;
            other.gameObject.GetComponent<Renderer>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BTHS_GhostChimney" && step == 5)
        {
            inPosition = false;
            other.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    public bool GetDroppedInPlace()
    {
        return droppedInPlace;
    }

    public void SetDroppedInPlace(bool droppedInPlace)
    {
        inPosition = droppedInPlace;
        this.droppedInPlace = droppedInPlace;
    }
}
