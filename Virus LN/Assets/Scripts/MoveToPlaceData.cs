using System;
using UnityEngine;

[System.Serializable]
public class MoveData
{
    public Transform newPlace;
    public float timeToMove;
    [Header("Posicion de profundidad. (0 = lobby, 1 = menu, 2 = opcion especifica)")]
    public int posicion;
}
public class MoveToPlaceData : MonoBehaviour
{
    public MoveData moveData;

    public void OnMove()
    {
        MoveToPlace.instance.MoveCameraToDataPlace(moveData);
    }
}
