using UnityEngine;
using UnityEngine.Events;

public class BotonDePagina : MonoBehaviour
{
    public MoveData data;
    public int seccionACargar;
    public UnityEvent OnClickEventoptional;
    private void OnMouseDown()
    {
        MoveToPlace.instance.MoveCameraToDataPlace(data);
        PageManager.instance.CloseAll();
        Manager.instance.ActivarSeccion(seccionACargar);
        OnClickEventoptional.Invoke();
    }

    public void OnBackButton()
    {
        PageManager.instance.CloseAll();
        Manager.instance.DesactivarTodasLasSecciones();
        MoveToPlace.instance.Volver();
    }
}
