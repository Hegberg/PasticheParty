using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour {

    public static GameControllerScript control;

    public Text player1Text;
    public Text player2Text;
    public Text scored;

    private int player1Score = 0;
    private int player2Score = 0;

    private int winAmount = 5;

    //flase if player 2 won, true if player 1 won
    private bool player1won = false;

    public Transform explosion;

	// Use this for initialization
	void Start () {
		if (control == null)
        {
            control = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        player1Text.transform.Translate(new Vector3(4, 4, 0));
        player2Text.transform.Translate(new Vector3(-4, 4, 0));

        player1Text.text = "Player 1 Score: " + player1Score.ToString();
        player2Text.text = "Player 2 Score: " + player2Score.ToString();
        scored.text = "";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateText()
    {
        player1Text.text = "Player 1 Score: " + player1Score.ToString();
        player2Text.text = "Player 2 Score: " + player2Score.ToString();
    }

    public void Player1Scored()
    {
        player1Score += 1;
        if (player1Score < 5)
        {
            scored.text = "Player 1 Scored!";
        }
        CheckWin();
    }

    public void Player2Scored()
    {
        player2Score += 1;
        if (player2Score < 5)
        {
            scored.text = "Player 2 Scored!";
        }
        CheckWin();
    }

    private void CheckWin()
    {
        if(player1Score == winAmount)
        {
            SceneManager.LoadScene("VictoryScene");
            player1won = true;
        }
        else if (player2Score == winAmount)
        {
            SceneManager.LoadScene("VictoryScene");
            player1won = false;
        }
        UpdateText();
        ResetPlaces();
    }

    public bool GetWhoWon()
    {
        return player1won;
    }

    public void ResetPlaces()
    {
        BroadcastMessage("ResetPlace");
        Transform temp = (Transform)Instantiate(explosion, Vector3.zero, Quaternion.identity);
        temp.SetParent(this.gameObject.transform);
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2.0f);
        scored.text = "";
        BroadcastMessage("ResetComplete");
    }
}
