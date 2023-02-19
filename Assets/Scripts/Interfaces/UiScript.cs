using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    private static UiScript instance;
    public static UiScript getInstance() { return instance; }
    [SerializeField]TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] Image dashSprite;
    [SerializeField] Image chargeBar;
    [SerializeField] TextMeshProUGUI moneyText;
    private Animator moneyAnimation;
    [SerializeField] TextMeshProUGUI useText;
    public void updateHealth(float health)
    {
        healthText.text = health.ToString();
    }
    public void updateAmmo(float ammo)
    {
        ammoText.text = Mathf.Floor(ammo).ToString();
      
    }
    public void DashMeter(float percentage)
    {
        dashSprite.fillAmount = percentage;
    }
    public void EnableChargeBar()
    {
        chargeBar.gameObject.SetActive(true);
        
        //Debug.Log("Enabled");
    }
    public void DisableChargeBar()
    {
        chargeBar.gameObject.SetActive(false);
        //Debug.Log("Disabled");
    }
    public void UpdateChargeBar(float amount)
    {
        chargeBar.fillAmount = amount;
        
    }
    public void UpdateCashText(float amount)
    {
        moneyText.text = "$"+Mathf.Floor(amount).ToString();
    }
    public void UpdateCashText(float amount, bool gaining)
    {
        moneyText.text = "$" + Mathf.Floor(amount).ToString();
        if (gaining)
        {
            moneyAnimation.Play("GreenFlash");
        }
        else
        {
            moneyAnimation.Play("RedFlash");
        }
    }
    public void FlashCashTextRed()
    {
        moneyAnimation.Play("RedFlash");
    }
    public void FlashCashTextGreen()
    {
        moneyAnimation.Play("GreenFlash");
    }
    public void ShowUseText(bool show)
    {
        useText.gameObject.SetActive(show);
    }
    private void Awake()
    {
        if(instance == null) { instance = this; }
    }
    // Start is called before the first frame update
    void Start()
    {
        moneyAnimation = moneyText.GetComponent<Animator>();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
