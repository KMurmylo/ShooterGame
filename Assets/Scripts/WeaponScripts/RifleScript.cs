using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleScript : MonoBehaviour,IGun
{
    private float fireRate = 0.2f;
    private float nextShot;
    private float ammo = 150;
    private float damage = 15f;

    private bool shooting = false;
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

    public void ButtonUp()
    {
        shooting = false;
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
        if(shooting&&Time.time > nextShot) shoot();
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
