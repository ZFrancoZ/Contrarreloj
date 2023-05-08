using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Carrera : MonoBehaviour
{
    public TMP_Text Timer;
    public float Tiempo;
    private int minutos;
    private int segundos;
    private int centesimas;
    public GameObject[] Checkpoints;
    public int Checkpoint_Actual;
    public GameObject[] Botones;
    public GameManager Controller;
    public TMP_Text Regresiva;
    void Start()
    {
        Controller = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        StartCoroutine(CuentaRegresiva());
    }

    // Update is called once per frame
    void Update()
    {
        if(Controller.Puede_Correr)
        {
            Tiempo += Time.deltaTime;
            minutos = (int)(Tiempo / 60f);
            segundos = (int)(Tiempo - minutos * 60f);
            centesimas = (int)((Tiempo - (int)Tiempo) * 100f);
            Timer.text = string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, centesimas);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag ("Checkpoints"))
        {
            Debug.Log("Checkpoint");
            other.gameObject.SetActive(false);
            if(Checkpoint_Actual < Checkpoints.Length)
            {
                Checkpoint_Actual++;
                if(Checkpoint_Actual < Checkpoints.Length)
                {
                    Checkpoints[Checkpoint_Actual].SetActive(true);
                }
            }
        }
        if(other.CompareTag ("Meta"))
        {
            if(Checkpoint_Actual == Checkpoints.Length)
            {
                Controller.Puede_Correr = false;
                other.gameObject.SetActive(false);
                for (int i = 0; i < Botones.Length; i++)
                {
                    Botones[i].SetActive(true);
                }
                Controller.tiempo_Pista = Tiempo;
                Controller.Record();
                Hay_Record();
            }
        }
    }
    IEnumerator CuentaRegresiva()
    {
        yield return new WaitForSeconds(1);
        Regresiva.text = "3";
        yield return new WaitForSeconds(1);
        Regresiva.text = "2";
        yield return new WaitForSeconds(1);
        Regresiva.text = "1";
        yield return new WaitForSeconds(1);
        Regresiva.text = "";
        Controller.Puede_Correr = true;
    }

    public void Hay_Record()
    {
        if(Controller.Hay_Record)
        {
            Regresiva.text = "Nuevo Record";
        }
    }
}
