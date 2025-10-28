using UnityEngine;

public class SimpleMove: MonoBehaviour
{
    public float amplitude = 0.2f;   // Qué tan alto/bajo se mueve
    public float speed = 2f;         // Velocidad del movimiento

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.localPosition;
    }

    private void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }
}
