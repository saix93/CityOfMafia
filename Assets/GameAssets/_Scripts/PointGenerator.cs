using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGenerator : MonoBehaviour
{
    [SerializeField] int numberOfPoints = 10;
    [SerializeField] float minDistanceBetweenPoints = 1;

    Vector3[] pointArray;
    MeshRenderer rend;

    Vector3 positionZero;

    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
        pointArray = new Vector3[numberOfPoints];

        positionZero = transform.position - rend.bounds.size / 2;

        for (int i = 0; i < pointArray.Length; i++)
        {
            pointArray[i] = GenerateRandomPoint(i);
        }
    }

    Vector3 GenerateRandomPoint(int currentPoint = 0)
    {
        Vector3 generatedPoint = Vector3.zero;

        float xGeneratedPosition = Mathf.Lerp(positionZero.x, positionZero.x + rend.bounds.size.x, Random.value);
        float zGeneratedPosition = Mathf.Lerp(positionZero.z, positionZero.z + rend.bounds.size.z, Random.value);

        generatedPoint = new Vector3(xGeneratedPosition, positionZero.y, zGeneratedPosition);

        for (int i = 0; i < currentPoint; i++)
        {
            float distanceBetweenPoints = Mathf.Abs(Vector3.Distance(generatedPoint, pointArray[i]));
            if (distanceBetweenPoints < minDistanceBetweenPoints)
            {
                Debug.Log("Demasiado cerca: " + distanceBetweenPoints);
                generatedPoint = GenerateRandomPoint(currentPoint);
            }
        }

        return generatedPoint;
    }

    //Vector3 CreateRandomPoint(int currentPoint = 0)
    //{
    //    Vector3 point = Vector3.zero;

    //    point = GenerateRandomPoint();

    //    for (int i = 0; i < currentPoint; i++)
    //    {
    //        if (Mathf.Abs(Vector3.Distance(point, pointArray[i])) < minDistance)
    //        {
    //            Debug.Log("Demasiado cerca: " + Mathf.Abs(Vector3.Distance(point, pointArray[i])));
    //            point = GenerateRandomPoint();
    //        }
    //    }

    //    return point;
    //}

    //Vector3 GenerateRandomPoint()
    //{
    //    Vector3 generatedPoint = Vector3.zero;

    //    float xGeneratedPosition = Mathf.Lerp(positionZero.x, positionZero.x + renderer.bounds.size.x, Random.value);
    //    float zGeneratedPosition = Mathf.Lerp(positionZero.z, positionZero.z + renderer.bounds.size.z, Random.value);

    //    generatedPoint = new Vector3(xGeneratedPosition, positionZero.y, zGeneratedPosition);

    //    return generatedPoint;
    //}

    private void OnDrawGizmos()
    {
        if (rend)
        {
            Gizmos.color = Color.yellow;

            for (int i = 0; i < rend.bounds.size.x; i++)
            {
                Vector3 from = positionZero + Vector3.right * i;
                Vector3 to = from + Vector3.forward * rend.bounds.size.z;
                Gizmos.DrawLine(from, to);
            }

            for (int i = 0; i < rend.bounds.size.z; i++)
            {
                Vector3 from = positionZero + Vector3.forward * i;
                Vector3 to = from + Vector3.right * rend.bounds.size.x;
                Gizmos.DrawLine(from, to);
            }

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(positionZero, .2f);
            Gizmos.DrawSphere(positionZero + new Vector3(rend.bounds.size.x, 0, 0), .2f);
            Gizmos.DrawSphere(positionZero + new Vector3(0, 0, rend.bounds.size.z), .2f);
            Gizmos.DrawSphere(positionZero + rend.bounds.size, .2f);

            Gizmos.color = Color.green;

            Gizmos.color = Color.red;

            for (int i = 0; i < pointArray.Length; i++)
            {
                Gizmos.DrawSphere(pointArray[i], .1f);
            }
        }
    }
}
