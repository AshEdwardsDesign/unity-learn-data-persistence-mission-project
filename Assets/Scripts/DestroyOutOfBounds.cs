using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField]
    private float zBoundary;

    [SerializeField]
    private float yBoundary;

    // Update is called once per frame
    void Update()
    {
        DestroyIfOutOfBounds();
    }

    private void DestroyIfOutOfBounds()
    {
        if (transform.position.z < zBoundary || transform.position.y < yBoundary)
        {
            Destroy(gameObject);
        }
    }
}
