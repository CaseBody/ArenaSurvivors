using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField joinInput;
    public TMP_InputField createInput;
    public TMP_Text roomSizeText;
    public Slider roomSizeSlider;

    public void CreateRoomClicked()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = (byte)(roomSizeSlider.value + 1);
        PhotonNetwork.CreateRoom(createInput.text);

        transform.Translate(transform.forward);
    }


    public void onSliderChanged()
    {
        roomSizeText.text = "Size: " + (roomSizeSlider.value + 1);
    }

    public void JoinRoomClicked()
    {
        JoinRoom();
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }
}
