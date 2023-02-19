using System.Collections;
using System.Collections.Generic;
using UnityEngine;
interface IGun 
{
    // Start is called before the first frame update
    abstract public void ButtonDown();
    abstract public void ButtonUp();
    abstract public bool IsShooting();
    abstract public void UpdateAmmo(float ammo);
    abstract public void playerMoving(bool moving);
    abstract public void getAmmo(float amount);

    abstract public void Ready();
    abstract public void Holster();


}
