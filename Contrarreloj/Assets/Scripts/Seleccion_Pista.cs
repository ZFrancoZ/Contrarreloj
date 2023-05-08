using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mono.Data.Sqlite;

public class Seleccion_Pista : MonoBehaviour
{
    SqliteConnection Conexion_Base_Dato;
    SqliteCommand Que_Quiero_Consultar;
    SqliteDataReader Lector_Datos;
    public int Seleccion;
    public GameObject[] Pistas;
    public TMP_Text[] Datos;
    public GameManager Controller;
    public float Tiempo_Pista;
    public int minutos;
    public int segundos;
    public int centesimas;
    void Start()
    {
        Controller = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        Lectura_Datos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Lectura_Datos()
    {
        try
        {
            Conexion_Base_Dato = new SqliteConnection("Data Source = Pistas.db");
            Conexion_Base_Dato.Open();
            Que_Quiero_Consultar = new SqliteCommand("SELECT * FROM Pista", Conexion_Base_Dato);
            Lector_Datos = Que_Quiero_Consultar.ExecuteReader();
            while(Lector_Datos.Read())
            {
                if (Lector_Datos[0].ToString() == Seleccion.ToString())
                {
                    Pistas[Seleccion].SetActive(true);
                    for (int i = 1; i < Datos.Length; i++)
                    {
                        if(i == 3)
                        {
                            Tiempo_Pista = System.Convert.ToSingle(Lector_Datos[i]);
                        }
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
        minutos = (int)(Tiempo_Pista / 60f);
        segundos = (int)(Tiempo_Pista - minutos * 60f);
        centesimas = (int)((Tiempo_Pista - (int)Tiempo_Pista) * 100f);
        Datos[3].text = string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, centesimas);
        Controller.Pista_Elegida = Seleccion;
    }
    public void Cambiar_Mas()
    {
        if (Seleccion < 4)
        {
            Pistas[Seleccion].SetActive(false);
            Seleccion++;
        }
        Lectura_Datos();
    }
    public void Cambiar_Menos()
    {
        if (Seleccion > 1)
        {
            Pistas  [Seleccion].SetActive(false);
            Seleccion--;
        }
        Lectura_Datos();
    }

    public void Jugar()
    {
        Controller.jugar();
    }
    public void Volver_Usuario()
    {
        Controller.Volver_Usuario();
    }
}
