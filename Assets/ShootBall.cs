using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBall : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector2 clickStartPosition;
    private bool isDragging = false;

    public static float forceMultiplier = 1f; // Adjust this to control the force applied to the ball
    public static float maxForce = 10f; // Maximum allowed force

    public LineRenderer lineRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Ensure the line renderer is assigned and initialized
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
            lineRenderer.positionCount = 2;
        }
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Check for right-click
        {
            isDragging = true;
            clickStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }

        if (isDragging)
        {
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, currentMousePosition);
        }

        if (Input.GetMouseButtonUp(1)) // Check for right-click release
        {
            if (isDragging)
            {
                Vector2 releasePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 direction = clickStartPosition - releasePosition;
                float forceMagnitude = Mathf.Min(direction.magnitude * forceMultiplier, maxForce);
                Vector2 clampedForce = direction.normalized * forceMagnitude;
                rb.AddForce(clampedForce, ForceMode2D.Impulse);
            }
            isDragging = false;
            lineRenderer.enabled = false;
        }
    }
}
