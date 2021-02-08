using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class qingyang : Player
{
    // Player player;
    QinyangControls controls;
    Vector2 move;

    // Start is called before the first frame update
    void Awake() {
        // player = new Player();
        controls = new QinyangControls();
        controls.Gameplay.Attack.performed += context => Attack();
        controls.Gameplay.Move.performed += context => move = context.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += context => move = Vector2.zero;
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
		PlaySong(collisionSound);
    }

    void Update() {
        Vector3 m = new Vector3(move.x * currentSpeed, rb.velocity.y, move.y * currentSpeed) * Time.deltaTime;
        transform.Translate(m, Space.World);
    }

    void OnEnable() {
        controls.Gameplay.Enable();
    }

    void Ondisable() {
        controls.Gameplay.Disable();
    }

}
