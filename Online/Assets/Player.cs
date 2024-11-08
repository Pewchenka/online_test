using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static bool dead;
    
    public PhotonView photonView;
    
    public GameObject bullet;
    public Transform shotDir;

    private float timeShot;
    public float startTime;
    
    public float offset;
    
    public float speed;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;
    void Start()
    {
        dead = false;
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        if (photonView.Owner.IsLocal)
            Camera.main.GetComponent<camera>().player = gameObject.transform;
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        if (timeShot <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bullet, shotDir.position, transform.rotation);
                timeShot = startTime;
            }
        }
        else
        {
            timeShot -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
        
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
        if (Input.GetKey(KeyCode.Escape))
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(0);
        }



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (photonView.IsMine && collision.gameObject.tag == "At")
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(0);
        }
    }
}

