using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {

    [SerializeField]
    private GameObject gameManager;
    [SerializeField]
    private int playerGoal;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "puk")
        {
            Destroy(other.gameObject);
            gameManager.GetComponent<GameLogic>().CreateBall();

            //SCORE
            gameManager.GetComponent<GameLogic>().GetGoal(playerGoal);
        }
    }
}
