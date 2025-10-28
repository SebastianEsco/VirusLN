using System.Collections;
using UnityEngine;

public class MoveToPlace : MonoBehaviour
{
    public static MoveToPlace instance;

    [Header("Referencias")]
    public Transform MainCamera;
    public Transform initialPosition;
    public Transform menuPosition;

    public GameObject UILobby, backButton;

    private Coroutine currentMoveRoutine;

    public float defaultTime;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UILobby.SetActive(true);
        backButton.SetActive(false);
    }

    private void MoveCameraTo(Transform newPlace, float timeToChange)
    {
        // Si ya hay una corrutina moviendo la cámara, la detenemos
        if (currentMoveRoutine != null)
            StopCoroutine(currentMoveRoutine);

        // Inicia la nueva transición
        currentMoveRoutine = StartCoroutine(MoveCamera(newPlace, timeToChange));
    }

    public void MoveCameraToDataPlace(MoveData data)
    {
        // Si ya hay una corrutina moviendo la cámara, la detenemos
        if (currentMoveRoutine != null)
            StopCoroutine(currentMoveRoutine);

        // Inicia la nueva transición
        currentMoveRoutine = StartCoroutine(MoveCamera(data.newPlace, data.timeToMove));

        Manager.instance.ActualizarNuevaPosicion(data);
    }

    public void Volver()
    {
        if (Manager.instance.posicionActual == 2) ReturnToMenu();
        else ReturnToLobby();

        Manager.instance.DesactivarTodasLasSecciones();
    }

    public void ReturnToMenu()
    {
        MoveCameraTo(menuPosition, defaultTime); // puedes ajustar el tiempo
        UILobby.SetActive(false);
        backButton.SetActive(true);
        Manager.instance.ActualizarNuevaPosicion(1);
    }

    public void ReturnToLobby()
    {
        MoveCameraTo(initialPosition, defaultTime);
        UILobby.SetActive(true);
        backButton.SetActive(false);
        Manager.instance.ActualizarNuevaPosicion(0);
    }

    private IEnumerator MoveCamera(Transform newPlace, float timeToChange)
    {
        Vector3 startPos = MainCamera.position;
        Quaternion startRot = MainCamera.rotation;

        Vector3 endPos = newPlace.position;
        Quaternion endRot = newPlace.rotation;

        float elapsed = 0f;

        while (elapsed < timeToChange)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / timeToChange);

            // Movimiento suave (Slerp para rotación, Lerp para posición)
            MainCamera.position = Vector3.Slerp(startPos, endPos, t);
            MainCamera.rotation = Quaternion.Slerp(startRot, endRot, t);

            yield return null;
        }

        // Asegura que quede exactamente en el destino
        MainCamera.position = endPos;
        MainCamera.rotation = endRot;

        currentMoveRoutine = null;
    }
}
