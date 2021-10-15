using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_BuildManager : MonoBehaviour
{
    [Header("Inspector loaded data")]
    public List<GameObject> tiles;
    public GameObject Theseus;
    public GameObject Minotaur;

    [Header("in game loaded data")]
    public GameObject player;
    public GameObject enemy;
    sc_playerMovement playerScript;
    sc_enemyMovement enemyScript;


    private void Start()
    {
        foreach (GameObject tile in tiles)
        {
            sc_tile tilescript = tile.GetComponent<sc_tile>();
            if (tilescript.isTheseusSpawnTile)
            {
                player = Instantiate(Theseus, tilescript.midPoint.transform.position, tilescript.midPoint.transform.rotation);
                playerScript= player.GetComponent<sc_playerMovement>();
                playerScript.setCurrentTile(tilescript);
            }
            else if (tilescript.isMinotaurSpawnTile)
            {
                enemy = Instantiate(Minotaur, tilescript.midPoint.transform.position, tilescript.midPoint.transform.rotation);
                enemyScript= enemy.GetComponent<sc_enemyMovement>();
                enemyScript.setCurrentTile(tilescript);
            }
        }
    }

    public void redoPlayers()
    {
        Debug.Log("redo");
        playerScript.setCurrentTile(playerScript.pastTileScript, true);
        enemyScript.setCurrentTile(enemyScript.pastTileScript, true);
        refreshEnemyStats();
    }

    //GAME STATUS:
    //0 MEANS PLAYER WON
    //1 MEANS pla
    public int checkGameStatus()
    {
        refreshEnemyStats();
        if (player.GetComponent<sc_playerMovement>().isPlayerAtFinal())
        {
            return 0;
        }else if (Vector3.Distance(player.transform.position,enemy.transform.position)<=1)
        {
            return 1;
        }else
        {
            return -1;
        }
    }

    void refreshEnemyStats()
    {
        enemyScript.setPlayerTile(playerScript.currentTileScript);
        enemyScript.moveCount = 0;
    }
}
