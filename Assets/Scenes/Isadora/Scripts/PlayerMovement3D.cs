using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float gravity = 20.0f;
    Animator anim = null;
    private Vector3 moveDirection = Vector3.zero;
    private PlayerManager player;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerManager>();
        
    }

    void Update()
    {
        if (GameManager.instance.paused == false && GameManager.instance.playerManager.dead == false)
        {
            if (characterController.isGrounded)
            {
                // We are grounded, so recalculate
                // move direction directly from axes
                if ((Input.GetAxisRaw("Horizontal") !=  0  || Input.GetAxisRaw("Vertical") != 0) && player.health > 0)
                {
                    moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
                    moveDirection = moveDirection.normalized;
                    moveDirection *= speed;
                    //Set Walking Animation
                    GetComponent<Animator>().SetBool("Dead", false);
                    GetComponent<Animator>().SetBool("Idle", false);
                    GetComponent<Animator>().SetBool("Walking", true);
                }
                else
                {
                    moveDirection = new Vector3(0, 0, 0);
                    //Set Idle Animation
                    GetComponent<Animator>().SetBool("Dead", false);
                    GetComponent<Animator>().SetBool("Idle", true);
                    GetComponent<Animator>().SetBool("Walking", false);
                }
                
            }
            else
            {
                // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
                // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
                // as an acceleration (ms^-2)
                moveDirection.y -= gravity * Time.deltaTime;
            }

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);
        }
    }

    public void ResetMoveSpeed()
    {
        moveDirection = Vector3.zero;
    }

    private enum PlayerState
    {
        IDLE,
        WALKING,
        DEATH,
    }
}
