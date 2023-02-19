using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPitScript : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    [SerializeField] Transform respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
            collision.gameObject.GetComponent<Transform>().position = respawnPoint.position;
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
