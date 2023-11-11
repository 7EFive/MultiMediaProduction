using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator anime;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;


    // Update is called once per frame
    void Update()
    {
        horizontalMove= Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            anime.SetBool("IsJumping", true);
        }
    }
    public void OnLanding()
    {
        anime.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove* Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
