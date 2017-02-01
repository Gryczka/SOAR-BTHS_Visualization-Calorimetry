using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour {
    public string step1Description;
    public string step2Description;
    public string step3Description;
    public string step4Description;
    public string step5Description;
    public string step6Description;
    public string step7Description;
    public string step8Description;
    public string step9Description;
    public string step10Description;
    public string step11Description;

    public Text stepIndicator;
    public Text stepDescriber;

    public Text weightOfCandleBefore;
    public Text weightOfCandleAfter;
    public Text weightOfWaxBurned;

    public Text weightOfCanEmpty;
    public Text weightOfCanFull;
    public Text weightOfWaterInCan;

    public Text initialTemperature;
    public Text temperatureAfter;

    public GameObject can;
    public GameObject ghostCan;
    public GameObject candle;
    public GameObject ghostCandle;
    public GameObject skewer;
    public Light candleLight;
    public GameObject chimney;
    public GameObject ghostChimney;
    public GameObject balance;

    public Vector3 canPosition0;
    public Vector3 canPosition2;
    public Vector3 canPosition4;
    public Vector3 canPosition6;

    public Vector3 candlePosition0;
    public Vector3 candlePosition1;
    public Vector3 candlePosition4;
    public Vector3 candlePosition6;

    public Vector3 chimneyPosition0;
    public Vector3 chimneyPosition4;
    public Vector3 chimneyPosition6;

    public Vector3 balancePosition0;
    public Vector3 balancePosition1;

    private int currentStep;

    private float waterWeight = 0;
    private float weightOfWax = 0;

    private Vector3 chimneyPosition6_Elevated;
    int finalStep = 11;

    // Use this for initialization

    static private Quaternion origin = new Quaternion(0, 0, 0, 0);
	void Start () {
        currentStep = 1;

        candleLight.enabled = false;

        updateObjects();
        chimneyPosition6_Elevated = chimneyPosition6;
        chimneyPosition6_Elevated.y += 0.1f;
    }

    public int getCurrentStep()
    {
        return currentStep;
    }

    void Update()
    {
        stepIndicator.text = string.Concat("Step ", currentStep.ToString());
        //updateObjects();
        if (can.GetComponent<Can>().GetDroppedOnSkewer())
        {
            ghostCan.GetComponent<Renderer>().enabled = false;
        }

        if (candle.GetComponent<Candle>().GetDroppedInPlace())
        {
            ghostCandle.GetComponent<Renderer>().enabled = false;
            foreach (Renderer renderer in ghostCandle.GetComponentsInChildren<Renderer>())
            {
                renderer.enabled = false;
            }
        }

        if (chimney.GetComponent<Chimney>().GetDroppedInPlace())
        {
            ghostChimney.GetComponent<Renderer>().enabled = false;
        }
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
        if(currentStep > 1)
        {
            currentStep--;
            updateObjects();
        }
    }

    private void updateObjects()
    {
        switch (currentStep)
        {
            case 1:
                //ghostCan.SetActive(false);
                //ghostCan.GetComponent<Renderer>().enabled = false;
                //ghostCandle.GetComponent<Renderer>().enabled = false;
                ghostCan.GetComponent<Renderer>().enabled = false;
                ghostChimney.GetComponent<Renderer>().enabled = false;
                foreach (Renderer renderer in ghostCandle.GetComponentsInChildren<Renderer>())
                {
                    renderer.enabled = false;
                }
                //ghostCandle.SetActive(false);
                candleLight.enabled = false;
                chimney.transform.position = chimneyPosition0;
                candle.transform.position = candlePosition0;
                can.transform.position = canPosition0;
                balance.transform.position = balancePosition0;
                chimney.transform.rotation = origin;
                candle.transform.rotation = origin;
                can.transform.rotation = origin;
                chimney.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                candle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                can.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                skewer.SetActive(false);
                stepDescriber.text = step1Description;
                break;
            case 2:
                candle.transform.position = candlePosition1;
                chimney.transform.position = chimneyPosition0;
                balance.transform.position = balancePosition1;
                chimney.transform.rotation = origin;
                candle.transform.rotation = origin;
                can.transform.rotation = origin;
                candleLight.enabled = false;
                chimney.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                candle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                can.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                can.transform.position = canPosition0;
                skewer.SetActive(false);
                stepDescriber.text = step2Description;
                weightOfCandleBefore.text = string.Concat(candle.GetComponent<Candle>().massBeforeBurn.ToString(), " g");
                break;
            case 3:
                can.transform.position = canPosition2;
                chimney.transform.position = chimneyPosition0;
                candle.transform.position = candlePosition0;
                balance.transform.position = balancePosition1;
                chimney.transform.rotation = origin;
                candle.transform.rotation = origin;
                can.transform.rotation = origin;
                chimney.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                candle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                can.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                candleLight.enabled = false;
                skewer.SetActive(false);
                stepDescriber.text = step3Description;
                weightOfCanEmpty.text = string.Concat(can.GetComponent<Can>().emptyWeight.ToString(), " g");
                break;
            case 4:
                candleLight.enabled = false;
                candle.transform.position = candlePosition0;
                skewer.SetActive(false);
                chimney.transform.position = chimneyPosition0;
                can.transform.position = canPosition2;
                balance.transform.position = balancePosition1;
                stepDescriber.text = step4Description;
                weightOfCanFull.text = string.Concat(can.GetComponent<Can>().weightWithWater.ToString(), " g");
                waterWeight = can.GetComponent<Can>().weightWithWater - can.GetComponent<Can>().emptyWeight;
                weightOfWaterInCan.text = string.Concat(waterWeight.ToString(), " g");
                chimney.transform.rotation = origin;
                candle.transform.rotation = origin;
                can.transform.rotation = origin;
                chimney.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                candle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                can.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                break;
            case 5:
                can.transform.position = canPosition0;
                skewer.SetActive(true);
                balance.transform.position = balancePosition0;
                can.GetComponent<Can>().SetOnSkewer(false);
                can.GetComponent<Can>().SetDroppedOnSkewer(false);
                can.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                chimney.transform.position = chimneyPosition0;
                chimney.GetComponent<Chimney>().SetDroppedInPlace(false);
                chimney.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                //candle.transform.position = candlePosition4;
                stepDescriber.text = step5Description;
                initialTemperature.text = string.Concat(can.GetComponent<Can>().initialTemp.ToString(), " C");
                chimney.transform.rotation = origin;
                candle.transform.rotation = origin;
                can.transform.rotation = origin;
                break;
            case 6:
                skewer.SetActive(true);
                can.transform.position = canPosition6;
                chimney.transform.position = chimneyPosition6;
                can.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                chimney.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                can.GetComponent<Can>().SetOnSkewer(true);
                //candleLight.enabled = true;
                //candle.transform.position = candlePosition4;
                candle.transform.position = candlePosition0;
                candle.GetComponent<Candle>().SetDroppedInPlace(false);
                candle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                stepDescriber.text = step6Description;
                chimney.transform.rotation = origin;
                candle.transform.rotation = origin;
                can.transform.rotation = origin;
                break;
            case 7:
                //candle.transform.position = candlePosition6;
                //balance.transform.position = balancePosition0;
                chimney.transform.position = chimneyPosition6_Elevated;
                //skewer.SetActive(true);
                //candleLight.enabled = true;
                candleLight.enabled = true;
                chimney.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                can.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                //candle.transform.position = candlePosition6;
                chimney.transform.rotation = origin;
                candle.transform.rotation = origin;
                can.transform.rotation = origin;
                stepDescriber.text = step7Description;
                break;
            case 8:
                candle.transform.position = candlePosition6;
                chimney.transform.position = chimneyPosition6;
                can.transform.position = canPosition6;
                balance.transform.position = balancePosition0;
                stepDescriber.text = step8Description;
                candleLight.enabled = true;
                chimney.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                candle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                can.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                break;
            case 9:
                skewer.SetActive(true);
                //candleLight.enabled = false;
                stepDescriber.text = step9Description;
                chimney.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                candle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                can.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                candleLight.enabled = false;
                temperatureAfter.text = string.Concat(can.GetComponent<Can>().finalTemp.ToString(), " C");
                candle.transform.position = candlePosition6;
                chimney.transform.position = chimneyPosition6;
                can.transform.position = canPosition6;
                chimney.transform.rotation = origin;
                candle.transform.rotation = origin;
                can.transform.rotation = origin;
                balance.transform.position = balancePosition0;
                break;
            case 10:
                skewer.SetActive(false);
                stepDescriber.text = step10Description;
                chimney.transform.position = chimneyPosition0;
                candle.transform.position = candlePosition1;
                can.transform.position = canPosition0;
                balance.transform.position = balancePosition1;
                weightOfCandleAfter.text = string.Concat(candle.GetComponent<Candle>().massAfterBurn.ToString(), " g");
                weightOfWax = candle.GetComponent<Candle>().massBeforeBurn - candle.GetComponent<Candle>().massAfterBurn;
                weightOfWaxBurned.text = string.Concat(weightOfWax.ToString(), " g");
                chimney.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                candle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                can.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                chimney.transform.rotation = origin;
                candle.transform.rotation = origin;
                can.transform.rotation = origin;
                break;
            case 11:
                candleLight.enabled = false;
                chimney.transform.position = chimneyPosition0;
                candle.transform.position = candlePosition0;
                can.transform.position = canPosition0;
                balance.transform.position = balancePosition0;
                skewer.SetActive(false);
                stepDescriber.text = step11Description;
                chimney.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                candle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                can.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                chimney.transform.rotation = origin;
                candle.transform.rotation = origin;
                can.transform.rotation = origin;
                break;
            default:
                currentStep = 1;
                updateObjects();
                break;
        }
    }
}
