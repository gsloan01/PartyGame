using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using Photon.Pun;



public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
{

    [Tooltip("The current Health of our player")]

    public float health = 10.0f;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]

    public static GameObject LocalPlayerInstance;


    void Awake()

    {
        // #Important 
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized 
        if (photonView.IsMine)
        {
            PlayerManager.LocalPlayerInstance = this.gameObject;
        }
        // #Critical 
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load. 
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            this.health = (float)stream.ReceiveNext();
        }
    }

    void Start()
    {
        
    }



    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        health -= Time.deltaTime;

        if (health <= 0) GameManager.Instance.LeaveRoom();
    }

}