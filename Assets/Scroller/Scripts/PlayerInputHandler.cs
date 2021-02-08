using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

//  [Serializable]
//  public class Multitype {
//    public Player x;
//    public Player2 y;
//  }

public class PlayerInputHandler : MonoBehaviour
{
    private ArrayList players = new ArrayList();
    private float index;
    private PlayerInput playerInput;
    private Player player1;
    private Player2 player2;
    QinyangControls controls;
    Vector2 move;


    void Awake() {
        // var player = null;
        
        playerInput = GetComponent<PlayerInput>();
        player1 = FindObjectOfType<Player>();
        player2 = FindObjectOfType<Player2>();
        players.Add(player1);
        players.Add(player2);
        // player1.playerIndex;
        index = playerInput.playerIndex;
        print("inx:"+index);

        // print("first:"+((Player)players[0]).
        // print("first:"+(Player)players[0]);
        // var player = players(p =>  == index);

        controls = new QinyangControls();
        // controls.Gameplay.Attack.performed += context => Attack();
        // controls.Gameplay.Move.performed += context => move = context.ReadValue<Vector2>();
        // controls.Gameplay.Move.canceled += context => move = Vector2.zero;

        //var player = player1.playerIndex;
        //for (int i = 0; i < players.Count; i++)
        //{
        //    // you could, of course, use any string variable to search for.
        //    if (players[i].playerIndex == index)
        //        lbl.Text = players.Items[i].ToString();
        //}

        //if (player1.playerIndex == index)
        //{
        //    Player player = player1;
        //} else if (player2.playerIndex == index)
        //{
        //    var player = player2;
        //}
        //print(player);
    }


    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    // void Update()
    // {
    //     var index = playerInput.playerIndex;
    //     print("inx:"+index);
    // }

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
        
        
    }

    public void onAttack(CallbackContext context) 
    {
        if(player1 != null && player2 != null)
        {
            if(index == 0)
            {
                player1.Attack();
            }
        }
        
        
    }

    public void onJump(CallbackContext context) 
    {
        if(player1 != null && player2 != null)
        {
            if(index == 0)
            {
                player1.Jump();
            }else{
                player2.Jump();
            }
        }
        
        
    }

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

    // void OnEnable() {
    //     controls.Gameplay.Enable();
    // }

    // void Ondisable() {
    //     controls.Gameplay.Disable();
    // }
}
