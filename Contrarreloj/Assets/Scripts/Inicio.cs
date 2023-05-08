using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio : MonoBehaviour
{
    public GameObject[] Autos;
    public GameManager Controller;
    void Start()
    {
        Controller = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        Autos[Controller.Auto_Elegido].SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jugar()
    {
        Controller.jugar();
    }
    public void Volver()
    {
        Controller.Volver();
    }
}
