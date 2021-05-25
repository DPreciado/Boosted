using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapCheckPoint : MonoBehaviour
{
    public int Index;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CarLap>())
        {
            CarLap Car = other.GetComponent<CarLap>();

            if(Car.CheckpointIndex == Index + 1 || Car.CheckpointIndex == Index - 1)
            {
                Car.CheckpointIndex = Index;

                Debug.Log(Index);
            }
        }
    }
}
