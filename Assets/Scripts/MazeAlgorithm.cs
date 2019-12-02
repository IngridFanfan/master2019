using UnityEngine;
using System.Collections;

public abstract class MazeAlgorithm { //an abstract class can't be instantiated with a new command, it needs to have a subclass that implements certain methodes -- CreateMaze, 
	protected MazeCell[,] mazeCells;
	protected int mazeRows, mazeColumns;

	protected MazeAlgorithm(MazeCell[,] mazeCells) : base() { //abstract class can have constructors and the subclass could call the constructor, because it's not being called directly.
		this.mazeCells = mazeCells; //initialize the cells
		mazeRows = mazeCells.GetLength(0); //get length of first dimension
		mazeColumns = mazeCells.GetLength(1); //get length of second dimension
    }

	public abstract void CreateMaze ();
}
