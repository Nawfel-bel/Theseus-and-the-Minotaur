using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_playerMovement : MonoBehaviour
{
    bool enableControls = true;
    bool isMoving = false;


    Vector3 movePoint;
    public float moveSpeed;

    public sc_tile pastTileScript;
    public sc_tile currentTileScript;

    private void Update()
    {
        if (GameManager.instance.turn == 0)
        {
            if (enableControls) { checkInput(); }
            else if (isMoving)
            {
                startMoving();
            }
        }
    }

    public void setCurrentTile(sc_tile tile,bool setup=false)
    {

        if (setup)
        {
            pastTileScript = tile;
            if (currentTileScript.isPlayerHere)
            {
                currentTileScript.toggleIsPlayerHere();
            }
            transform.position = pastTileScript.midPoint.transform.position;
        }
        currentTileScript = tile;
        currentTileScript.toggleIsPlayerHere();
       
    }

    void checkInput() { 
        if (Input.GetKeyDown(KeyCode.W))
        {
            endTurn();
            return;
        }else if (Input.GetKeyDown(KeyCode.U))
        {
            GameManager.instance.redo();
        }
        sc_tile nextTile;
        bool keydown = false;
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nextTile = currentTileScript.rightTile;
            pastTileScript = currentTileScript;
            movePoint = nextTile.midPoint.transform.position;
             keydown = true;
            currentTileScript.toggleIsPlayerHere();
            setCurrentTile(currentTileScript.rightTile);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            nextTile = currentTileScript.leftTile;
            pastTileScript = currentTileScript;
            movePoint = nextTile.midPoint.transform.position;
            keydown = true;
            currentTileScript.toggleIsPlayerHere();
            setCurrentTile(currentTileScript.leftTile);
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            nextTile = currentTileScript.topTile;
            pastTileScript = currentTileScript;
            movePoint = nextTile.midPoint.transform.position;
            keydown = true;
            currentTileScript.toggleIsPlayerHere();
            setCurrentTile(currentTileScript.topTile);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            nextTile = currentTileScript.bottomTile;
            pastTileScript = currentTileScript;
            movePoint = nextTile.midPoint.transform.position;
            keydown = true;
            currentTileScript.toggleIsPlayerHere();
            setCurrentTile(currentTileScript.bottomTile);

        }
        if (keydown)
        {
            
            isMoving = true;
            enableControls = false;
        }
        
    }
    
    void startMoving()
    {
        transform.localPosition =  Vector3.MoveTowards(transform.position, movePoint,moveSpeed*Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint) <= 0.2)
        {
            endTurn();
            isMoving = false;
            enableControls = true;
        }
    }

    void endTurn()
    {
        GameManager.instance.switchTurns();
    }

    public bool isPlayerAtFinal()
    {
        return currentTileScript.isFinalTile;
    }
}
