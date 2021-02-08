using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime;
    public bool isEnemyBullet = false;
    public Sprite TorchOn;
    private Vector2 lastPos;
    private Vector2 curPos;
    private Vector2 playerPos;
    private TorchController _torchController;


    // Start is called before the first frame update
    void Start()
    {
        // Coroutine are liked timed functions
        StartCoroutine(DeathDelay());
        if(!isEnemyBullet){
            transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnemyBullet){
            curPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);
            if(curPos == lastPos){
                Destroy(gameObject);
            }
            lastPos = curPos;
        }
    }

    public void GetPlayer(Transform player){
        playerPos = player.position;
    }

    IEnumerator DeathDelay(){
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col){
        //Debug.Log("Collide");
        if(col.tag == "Enemy" && !isEnemyBullet){
            col.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }

        if(col.tag == "Player" && isEnemyBullet){
            GameController.DamagePlayer(1);
            Destroy(gameObject);
        }

        if(col.tag == "Torch" && !isEnemyBullet)
        {
            col.gameObject.GetComponent<TorchController>().ChangeSprite(TorchOn);
            col.gameObject.GetComponent<TorchController>().isOn=true;
           // Debug.Log(col.gameObject.GetComponent<TorchController>().isOn);
            Destroy(gameObject);
           

        }

    }

}
