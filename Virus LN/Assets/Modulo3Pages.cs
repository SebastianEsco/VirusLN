using UnityEngine;

public class Modulo3Pages : MonoBehaviour
{
    public int indexActual = 0;
    public ImageChange imagenes;

    public void CambiarPagina(bool siguiente)
    {
        if (imagenes == null || imagenes.imagenes.Length == 0)
            return;

        // Cambiar el índice según la dirección
        if (siguiente)
            indexActual++;
        else
            indexActual--;

        // Hacer que el índice sea circular
        if (indexActual >= imagenes.imagenes.Length)
            indexActual = 0;
        else if (indexActual < 0)
            indexActual = imagenes.imagenes.Length - 1;

        // Cambiar la imagen actual
        imagenes.GetComponent<UnityEngine.UI.Image>().sprite = imagenes.imagenes[indexActual];
    }
}
