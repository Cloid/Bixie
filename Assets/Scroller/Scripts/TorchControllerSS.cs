using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Fungus;
using Photon.Pun;

public class TorchControllerSS : MonoBehaviour
{
    // Attributes
    public GameObject torch;
    public int ritualTime;
    public GameObject bamboo;
    SpriteRenderer sprite;
    public Sprite darkSprite;
    public Sprite litSprite;
    public bool isLit;
    public GameObject EnemySpawn;
    public GameObject litBar;

    public Flowchart Test;
    public bool isMeiInside;

    bool holding;
    QinyangControls controls;
    Coroutine lastRoutine = null;
    private GameObject tempBar;
    private Vector3 mei_torch_position;
    private Vector3 qing_torch_position;
    private LitBar barControl;
    private GameObject qing;
    private GameObject mei;
    private Player2 meiControl;
    private bool isHit;
    private bool isCharging;
    private bool isLighting;
    private PhotonView photonView;
    private Animator anim;
    private Animator mei_anim;
    // Start is called before the first frame update

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        
        controls = new QinyangControls();
        // controls.Gameplay.Interact.performed += ctx => photonView.RPC("lantern",RpcTarget.All);//lantern();
        // controls.Gameplay.Interact.canceled += ctx => photonView.RPC("disableLantren",RpcTarget.All);//disableLantren();
        // controls.Gameplay.Interact.performed += ctx => lantern();
        // controls.Gameplay.Interact.canceled += ctx => disableLantren();
        
        // controls.Gameplay.Interact.performed += ctx => holding = true;
        // controls.Gameplay.Interact.canceled += ctx => holding = false;
        qing = GameObject.FindGameObjectWithTag("Player");
        mei = GameObject.FindGameObjectWithTag("Player2");

        meiControl = FindObjectOfType<Player2>();
        isHit = meiControl.isHit;

        anim = GetComponent<Animator>();
        // mei_anim = mei.GetComponent<Animator>();
        
    }
    void Start()
    {
        // Initialization
        sprite = torch.GetComponent<SpriteRenderer>();
        isLit = false;
        barControl = litBar.GetComponent<LitBar>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(holding){
        //     lantern();
        // }else{
        //     disableLantren();
        // }
        // print("actually lit: " + isLit);
        mei_torch_position = transform.position - mei.transform.position;
        qing_torch_position = transform.position - qing.transform.position;
        isMeiInside = (Mathf.Abs(mei_torch_position.x) <= 1.5f) && (Mathf.Abs(mei_torch_position.y) <= 1.5f) && (Mathf.Abs(mei_torch_position.z) <= 1.5f);
        // mei_torch_dist = mei_torch_position.x;
        // qing_torch_dist = qing_torch_position.x;

        isHit = meiControl.isHit;
        
        if (isHit)
        {
            disableLantren();
        }
        anim.SetBool("isCharging", isCharging);
        meiControl.isLighting = isCharging;
    }

    // private void OnTriggerEnter(Collider other) {
    //     if(other.CompareTag("Player2")){
    //         lightLantern();
    //     }
    // }

    // Lights lantern
    [PunRPC]
    public void lantern()
    {
        bool isMeiInside = (Mathf.Abs(mei_torch_position.x) <= 1.5f) && (Mathf.Abs(mei_torch_position.y) <= 1.5f) && (Mathf.Abs(mei_torch_position.z) <= 1.5f);
        // print("started ritual");
        // lastRoutine = StartCoroutine(lightLantern());
    }

    [PunRPC]
    public void disableLantren()
    {
        isCharging = false;
        isLighting = false;
        print("canceled ritual");
        litBar.SetActive(false);
        // StopCoroutine(lightLantern());
        // StopCoroutine(lastRoutine);
    }

    public IEnumerator lightLantern()
    {
        // Offset position above object bbox (in world space)
        // float offsetPosY = transform.position.y + 1.5f;

        // // Final position of marker above GO in world space
        // Vector3 offsetPos = new Vector3(transform.position.x, offsetPosY, transform.position.z);

        // // Calculate *screen* position (note, not a canvas/recttransform position)
        // Vector2 canvasPos;
        // Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

        // // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
        // RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPos);

        // Set
        // markerR.localPosition = canvasPos;

        // var spawnPoint = litBar.gameObject.transform;
        // var pos = new Vector3(transform.position.x, transform.position.y+96f,transform.position.z);
        // // var pos = RectTransformUtility.WorldToScreenPoint(Camera.main, litBar.transform.position);
        // tempBar = Instantiate(litBar, pos, transform.rotation);

        if (isLit == false)
        {
            print("this" + this);
            bool isQingInside = (Mathf.Abs(qing_torch_position.x) <= 1.5f) && (Mathf.Abs(qing_torch_position.y) <= 1.5f) && (Mathf.Abs(qing_torch_position.z) <= 1.5f);
            if (isMeiInside)
            {
                print("within the DIS");
                meiControl.isLighting = true;
                isCharging = true;
                litBar.SetActive(true);
                litBar.transform.position = new Vector3(
                        Camera.main.WorldToScreenPoint(transform.position).x,
                        Camera.main.WorldToScreenPoint(transform.position).y + 116f,
                        litBar.transform.position.z
                        );
                barControl.mask.fillAmount = 1;
                yield return new WaitForSeconds(ritualTime);
                litBar.SetActive(false);
                print("Lantern lit!");
                if (Test != null)
                {
                    //Test.StopAllBlocks();
                    Test.ExecuteBlock("blah");
                }

                sprite.sprite = litSprite;
                isLit = true;
                anim.SetTrigger("isActivated");

                // Play FMOD lit lantern sound
                FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/Lantern_Ignite", GetComponent<Transform>().position);
                if(gameObject.name == "TorchBB_TP"){
                    Debug.Log("TP Time");
                    if(PhotonNetwork.IsMasterClient){
                        PhotonNetwork.LoadLevel("Scroller_1_Boss");
                    }
                } else {
                    Destroy(bamboo);
                }
                if (EnemySpawn != null)
                {
                    Destroy(EnemySpawn);
                }
            }


        }else{
            meiControl.isLighting = false;
            isCharging = false;
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.tag == "Player2")
    //     {
    //         if (Test != null)
    //         {
    //             Test.StopAllBlocks();
    //             Test.ExecuteBlock("blah");
    //         }
    //         sprite.sprite = litSprite;
    //         isLit = true;

    //         // Play FMOD lit lantern sound
    //         FMODUnity.RuntimeManager.PlayOneShot("event:/Sounds/Lantern_Ignite", GetComponent<Transform>().position);

    //         Destroy(bamboo);
    //         if (EnemySpawn != null)
    //         {
    //             Destroy(EnemySpawn);
    //         }
    //     }



    // }


    public void darkLantern()
    {
        print("Lantern dark!");
        sprite.sprite = darkSprite;
        isLit = false;

    }


    // void OnEnable()
    // {
    //     controls.Gameplay.Enable();
    // }

    // void OnDisable()
    // {
    //     controls.Gameplay.Disable();
    // }
}
