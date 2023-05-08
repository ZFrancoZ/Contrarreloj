using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;
public class Login : MonoBehaviour
{
    SqliteConnection Conexion_Base_Dato;
    SqliteCommand Que_Quiero_Consultar;
    SqliteDataReader Lector_Datos;
    public TMP_InputField Nombre;
    public TMP_InputField Contraseña;
    public TMP_Text texto;
    public GameManager Controller;
    void Start()
    {
        Controller = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login_Usuario()
    {
        try
        {
            Conexion_Base_Dato = new SqliteConnection("Data Source = Usuarios.db");
            Conexion_Base_Dato.Open();
            Que_Quiero_Consultar = new SqliteCommand("SELECT * FROM Usuarios", Conexion_Base_Dato);
            Lector_Datos = Que_Quiero_Consultar.ExecuteReader();
            while (Lector_Datos.Read())
            {
                if (Nombre.text == Lector_Datos[1].ToString())
                {
                    if(Contraseña.text == Lector_Datos[2].ToString())
                    {
                        Controller.Usuario = Nombre.text;
                        Lector_Datos.Close();
                        SceneManager.LoadScene("Mostrar_DB");
                    }
                    else
                    {
                        texto.text = "Contraseña incorrecta";
                    }
                }
                else
                {
                    texto.text = "No se encontro el usuario";
                }
            }
            Lector_Datos.Close();
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

    public void Reiniciar_Texto()
    {
        texto.text = "Ingrese su usuario y contraseña";
        Nombre.text = "";
        Contraseña.text = "";
    }
}
