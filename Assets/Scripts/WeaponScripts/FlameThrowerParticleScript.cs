using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerParticleScript : MonoBehaviour
{
   
    private void OnParticleCollision(GameObject other)
    {
        //other.SendMessage("TakeDamage",1f,SendMessageOptions.DontRequireReceiver);
        other.GetComponent<IDamageable>()?.TakeDamage(1f);
        //Debug.Log("Triggered2");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
