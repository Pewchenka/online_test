using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerInGame : MonoBehaviourPunCallbacks
{
    public GameObject PlayerPref;
    void Start()
    {
        Vector3 pos = new Vector3(Random.Range(-25f, 25f), Random.Range(-14f, 14f));
        PhotonNetwork.Instantiate(PlayerPref.name, pos, Quaternion.identity);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Player.dead == true)
        {
            //PhotonNetwork.Destroy(PlayerPref);
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(0);

        }
    }
}
