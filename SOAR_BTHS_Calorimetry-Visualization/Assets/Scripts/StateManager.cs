using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour {
    public Text stepIndicator;

    public GameObject can;
    public GameObject glassRod;
    public GameObject ringStand;
    public GameObject candle;
    public Light candleLight;
    public GameObject chimney;

    public Vector3 origin;

    public Vector3 ringStandPosition0;
    public Vector3 ringStandPosition1;

    public Vector3 canPosition0;
    public Vector3 canPosition1;

    public Vector3 rodPosition0;
    public Vector3 rodPosition1;

    public Vector3 candlePosition0;
    public Vector3 candlePosition1;

    public Vector3 chimneyPosition0;
    public Vector3 chimneyPosition1;

    private int currentStep;
    int finalStep = 4;
	// Use this for initialization
	void Start () {
        currentStep = 0;

        candleLight.enabled = false;

        updateObjects();
    }

    void Update()
    {
        stepIndicator.text = string.Concat("Step ", currentStep.ToString());
        updateObjects();
    }

    public void incrementStep()
    {
        if(currentStep < finalStep)
        {
            currentStep++;
            updateObjects();
        }
    }

    public void decrementStep()
    {
        if(currentStep > 0)
        {
            currentStep--;
            updateObjects();
        }
    }

    private void updateObjects()
    {
        switch (currentStep)
        {
            case 0:
                Debug.Log("Objects Updated to Step 0");
                candleLight.enabled = false;
                chimney.transform.position = chimneyPosition0;
                candle.transform.position = candlePosition0;
                can.transform.position = canPosition0;
                //glassRod.transform.position = rodPosition0;
                ringStand.transform.position = ringStandPosition0;
                break;
            case 1:
                ringStand.transform.position = ringStandPosition1;
                can.transform.position = canPosition1;
                //glassRod.transform.position = rodPosition1;
                candle.transform.position = candlePosition1;
                chimney.transform.position = chimneyPosition1;
                candleLight.enabled = false;
                break;
            case 2:
                candleLight.enabled = true;
                break;
            default:
                currentStep = 0;
                updateObjects();
                break;
        }
    }
}
