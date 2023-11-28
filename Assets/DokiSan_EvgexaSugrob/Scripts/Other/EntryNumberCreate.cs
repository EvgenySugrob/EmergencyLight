using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryNumberCreate : MonoBehaviour
{
    [Header("Создание точек за заход в триггер")]
    [SerializeField] GameObject point;
    [SerializeField] List<Transform> pointList;
    [SerializeField] LineRenderer lineRenderer;
    private float correctCoefY = 1f;

    [Header("Подвязка к игроку для координат")]
    [SerializeField] Transform player;

    public void GetPlayerParam(Transform playerTransform)
    {
        player = playerTransform;
        Vector3 correctPointPositionY = new Vector3(player.position.x,player.position.y+correctCoefY,player.position.z);
        GameObject currentPoint = Instantiate(point, correctPointPositionY,player.rotation,transform);
        pointList.Add(currentPoint.transform);
    }

    public void DrawTracePlayer()
    {
        lineRenderer.positionCount = pointList.Count;
        for (int i = 0; i < pointList.Count; i++)
        {
            lineRenderer.SetPosition(i, pointList[i].position);
        }
    }
}
