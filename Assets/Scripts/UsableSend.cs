using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableSend : MonoBehaviour,IUsable
{
    [SerializeField] private GameObject receiver;
    public void OnUsed()
    {
        receiver.GetComponent<IUsable>().OnUsed();
    }
}
