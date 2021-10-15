using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // 0: means the players turn, 1 : means the enemy's turn
    public int turn = 1;
    sc_BuildManager buildManager;




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Debug.Log("we have a problemo");
    }

    private void Start()
    {
        buildManager = GetComponent<sc_BuildManager>();
        
    }


    public void switchTurns() {
        checkStatus();
        turn = 1 - turn;
    }

    
    public void redo()
    {
        buildManager.redoPlayers();
    }

    void checkStatus()
    {
        int status = buildManager.checkGameStatus();
        if (status == 0)
        {
            if (isLastLevel())
            {
                SceneManager.LoadScene(0);
                return;
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

        }
        else if (status == 1)
        {
           
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        }
    }

 

    bool isLastLevel()
    {
        return SceneManager.GetActiveScene().buildIndex == 6;
    }

    
}
