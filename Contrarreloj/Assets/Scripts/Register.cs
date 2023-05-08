using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;

public class Register : MonoBehaviour
{
    SqliteConnection Conexion_Base_Dato;
    SqliteCommand Que_Quiero_Consultar;
    SqliteDataReader Lector_Datos;
    public TMP_InputField Nombre;
    public TMP_InputField Contraseña;
    public TMP_Text texto;
    private bool existe = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Registrar_Usuario()
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
                    existe = true;
                }
            }
            Lector_Datos.Close();
            if (!existe)
            {
                Que_Quiero_Consultar.CommandText = ( "INSERT INTO Usuarios(Nombre_Usuario , Contraseña) VALUES (@Nombre , @Contraseña)");
                Que_Quiero_Consultar.Parameters.AddWithValue("@Nombre", Nombre.text);
                Que_Quiero_Consultar.Parameters.AddWithValue("@Contraseña", Contraseña.text);
                Que_Quiero_Consultar.ExecuteNonQuery();
                texto.text = "Usuario creado";
            }
            else
            {
                texto.text = "Ya existe el usuario";
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
        existe = false;
        
    }
    public void Reiniciar_Texto()
    {
        texto.text = "Ingrese el usuario y contraseña deseado";
        Nombre.text = "";
        Contraseña.text = "";
    }
}
