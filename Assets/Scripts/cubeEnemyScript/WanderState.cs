using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{

    Vector3 destination;
    float aggroRange;
    Transform player;
    Transform myTransform;
    bool destinationSet;
    Quaternion targetRotation;
    Vector3 direction;
    StateManager manager;
    float chaseRange = 45f;
    RaycastHit rayHit;
    public WanderState(GameObject self) : base(self)
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();      
        myTransform = self.GetComponent<Transform>();
        manager = self.GetComponent<StateManager>();
    }

    public override System.Type Tick()
    {
        if (manager.wasDamaged())
        {
            return typeof(ChaseState);
        }
        if (!destinationSet)
        {
            NewDestination();

        }

        myTransform.rotation = Quaternion.Slerp(myTransform.rotation,targetRotation,Time.deltaTime*10);
        //Debug.DrawRay(myTransform.position,myTransform.forward*5,Color.white);
        if (Physics.Raycast(myTransform.position, myTransform.forward, 5))
        {
            Turn();
        }
        else
        {
            myTransform.Translate(myTransform.forward * Time.deltaTime * 10f,Space.World);
            //Debug.Log(myTransform.forward);
        }

       
        
        if(Vector3.Distance(myTransform.position, destination) < 1f)
        { destinationSet = false; }
        if (Vector3.Distance(myTransform.position, player.position) < chaseRange&&Physics.Raycast(myTransform.position,player.position-myTransform.position,out rayHit))
        {   if(rayHit.collider.GetComponent<PlayerControlerScript>()!=null)
            return typeof(ChaseState);
        }
       
        return null;
        
    }
    void NewDestination()
    {
        
        destination = myTransform.position+ (myTransform.forward*Random.Range(0,30))+(myTransform.right*Random.Range(0,40));
        
        myTransform.rotation=Quaternion.LookRotation(destination);
        SetRotation();
        destinationSet = true;
    }
    void Turn()
    {
        //Debug.Log("Turned");
        destination = myTransform.position+ (-myTransform.forward * Random.value * 20)+(myTransform.right * (Random.value-0.5f) * 50);

        SetRotation();
        destinationSet = true;
        
    }
    void SetRotation()
    {
        direction =(destination -myTransform.position) ;
        direction=new Vector3(direction.x,0,direction.z);
        
        targetRotation = Quaternion.LookRotation(direction);
        
    }
}
