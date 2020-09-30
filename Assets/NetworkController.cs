using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class NetworkController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    [SerializeField]
    Button startbutton;
    [SerializeField]


    void Start()
    {
        startbutton.interactable = false;
        PhotonNetwork.ConnectUsingSettings();
        print("conecting...");
    }

    public override void OnConnectedToMaster()
    {
        print("connekted to cerver in" + PhotonNetwork.CloudRegion);
        startbutton.interactable = true;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void shooting()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
