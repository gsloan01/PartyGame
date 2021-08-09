using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance { get { return instance; } }
    static GameManager instance;

    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;

    public bool showDebugs = false;
    public float lobbyWaitTime = 10;

    private void Start()
    {
        instance = this;
        if (PlayerManager.LocalPlayerInstance == null)
        {
            if (showDebugs) Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate 
            PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
        }
        else
        {
            if (showDebugs) Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        }
    }

    #region Photon Callbacks
    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    #endregion

    #region Public Methods
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    #endregion

    #region Private Methods
    void LoadArena()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            if (showDebugs) Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
        }

        if (showDebugs) Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
        PhotonNetwork.LoadLevel("CannonBall");
    }
    #endregion

    #region Photon Callbacks
    public override void OnPlayerEnteredRoom(Player other)
    {
        if (showDebugs) Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

        if (PhotonNetwork.IsMasterClient)
        {
            if (showDebugs) Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

            if (PhotonNetwork.CurrentRoom.PlayerCount > Launcher.Instance.maxPlayers * 0.5f)
            {
                StartCoroutine(StartArena());
            }
        }
    }

    IEnumerator StartArena()
    {
        yield return new WaitForSeconds(lobbyWaitTime);
        LoadArena();
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        if (showDebugs) Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects

        if (PhotonNetwork.IsMasterClient)
        {
            if (showDebugs) Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

            LoadArena();
        }
    }
    #endregion
    
}