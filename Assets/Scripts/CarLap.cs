using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

public class CarLap : NetworkBehaviour
{
    public int lapNumber;
    public int CheckpointIndex;
    private void Start()
    {
        lapNumber = 1;
        CheckpointIndex = 0;
    }

    void Update()
    {
        if(lapNumber == 4)
        {
            Time.timeScale = 0f;
        }
    }
}
