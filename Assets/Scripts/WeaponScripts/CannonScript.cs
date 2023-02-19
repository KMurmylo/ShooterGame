using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour, IGun
{
    
    [SerializeField] Transform chargingCircle;
    private float charge;
    private float maxAmmo=500;
    private float ammo=500;
    private float ammoCost = 15f;
    private UiScript uiScript;
    private GameObject prefab;
    bool isCharging = false;
    private float chargeRate = 45f;
    private HingeJoint joint;
    private bool fired = false;
    public void ButtonDown()
    {
        //throw new System.NotImplementedException();
        if(!fired)
        isCharging = true;
        
        

    }

    public void ButtonUp()
    {
        //throw new System.NotImplementedException();
        if (charge > 40f)
        {
            spawnProjectile(50f, transform.position+(transform.forward*3), transform.rotation, charge);
           
        }
        isCharging=false;
        fired = true;
        
    }

    public bool IsShooting()
    {
        //throw new System.NotImplementedException();
        return charge > 0f;
    }

    public void playerMoving(bool moving)
    {
       
    }

    // Start is called before the first frame update
    void Awake()
    {
        uiScript = UiScript.getInstance();
    }

    void Start()
    {
        prefab = Resources.Load("Bullets/CannonProjectile", typeof(GameObject)) as GameObject;
        


    }
    public void spawnProjectile(float force, Vector3 position, Quaternion rotation, float charge)
    {
        GameObject projectile;
        projectile = Instantiate(prefab, position, rotation) as GameObject;
        projectile.GetComponent<CannonProjectile>().Shoot(force, rotation, charge);

    }
    public void Ready()
    {
        UpdateAmmo(ammo);
        uiScript.EnableChargeBar();
        Debug.Log("CannonEnabled");
    }
    public void Holster()
    {
       uiScript.DisableChargeBar();
        
    }
    // Update is called once per frame
    void Update()
    {
        uiScript.UpdateChargeBar(charge/100);
        chargingCircle.rotation = Quaternion.Lerp(chargingCircle.rotation, chargingCircle.rotation * Quaternion.Euler(0, 0, charge/2), 0.1f);
        if (charge == 100f && isCharging)
        {
            ammo =Mathf.Clamp(ammo- (ammoCost/4)*Time.deltaTime,0,maxAmmo);
            UpdateAmmo(ammo);
        }
        else if (isCharging && ammo > 0)
        { charge = System.Math.Clamp(charge+(chargeRate * Time.deltaTime),0f,100f);

            ammo = Mathf.Clamp(ammo - (ammoCost) * Time.deltaTime, 0, maxAmmo);
            UpdateAmmo(ammo);
        }
        else if(isCharging&&ammo <= 0)
        {
            ButtonUp();
        }
        else
        {
            charge = System.Math.Clamp(charge - ((3*chargeRate) * Time.deltaTime), 0f, 100f);
            if (charge <= 0) fired = false;
        }
        
        
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
