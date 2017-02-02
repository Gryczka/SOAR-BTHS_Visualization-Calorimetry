using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chimney : MonoBehaviour {
    public Vector3 inactivePosition;
    public Vector3 liftedPosition;
    public Vector3 activePosition;

    private bool interactionEnabled = false;
    private bool dragging = false;
    private float distance;
    private bool movedForward = false;
    private bool inPosition;
    private bool droppedInPlace;
    static private Quaternion origin = new Quaternion(0, 0, 0, 0);
    private bool dragged = false;
    void OnMouseDown()
    {
        if (interactionEnabled && !inPosition)
        {
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            dragging = true;
            dragged = true;
        }
    }

    void OnMouseUp()
    {
        dragging = false;
        if (inPosition)
        {
            gameObject.transform.rotation = origin;
            gameObject.transform.position = activePosition;
            droppedInPlace = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            interactionEnabled = false;
        }
    }

    private void OnMouseEnter()
    {
        if (interactionEnabled && !inPosition && !movedForward && !dragged)
        {
            gameObject.transform.Translate(-0.1f, 0, -0.5f);
            movedForward = true;
        }
    }

    private void OnMouseExit()
    {
        if (interactionEnabled && !inPosition && movedForward)
        {
            gameObject.transform.Translate(0.1f, 0, 0.5f);
            movedForward = false;
        }
    }

    private void Start()
    {
        interactionEnabled = false;
        inPosition = false;
        droppedInPlace = false;
    }

    private void Update()
    {
        if (dragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 rayPoint = ray.GetPoint(distance);
            rayPoint.z = -2.5f;
            transform.position = rayPoint;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BTHS_GhostChimney" && interactionEnabled)
        {
            inPosition = true;
            other.gameObject.GetComponent<Renderer>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BTHS_GhostChimney")
        {
            inPosition = false;
            other.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    public bool GetDroppedInPlace()
    {
        return droppedInPlace;
    }


    public void updateObject(int step)
    {
        gameObject.transform.rotation = origin;
        gameObject.SetActive(step != 0);
        switch (step)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 10:
            case 11:
                interactionEnabled = false;
                gameObject.transform.position = inactivePosition;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                break;
            case 5:
                interactionEnabled = true;
                gameObject.transform.position = inactivePosition;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                break;
            case 6:
            case 8:
            case 9:
                interactionEnabled = false;
                gameObject.transform.position = activePosition;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                break;
            case 7:
                interactionEnabled = false;
                gameObject.transform.position = liftedPosition;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                break;
        }
    }
}
