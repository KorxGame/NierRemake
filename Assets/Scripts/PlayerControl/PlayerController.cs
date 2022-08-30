using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _cc;
    public CharacterController CC
    {
        get
        {
            if (_cc == null)
            {
                if (!transform.TryGetComponent(out _cc))
                {
                    Debug.LogError("未找到CharacterController组件");
                }
            }
            return _cc;
        }
    }

    //[HideInInspector]
    public Animator anim;
    
    
    
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     
    
}
