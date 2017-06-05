using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Maze mazePrefab;

    private Maze mazeInstance;

    public Player playerPrefab;

    private Player playerInstance;

    public Key keyPrefab;

    private Key keyInstance;


    // Use this for initialization
    public void Start () {
        StartCoroutine(BeginGame());
        
	}
	
	// Update is called once per frame
	public void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

    }

    private IEnumerator BeginGame()
    {
        //Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
        mazeInstance = Instantiate(mazePrefab) as Maze;
        yield return StartCoroutine(mazeInstance.Generate());
        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
        keyInstance = Instantiate(keyPrefab, mazeInstance.GetCell(mazeInstance.RandomCoordinates).transform) as Key;
        //keyInstance.set
        //Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
    }

    public void RestartGame() {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        if (playerInstance != null)
        {
            Destroy(playerInstance.gameObject);
        }
        StartCoroutine(BeginGame());
    }
}
