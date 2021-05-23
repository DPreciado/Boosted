using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class Gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Singleton.StartHost();
    }

}
