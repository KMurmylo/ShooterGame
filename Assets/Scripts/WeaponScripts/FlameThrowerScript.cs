using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerScript : MonoBehaviour, IGun
{
    [SerializeField] private ParticleSystem ps;
    private bool isShooting;
    private float ammo = 250;
    private float ammoCost = 15f;
    private UiScript uiScript;

    public void ButtonDown()
    {
        if (ammo > 0) { 
        isShooting = true;
        ps.Play();
        }
    }

    public void ButtonUp()
    {   isShooting = false;
        ps.Stop();
    }

    public void getAmmo(float amount)
    {
        ammo += amount;
    }

    public bool IsShooting()
    {
        return isShooting;
    }

    public void playerMoving(bool moving)
    {
        
    }

    public void UpdateAmmo(float ammo)
    {
        uiScript.updateAmmo(ammo);
    }
    public void Ready()
    {
        uiScript = UiScript.getInstance();
        uiScript.updateAmmo(ammo);
    }
    public void Holster()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting)
        {if (ammo <= 0)
            {
                ammo = 0;
                ButtonUp();
                return;
            }
            ammo -= ammoCost * Time.deltaTime;
            UpdateAmmo(ammo);
        }
    }
}
