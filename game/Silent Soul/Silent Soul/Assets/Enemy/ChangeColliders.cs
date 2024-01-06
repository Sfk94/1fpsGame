using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColliders : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PolygonCollider2D poly = GetComponent<PolygonCollider2D>();
        Vector2[] points = poly.points;
        EdgeCollider2D edge = gameObject.AddComponent<EdgeCollider2D>();
        edge.points = points;
        Destroy(poly);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
