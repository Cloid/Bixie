﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SceneManagement;

public class PlayerInputHandler : MonoBehaviour
{
    // Input variables
    private ArrayList players = new ArrayList();
    private float index;
    private int sceneIndex;
    private PlayerInput playerInput;
    private Player player1;
    private Player2 player2;
    private CS_Control cs;
    QinyangControls controls;
    Vector2 move;


    // Initialization
    void Awake() {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        cs = GameObject.FindObjectOfType<CS_Control>();
        playerInput = GetComponent<PlayerInput>();
        player1 = FindObjectOfType<Player>();
        player2 = FindObjectOfType<Player2>();
        players.Add(player1);
        players.Add(player2);
        index = playerInput.playerIndex;
        print("inx:"+index);
        controls = new QinyangControls();
    }

    // onMove function 
    // Allows players to move through callback context from input device
    public void OnMove(CallbackContext context) 
    {
        if(player1 != null && player2 != null)
        {
            if(index == 0)
            {
                player1.SetInputVector(context.ReadValue<Vector2>());
            }else{
                player2.SetInputVector(context.ReadValue<Vector2>());
            }
        }

        if(sceneIndex == 3){
            cs.moveMe(context.ReadValue<Vector2>());
        }

    }

    // onAttack function 
    // Allows players to call their attack function through callback context from input device
    public void onAttack(CallbackContext context) 
    {
        if(player1 != null && player2 != null)
        {
            if(index == 0 && context.performed)
            {
                player1.Attack();
            }
        }
    }

    // onHeavyAttack function 
    // Allows players to call their attack function through callback context from input device
    public void onHeavyAttack(CallbackContext context)
    {
        if (player1 != null && player2 != null)
        {
            if (index == 0 && context.performed)
            {
                player1.HeavyAttack();
            }
        }
    }

    // onSpecial function 
    // Allows players to call their attack function through callback context from input device
    public void onSpecial(CallbackContext context)
    {
        if (player1 != null && player2 != null)
        {
            if (index == 0 && context.performed)
            {
                player1.Special();
            }
        }
    }

    // onJump function 
    // Allows players to call their jump function through callback context from input device
    public void onJump(CallbackContext context) 
    {
        if(player1 != null && player2 != null)
        {
            if(index == 0)
            {
                if(context.performed) player1.Dash();
            }else{
                if(context.performed) player2.Jump();
            }
        }
        
        
    }

    // onJump function 
    // Allows players to call their interact function through callback context from input device
    public void onInteract(CallbackContext context)
    {
        if (player1 != null && player2 != null)
        {
            if (index == 0)
            {
                if (player1.interactObj != null)
                {
                    player1.Interact(player1.interactObj);
                }
                
            }
            else
            {
                
                if (player2.interactObj != null)
                {
                    print("pls2");
                    player2.Interact(player2.interactObj);
                }
            }
        }
    }
}
