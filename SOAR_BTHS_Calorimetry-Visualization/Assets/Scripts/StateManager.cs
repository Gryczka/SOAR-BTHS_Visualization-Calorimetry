using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour {
    private string step1Description = "For this lab, you will need the following materials: A candle with a pan, a small can, a chimney, and a balance. (You will also need a coathanger - not portrayed here.)";
    private string step2Description = "First, weigh the candle and pan together on the balance to the nearest 0.01 g. In this example, the weight is 1.02g.";
    private string step3Description = "Now, weigh the empty can on the balance to the nearest 0.01g. In this example, the can weighs 14.91g.";
    private string step4Description = "Fill the can 2/3 of the way with water, then weigh the now-fillled can, and determine the weight of the water. In this example, the can now weighs 264.91g, meaning that there is 250g of water in the can.";
    private string step5Description = "Finally, assemble the experiment as follows: suspend the can from the top of the hood with the coat hanger, and hang it within the chimney. Now, measure the temperature of the water to the nearest 0.1 degrees Celsius. In this example, it is at room temperature.";
    private string step6Description = "Now, you may light the candle. Exercise caution: it may be a small fire, but it is still a fire.";
    private string step7Description = "Lift the chimney, and place the candle and pan underneath it. Make sure that the flame is directly beneath the filled can.";
    private string step8Description = "Begin to stir the water with the thermometer. Be careful not to hit the edges of the can, as this could result in the thermometer measuring the temperature of the can and not the water.";
    private string step9Description = "When the thermometer reads 15 degrees higher than the initial reading - in this instance, 38 degrees - blow out the candle, but keep stirring until the temperature no longer increases. Once it is no longer increasing, record the temperature to the nearest 0.1 degrees Celsius.";
    private string step10Description = "Lift the chimney using a pair of foreceps, and carefully remove the pan. Make sure not to spill any of the wax!  Weigh the candle and pan again. Record the new weight. The difference between the initial weight and this weight is the mass of wax lost to burning.";
    private string step11Description = "Perform the necessary calculations, and disassemble the experiment. Use forceps to move both the can and the chimney - they are both hot from the candle!";

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

    public Image leftBanner;
    public Image rightBanner;
    public Image titleBanner;

    public GameObject can;
    public GameObject ghostCan;
    public GameObject candle;
    public GameObject ghostCandle;
    public GameObject skewer;
    public GameObject chimney;
    public GameObject ghostChimney;
    public GameObject balance;

    private int currentStep;

    private float waterWeight = 0;
    private float weightOfWax = 0;

    int finalStep = 11;

	void Start () {
        currentStep = 0;

        updateObjects();
        ghostCan.GetComponent<Renderer>().enabled = false;
        ghostCandle.GetComponent<Renderer>().enabled = false;
        foreach (Renderer renderer in ghostCandle.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = false;
        }
        ghostChimney.GetComponent<Renderer>().enabled = false;
    }

    public int getCurrentStep()
    {
        return currentStep;
    }

    void Update()
    {
        stepIndicator.text = string.Concat("Step ", currentStep.ToString());
        if (can.GetComponent<Can>().GetDroppedInPlace())
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
        titleBanner.gameObject.SetActive(currentStep == 0);
        leftBanner.gameObject.SetActive(currentStep != 0);
        rightBanner.gameObject.SetActive(currentStep != 0);
        switch (currentStep)
        {
            case 0:
                break;
            case 1:
                stepDescriber.text = step1Description;
                break;
            case 2:
                weightOfCandleBefore.text = string.Concat(candle.GetComponent<Candle>().massBeforeBurn.ToString(), " g");
                stepDescriber.text = step2Description;
                break;
            case 3:
                weightOfCanEmpty.text = string.Concat(can.GetComponent<Can>().emptyWeight.ToString(), " g");
                stepDescriber.text = step3Description;
                break;
            case 4:
                weightOfCanFull.text = string.Concat(can.GetComponent<Can>().weightWithWater.ToString(), " g");
                waterWeight = can.GetComponent<Can>().weightWithWater - can.GetComponent<Can>().emptyWeight;
                weightOfWaterInCan.text = string.Concat(waterWeight.ToString(), " g");
                stepDescriber.text = step4Description;
                break;
            case 5:
                stepDescriber.text = step5Description;
                break;
            case 6:
                stepDescriber.text = step6Description;
                break;
            case 7:
                initialTemperature.text = string.Concat(can.GetComponent<Can>().initialTemp.ToString(), " C");
                stepDescriber.text = step7Description;
                break;
            case 8:
                stepDescriber.text = step8Description;
                break;
            case 9:
                temperatureAfter.text = string.Concat(can.GetComponent<Can>().finalTemp.ToString(), " C");
                stepDescriber.text = step9Description;
                break;
            case 10:
                weightOfCandleAfter.text = string.Concat(candle.GetComponent<Candle>().massAfterBurn.ToString(), " g");
                weightOfWax = candle.GetComponent<Candle>().massBeforeBurn - candle.GetComponent<Candle>().massAfterBurn;
                weightOfWaxBurned.text = string.Concat(weightOfWax.ToString(), " g");
                stepDescriber.text = step10Description;
                break;
            case 11:
                stepDescriber.text = step11Description;
                break;
            default:
                currentStep = 1;
                updateObjects();
                break;
        }
        can.GetComponent<Can>().updateObject(currentStep);
        candle.GetComponent<Candle>().updateObject(currentStep);
        chimney.GetComponent<Chimney>().updateObject(currentStep);
        balance.GetComponent<Balance>().updateObject(currentStep);
        skewer.GetComponent<Skewer>().updateObject(currentStep);
    }
}
