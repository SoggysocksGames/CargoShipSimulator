using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boatController : MonoBehaviour {

    public GameObject playerBoat;

    public float currentBoatSpeed;
    public float maximumBoatSpeed;
    public float slowdownRate; // Max = 100

    public float currentBoatTurnSpeed;
    public float minimumTurnSpeed;
    public float maximumTurnSpeed;

    public bool forwardControlManual;
    public bool backwardControlManual;

    public bool cruiseControl;
    //-----------------------------------------
    //Example Code

    public Text currentBoatSpeedText;
    public Text maximumBoatSpeedText;
    public Text slowdownRateText;

    public Text currentBoatTurnSpeedText;
    public Text minimumTurnSpeedText;
    public Text maximumTurnSpeedText;

    public GameObject wExample;
    public GameObject aExample;
    public GameObject sExample;
    public GameObject dExample;

    //-----------------------------------------

    // Use this for initialization
    void Start () {
        currentBoatSpeed = 0;
        forwardControlManual = false;
        backwardControlManual = false;

        //-----------------------------
        //Example Code
        wExample.SetActive(false);
        aExample.SetActive(false);
        sExample.SetActive(false);
        dExample.SetActive(false);
        //-----------------------------
    }

    // Update is called once per frame
    void Update() {
        currentBoatSpeedText.text = "Speed: " + currentBoatSpeed;
        maximumBoatSpeedText.text = "Max Speed: " + maximumBoatSpeed;
        slowdownRateText.text = "Slow Down Rate: " + slowdownRate;

        currentBoatTurnSpeedText.text = "Turn Speed: " + currentBoatTurnSpeed;
        minimumTurnSpeedText.text = "Min: " + minimumTurnSpeed;
        maximumTurnSpeedText.text = "Max: " + maximumTurnSpeed;

        //-----------------------------
        //Example Code
        if(Input.GetKeyDown(KeyCode.W))
        {
            wExample.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            wExample.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            aExample.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            aExample.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            sExample.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            sExample.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            dExample.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            dExample.SetActive(false);
        }
        //-----------------------------

        if (Input.GetKeyDown(this.GetComponent<keyBinding>().cruiseControlKey))
        {
            cruiseControl = !cruiseControl;
        }

        if (cruiseControl == false)
        {
            if (forwardControlManual == false)
            {
                if (currentBoatSpeed > 0)
                {
                    currentBoatSpeed -= currentBoatSpeed * (slowdownRate / 100);
                }
            }
            if (backwardControlManual == false)
            {
                if (currentBoatSpeed < 0)
                {
                    currentBoatSpeed -= currentBoatSpeed * (slowdownRate / 100);
                }
            }
        }


        //Moves boat
        playerBoat.transform.Translate(Vector3.forward * (currentBoatSpeed / 100)); // Positive = Forward, Negative = Backwards
        playerBoat.transform.Rotate(Vector3.up * currentBoatTurnSpeed); // Positive = Right, Negative = Left

        //max speeds
        if(currentBoatSpeed > 0)
        { 
            maximumTurnSpeed = (maximumBoatSpeed - currentBoatSpeed) / 100;
        }
        if (currentBoatSpeed < 0)
        {
            maximumTurnSpeed = (maximumBoatSpeed - (currentBoatSpeed - (currentBoatSpeed * 2))) / 100;
        }

        if (currentBoatTurnSpeed >= maximumTurnSpeed)
        {
            currentBoatTurnSpeed = maximumTurnSpeed;
        }

        if (currentBoatSpeed >= maximumBoatSpeed)
        {
            currentBoatSpeed = maximumBoatSpeed;
        }
        if (currentBoatSpeed <= (-maximumBoatSpeed / 5))
        {
            currentBoatSpeed = (-maximumBoatSpeed / 5);
        }



        // Turning Ease in Ease out
        if (currentBoatTurnSpeed <= -maximumTurnSpeed)
        {
            currentBoatTurnSpeed = -maximumTurnSpeed;
        }

        if (currentBoatTurnSpeed > minimumTurnSpeed)
        {
            currentBoatTurnSpeed -= 0.003f * (currentBoatTurnSpeed * 4);
        }
        if (currentBoatTurnSpeed < -minimumTurnSpeed)
        {
            currentBoatTurnSpeed -= 0.003f * (currentBoatTurnSpeed * 4);
        }

        if(currentBoatTurnSpeed > -minimumTurnSpeed && currentBoatTurnSpeed < (minimumTurnSpeed / 2))
        {
           currentBoatTurnSpeed -= 0.003f * (currentBoatTurnSpeed * 3);
        }
        if (currentBoatTurnSpeed < minimumTurnSpeed && currentBoatTurnSpeed > (minimumTurnSpeed / 2))
        {
            currentBoatTurnSpeed -= 0.003f * (currentBoatTurnSpeed * 3); ;
        }

        if (currentBoatTurnSpeed > (-minimumTurnSpeed / 2) && currentBoatTurnSpeed < 0)
        {
            currentBoatTurnSpeed -= 0.003f * currentBoatTurnSpeed;
        }
        if (currentBoatTurnSpeed < (minimumTurnSpeed / 2) && currentBoatTurnSpeed > 0)
        {
            currentBoatTurnSpeed -= 0.003f * currentBoatTurnSpeed;
        }

        if(currentBoatTurnSpeed < 0.01f && currentBoatTurnSpeed > -0.01f)
        {
            currentBoatTurnSpeed = 0;
        }

        ////////////
        //Controls//
        ////////////
        if (Input.GetKey(this.GetComponent<keyBinding>().forwardKey1) || Input.GetKey(this.GetComponent<keyBinding>().forwardKey2)) //Sets the 'forward' input
        {
            forwardControlManual = true;
            currentBoatSpeed += 0.1f;
        }
        else
        {
            forwardControlManual = false;
        }

        if (Input.GetKey(this.GetComponent<keyBinding>().backwardKey1) || Input.GetKey(this.GetComponent<keyBinding>().backwardKey2)) //Sets the 'reverse' input
        {
            backwardControlManual = true;
            currentBoatSpeed -= 0.1f;
        }
        else
        {
            backwardControlManual = false;
        }

        if(currentBoatSpeed >= 0)
        {
            if (Input.GetKey(this.GetComponent<keyBinding>().harshBrakeKey)) //Sets the 'brake' input
            {
                currentBoatSpeed -= 0.1f;

                if (currentBoatSpeed > -0.3f && currentBoatSpeed < 0.3f)
                {
                    currentBoatSpeed = 0f;
                }
            }
        }
        if (currentBoatSpeed <= 0)
        {
            if (Input.GetKey(this.GetComponent<keyBinding>().harshBrakeKey)) //Also sets the 'brake' input
            {
                currentBoatSpeed += 0.1f;

                if (currentBoatSpeed > -0.3f && currentBoatSpeed < 0.3f)
                {
                    currentBoatSpeed = 0f;
                }
            }
        }

        if (Input.GetKey(this.GetComponent<keyBinding>().turnLeftKey1) || Input.GetKey(this.GetComponent<keyBinding>().turnLeftKey2)) //Sets the 'forward' input
        {
            currentBoatTurnSpeed -= 0.011f;
        }
        if (Input.GetKey(this.GetComponent<keyBinding>().turnRightKey1) || Input.GetKey(this.GetComponent<keyBinding>().turnRightKey2)) //Sets the 'forward' input
        {
            currentBoatTurnSpeed += 0.011f;
        }
    }
}
