using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class gameController : MonoBehaviour {
	public GameObject cube;
	Vector3 cubePosition; 
	public static int airplaneX, airplaneY;
	static GameObject[,] grid;
	static bool isActive;
	public static float scaleSize;
	float turnLength,turnTime;
	public static int airplaneCapacity, cargo, cargoGain;
	public static int startPositionX,startPositionY;
	int score;
	public static int deliveryX, deliveryY;
	public static int gridX, gridY;
	public static int totalCargo;
	// Use this for initialization
	void Start (){
		
	//location variables 
	gridX = 16; 
	gridY = 9;
	grid = new GameObject[gridX,gridY];
	airplaneY = gridY-1; 
	airplaneX = 0;
	startPositionX = 0; 
	startPositionY = gridY-1;
	deliveryX = gridX-1; 
	deliveryY = 0;
	//cargo = airplaneCargo.
	//score variables
	cargo = 0; 
	totalCargo = cargo;
	airplaneCapacity = 90;
	cargoGain = 10;
	score = cargo;
	//setting variables
	isActive = false;
	scaleSize = 1.5f;
	turnLength = 1.5f; 
	turnTime = turnLength;
	//makes grid (look comments) 
		for (int z = 0; z < 9; z++) {
			for (int i = 0; i < 16; i++) {
				cubePosition = new Vector3 (i * 2 - 16, z * 2 - 5, 0); 
				grid[i,z] = Instantiate (cube, cubePosition, Quaternion.identity);
				grid[i,z].GetComponent<cubeController>().x = i; 
				grid[i,z].GetComponent<cubeController>().y = z;
			}
		}
		//makes airplaine start at left corner 
		grid [airplaneX, airplaneY].GetComponent<Renderer> ().material.color = Color.red;
		grid [deliveryX, deliveryY].GetComponent<Renderer> ().material.color = Color.black;
	}


	// Update is called once per frame
	void Update () {
		//print score and deal with cargo.
		if (Time.time > turnLength) {
			LoadCargo ();
			DeliverCargo ();
			print ("Your airplane cargo = " + cargo + "your total cargo = " +  totalCargo + "your score = " + score);
			turnLength += turnTime;
			}
		}


	public static void ProcessClick(GameObject clickedCube, int x, int y) {
		//checks to see if the airplane is active or not and then makes it active or not active, respectively
		if (x == airplaneX && y == airplaneY) {
			if (isActive == false) {
				isActive = true;
				//makes it look active 
				clickedCube.GetComponent<Renderer> ().material.color = Color.red;
				clickedCube.transform.localScale *= scaleSize; 
			} else if (isActive == true) { 
				isActive = false;
				//makes it look unactive
				clickedCube.GetComponent<Renderer> ().material.color = Color.yellow;
				clickedCube.transform.localScale /= scaleSize; 
			}
		} else {
			//makes the other cubes look inactive and the new cube the airplane is at look active.
			if (isActive == true) {
				//removes the airplane from the old spot.
				if (airplaneX == deliveryX && airplaneY == deliveryY) {
					grid [airplaneX, airplaneY].GetComponent<Renderer> ().material.color = Color.black;
				} else {
					grid [airplaneX, airplaneY].GetComponent<Renderer> ().material.color = Color.white;
				}  

				grid [airplaneX, airplaneY].transform.localScale /= scaleSize;
				airplaneX = x; 
				airplaneY = y; 
				grid [airplaneX, airplaneY].GetComponent<Renderer> ().material.color = Color.red;
				grid [airplaneX, airplaneY].transform.localScale *= scaleSize;

			}
		}
	}
	public void LoadCargo () {
		if (airplaneX == startPositionX && airplaneY == startPositionY && cargo < 90) { 
			//updates airplane cargo if the airplane capacity is less then the cargo.
			cargo += cargoGain;
			if (cargo > airplaneCapacity) {
				cargo = airplaneCapacity;
			}
		}

	}
	public void  DeliverCargo() {
		if (airplaneX == deliveryX && airplaneY == deliveryY) {
			score += cargo;
			cargo -= cargo; 
			print (score);
		}
	}
}


