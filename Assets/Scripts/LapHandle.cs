using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LapHandle : MonoBehaviour
{
    public TMP_Text laps;
    public GameObject gameOver;
    public int CheckpointAmt;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarLap>())
        {
            CarLap Car = other.GetComponent<CarLap>();

            if(Car.CheckpointIndex == CheckpointAmt)
            {
                Car.CheckpointIndex = 0;
                Car.lapNumber++;

                laps.text ="Laps: "+ Car.lapNumber.ToString() +" / 3";
                if(Car.lapNumber == 4)
                {
                    gameOver.SetActive(true);
                }
                //Debug.Log(Car.lapNumber);
            }
        }
    }
}
