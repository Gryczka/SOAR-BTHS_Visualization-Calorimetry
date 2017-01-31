using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Can : MonoBehaviour {
    public float emptyWeight;
    public float weightWithWater;
    public float initialTemp;
    public float finalTemp;

    public GameObject stateManagerContainer;
    private int step;
    private bool onSkewer;
    private bool droppedOnSkewer;

    private bool dragging = false;
    private float distance;
    private bool movedForward = false;
    static private Quaternion origin = new Quaternion(0, 0, 0, 0);
    void OnMouseDown()
    {
        if (!droppedOnSkewer && step == 5)
        {
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            dragging = true;
        }
    }

    void OnMouseUp()
    {
        dragging = false;
        if (onSkewer)
        {
            gameObject.transform.position = stateManagerContainer.GetComponent<StateManager>().canPosition6;
            gameObject.transform.rotation = origin;
            droppedOnSkewer = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        //else
        //{
        //    gameObject.transform.position = stateManagerContainer.GetComponent<StateManager>().canPosition0;
        //}
    }

    public void SetOnSkewer(bool onSkewer)
    {
        this.onSkewer = onSkewer;
    }

    public void SetDroppedOnSkewer(bool droppedOnSkewer)
    {
        this.droppedOnSkewer = droppedOnSkewer;
    }

    public bool GetDroppedOnSkewer()
    {
        return droppedOnSkewer;
    }

    private void OnMouseEnter()
    {
        if (step == 5 && !onSkewer && !movedForward)
        {
            gameObject.transform.Translate(0.1f, 0, -0.5f);
            movedForward = true;
        }
    }

    private void OnMouseExit()
    {
        if(step == 5 && !onSkewer && movedForward)
        {
            gameObject.transform.Translate(-0.1f, 0, 0.5f);
            movedForward = false;
        }
    }

    private void Start()
    {
        step = 1;
        onSkewer = false;
        droppedOnSkewer = false;
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
        if(other.gameObject.tag == "BTHS_GhostCan" && step == 5)
        {
            onSkewer = true;
            other.gameObject.GetComponent<Renderer>().enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BTHS_GhostCan" && step == 5)
        {
            onSkewer = false;
            other.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
