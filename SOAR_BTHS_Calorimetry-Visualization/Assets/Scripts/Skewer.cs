using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skewer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateObject(int step)
    {
        switch (step)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 10:
            case 11:
                gameObject.SetActive(false);
                break;
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
                gameObject.SetActive(true);
                break;
        }
    }
}
