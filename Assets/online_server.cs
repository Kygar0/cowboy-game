using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class online_server : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); 
    }

    // Update is called once per frame
    void Update()
    {
        print("hey i'm conekted" + PhotonNetwork.CloudRegion);
    }
}
