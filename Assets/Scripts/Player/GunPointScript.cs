using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPointScript : MonoBehaviour
{
    public GameObject[] guns;
    int currentGun;
    IGun gunInterface;
    // Start is called before the first frame update
    void Start()
    {
        currentGun = 0;
        gunInterface = guns[currentGun].transform.GetComponent<IGun>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwapGun(int gunNumber)
    {
        --gunNumber;
        if (gunNumber == currentGun||gunInterface.IsShooting()) return;
        guns[currentGun].GetComponent<IGun>().Holster();
        guns[currentGun].SetActive(false);
        guns[gunNumber].SetActive(true);
        
        currentGun = gunNumber;
        gunInterface = guns[currentGun].GetComponent<IGun>();
        guns[currentGun].GetComponent<IGun>().Ready();

    }

    public IGun GetCurrentInterface()
    {
        return gunInterface;

    }

    public void ButtonDown()
    {
        gunInterface.ButtonDown();
    }
   /* public void ButtonUp()
    {
        gunInterface.ButtonUp();
    }
    public void playerMoving(bool moving)
    {
        gunInterface.playerMoving(moving);
    }*/
}
