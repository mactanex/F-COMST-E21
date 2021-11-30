using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//kilde/inspiration: https://www.youtube.com/watch?v=UjkSFoLxesw&ab_channel=Dave%2FGameDevelopment

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent; //enemy?
    public Transform player; //player
    private Animator anim;
    public HealthController HC;

    public LayerMask whatIsGround, whatIsPlayer;


    //patrolling
    public Vector3 walkPoint;
    public Vector3 lastPosition;
    public int key;
    public bool walkPointSet;
    public bool xForward;
    public bool zForward;
    public bool xBackward;
    public bool zBackward;
    public bool stuckFlag;
   // public bool longTime;
    public float walkPointRange;
    public int count;
    public int secondCount;
    public int thirdCount;
    /*public int fourthCount;
    public int sixthCount;*/
    //attacking




 /*    //Brain
    public int bCount1;
    public int bCount2;
    public int bCount3;
    public int bCount4;
    public bool RecognisePath;
    public bool RecogniseRoom;
    public bool thinkingFlag = false;
    public Vector3 HalfExtent =new Vector3(10f,0.1f,10f);
    //public Quaternion Orientation = new Quaternion();
    //get close to player
    public int fifthCount;*/
  

    //states

    public float sightRange, attackRange;

    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform; //PlayerObject skal hedde hvad spiller objektet hedder
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
        void Start()
     {
        //agent.autoBraking = false; //skal måske fjernes

     }

     // Update is called once per frame
     void Update()
     {
       //  playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        //playerInSightRange = Physics.Raycast(transform.position, transform.forward, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        // Brain();

        if (playerInSightRange== false)
        {
            anim.SetBool("IsRunning", true);
            Patrolling();
        }
      /*  else if (thinkingFlag == true) 
        {
            anim.SetBool("IsRunning", false);
            Thinking();

        }*/
       /* else if (playerInSightRange == true && playerInAttackRange == false) 
        {
            anim.SetBool("IsRunning", true);
            ChasePlayer();
            playerInSightRange = true;
        }
      /*  else if (playerInAttackRange == true)
        {
            anim.SetBool("IsRunning", true);
            AttackPlayer();
        }
      /*  else if (longTime == true && playerInSightRange == false && playerInAttackRange == false && thinkingFlag == false) 
        {
            anim.SetBool("IsRunning", true);
            GetCloseToPlayer();
        }*/

        //float x = lastPosition.magnitude - transform.position.magnitude;

        if(lastPosition.magnitude - transform.position.magnitude ==0) 
        {
            secondCount++;
        }
        lastPosition = transform.position;
    }


   /* private void Brain() //do smart things like not take same path twice etc.
    {

        // var newPath = Physics.CheckSphere;

        if (RecognisePath = Physics.Raycast(transform.position, transform.forward, 5f, whatIsObstacle) == true)
            bCount1++;

        if (RecogniseRoom = Physics.CheckBox(transform.position,HalfExtent,Quaternion.identity,whatIsObstacle) == true)
            bCount2++;

        if (bCount1 >= 30 && bCount2 >= 30)
        {
            thinkingFlag = true;
            bCount1 = 0;
            bCount2 = 0;
        }
            
        
    }*/

   /* private void Thinking() 
    {
        agent.isStopped = true;
        transform.Rotate(transform.right, 5);
        bCount3++;

        if (bCount3 >= 300) 
        {
            bCount3 = 0;
            thinkingFlag = false;
            agent.isStopped = false;
        }
    }*/

    private void Patrolling() 
    {
        if (secondCount >= 90) 
        {
            stuckFlag = true;
            secondCount = 0;
        
        }


        if (walkPointSet==false)
        {
            searchWalkPoint();
           
        }

        if (walkPointSet == true)
        {
            agent.SetDestination(walkPoint);
        }

     /*   if (thirdCount >= 1000) 
        {
            fourthCount++;
            thirdCount = 0;
        }

        if (fourthCount >= 5) 
        {
           // longTime = true;
            fourthCount = 0;
        }*/
       
         Vector3 distanceToWalkPoint = transform.position - walkPoint; //tjek om walkpoint er indenfor en realistisk distance
        if (distanceToWalkPoint.magnitude < 1f) //hvis ikke lav et nyt walkpoint
        { walkPointSet = false;
            Debug.Log("walkpoint ikke realistisk distance");
        }



        if (transform.hasChanged!) 
        {
            count++;
            //Debug.Log("hasChanged!");
        }

        if (count >= 90) 
        {
            walkPointSet = false;
            count = 0;
        }

        

    }

    private void searchWalkPoint()
    {

         float randomZ, randomX;

         FindKey();


       // Debug.Log("key is " + key);

         switch (key)
        {
            case 1:
                randomZ = Random.Range(0, walkPointRange); //find z-koordinat "walk-point"
                randomX = Random.Range(0, 0);//find x-koordinat "walk-point"
                                             //  Debug.Log("Z");
                zForward = true;
                zBackward = false;
                xForward = false;
                xBackward = false;
                //Debug.Log("z forward");
                break;
            case 2:
                randomZ = Random.Range(-walkPointRange, 0); //find z-koordinat "walk-point"
                randomX = Random.Range(0, 0);//find x-koordinat "walk-point"
                                             // Debug.Log("minus Z");
                zForward = false;
                zBackward = true;
                xForward = false;
                xBackward = false;
               // Debug.Log("z backward");
                break;
            case 3:
                randomZ = Random.Range(0, 0); //find z-koordinat "walk-point"
                randomX = Random.Range(0, walkPointRange);//find x-koordinat "walk-point"
                                                          //   Debug.Log("X");
                zForward = false;
                zBackward = false;
                xForward = true;
                xBackward = false;
              //  Debug.Log("x forward");
                break;
            case 4:
                randomZ = Random.Range(0, 0); //find z-koordinat "walk-point"
                randomX = Random.Range(-walkPointRange, 0);//find x-koordinat "walk-point"
                                                           // Debug.Log("minus X");
                xForward = false;
                zBackward = false;
                xForward = false;
                xBackward = true;
               // Debug.Log("x backward");
                break;
            default:
                randomZ = Random.Range(-walkPointRange, walkPointRange); //find z-koordinat "walk-point"
                randomX = Random.Range(-walkPointRange, walkPointRange);//find x-koordinat "walk-point"
               // Debug.Log("default walkrange");
                break;
        }


        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ); //flyt enemy til walkpoint


        if (Physics.Raycast(walkPoint, -transform.up, 1f, whatIsGround)) //tjek om walkpoint er indenfor map
        {
            walkPointSet = true;
            //Debug.Log("raycast works");
        }
        else 
        {
              walkPointSet = false;
           // Debug.Log("raycast works");
        }

    }


    private int FindKey()
    {
        key = Random.Range(1, 4);

        if (stuckFlag == false)
        {
           //Debug.Log("should go here");
            if (zForward == true)
            {
                while (key == 2)
                {
                    key = Random.Range(1, 4);
                }
            }
            else if (zBackward == true)
            {
                while (key == 1)
                {
                    key = Random.Range(1, 4);
                }
            }
            else if (xForward == true)
            {
                while (key == 4)
                {
                    key = Random.Range(1, 4);
                }
            }
            else if (xBackward == true)
            {
                while (key == 3)
                {
                    key = Random.Range(1, 4);
                }
            }
        }

        else
        {
            if (thirdCount <= 1)
            {
                Debug.Log("stuck");

                if (zForward == true)
                    key = 2;
                else if (zBackward == true)
                    key = 1;
                else if (xForward == true)
                    key = 4;
                else if (xBackward == true)
                    key = 3;
            }
            thirdCount++;

            if (thirdCount >= 300)
            {
                stuckFlag = false;
            }
        }

        return key;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position); //enemy position skal være players position
        
    }

    private void AttackPlayer()
    {
        HC.TakeDamage();
        //agent.isStopped = true;
    }

 /*   private void GetCloseToPlayer() 
    {
        float x;
        float y = 0;
        float z;
        
        if (player.position.x >= 0)
            x = 5;
        else
            x = -5;

        if (player.position.z >= 0)
            z = 5;
        else
            z = -5;

        agent.SetDestination(player.position + new Vector3(x,y,z));
        fifthCount++;
         
        if(fifthCount >= 900) 
        {
            longTime = false;
            fifthCount = 0;
        }
    }*/
  

}
