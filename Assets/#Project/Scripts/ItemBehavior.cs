using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public int id = -1;
    public LevelManager manager;

    public bool mouseOver = false; // normalement privé!
    // Start is called before the first frame update

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0) && mouseOver) // quand on relache le clic de la souris, on obtient le print.
        {
            manager.RevealMaterial(id);
        }
    }

    void OnMouseOver() 
    {
        mouseOver = true;
        animator.SetBool("MouseOver",true);
    }

    void OnMouseExit() 
    {
        mouseOver = false;
        animator.SetBool("MouseOver",false);
    }

    public void HasBeenSelected(bool selected)
    {
        animator.SetBool("ItemSelected",selected);
    }

    public void HasBeenMatched()
    {
        animator.SetBool("MatchingPair", true);
    }
}
