using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dupliateFloors : MonoBehaviour
{
    public GameObject preFloor;
    public GameObject preWall;
    
    private GameObject floors;
    private int r;
    private int c;
    private float size;

    


    void Start()
    {
        r = GameObject.Find("GameManager (Maze Loader Holder)").GetComponent<MazeLoader>().mazeRows;
        c = GameObject.Find("GameManager (Maze Loader Holder)").GetComponent<MazeLoader>().mazeColumns;
        size = GameObject.Find("GameManager (Maze Loader Holder)").GetComponent<MazeLoader>().size;

        

        //Floor create
        for(int i = r-1; i< r+1; i++)
        {
            for(int j = c; j < c+2; j++)
            {
                floors = Instantiate(preFloor, new Vector3((i+1) * size, -(size / 6f), (j-2) * size), Quaternion.identity) as GameObject;
                floors.name = "Floor " + i + "," + j;
                floors.transform.Rotate(Vector3.right, 90f);
            }
        }         

        //Northwall create
        for (int i = r ; i < r + 2; i++)
        {
            for (int j = c-3; j < c ; j++)
            {
                floors = Instantiate(preWall, new Vector3(i * size, 0, (j * size) + (size / 2f)), Quaternion.identity) as GameObject;
                floors.name = "East Wall " + i + "," + j; ;
            }
        }

        GameObject.Find("East Wall " + r + "," + (c - 2)).SetActive(false);
        GameObject.Find("East Wall " + (r + 1) + "," + (c - 2)).SetActive(false);

    }

    void Update()
    {
        
    }
    
}
