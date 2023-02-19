using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseableCube : MonoBehaviour, IUsable
{
    public void OnUsed()
    {
        Debug.Log("Cube got used");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
