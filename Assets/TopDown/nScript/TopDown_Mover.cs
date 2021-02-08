using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDown_Mover : MonoBehaviour
{
   [SerializeField]
    private float MoveSpeed = 3f;

    [SerializeField]
    private int playerIndex = 0;
    
    private Rigidbody2D controller;
    
    private Vector3 moveDirection = Vector3.zero;
    private Vector2 inputVector = Vector2.zero;
    // Start is called before the first frame update
    private void Awake() {
        controller = GetComponent<Rigidbody2D>();
    }

    public int GetPlayerIndex(){
        return playerIndex;
    }
    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(inputVector.x, inputVector.y, 0).normalized;
        //moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= MoveSpeed * Time.deltaTime;

        controller.MovePosition(controller.transform.position + moveDirection);
    }
}
