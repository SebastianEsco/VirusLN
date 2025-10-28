using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{
    public Slider slider;
    public Sprite[] imagenes;
    private Image image;
    public bool usingSlider;

    private void Start()
    {
        image = GetComponent<Image>();
        if(usingSlider) slider.maxValue = imagenes.Length - 1;

    }

    private void Update()
    {
        if (!usingSlider) return;
        // Redondear el valor del slider al entero m�s cercano
        int index = Mathf.RoundToInt(slider.value);

        // Limitar el �ndice al rango v�lido
        index = Mathf.Clamp(index, 0, imagenes.Length - 1);

        // Cambiar la imagen
        image.sprite = imagenes[index];
    }
}
