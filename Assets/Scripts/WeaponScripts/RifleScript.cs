using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleScript : MonoBehaviour,IGun
{
    private float fireRate = 0.2f;
    private float shotgunFireRate=1f;
    private float shotgunCost = 4;
    private float shotgunSpread = 4f;
    private float shotgunPelletCount=7;
    private float nextShot;
    private float ammo = 150;
    private float damage = 15f;

    private bool shooting = false;
    private bool firingShotgun = false;
    GameObject bulletPrefab;
    private RaycastHit hit;
    private ParticleSystem particleSystem;
    private ParticleSystem.EmitParams emitParams;
    private Animator animator;
    Vector2 middle;
    private UiScript uiScript;

    public void ButtonDown()
    {
        shooting = true;
    }

    
    public void ButtonUp()
    {
        shooting = false;
    }
    private void shoot()
    {
        //Debug.Log("Pew!");
        if(ammo > 0) {
        Ray ray = Camera.main.ScreenPointToRay(middle);
        animator.Play("Shooting", 0, 0f);
        if(Physics.Raycast(ray, out hit))
        {
            emitParams.position = hit.point;
            particleSystem.Emit(emitParams, 1);
            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(transform.forward*200);
                //hit.collider.gameObject.SendMessage("TakeDamage", 15f,SendMessageOptions.DontRequireReceiver);
                hit.collider.GetComponent<IDamageable>()?.TakeDamage(damage);

        }
        
        nextShot = Time.time+fireRate;
            ammo -= 1;
            UpdateAmmo(ammo);
           
        }
    }
    public void AltFireDown()
    {
        firingShotgun = true;
    }
    public void AltFireUp()
    {
        firingShotgun = false;
    }
    private void shotgun()
    {
        if (ammo >= shotgunCost)
        {
            
          
            animator.Play("Shooting", 0, 0f);
            for(int i = 0; i < shotgunPelletCount; i++) {
                float spreadx = UnityEngine.Random.Range(-shotgunSpread, shotgunSpread);
                float spready = UnityEngine.Random.Range(-shotgunSpread, shotgunSpread);
                var direction = Camera.main.transform.forward;
                direction = Quaternion.AngleAxis(spreadx, Camera.main.transform.right) * direction;
                direction = Quaternion.AngleAxis(spready, Camera.main.transform.up) * direction;

                Ray ray = new Ray(Camera.main.transform.position, direction);
                //Debug.Log(ray);
                if (Physics.Raycast(ray, out hit))
            {   
                    
                    emitParams.position = hit.point;
                particleSystem.Emit(emitParams, 1);
                if (hit.rigidbody != null)
                    hit.rigidbody.AddForce(transform.forward * 200);
                //hit.collider.gameObject.SendMessage("TakeDamage", 15f,SendMessageOptions.DontRequireReceiver);
                hit.collider.GetComponent<IDamageable>()?.TakeDamage(damage);

            }}

            nextShot = Time.time + shotgunFireRate;
            ammo -= shotgunCost;
            UpdateAmmo(ammo);

        }
    }
    public void Ready()
    {
        uiScript = UiScript.getInstance();
        uiScript.updateAmmo(ammo);
    }
    public void Holster()
    {

    }
    void Update()
    {
        if (shooting && Time.time > nextShot) shoot();
        else 
        if(firingShotgun && Time.time > nextShot)
            shotgun();
    }
    void Start()
    {
        
        particleSystem=GetComponent<ParticleSystem>();
        emitParams.velocity= Vector3.zero;
        animator=GetComponent<Animator>();
        middle=new Vector2(Screen.width/2,Screen.height/2);
    }
    public bool IsShooting()
    {
        return shooting;
    }

    public void playerMoving(bool moving)
    {
        animator.SetBool("moving", moving);
        //Debug.Log(moving);
    }

    public void UpdateAmmo(float ammo)
    {
        uiScript.updateAmmo(ammo);
    }

    public void getAmmo(float amount)
    {
        ammo += amount;
    }
}
