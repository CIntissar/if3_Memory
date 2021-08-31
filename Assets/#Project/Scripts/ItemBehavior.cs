using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public int id = -1;
    public LevelManager manager;

    public bool mouseOver = false; // normalement privé!
    // Start is called before the first frame update
    void Start()
    {
        
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
        transform.localScale = new Vector3(1,2,1); //transforme la taille de l'objet pour montrer lequel on sélectionne
        mouseOver = true;
    }

    void OnMouseExit() 
    {
        transform.localScale = new Vector3(1,1,1); // le remet à son état d'origine / On peut l'écrire aussi ainsi Vector3.one
        mouseOver = false;
    }
}
