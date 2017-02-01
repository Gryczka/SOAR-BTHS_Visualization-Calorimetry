using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour {
    public float massBeforeBurn;
    public float massAfterBurn;
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
        if (!inPosition && !droppedInPlace && step == 7)
        {
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            dragging = true;
        }
    }

    void OnMouseUp()
    {
        dragging = false;
        if (inPosition && step == 7)
        {
            gameObject.transform.position = stateManagerContainer.GetComponent<StateManager>().candlePosition6;
            gameObject.transform.rotation = origin;
            droppedInPlace = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnMouseEnter()
    {
        if (step == 7 && !inPosition && !movedForward)
        {
            gameObject.transform.Translate(0.1f, 0, -0.1f);
            movedForward = true;
        }
    }

    private void OnMouseExit()
    {
        if (step == 7 && !inPosition && movedForward)
        {
            gameObject.transform.Translate(-0.1f, 0, 0.1f);
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
        if (other.gameObject.tag == "BTHS_GhostCandle" && step == 7)
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
        if (other.gameObject.tag == "BTHS_GhostCandle" && step == 7)
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

    public void SetDroppedInPlace(bool droppedInPlace)
    {
        inPosition = droppedInPlace;
        this.droppedInPlace = droppedInPlace;
    }
}
