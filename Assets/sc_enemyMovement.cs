using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_enemyMovement : MonoBehaviour
{
    //for enemy
    public sc_tile currentTileScript;
    public sc_tile pastTileScript;
    //for the plyaer (theseus)
     sc_tile playerTile;

    Vector3 movePoint;
    public float moveSpeed;


    public bool enableMove = true;
    public bool isMoving = false;

    public int moveCount = 0;
    private void Update()
    {
        if (GameManager.instance.turn == 1)
        {
            if (enableMove)
            {
                makeMove();
            }else if (isMoving)
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
            transform.position = pastTileScript.midPoint.transform.position;
        }
        currentTileScript = tile;
        
     
    }
    public void setPlayerTile(sc_tile tile)
    {
        playerTile = tile;
    }


void makeMove()
    {
        float topDist = Mathf.Infinity;
        float rightDist= Mathf.Infinity;
        float leftDist = Mathf.Infinity;
        float botDist= Mathf.Infinity;
        if (currentTileScript.topTile)
        {
            topDist = getDistToPlayer(currentTileScript.topTile);
        }
        if (currentTileScript.rightTile )
        {
            rightDist = getDistToPlayer(currentTileScript.rightTile);
        }
        if (currentTileScript.bottomTile )
        {
            botDist = getDistToPlayer(currentTileScript.bottomTile);
        }
        if (currentTileScript.leftTile )
        {
            leftDist = getDistToPlayer(currentTileScript.leftTile);
        }

        float minHor = Mathf.Min(rightDist, leftDist);
        float minVert = Mathf.Min(topDist, botDist);
        bool choseMove = false;
        if (minHor <= minVert)
        {
            if (Mathf.Abs(rightDist - leftDist) <= 0.2f)
            {
                endTurn();
                return;
            }else if (currentTileScript.rightTile && minHor ==rightDist && getDistToPlayer(currentTileScript.rightTile) < getDistToPlayer(currentTileScript))
            {
                if(moveCount==0)
                pastTileScript = currentTileScript;
                movePoint = currentTileScript.rightTile.midPoint.transform.position;
                currentTileScript = currentTileScript.rightTile;
                choseMove = true;
            }
            else if (currentTileScript.leftTile && minHor == leftDist && getDistToPlayer(currentTileScript.leftTile) < getDistToPlayer(currentTileScript))
            {
                if (moveCount == 0)
                    pastTileScript = currentTileScript;
                movePoint = currentTileScript.leftTile.midPoint.transform.position;
                currentTileScript = currentTileScript.leftTile;
                choseMove = true;
            }else
            {
                endTurn();
                return;
            }

        }
        else
        {
            if (Mathf.Abs(topDist - botDist) <= 0.2f)
            {
                endTurn();
                return;
            }else if (currentTileScript.topTile && minVert == topDist && getDistToPlayer(currentTileScript.topTile) < getDistToPlayer(currentTileScript))
            {
                if (moveCount == 0)

                    pastTileScript = currentTileScript;
                movePoint = currentTileScript.topTile.midPoint.transform.position;
                currentTileScript = currentTileScript.topTile;
                choseMove = true;
            }
            else if(currentTileScript.bottomTile && minVert == botDist && getDistToPlayer(currentTileScript.bottomTile) < getDistToPlayer(currentTileScript))
            {
                if (moveCount == 0)

                    pastTileScript = currentTileScript;
                movePoint = currentTileScript.bottomTile.midPoint.transform.position;
                currentTileScript = currentTileScript.bottomTile;
                choseMove = true;
            } else
            {
                endTurn();
                return;
            }
        }
        if (choseMove)
        {
            enableMove = false;
            isMoving = true;
        }

    }

    void startMoving()
    {

        transform.localPosition = Vector3.MoveTowards(transform.position, movePoint, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoint) <= 0.2)
        {
            endTurn();
            isMoving = false;
            enableMove = true;
            
        }
    }

    void endTurn() {
        if (moveCount >= 1)
        {
            GameManager.instance.switchTurns();
        }
        else
        {
            moveCount++;
        }
    }

float getDistToPlayer(sc_tile _tile)
    {
        return Vector3.Distance(_tile.midPoint.transform.position, playerTile.midPoint.transform.position);
    }
}


