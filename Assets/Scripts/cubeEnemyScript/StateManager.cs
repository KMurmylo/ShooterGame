using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour,IDamageable
{
    Dictionary<Type, State> states;
    private float health;
    [SerializeField] private float maxHealth;
    public State currentState { get; private set; }
    private System.Type nextState;
    public Transform myTransform { get; private set; }
    public bool damaged = false;
    [SerializeField]private float bounty=100;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        states=new Dictionary<Type, State>();
        states.Add(typeof(WanderState), new WanderState(gameObject));
        states.Add(typeof(ChaseState),new ChaseState(gameObject));
        states.Add(typeof(AttackState), new AttackState(gameObject));
         currentState=states[typeof(WanderState)];

        health = maxHealth;


    }
    private void OnDestroy()
    {
        PlayerControlerScript.GetInstance().GiveMoney(bounty);
    }
    public bool wasDamaged()
    {
        return damaged;
    }
   
    // Update is called once per frame
    void Update()
    {
        
        nextState=currentState.Tick(); 
        if (nextState != null)
        {
            currentState = states[nextState];
        }
    }
    void HitDetection()
    {
        AttackState helper= (AttackState)states[typeof(AttackState)];
        helper.HitDetecion();
    }

    public void TakeDamage(float amount)
    {
        damaged = true;
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
