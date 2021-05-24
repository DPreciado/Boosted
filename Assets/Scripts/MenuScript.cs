using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Spawning;
using MLAPI.Transports.UNET;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public GameObject menuPanel;
    public TMP_InputField inputField;

    private void Start()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
    }

    private void ApprovalCheck(byte[] connectionData, ulong clientId, MLAPI.NetworkManager.ConnectionApprovedDelegate callback)
    {
        bool approve = false;
        string password = System.Text.Encoding.ASCII.GetString(connectionData);
        if(password == "pass")
        {
            approve = true;
        }
        callback(true, null, approve, new Vector3(2,0,0), Quaternion.identity);
    }

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
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = "192.168.56.1";
        }
        else
        {
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = inputField.text;
        }
        NetworkManager.Singleton.NetworkConfig.ConnectionData = System.Text.Encoding.ASCII.GetBytes("pass");
        NetworkManager.Singleton.StartClient();
        menuPanel.SetActive(false);
    }


}
