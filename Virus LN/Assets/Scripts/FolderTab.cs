using UnityEngine;
using System.Collections;

public class FolderTab : MonoBehaviour
{
    public int index;                      // Orden de esta hoja
    public Vector3 closedRotation;         // Rotación cuando está cerrada
    public Vector3 openRotation;           // Rotación cuando está abierta
    public float rotationDuration = 0.5f;  // Tiempo de animación

    private bool isOpen = false;
    private Coroutine currentRotation;

    private void Start()
    {
        transform.localEulerAngles = closedRotation;
    }

    private void OnMouseDown()
    {
        OnTabClicked();
    }

    public void OnTabClicked()
    {
        PageManager.instance.OnPageSelected(this);
    }

    public void RotateTo(Vector3 targetRotation)
    {
        if (currentRotation != null)
            StopCoroutine(currentRotation);

        currentRotation = StartCoroutine(RotateCoroutine(targetRotation));
    }

    private IEnumerator RotateCoroutine(Vector3 targetRotation)
    {
        Quaternion startRot = transform.localRotation;
        Quaternion endRot = Quaternion.Euler(targetRotation);
        float elapsed = 0;

        while (elapsed < rotationDuration)
        {
            elapsed += Time.deltaTime;
            transform.localRotation = Quaternion.Slerp(startRot, endRot, elapsed / rotationDuration);
            yield return null;
        }

        transform.localRotation = endRot;
        currentRotation = null;
    }

    public void SetOpen(bool open)
    {
        isOpen = open;
        RotateTo(open ? openRotation : closedRotation);
    }

    public bool IsOpen() => isOpen;
}
