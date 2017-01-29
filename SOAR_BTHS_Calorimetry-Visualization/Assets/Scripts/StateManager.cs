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
    public GameObject candle;
    public GameObject skewer;
    public Light candleLight;
    public GameObject chimney;
    public GameObject balance;

    public Vector3 origin;

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
    int finalStep = 10;
	// Use this for initialization
	void Start () {
        currentStep = 1;

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
                candleLight.enabled = false;
                chimney.transform.position = chimneyPosition0;
                candle.transform.position = candlePosition0;
                can.transform.position = canPosition0;
                balance.transform.position = balancePosition0;
                skewer.SetActive(false);
                stepDescriber.text = step1Description;
                break;
            case 2:
                candle.transform.position = candlePosition1;
                chimney.transform.position = chimneyPosition0;
                balance.transform.position = balancePosition1;
                candleLight.enabled = false;
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
                break;
            case 5:
                can.transform.position = canPosition4;
                balance.transform.position = balancePosition0;
                chimney.transform.position = chimneyPosition4;
                candle.transform.position = candlePosition4;
                skewer.SetActive(true);
                candleLight.enabled = false;
                stepDescriber.text = step5Description;
                initialTemperature.text = string.Concat(can.GetComponent<Can>().initialTemp.ToString(), " C");
                break;
            case 6:
                skewer.SetActive(true);
                candleLight.enabled = true;
                chimney.transform.position = chimneyPosition4;
                can.transform.position = canPosition4;
                candle.transform.position = candlePosition4;
                stepDescriber.text = step6Description;
                break;
            case 7:
                candle.transform.position = candlePosition6;
                can.transform.position = canPosition6;
                balance.transform.position = balancePosition0;
                chimney.transform.position = chimneyPosition6;
                skewer.SetActive(true);
                candleLight.enabled = true;
                stepDescriber.text = step7Description;
                break;
            case 8:
                skewer.SetActive(true);
                candleLight.enabled = false;
                stepDescriber.text = step8Description;
                candle.transform.position = candlePosition6;
                can.transform.position = canPosition6;
                balance.transform.position = balancePosition0;
                chimney.transform.position = chimneyPosition6;
                temperatureAfter.text = string.Concat(can.GetComponent<Can>().finalTemp.ToString(), " C");
                break;
            case 9:
                skewer.SetActive(false);
                stepDescriber.text = step9Description;
                chimney.transform.position = chimneyPosition0;
                candle.transform.position = candlePosition1;
                can.transform.position = canPosition0;
                balance.transform.position = balancePosition1;
                weightOfCandleAfter.text = string.Concat(candle.GetComponent<Candle>().massAfterBurn.ToString(), " g");
                weightOfWax = candle.GetComponent<Candle>().massBeforeBurn - candle.GetComponent<Candle>().massAfterBurn;
                weightOfWaxBurned.text = string.Concat(weightOfWax.ToString(), " g");
                break;
            case 10:
                candleLight.enabled = false;
                chimney.transform.position = chimneyPosition0;
                candle.transform.position = candlePosition0;
                can.transform.position = canPosition0;
                balance.transform.position = balancePosition0;
                skewer.SetActive(false);
                stepDescriber.text = step10Description;
                break;
            default:
                currentStep = 1;
                updateObjects();
                break;
        }
    }
}
