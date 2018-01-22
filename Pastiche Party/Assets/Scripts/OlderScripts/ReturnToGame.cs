using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReturnToGame : MonoBehaviour {

    public Text VictoryText;

	// Use this for initialization
	void Start () {
        if (GameControllerScript.control.GetWhoWon())
        {
            VictoryText.text = "Player 1 Wins!";
        } 
        else
        {
            VictoryText.text = "Player 2 Wins!";
        }
        StartCoroutine(ReturnToGameScreen());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator ReturnToGameScreen()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Intro Scene");
    }
}
