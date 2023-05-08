using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;

public class GameManager : MonoBehaviour
{
    public int Auto_Elegido;
    public string Auto_Nombre;
    public int Pista_Elegida;
    public string Usuario;
    SqliteConnection Conexion_Base_Dato;
    SqliteCommand Que_Quiero_Consultar;
    SqliteDataReader Lector_Datos;
    public float tiempo_Pista;
    public bool Hay_Record;
    public float Tiempo_DB;
    public bool Puede_Correr;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void jugar()
    {
        switch (Pista_Elegida)
        {
            case 1:
                {
                    SceneManager.LoadScene("CoastalTrackDisplayScene");
                }
                break;
            case 2:
                {
                    SceneManager.LoadScene("F1TrackDisplayScene");
                }
                break;
            case 3:
                {
                    SceneManager.LoadScene("F8DisplayScene");
                }
                break;
            case 4:
                {
                    SceneManager.LoadScene("OvalDisplayScene");
                }
                break;
        }
    }
    public void Volver()
    {
        SceneManager.LoadScene("Mostrar_DB");
    }

    public void Volver_Usuario()
    {
        SceneManager.LoadScene("Login");
    }
    public void Record()
    {
        try
        {
            Conexion_Base_Dato = new SqliteConnection("Data Source = Pistas.db");
            Conexion_Base_Dato.Open();
            Que_Quiero_Consultar = new SqliteCommand("SELECT * FROM Pista", Conexion_Base_Dato);
            Lector_Datos = Que_Quiero_Consultar.ExecuteReader();
            while (Lector_Datos.Read())
            {
                float Nivel = System.Convert.ToSingle(Lector_Datos[0]);
                if(Nivel == Pista_Elegida)
                {
                    Debug.Log(Nivel);
                    Tiempo_DB = System.Convert.ToSingle(Lector_Datos[3]);
                    if (tiempo_Pista < Tiempo_DB || Tiempo_DB == 0)
                    {
                        Hay_Record = true;
                    }
                }
            }
            Lector_Datos.Close();
            if (Hay_Record)
            {
                Que_Quiero_Consultar = Conexion_Base_Dato.CreateCommand();
                Que_Quiero_Consultar.CommandText = string.Format("UPDATE Pista SET Usuario_Record = \"{0}\", Tiempo_Record = \"{1}\", Auto = \"{2}\"WHERE ID = \"{3}\"", Usuario, tiempo_Pista, Auto_Nombre , Pista_Elegida);
                Que_Quiero_Consultar.ExecuteScalar();
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
    }
}
