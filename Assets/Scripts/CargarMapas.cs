using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarMapas : MonoBehaviour
{
    public void CargarMapa1()
    {
        SceneManager.LoadScene("Mapa1");
    }

    public void CargarMapa2()
    {
        SceneManager.LoadScene("Mapa2");
    }

    public void CargarMapa3()
    {
        SceneManager.LoadScene("Mapa1");
    }
}
