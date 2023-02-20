using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IGun 
{
    // Start is called before the first frame update
    abstract public void ButtonDown();
    abstract public void ButtonUp();
     public void AltFireDown() { }
    public void AltFireUp() { }
     public void Reload() { }
    abstract public bool IsShooting();
    abstract public void UpdateAmmo(float ammo);
    abstract public void playerMoving(bool moving);
    abstract public void getAmmo(float amount);

    abstract public void Ready();
    abstract public void Holster();


}
