using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour {

    public bool detectedPlayer1 = false;
    public bool detectedPlayer2 = false;
    [SerializeField]
    private GameObject ballPrefab;

    [SerializeField]
    private Slider sliderPlayer1;

    [SerializeField]
    private Slider sliderPlayer2;

    public int player1Health;
    public int player2Health;



	// Use this for initialization
	void Start () {

        player1Health = 3;
        player2Health = 3;
	
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(detectedPlayer1);
       
	
	}

    public void CreateBall()
    {
        if(detectedPlayer1 && detectedPlayer2)
        {
            Vector3 position = new Vector3(0, 17,-100);
            Instantiate(ballPrefab, position, Quaternion.identity);
        }
    }

    public void DetectPlayer(int player)
    {
        if(player == 1)
        {
            detectedPlayer1 = true;
        }

        if(player == 2)
        {
            detectedPlayer2 = true;
        }

        CreateBall();
    }

    public void GetGoal(int player)
    {
        if(player == 1)
        {
            player1Health -= 1;
            sliderPlayer1.value = player1Health;

        }

        else if(player == 2)
        {
            player2Health -= 1;
            sliderPlayer2.value = player2Health;
        }

        CheckHealth();
    }

    private void CheckHealth()
    {
        if(player1Health <= 0)
        {
            Debug.Log("Game Over. Player 2 has won!");
            SceneManager.LoadScene("gameOver");
        }

        if (player2Health <= 0)
        {
            Debug.Log("Game Over. Player 1 has won!");
            SceneManager.LoadScene("gameOver");
        }
    }

}
