using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour {
    public float massBeforeBurn;
    public float massAfterBurn;

    public Vector3 inactivePosition;
    public Vector3 weighingPosition;
    public Vector3 activePosition;

    private bool dragging = false;
    private float distance;
    private bool movedForward = false;
    private bool inPosition;
    private bool droppedInPlace;
    private bool interactionEnabled;
    private bool dragged = false;
    static private Quaternion origin = new Quaternion(0, 0, 0, 0);
    void OnMouseDown()
    {
        if (interactionEnabled && !inPosition && !droppedInPlace)
        {
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            dragging = true;
            dragged = true;
        }
    }

    void OnMouseUp()
    {
        dragging = false;
        if (interactionEnabled && inPosition)
        {
            gameObject.transform.rotation = origin;
            gameObject.transform.position = activePosition;
            droppedInPlace = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnMouseEnter()
    {
        if(interactionEnabled && !inPosition && !movedForward && !dragged)
        {
            gameObject.transform.Translate(0.0f, 0.0f, -0.2f);
            movedForward = true;
        }
    }

    private void OnMouseExit()
    {
        if(interactionEnabled && !inPosition && !movedForward)
        {
            gameObject.transform.Translate(0.0f, 0.0f, 0.2f);
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
        if (interactionEnabled && other.gameObject.tag == "BTHS_GhostCandle" && interactionEnabled)
        {
            inPosition = true;
            foreach (Renderer renderer in other.gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (interactionEnabled && other.gameObject.tag == "BTHS_GhostCandle")
        {
            inPosition = false;
            foreach (Renderer renderer in other.gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
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
            case 3:
            case 4:
            case 5:
            case 11:
                gameObject.transform.position = inactivePosition;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                gameObject.GetComponentInChildren<Light>().enabled = false;
                interactionEnabled = false;
                break;
            case 2:
            case 10:
                gameObject.transform.position = weighingPosition;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                gameObject.GetComponentInChildren<Light>().enabled = false;
                interactionEnabled = false;
                break;
            case 6:
                gameObject.transform.position = inactivePosition;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                gameObject.GetComponentInChildren<Light>().enabled = false;
                interactionEnabled = false;
                break;
            case 7:
                gameObject.transform.position = inactivePosition;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                gameObject.GetComponentInChildren<Light>().enabled = true;
                interactionEnabled = true;
                break;
            case 8:
                gameObject.transform.position = activePosition;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                gameObject.GetComponentInChildren<Light>().enabled = true;
                interactionEnabled = false;
                break;
            case 9:
                gameObject.transform.position = activePosition;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                gameObject.GetComponentInChildren<Light>().enabled = false;
                interactionEnabled = false;
                break;
        }
    }
}
