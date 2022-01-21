using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour {
    public Vector3 inactivePosition;
    public Vector3 activePosition;

    public void updateObject(int step)
    {
        gameObject.SetActive(step != 0);
        switch (step)
        {
            case 1:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 11:
                gameObject.transform.position = inactivePosition;
                break;
            case 2:
            case 3:
            case 4:
            case 10:
                gameObject.transform.position = activePosition;
                break;
        }
    }
}
