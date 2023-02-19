using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    private Transform player;
    private Transform myTransform;
    float chaseRange = 100f;
    float attackRange = 2f;
    private Vector3 direction;
    private Quaternion targetRotation;
    private StateManager manager;

    public ChaseState(GameObject self) : base(self)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        myTransform = self.GetComponent<Transform>();
        manager=self.GetComponent<StateManager>();
    }

    // Start is called before the first frame update

    public override System.Type Tick()
    {   SetRotation();
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRotation, Time.deltaTime * 10);
        myTransform.Translate(myTransform.forward * Time.deltaTime * 10f, Space.World);
        //Debug.Log("Am chasing now!");
        if (Vector3.Distance(myTransform.position, player.position) < attackRange)
        {
            return typeof(AttackState);
        }
        if (!manager.wasDamaged()&&(Vector3.Distance(myTransform.position, player.position) > chaseRange))
        {
            return typeof(WanderState);
        }
        return null;
    }

    void SetRotation()
    {
        direction = (player.position - myTransform.position);
        direction = new Vector3(direction.x, 0, direction.z);

        targetRotation = Quaternion.LookRotation(direction);

    }

}
