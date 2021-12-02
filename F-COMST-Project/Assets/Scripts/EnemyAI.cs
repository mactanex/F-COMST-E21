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
    public float walkPointRange;
    public int count;
    public int secondCount;
    public int thirdCount;
    public bool[] Path;

    






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
        walkPoint = new Vector3(26 + 8.74f, transform.position.y, 0 + 10.07f); //walkpoint 10 ,  starter her
        agent.SetDestination(walkPoint);

        Path = new bool[11];

    }

     // Update is called once per frame
     void Update()
     {
         playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        //playerInSightRange = Physics.Raycast(transform.position, transform.forward, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
       

        if (playerInSightRange== false)
        {
            anim.SetBool("IsRunning", true);
            Patrolling2();
        }
        else if (playerInSightRange == true && playerInAttackRange == false) 
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

/*        if(lastPosition.magnitude - transform.position.magnitude ==0) 
        {
            secondCount++;
        }
        lastPosition = transform.position; */
    }


  

    private void Patrolling2() 
    {

      //  Debug.Log("it doesnt work");

        if (Vector3.Distance(transform.position,walkPoint) <=1) //tjek om destination er nået
        {
           
            Debug.Log("it works");
            FixedWalkPoint();
        }

        agent.SetDestination(walkPoint);


    }


  /*  private void Patrolling() 
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
        }
       
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

        */

    public void FixedWalkPoint() 
    {
        if(transform.position.x >= 30) //ved walkpoint 10
        {
            key = 1;
            Debug.Log("walkpoint 10/" + Path.Length);
            MakeBoolFalse();
            Path[10] = true;
        }

        //random.range(min,max), er max ikke inkluderet

        else if (transform.position.x >= 0 && transform.position.z <=20 && transform.position.z >=0) //ved walkpoint 1
        {
            Debug.Log("doesnt work");
            if (Path[10] == true)
            {
                Debug.Log("path(10) is true");
                while (key != 2 && key!=4)
                {
                    key = Random.Range(2, 5);
                }

                Debug.Log("key is=" + key);
            }
            else if (Path[2] == true)
            {
                key = Random.Range(3, 5);
            }
            else if (Path[9] == true)
            {
                key = Random.Range(2, 4);
            }
            else
            {

                key = Random.Range(2, 5);
                Debug.Log(key);
                Debug.Log("walkpoint 1/" + key);

            }
            MakeBoolFalse();
            Path[1] = true;
        }

        else if (transform.position.x >= 0 &&  transform.position.z >= 30) //ved walkpoint 2
        {
            
            if (Path[1] == true)
            {
                key = 5;
            }
            else if (Path[3] == true)
            {
                key = 1;
            }
            else
            {
                while (key != 1 && key != 5)
                {
                    key = Random.Range(1, 6);
                }
            }
          
            Debug.Log("walkpoint 2/" + key);

            MakeBoolFalse();
            Path[2] = true;
        }

        else if (transform.position.x <= 0 && transform.position.x >= -20 && transform.position.z >= 30) //ved walkpoint 3
        {

      

            if (Path[2] == true)
            {
                key = Random.Range(6, 8);
               
            }
            else if (Path[5] == true)
            {
                while (key != 2 && key != 6)
                {
                    key = Random.Range(2, 7);
                }
             
            }
            else if (Path[4] == true)
            {
                while (key != 2 && key != 7)
                {
                    key = Random.Range(2, 8);
                }
             

            }
            else
            {

                while (key != 2 && key != 7 && key != 6)
                {
                    key = Random.Range(2, 8);
                }
            }


            Debug.Log("walkpoint 3/" + key);
            MakeBoolFalse();
            Path[3] = true;
        }
        else if (transform.position.x <= -30 && transform.position.z >= 30) //ved walkpoint 5
        {

            if (Path[3] == true)
            {
                key = 8;
            }
            
            else if (Path[6] == true)
            {
                key = 5;
            }

            else
            {
                while (key != 5 && key != 8)
                {
                    key = Random.Range(5, 9);
                }
            }

            Debug.Log("why does it fuck up here=" + key);
            Debug.Log("walkpoint 5/" + key);

            MakeBoolFalse();
            Path[5] = true;
        }
        else if (transform.position.x <= -30 && transform.position.z <= 30 && transform.position.z >= 0) //ved walkpoint 6
        {
            if (Path[5] == true)
            {
                while (key != 6 && key != 9)
                {
                    key = Random.Range(6, 10);
                }
            }

            else if (Path[7] == true)
            {
                while (key != 6 && key != 7)
                {
                    key = Random.Range(6, 10);
                }

            }
            else if (Path[4])
            {
                while (key != 7 && key != 9)
                {
                    key = Random.Range(6, 10);
                }

            }
            else
            {
                while (key == 8)
                {
                    key = Random.Range(6, 10);
                }
            }
            Debug.Log("walkpoint 6/" + key);

            MakeBoolFalse();
            Path[6] = true;
        }

        else if (transform.position.x <= -30 && transform.position.z <= 0) //ved walkpoint 7
        {
            if (Path[6] == true)
            {
                key = 10;
            }
            else if (Path[8] == true)
            {
                key = 8;
            }

            else
            {
                while (key == 9)
                {
                    key = Random.Range(8, 11);
                }
            }
            Debug.Log("walkpoint 7/" + key);
            MakeBoolFalse();
            Path[7] = true;
        }
        else if (transform.position.x >= -30 && transform.position.x <= 0 && transform.position.z <= 0) //ved walkpoint 8
        {
            if (Path[7] == true)
            {
                while (key != 4 && key != 6)
                {
                    key = Random.Range(4, 7);
                }

            }
            else if (Path[9] == true)
            {
                while (key != 6 && key != 9)
                {
                    key = Random.Range(6, 10);
                }

            }
            else if (Path[4] == true)
            {
                while (key != 4 && key != 9)
                {
                    key = Random.Range(4, 10);
                }

            }
            else
            {

                while (key != 4 && key != 6 && key != 9)
                {
                    key = Random.Range(4, 10);
                }
            }
            Debug.Log("walkpoint 8/" + key);
            MakeBoolFalse();
            Path[8] = true;
        }

        else if (transform.position.x >= 0 && transform.position.z <= 0) //ved walkpoint 9
        {
            if (Path[8] == true)
            {
                key = 1;
            }
            else if (Path[1] == true)
            {
                key = 10;
            }
            else
            {
                key = Random.Range(1, 11);
                if (key >= 6)
                {
                    key = 10;
                }
                else
                {
                    key = 1;
                }
            }
            Debug.Log("walkpoint 9/" + key);
            MakeBoolFalse();
            Path[9] = true;
        }

        else if (transform.position.x <= 0 && transform.position.x >= -30 && transform.position.z >= 0 && transform.position.z <= 30) //ved walkpoint 4
        {
            if (Path[3] == true)
            {
                while (key != 8 && key != 10)
                {
                    key = Random.Range(8, 11);
                }

            }
            else if (Path[6] == true)
            {
                while (key != 5 && key != 10)
                {
                    key = Random.Range(5, 11);
                }

            }
            else if (Path[8] == true)
            {
                while (key != 5 && key != 8)
                {
                    key = Random.Range(5, 9);
                }

            }
            else
            {
                while (key != 5 && key != 8 && key != 10)
                {
                    key = Random.Range(5, 11);
                }
            }
            Debug.Log("walkpoint 4/" + key);
            MakeBoolFalse();
            Path[4] = true;

        }



       


        switch (key)
        {
            case 1:
                walkPoint = new Vector3(0 + 8.74f, transform.position.y, 0 + 10.07f); //hen til walkpoint 1
                break;
            case 2:
                walkPoint = new Vector3(0 + 8.74f, transform.position.y, 33 + 10.07f); // hen til walkpoint 2
                break;
            case 3:
                walkPoint = new Vector3(26 + 8.74f, transform.position.y, 0 + 10.07f); //hen til walkpoint 10
                break;
            case 4:
                walkPoint = new Vector3(0 + 8.74f, transform.position.y, -29 + 10.07f); //hen til walkpoint 9
                break;
            case 5:
                walkPoint = new Vector3(-16 + 8.74f, transform.position.y, 33 + 10.07f); //hen til walkpoint 3
                break;
            case 6:
                walkPoint = new Vector3(-15 + 8.74f, transform.position.y, 2.5f + 10.07f); //hen til walkpoint 4
                break;
            case 7:
                walkPoint = new Vector3(-46 + 8.74f, transform.position.y, 33 + 10.07f); //hen til walkpoint 5
                break;
            case 8:
                walkPoint = new Vector3(-46 + 8.74f, transform.position.y, 0 + 10.07f); //hen til walkpoint 6
                break;
            case 9:
                walkPoint = new Vector3(-46 + 8.74f, transform.position.y, -29 + 10.07f); //hen til walkpoint 7
                break;
            case 10:
                walkPoint = new Vector3(-15.5f + 8.74f, transform.position.y, -27 + 10.07f); //hen til walkpoint 8
                break;
            default:
                walkPoint = new Vector3(-15 + 8.74f, transform.position.y, 2.5f + 10.07f); //hen til walkpoint 4
                break;
        }

    }


    public void MakeBoolFalse() 
    {

        for(int i=0; i < Path.Length-1; i++) 
        {
            Path[i] = false;
        }
    }

 /*   private void searchWalkPoint()
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

        walkPoint = new Vector3(55f,transform.position.y,50f);

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
    }*/

    private void ChasePlayer()
    {
        agent.SetDestination(player.position); //enemy position skal være players position
        
    }

    private void AttackPlayer()
    {
        HC.TakeDamage(75);
        //agent.isStopped = true;
    }


}
