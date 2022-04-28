/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: 
 * Last Edited:
 * 
 * Description: Controls the ball and sets up the intial game behaviors. 
****/

/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ball : MonoBehaviour
{
    
    

    [Header("General Settings")]

    public float score;
    public GameObject paddle;
    public AudioClip sound;
    private AudioSource audioSource;
    public Text scoreTxt;
    public Text ballTxt;



    



    [Header("Ball Settings")]
    
    public float numBalls;
    public float speed;
    public Vector3 initialForce;
    public bool isInPlay=false;
    private Rigidbody rb;

   


 


    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource>();

    }//end Awake()


        // Start is called before the first frame update
        void Start()
    {
        SetStartingPos(); //set the starting position

    }//end Start()


    // Update is called once per frame
    void Update()
    {
        
        if (isInPlay==false){
            Vector3 pos=this.transform.position;
            pos.x=paddle.transform.position.x;
            this.transform.position=pos;
        }
        if (Input.GetKeyDown(KeyCode.Space) ){
            if (isInPlay==false){
                isInPlay=true;
                Move();
            }

        }

        
    }//end Update()


    private void LateUpdate()
    {
        if (isInPlay==true){
            
            initialForce.y=speed;
        }


    }//end LateUpdate()
    void Move(){
        rb.AddForce(initialForce);
        Debug.Log(initialForce);
        
    }
    private void OnCollisionEnter(Collision collision){
        GameObject GO=collision.gameObject;
        if (GO.tag=="Brick"){
            score+=100;
            Destroy(GO);
        }
    }
    private void OnTriggerEnter(Collider other){
        
        if (other.tag=="OutBounds"){
            numBalls-=1;
        }
        if (numBalls>0){
            Invoke("SetStartingPos",2);
        }
    }

    

    
    void SetStartingPos()
    {
        isInPlay = false;//ball is not in play
        rb.velocity = Vector3.zero;//set velocity to keep ball stationary

        Vector3 pos = new Vector3();
        pos.x = paddle.transform.position.x; //x position of paddel
        pos.y = paddle.transform.position.y + paddle.transform.localScale.y; //Y position of paddle plus it's height

        transform.position = pos;//set starting position of the ball 
    }//end SetStartingPos()

    




}
