using UnityEngine;

public class BotonDePagina : MonoBehaviour
{
    public MoveData data;
    public int seccionACargar;
    private void OnMouseDown()
    {
        MoveToPlace.instance.MoveCameraToDataPlace(data);
        PageManager.instance.CloseAll();
        Manager.instance.ActivarSeccion(seccionACargar);
    }

    public void OnBackButton()
    {
        PageManager.instance.CloseAll();
        Manager.instance.DesactivarTodasLasSecciones();
        MoveToPlace.instance.Volver();
    }
}
