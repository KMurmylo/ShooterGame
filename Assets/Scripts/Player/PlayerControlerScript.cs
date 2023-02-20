using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControlerScript : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject feet;
    public Rigidbody body;
    GunPointScript gunpoint;
    private bool menuShowing = false;

    Vector2 movementValues;

    [SerializeField] Transform camera;
    float yCamera = 0f;

    int ActiveWeapon = 1;

    private float dashTimer;
    private float dashCooldown = 2f;

    private UiScript ui;

    [SerializeField] private float maxHealth = 100;
    private float health;
    private float money = 0;

    private PlayerInput pI;
    static private PlayerControlerScript instance;

    private Ray useRay;
    private RaycastHit useHit;
    private Vector2 middle;
    private IUsable usable;
    private bool seeUsable;
    private const float useRange = 4f;
    // Start is called before the first frame update
    private PlayerControlerScript()
    { }
    public static PlayerControlerScript GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance == null) { instance = this; }
    }
    void Start()
    {
        gunpoint = GetComponentInChildren<GunPointScript>();
        ui = UiScript.getInstance();
        health = maxHealth;
        ui.updateHealth(health);
        pI = GetComponentInChildren<PlayerInput>();
        middle = new Vector2(Screen.width / 2, Screen.height / 2);

    }

    // Update is called once per frame
    void Update()
    {
        body.AddForce(((transform.forward * movementValues.y) + (transform.right * movementValues.x)) * 7500 * Time.deltaTime);
        if (!dashReady())
        {

            ui.DashMeter(1 - ((dashTimer - Time.time) / dashCooldown));
        }

        useRay= Camera.main.ScreenPointToRay(middle);

        if (Physics.Raycast(useRay,out useHit,useRange))
        {
            seeUsable = useHit.collider.TryGetComponent<IUsable>(out usable);

            ui.ShowUseText(seeUsable);
        }
        else
        {
            ui.ShowUseText(false);
        }

        
        
    }
    public void TakeDamage(float amount)
    {
        //Debug.Log("took " + amount + " damage");
        health -= amount;
        health = Mathf.Floor(health);
        ui.updateHealth(health);
    }


    private bool dashReady()
    {
        return (Time.time > dashTimer);
    }

    public bool SpendMoney(float amount)
    {
        if (amount < money)
        {
            money -= amount;
            ui.UpdateCashText(money,true);
            return true;
        }
        ui.FlashCashTextRed();
        return false;
        
    }
    public void GiveMoney(float amount)
    {
        money += amount;
        ui.UpdateCashText(money,true);
    }
    //Controls
    private void OnMove(InputValue input)
    {
        movementValues = input.Get<Vector2>();
        gunpoint.playerMoving(movementValues != Vector2.zero);
        

    }
    private void OnDash()
    {   
        if (dashReady() && movementValues != Vector2.zero)
        {
            //Debug.Log("Dashed");
            body.AddForce(((transform.forward * movementValues.y) + (transform.right * movementValues.x)) * 6500);
            dashTimer = Time.time+dashCooldown;
        }
        
    }
   
    private void OnJump()
    {
        if(Physics.Raycast(feet.transform.position, Vector3.down,0.5f))
        {
            body.AddForce(Vector3.up * 2000);
        }
    }
    private void OnLook(InputValue input)
    {   Vector2 cameraInput = input.Get<Vector2>();
        //camera.Rotate(-cameraInput.y,0,0 );
        //Debug.Log(camera.rotation.eulerAngles.y);
        yCamera = Mathf.Clamp(yCamera - cameraInput.y, -90, 90);
        camera.rotation = Quaternion.Euler(yCamera,camera.rotation.eulerAngles.y,  camera.rotation.eulerAngles.z);
        
        transform.Rotate(0, cameraInput.x, 0);
        
        //Debug.Log(movementValues.x + " " + movementValues.y);
    }
    private void OnFire(InputValue inputValue)
    {  
        
        if (inputValue.Get<float>()==1) gunpoint.ButtonDown();
        else gunpoint.ButtonUp();

    }
    private void OnWeapon1()
    {
        gunpoint.SwapGun(1);

    }
    private void OnWeapon2()
    {
        gunpoint.SwapGun(2);
        
    }
    private void OnWeapon3()
    {
        gunpoint.SwapGun(3);

    }
    private void OnWeapon4()
    {
        gunpoint.SwapGun(4);

    }

    private void OnMenu()
    {   if (PauseMenuScript.isActive)
        { PauseMenuScript.GetInstance().Continue(); }
        else
        {
            PauseMenuScript.GetInstance().Pause();
        }
    }
    private void OnUse()
    {
        Ray ray = Camera.main.ScreenPointToRay(middle);
        if (Physics.Raycast(ray, out useHit,useRange))
        {
            useHit.collider.GetComponent<IUsable>()?.OnUsed();
        }
    }
}
