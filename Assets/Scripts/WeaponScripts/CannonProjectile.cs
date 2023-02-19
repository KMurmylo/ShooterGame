using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour
{
    
    private Rigidbody rb;
    private Rigidbody rbo;
    private float charge;
    Collider[] nearbyColliders;
    [SerializeField]private GameObject explosionPrefab;
    public void Shoot(float force, Quaternion rotation,float gunCharge)
    {
        Debug.Log("Shot");
        //rb.AddForce(transform.forward * force);
        Vector3 direction = rotation * Vector3.forward;
        //rb.velocity = direction * force;
        //rb.velocity = transform.forward * 50;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            // Calculate the direction vector from the projectile's position to the hit point
            Vector3 direction2 = hit.point - transform.position;

            // Set the velocity of the rigidbody to the direction multiplied by the force
            rb.velocity = direction2.normalized * force;
        }
        else
        {
            Vector3 direction2=Camera.main.transform.forward;
            rb.velocity = direction2.normalized * force;
        }
        
        charge = gunCharge;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 25f);
    }
    void Start()
    {
        Debug.Log("Spawned");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DelayedExplosion(float time)
    {
        yield return new WaitForSeconds(time);
        explode();
    }

    private void explode()
    {
        nearbyColliders = Physics.OverlapSphere(transform.position, 5f + (charge / 20));
        foreach (Collider collider in nearbyColliders)
        {
            if (collider.gameObject.TryGetComponent<Rigidbody>(out rbo))
            {
                rbo.AddExplosionForce(100 + charge * 20, transform.position, 15f + (charge / 10));
            }
            collider.gameObject.SendMessage("TakeDamage", 2*charge / Vector3.Distance(transform.position, collider.transform.position), SendMessageOptions.DontRequireReceiver);
        }
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            explode();
        }
        else
        {
            StartCoroutine(DelayedExplosion(2.5f));
        }
    }
}
