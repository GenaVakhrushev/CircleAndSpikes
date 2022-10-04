using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    private List<Vector3> movePoints = new List<Vector3>();

    private Camera mainCamera;

    private LineRenderer lineRenderer;
    public LineRenderer LineRenderer => lineRenderer;

    [SerializeField] private float speed;

    void Start()
    {
        if(speed <= 0)
        {
            Debug.LogWarning("Wrong speed value");
        }

        mainCamera = Camera.main;

        lineRenderer = GetComponent<LineRenderer>();   
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPos = Input.GetTouch(0).position;

            AddMovePoint(mainCamera.ScreenToWorldPoint(touchPos));
        }
    }

    private void FixedUpdate()
    {
        if (movePoints.Count <= 0)
            return;

        float moveVecLength = speed * Time.deltaTime;
        Vector2 moveVec = (movePoints[0] - transform.position).normalized * moveVecLength;

        if (Vector2.Distance(transform.position, movePoints[0]) <= moveVecLength)
        {
            transform.position = movePoints[0];

            RemoveMovePoint(0);
        }
        else
        {
            transform.Translate(moveVec, Space.World);
        }

        lineRenderer.SetPosition(0, transform.position);
    }

    private void AddMovePoint(Vector2 point)
    {
        movePoints.Add(point);

        UpdateLinePoints();
    }

    private void RemoveMovePoint(int index)
    {
        movePoints.RemoveAt(index);

        UpdateLinePoints();
    }

    private void UpdateLinePoints()
    {
        List<Vector3> linePoints = new List<Vector3>(movePoints);

        linePoints.Insert(0, transform.position);

        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());
    }

    private void OnDisable()
    {
        lineRenderer.enabled = false;
    }
}
