using TMPro;
using UnityEngine;

public class Winner : MonoBehaviour
{
    public GameObject truckmesh;
    public Material[] truckMaterials;
    public TextMeshProUGUI ganadorTexto;

/*
    void Awake()
    {
        PlayerPrefs.SetString("Winner", "Jugador 2");
        PlayerPrefs.Save();
    }
*/

    void Start()
    {
        string winnerName = PlayerPrefs.GetString("Winner");

        if(winnerName == "P1")
        {
            
        }
        else
        {
            Renderer rend = truckmesh.GetComponent<MeshRenderer>();
            rend.material = truckMaterials[1];

            ganadorTexto.text = "Ganador: Jugador 2";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
