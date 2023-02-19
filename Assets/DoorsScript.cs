using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsScript : MonoBehaviour,IUsable
{
    private Animator animator;
    private bool open=false;
    public void OnUsed()
    {
        if (open)
        {
            animator.Play("Close");
            open = false;
        }
        else
        {
            animator.Play("Open");
            open = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
