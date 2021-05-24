using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Transports.UNET;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public GameObject menuPanel;
    public TMP_InputField inputField;

    public void Host()
    {
        NetworkManager.Singleton.StartHost();
        menuPanel.SetActive(false);
    }
    public void Join()
    {
        //clicked join
        if(inputField.text.Length <= 0)
        {
            //NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = "127.0.0.1";
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = "177.227.46.140";
        }
        else
        {
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = inputField.text;
        }
        NetworkManager.Singleton.StartClient();
        menuPanel.SetActive(false);
    }


}
