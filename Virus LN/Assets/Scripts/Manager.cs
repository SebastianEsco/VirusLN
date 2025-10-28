using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    public List<conjuntos> objetosDeSeccion;
    public int posicionActual;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }


    private void Start()
    {
        DesactivarTodasLasSecciones();
    }

    public void ActivarSeccion(int indexSeccion)
    {
        foreach (var lista in objetosDeSeccion)
        {
            lista.Desactivar();
        }
        objetosDeSeccion[indexSeccion].Activar();
    }

    public void DesactivarTodasLasSecciones()
    {
        foreach (var lista in objetosDeSeccion)
        {
            lista.Desactivar();
        }
    }

    public void ActualizarNuevaPosicion(MoveData data)
    {
        posicionActual = data.posicion;
    }

    public void ActualizarNuevaPosicion(int nuevaPosicion)
    {
        posicionActual = nuevaPosicion;
    }





}

[System.Serializable]
public class conjuntos
{
    public GameObject[] objetos;

    public void Desactivar()
    {
        foreach(var obj in objetos)
        {
            obj.SetActive(false);
        }
    }

    public void Activar()
    {
        foreach (var obj in objetos)
        {
            obj.SetActive(true);
        }
    }
}

