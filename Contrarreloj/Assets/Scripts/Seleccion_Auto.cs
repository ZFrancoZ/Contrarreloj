using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using TMPro;

public class Seleccion_Auto : MonoBehaviour
{
    public GameObject[] Autos;
    public TMP_Text[] Datos;
    SqliteConnection Conexion_Base_Dato;
    SqliteCommand Que_Quiero_Consultar;
    SqliteDataReader Lector_Datos;
    public GameManager Controller;
    public int Seleccion;
    public float GiraX;
    public float GiraY;
    public float GiraZ;
    void Start()
    {
        Controller = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        Lectura_Datos();
    }
    public void Lectura_Datos()
    {
        try
        {
            Conexion_Base_Dato = new SqliteConnection("Data source = Autos.db");
            Conexion_Base_Dato.Open();
            Que_Quiero_Consultar = new SqliteCommand("SELECT * FROM Auto", Conexion_Base_Dato);
            Lector_Datos = Que_Quiero_Consultar.ExecuteReader();
            while (Lector_Datos.Read())
            {
                if (Lector_Datos[0].ToString() == Seleccion.ToString())
                {
                    Autos[Seleccion].SetActive(true);
                    for (int i = 1; i < Datos.Length; i++)
                    {
                        Datos[i].text = Lector_Datos[i].ToString();
                    }
                }
            }
        }
        catch (System.Exception mensaje)
        {

            Debug.Log(mensaje.Message);
        }
        finally
        {
            Conexion_Base_Dato.Close();
        }
        Controller.Auto_Nombre = Datos[1].text;
        Controller.Auto_Elegido = Seleccion;
    }
    public void Cambiar_Mas()
    {
        if (Seleccion < 8 )
        {
            Autos[Seleccion].SetActive(false);
            Seleccion++;
        }
        Lectura_Datos();
    }
    public void Cambiar_Menos()
    {
        if(Seleccion > 1)
        {
            Autos[Seleccion].SetActive(false);
            Seleccion--;
        }
        Lectura_Datos();
    }

    void Update()
    {
        Autos[Seleccion].transform.Rotate(GiraX, GiraY, GiraZ);
    }
}
