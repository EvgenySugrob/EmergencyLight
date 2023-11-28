using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    [SerializeField] GameObject prefabEntryNumber;
    [SerializeField] GameObject objectJob;
    [SerializeField] List<Vector3> allFloorPointsList;
    [SerializeField] List<Quaternion> allFlootPointsRotateList;  
    [SerializeField] Transform playerTransform;
    [SerializeField]private Vector3 prevPosition;
    [Range(0.5f, 2)]
    [SerializeField] float coefCorrect;
    [SerializeField] float secWait;
    [SerializeField]private bool isStay;
    private Coroutine coroutine;


    private void OnTriggerEnter(Collider player)
    {
        if (!isStay) 
        {
            Debug.Log("Вход в " + transform.name);
            isStay = true;

            playerTransform = player.transform;
            prevPosition = playerTransform.position;

            GameObject entryPlayer = Instantiate(prefabEntryNumber,transform);
            objectJob = entryPlayer;
            objectJob.GetComponent<EntryNumberCreate>().GetPlayerParam(playerTransform);

            coroutine = StartCoroutine(RecordPoints());

        }
    }
    private void OnTriggerExit(Collider player)
    {
        if (isStay)
        {
            Debug.Log("Выход из " + transform.name);
            StopCoroutine(coroutine);

            isStay = false;
        }
    }

    IEnumerator RecordPoints()
    {
        yield return new WaitForSeconds(secWait);

        if (prevPosition!=playerTransform.position)
        {
            objectJob.GetComponent<EntryNumberCreate>().GetPlayerParam(playerTransform);

            prevPosition = playerTransform.position;
        }
        coroutine = StartCoroutine(RecordPoints());
    }

    public void StartDrawTrace()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            foreach (Transform transform in transform)
            {
                transform.GetComponent<EntryNumberCreate>().DrawTracePlayer();
            }
        }
    }
    public void VisibleHiddenTrack(bool isActive)
    {
        foreach (Transform transform in transform)
        {
            transform.gameObject.SetActive(isActive);
        }
    }

    public void GenerateNewTrace()
    {
        foreach (Transform transform in transform)
        {
            Destroy(transform.gameObject);
        }
    }
}
