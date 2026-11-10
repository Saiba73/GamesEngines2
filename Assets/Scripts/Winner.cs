using TMPro;
using UnityEngine;

public class Winner : MonoBehaviour
{
    public GameObject truckmesh;
    public GameObject player;
    public Material[] truckMaterials;
    public TextMeshProUGUI ganadorTexto;
    private Vector3 endPosition = new Vector3(1.31f, 0.18f, 3.18f);
    private Vector3 endRotation = new Vector3(0, 221.299973f, 0);

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
            //player.transform.position = Vector3.Slerp(player.transform.position, endPosition, 0.2f);
            //Debug.Log("WE WIN");
            ganadorTexto.color = Color.red;
        }
        else
        {
            ganadorTexto.color = Color.green;
            Renderer rend = truckmesh.GetComponent<MeshRenderer>();
            rend.material = truckMaterials[1];
            
            //Debug.Log("WE WIN");
            ganadorTexto.text = "Ganador: Jugador 2";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        player.transform.position = Vector3.Slerp(player.transform.position, endPosition, 1.4f * Time.deltaTime);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(endRotation), 1.4f * Time.deltaTime);
    }
    //Vector3(1.30999994,0.180000007,5.15999985)
}   //Vector3(0,221.299973,0)
