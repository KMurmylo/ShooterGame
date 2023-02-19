using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour, IGun
{
    private float fireRate=0.6f;
    private float nextShot;
    private float ammo=50;
    GameObject bulletPrefab;
    private bool shooting =false;
    private Animator animator;
    private UiScript uiScript;

    public void ButtonDown()
    {
        if (Time.time > nextShot&&ammo>0)
        {
            shoot();
            
            animator.Play("Shooting",0,0f);
            shooting = true;
            ammo -= 1;
            UpdateAmmo(ammo);
        };
       
        
    }

    public void ButtonUp()
    {
        shooting = false;
    }

    public void getAmmo(float amount)
    {
        ammo += amount;
    }

    public bool IsShooting()
    {
        return shooting;
    }

    public void playerMoving(bool moving)
    {
        animator.SetBool("moving", moving);
       
    }

    public void UpdateAmmo(float amount)
    {
        
        uiScript.updateAmmo(ammo);
    }
    public void Ready()
    {
        UpdateAmmo(ammo);
    }
    public void Holster()
    {

    }
    private void shoot()
    {
        //Debug.Log("Pew!");
        Instantiate(bulletPrefab,transform.position+(transform.forward*0.2f), Camera.main.transform.rotation);
        nextShot = Time.time + fireRate;
    }
    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab = (GameObject)Resources.Load("Bullets/RifleBullet", typeof(GameObject));
        animator=gameObject.GetComponent<Animator>();
        uiScript = UiScript.getInstance();
        UpdateAmmo(ammo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
