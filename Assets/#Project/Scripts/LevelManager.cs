using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    private int row;
    private int col;
    public string level;
    public float gapRow = 1.5f;
    public float gapCol = 1.5f;

    private float timer;
    public Text text; 

    [Range(0f,5f)] // permet d'avoir un slider!!!!
    public float timeBeforeReset = 1f;
    private bool resetOnGoing = false;

    public GameObject itemPrefab;
    public Material[] materials;
    public Material defaultMaterial;
    public ItemBehavior[] items;


    public List<int> selected = new List<int>();
    public List<int> matches = new List<int>();

    private Dictionary<int,Material> itemMaterial = new Dictionary<int, Material>(); // int = clé / Material = valeur / new dictionary -> instancier!

    public UnityEvent whenPlayerWins;    
    void Start()
    {   
        col = PlayerPrefs.GetInt("col",4);
        row = PlayerPrefs.GetInt("row",3);  //(( , 3)) -> 3 valeur défaut en cas où le programme ne trouve pas de données

        // METHODE AVEC STRING
        // string level = PlayerPrefs.GetString("level");
        // if(level == "easy")
        // {
        //     col = 2;
        // }
        // else if (level == "normal")
        // {
        //     col = 4;
        // }

        items = new ItemBehavior[row * col];
        int index = 0;

        for(int x = 0; x < col; x++)
        {
            for(int z = 0 ;  z < row; z++)
            {
                Vector3 position = new Vector3 (x * gapCol,0, z * gapRow);
                GameObject item = Instantiate(itemPrefab, position, Quaternion.identity); 
                item.GetComponent<Renderer>().material = defaultMaterial; 

                items[index] = item.GetComponent<ItemBehavior>(); // il faut que le prefab possède un component ItemBehavior sinon erreur 
                items[index].id = index; // id = index / numéro unique pour référencier
                items[index].manager = this; // l'instance en cours du script même / this = self en python
                index++;

            }
        }

        GiveMaterials();
    }

    private void GiveMaterials()
    {
        List<int> possibilities = new List<int>(); // liste d'entier allant de 0 à 11

        for(int i = 0; i < row * col; i++)
        {
            possibilities.Add(i);
        }

        for(int i = 0; i < materials.Length; i++)  // on pourrait utiliser foreach mais il va manipuler des données lourds donc mieux d'utiliser l'index.
        {
            if(possibilities.Count < 2)break;

            int idPossibilities = Random.Range(0,possibilities.Count); // Count --> parce qu'une liste n'est pas défini par une longueur fixe
            int id1 = possibilities[idPossibilities];
            possibilities.RemoveAt(idPossibilities);

            idPossibilities = Random.Range(0,possibilities.Count); 
            int id2 = possibilities[idPossibilities];
            possibilities.RemoveAt(idPossibilities);

            itemMaterial.Add(id1, materials[i]);
            itemMaterial.Add(id2, materials[i]);

            //items[id1].GetComponent<Renderer>().material = materials[i];
            //items[id2].GetComponent<Renderer>().material = materials[i];
        }
    }

    private IEnumerator ResetMaterials(int id1, int id2)
    {
        resetOnGoing = true;
        yield return new WaitForSeconds(timeBeforeReset);   // elle repart de la dernière tache et ne reset pas
        ResetMaterial(id1);
        ResetMaterial(id2);
        resetOnGoing = false;
    }

    private IEnumerator Win()
    {
        yield return new WaitForSeconds(timeBeforeReset);   // elle repart de la dernière tache et ne reset pas
        whenPlayerWins?.Invoke(); // si c'est pas null!
    }

    public void RevealMaterial(int id)
    {
        if(resetOnGoing == false && !selected.Contains(id) && !matches.Contains(id)) // si id séléctionné n'est pas dans la liste, on peut l'ajouter dans la liste
        {
            selected.Add(id);

            items[id].GetComponent<Renderer>().material = itemMaterial[id]; 
            // correspond à :  materials material = itemMaterial[id];
            //                 items[id].GetComponent<Renderer>().material =  material;
            items[id].HasBeenSelected(true);
        }
    }

    private void ResetMaterial(int id)
    {
        items[id].GetComponent<Renderer>().material = defaultMaterial;
        items[id].HasBeenSelected(false);
    }
    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);

        string timeDisplay = timer.ToString("N2"); //format pour les float , pour avoir 2 chiffres derrière la virgule
        text.text = timeDisplay;

        
        if(selected.Count == 2)
        {
            if(itemMaterial[selected[0]] == itemMaterial[selected[1]])
            {
                matches.Add(selected[0]);
                matches.Add(selected[1]);
                items[selected[0]].HasBeenMatched();
                items[selected[1]].HasBeenMatched();

            if(matches.Count >= row * col) // condition qui montre qu'on a gagné!
            {
                PlayerPrefs.SetFloat("timeDisplay", timer); // Elle enregistre l'info du temps quand on gagne!
                StartCoroutine(Win());
            }

            }
            else
            {
                StartCoroutine(ResetMaterials(selected[0], selected[1]));
                items[selected[0]].HasBeenSelected(false);
                items[selected[1]].HasBeenSelected(false);
            }
            selected.Clear();
        }
    }
}
