using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsGenerator : MonoBehaviour
{
    [SerializeField] private GameObject trackPrefab;
    private List<GameObject> traks = new List<GameObject>();
    [SerializeField] private int limitTrack;
    [SerializeField] private float delayToCreate;

    private void Start()
    {

        StartCoroutine(WaitToCreate(trackPrefab));
    }

    IEnumerator WaitToCreate(GameObject trackPrefab)
    {
        while (true)
        {
            yield return new WaitForSeconds(delayToCreate);
            CreateTrack(trackPrefab);
        }
    }

    private void CreateTrack(GameObject tracPrefab)
    {
        var trak = Instantiate(tracPrefab, transform.position, transform.rotation);
        traks.Add(trak);

        if (traks.Count > limitTrack)
        {
            Destroy(traks[0].gameObject);
            traks.RemoveAt(0);

        }
    }
}