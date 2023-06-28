using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> roomPrefabs;
    public int rows = 3;
    public int columns = 3;
    public float roomWidth = 50f;
    public float roomHeight = 50f;
    private Room[,] grid;

    private void Start()
    {
        GenerateMap();
    }

    // Returns a random room
    public GameObject RandomRoomPrefab()
    {
        return roomPrefabs[Random.Range(0, roomPrefabs.Count)];
    }

    public void GenerateMap()
    {
        // Clear out the grid - "column" is our X, "row" is our Y
        grid = new Room[columns, rows];

        // For each grid row...
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            // for each column in that row
            for (int currentColumn = 0; currentColumn < columns; currentColumn++)
            {
                // Figure out the location. 
                float xPosition = roomWidth * currentColumn;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);

                // Create a new grid at the appropriate location
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                // Set its parent
                tempRoomObj.transform.parent = this.transform;

                // Give it a meaningful name
                tempRoomObj.name = "Room_" + currentColumn + "," + currentRow;

                // Get the room object
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                // Save it to the grid array
                grid[currentColumn, currentRow] = tempRoom;


                if (currentRow == 0)
                {
                    grid[currentColumn, currentRow].doorSouth.SetActive(true);
                }
                if (currentRow == (rows - 1))
                {
                    grid[currentColumn, currentRow].doorNorth.SetActive(true);
                }
                if (currentColumn == 0)
                {
                    grid[currentColumn, currentRow].doorWest.SetActive(true);
                }
                if (currentColumn == (columns - 1))
                {
                    grid[currentColumn, currentRow].doorEast.SetActive(true);
                }
            }
        }
    }

    public void OpenDoors()
    {

    }


}