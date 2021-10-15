using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_tile : MonoBehaviour
{
    public bool isTheseusSpawnTile = false;
    public bool isMinotaurSpawnTile = false;
    public bool isFinalTile = false;
    private int distanceBetweenTiles = 15;
    public LayerMask ignoredLayer;

    public GameObject midPoint;

    public sc_tile topTile;
    public sc_tile rightTile;
    public sc_tile leftTile;
    public sc_tile bottomTile;

    public bool isPlayerHere = false;
    private void Start()
    {
        registerNearTiles();
    }

    void registerNearTiles()
    {
        RaycastHit hit;
        if(Physics.Raycast(midPoint.transform.position,new Vector3(1,0,0), out hit,distanceBetweenTiles,~ignoredLayer))
        {
           if (hit.transform.tag == "wayPoint")
            rightTile = hit.transform.parent.parent.gameObject.GetComponent<sc_tile>();
        }
        if (Physics.Raycast(midPoint.transform.position, new Vector3(-1, 0, 0), out hit,distanceBetweenTiles, ~ignoredLayer))
        {
            if (hit.transform.tag == "wayPoint")
                leftTile = hit.transform.parent.parent.gameObject.GetComponent<sc_tile>();
        }
        if (Physics.Raycast(midPoint.transform.position, new Vector3(0, 0, 1), out hit, distanceBetweenTiles, ~ignoredLayer))
        {
            if (hit.transform.tag == "wayPoint")
                topTile = hit.transform.parent.parent.gameObject.GetComponent<sc_tile>();
        }
        if (Physics.Raycast(midPoint.transform.position, new Vector3(0, 0, -1), out hit, distanceBetweenTiles, ~ignoredLayer))
        {
            if (hit.transform.tag == "wayPoint")
                bottomTile = hit.transform.parent.parent.gameObject.GetComponent<sc_tile>();
        }
    }

    public void toggleIsPlayerHere()
    {
        isPlayerHere = !isPlayerHere;
    }
}
