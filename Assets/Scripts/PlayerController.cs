using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    public float speed = 10;
    Rigidbody rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        animator.SetBool("Idle", true);
    }



    void Update()
    {
        // return if not the local player and is a network game 
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
        {
            return;
        }

        //transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        //characterController.SimpleMove(transform.forward * Input.GetAxis("Vertical") * speed);

        //transform.Rotate(0, transform.position.x, 0);

        if (Input.GetAxis("Horizontal") > 0.1 || Input.GetAxis("Horizontal") < -0.1 || Input.GetAxis("Vertical") > 0.1 || Input.GetAxis("Vertical") < -0.1)
        {
            Move();
        }
        else
        {
            animator.SetBool("Idle", true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnPunch();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnKick();
        }
    }

    void Move()
    {
        //transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime);
        //rb.velocity += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * speed * Time.deltaTime;
        //rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -5, 5), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -5, 5));

        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 newPosition = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.LookAt(newPosition + transform.position);
        transform.Translate(newPosition * speed * Time.deltaTime, Space.World);

        animator.SetBool("Idle", false);

        //animator.SetFloat("Speed", speed);
    }


    void OnPunch()
    {
        animator.SetTrigger("Punch");
    }

    void OnKick()
    {
        animator.SetTrigger("Kick");
    }
}