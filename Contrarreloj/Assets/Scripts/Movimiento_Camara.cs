using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento_Camara : MonoBehaviour
{
    public Transform Pos_Autos;
    public Transform Pos_Pistas;
    public float speed;
    public bool Autos;
    public bool Pistas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Pistas)
        {
            if (transform.position != Pos_Pistas.position)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, Pos_Pistas.position, step);
            }
            else
            {
                Pistas = false;
            }
        }
        if(Autos)
        {
            if (transform.position != Pos_Autos.position)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, Pos_Autos.position, step);
            }
            else
            {
                Autos = false;
            }
        }
        
    }

    public void Moverse_Pistas()
    {
        Pistas = true;
    }
    public void Moverse_Autos()
    {
        Autos = true;
    }
}
