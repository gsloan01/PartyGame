using Photon.Pun;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;



[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviourPun

{

    public float speed = 10;

    CharacterController characterController;



    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }



    void Update()

    {

        // return if not the local player and is a network game 

        if (!photonView.IsMine && PhotonNetwork.IsConnected)

        {

            return;

        }
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);

        characterController.SimpleMove(transform.forward * Input.GetAxis("Vertical") * speed);

    }

}