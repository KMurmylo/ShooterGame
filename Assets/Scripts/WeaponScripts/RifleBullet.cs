using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        //rb.AddForce(Camera.main.transform.forward * 1000);
        rb.velocity = Camera.main.transform.forward * 350;
        Destroy(this.gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SendMessage("TakeDamage", 35f,SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject, 2f);
        }
    }
}
