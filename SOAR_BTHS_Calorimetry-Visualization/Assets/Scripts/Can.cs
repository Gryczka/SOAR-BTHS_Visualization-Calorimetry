using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour {
    public float emptyWeight;
    public float weightWithWater;
    public float initialTemp;
    public float finalTemp;

    public Vector3 inactivePosition;
    public Vector3 weighingPosition;
    public Vector3 activePosition;

    private bool interactionEnabled;

    private bool onSkewer;
    private bool droppedOnSkewer;

    private bool dragging = false;
    private float distance;
    private bool movedForward = false;
    private bool dragged = false;
    static private Quaternion origin = new Quaternion(0, 0, 0, 0);

    private void Start()
    {
        interactionEnabled = false;
        onSkewer = false;
        droppedOnSkewer = false;
    }

    void OnMouseDown()
    {
        if (interactionEnabled && !droppedOnSkewer)
        {
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            dragging = true;
            dragged = true;
        }
    }

    void OnMouseUp()
    {
        dragging = false;
        if (interactionEnabled && onSkewer)
        {
            gameObject.transform.rotation = origin;
            gameObject.transform.rotation = origin;
            gameObject.transform.position = activePosition;
            droppedOnSkewer = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnMouseEnter()
    {
        if (interactionEnabled && !onSkewer && !movedForward && !dragged)
        {
            gameObject.transform.Translate(0.1f, 0, -0.5f);
            movedForward = true;
        }
    }

    private void OnMouseExit()
    {
        if(interactionEnabled && !onSkewer && movedForward)
        {
            gameObject.transform.Translate(-0.1f, 0, 0.5f);
            movedForward = false;
        }
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
        if(interactionEnabled && other.gameObject.tag == "BTHS_GhostCan" && interactionEnabled)
        {
            onSkewer = true;
            other.gameObject.GetComponent<Renderer>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (interactionEnabled && other.gameObject.tag == "BTHS_GhostCan")
        {
            onSkewer = false;
            other.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }

    public bool GetDroppedInPlace()
    {
        return droppedOnSkewer;
    }

    public void updateObject(int step)
    {
        gameObject.transform.rotation = origin;
        gameObject.SetActive(step != 0);
        switch (step)
        {
            case 1:
            case 2:
            case 10:
            case 11:
                gameObject.transform.position = inactivePosition;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                interactionEnabled = false;
                break;
            case 3:
            case 4:
                gameObject.transform.position = weighingPosition;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                interactionEnabled = false;
                break;
            case 5:
                gameObject.transform.position = inactivePosition;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                interactionEnabled = true;
                break;
            case 6:
            case 7:
            case 8:
            case 9:
                gameObject.transform.position = activePosition;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                interactionEnabled = false;
                break;
        }
    }
}
