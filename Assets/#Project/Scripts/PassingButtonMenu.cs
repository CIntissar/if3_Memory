using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassingButtonMenu : MonoBehaviour
{
    private Animator animator;
    private bool isPassingOn = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(isPassingOn)
        {
            OnMouseOver();
        }
        else
        {
            OnMouseExit();
        }
    }

    private void OnMouseOver()
    {
        isPassingOn = true;
        animator.SetBool("IsPassingOn",true);
    }

    private void OnMouseExit()
    {
        isPassingOn = false;
        animator.SetBool("IsPassingOn",false);
    }
}
