using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    
    Wander,
    Follow,
    Follow2,
    Attack,
    Die

};

public enum EnemyType{
    Melee,
    Ranged

};

public class EnemyController : MonoBehaviour
{
    GameObject player;
    GameObject player2;
    public EnemyState currState = EnemyState.Wander;
    public EnemyType enemyType;
    public float range;
    public float speed;
    public float attackRange;
    public float coolDown;
    private bool chooseDir = false;
    //private bool dead = false;
    private bool coolDownAttack = false;
    private Vector3 randomDir;
    
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(currState){
            case(EnemyState.Wander):
                Wander();
            break;
            case(EnemyState.Follow):
                Follow();
            break;
            case(EnemyState.Follow2):
                Follow2();
            break;
            case(EnemyState.Attack):
                Attack();
            break;
            case(EnemyState.Die):
                Death();
            break;
        }

        if(IsPlayerInRange(range) && currState != EnemyState.Die){

            currState = EnemyState.Follow;

        } else if(IsPlayer2InRange(range) && currState != EnemyState.Die){

            currState = EnemyState.Follow2;

        }else if(!IsPlayerInRange(range) && currState != EnemyState.Die){
            currState = EnemyState.Wander;
        }

        if(Vector3.Distance(transform.position, player.transform.position) <= attackRange){
            currState = EnemyState.Attack;
        } else if(Vector3.Distance(transform.position, player2.transform.position) <= attackRange){
            currState = EnemyState.Attack;
        }

    }

    private bool IsPlayerInRange(float range){

        return Vector3.Distance(transform.position, player.transform.position) <= range;
        
    }

    private bool IsPlayer2InRange(float range){

        return Vector3.Distance(transform.position, player2.transform.position) <= range;
        
    }

    private IEnumerator ChooseDirection(){
        chooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        randomDir = new Vector3(0,0, Random.Range(0, 360));
        Quaternion nextRotation = Quaternion.Euler(randomDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2.5f));
        chooseDir = false;
    }


    void Wander(){
        if(!chooseDir){
            StartCoroutine(ChooseDirection());
        }

        transform.position += transform.right * speed * Time.deltaTime;
        if(IsPlayerInRange(range)){
            currState = EnemyState.Follow;
        }

        if(IsPlayer2InRange(range)){
            currState = EnemyState.Follow2;
        }

    }

    void Follow(){
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Follow2(){
        transform.position = Vector2.MoveTowards(transform.position, player2.transform.position, speed * Time.deltaTime);
    }

    void Attack(){
        if(!coolDownAttack){
            switch(enemyType){

                case(EnemyType.Melee):
                    GameController.DamagePlayer(1);
                    StartCoroutine(CoolDown());
                break;
                case(EnemyType.Ranged):
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet=true;
                    StartCoroutine(CoolDown());
                break;
            }
        }
        /*
        if(!coolDownAttack){
            GameController.DamagePlayer(1);
            StartCoroutine(CoolDown());
        }*/
    }

    public void Death(){
        Destroy(gameObject);
    }
    

    private IEnumerator CoolDown(){
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

}
