using UnityEngine;
using System.Collections;

public class MazeLoader : MonoBehaviour {
	public int mazeRows, mazeColumns;
    public GameObject cell;
	public GameObject wall;
	public float size = 2f;

	private MazeCell[,] mazeCells;

    private int r;
    private int c;

    void Start () {
		InitializeMaze ();

		MazeAlgorithm ma = new HuntAndKillMazeAlgorithm (mazeCells);
		ma.CreateMaze ();

        r = mazeCells.GetLength(0) - 1;
        c = mazeCells.GetLength(1) - 1;

        //disable last south wall
        GameObject.Find("South Wall " + r + "," + c).SetActive(false);

    }
	

	void Update () {

    }

	private void InitializeMaze() {

		mazeCells = new MazeCell[mazeRows,mazeColumns];

		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) { //only create south and east walls
				mazeCells [r, c] = new MazeCell ();

				// create floor
				mazeCells [r, c] .floor = Instantiate (cell, new Vector3 (r*size, -(size/6f), c*size), Quaternion.identity) as GameObject;
				mazeCells [r, c] .floor.name = "Floor " + r + "," + c;
				mazeCells [r, c] .floor.transform.Rotate (Vector3.right, 90f);

                //create wall
                if (c == 0) { //if it is the first column, it will create westwall
					mazeCells[r,c].westWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) - (size/2f)), Quaternion.identity) as GameObject;
					mazeCells [r, c].westWall.name = "West Wall " + r + "," + c;
				}

				mazeCells [r, c].eastWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) + (size/2f)), Quaternion.identity) as GameObject;
				mazeCells [r, c].eastWall.name = "East Wall " + r + "," + c;

				if (r == 0) { //if it is the first row, it will create the northwall
					mazeCells [r, c].northWall = Instantiate (wall, new Vector3 ((r*size) - (size/2f), 0, c*size), Quaternion.identity) as GameObject;
					mazeCells [r, c].northWall.name = "North Wall " + r + "," + c;
					mazeCells [r, c].northWall.transform.Rotate (Vector3.up * 90f);
				}

				mazeCells[r,c].southWall = Instantiate (wall, new Vector3 ((r*size) + (size/2f), 0, c*size ), Quaternion.identity) as GameObject;
				mazeCells [r, c].southWall.name = "South Wall " + r + "," + c;
				mazeCells [r, c].southWall.transform.Rotate (Vector3.up * 90f);
			}
		}
    }
}


//resource: https://www.youtube.com/watch?v=IrO4mswO2o4
/* 
some others: https://www.youtube.com/watch?v=pMD0DnOqYnU
https://www.youtube.com/watch?v=L8sfSBLBRGY

*/
