using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField joinInput;
    public TMP_InputField createInput;

    public void AcceptClicked()
    {
        if (createInput.text == "")
        {
            JoinRoom();
        }
        else
        {
            CreateRoom();
        }

    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }
}
