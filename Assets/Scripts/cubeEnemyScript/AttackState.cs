using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{   private float attackCooldown=1.5f;
    private float attackTimer;
    private Transform player;
    private Transform myTransform;
    private Vector3 direction;
    private Quaternion targetRotation;
    private Animator animation;
    private float attackRange=4f;
    private RaycastHit[] hits;
    private Vector3 hitbox;
    Rigidbody rbref;
    IEnumerator hitDelay;

    public AttackState(GameObject self) : base(self)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myTransform = self.GetComponent<Transform>();
        animation = self.GetComponent<Animator>();
        hitbox = new Vector3(1, 1, 1);
        hitDelay = HitDelay();
    }

    public override System.Type Tick()
    {
        //Debug.Log("Hitting");
        if (Vector3.Distance(myTransform.position, player.position) > attackRange)
        {
            return typeof(ChaseState);
            
        }
        SetRotation();
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, Time.deltaTime * 10);
        if (attackTimer < Time.time) { 
            animation.Play("Attack",0,0f);
            attackTimer = Time.time + attackCooldown;
            //myTransform.gameObject.GetComponent<StateManager>().StartCoroutine(hitDelay);
            
            
            
        }
        return null;
    }
    private IEnumerator HitDelay()
    {   while (true)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Fly");
            HitDetecion();
            myTransform.gameObject.GetComponent<StateManager>().StopCoroutine(hitDelay);
        }
        

    }
    public void HitDetecion()
    {   
        hits = Physics.BoxCastAll(myTransform.position + (myTransform.forward * 2), hitbox, myTransform.forward,Quaternion.identity,2f);
        foreach (RaycastHit hit in hits)
        {
            //Debug.Log(hit.collider.gameObject.name);

            if (hit.collider.gameObject.TryGetComponent<Rigidbody>(out rbref))
            {
                rbref.AddForce(((myTransform.forward*2) + Vector3.up) * 1500);
            }
            hit.collider.gameObject.SendMessage("TakeDamage",15f,SendMessageOptions.DontRequireReceiver);

        }
        
    }
    void SetRotation()
    {
        direction = (player.position - myTransform.position);
        direction = new Vector3(direction.x, 0, direction.z);

        targetRotation = Quaternion.LookRotation(direction);

    }


}
