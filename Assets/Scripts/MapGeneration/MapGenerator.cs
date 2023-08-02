using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
 public class MapGeneratedEvent : UnityEvent
{

}
public class MapGenerator : MonoBehaviour
{
    public MapGeneratedEvent OnMapGenerated = new MapGeneratedEvent();
    public List<GameObject> roomPrefabs;
    public int rows = 3;
    public int columns = 3;
    public float roomWidth = 50f;
    public float roomHeight = 50f;
    private Room[,] grid;
    public int mapSeed = 13;
    public enum RandomType { Seeded, Random, MapOfTheDay }
    public RandomType randomType = RandomType.MapOfTheDay;

    private void Start()
    {
        //GenerateMap();
    }

    public void ChangeRandomization(bool isMapOfDay)
    {
        if (isMapOfDay)
        {
            randomType = RandomType.MapOfTheDay;
        }
        else
        {
            randomType = RandomType.Random;
        }
    }

    // Returns a random room
    public GameObject RandomRoomPrefab()
    {
        return roomPrefabs[UnityEngine.Random.Range(0, roomPrefabs.Count)];
    }
    public int DateToInt(DateTime dateToUse)
    {
        // Add our date up and return it
        return (dateToUse.Month * 1000000) + (dateToUse.Day * 10000) + dateToUse.Year;

    }

    public int DateAndTimeToInt(DateTime dateToUse)
    {
        return DateToInt(dateToUse) + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    public void GenerateMap()
    {
        switch (randomType)
        {
            case RandomType.Random:
                mapSeed = DateAndTimeToInt(DateTime.Now);
                break;
            case RandomType.Seeded:
                break;
            case RandomType.MapOfTheDay:
                mapSeed = DateToInt(DateTime.Now.Date);
                break;
            default:
                Debug.LogWarning("Unrecognized type of random map");
                break;
        }
        UnityEngine.Random.InitState(mapSeed);
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

        OnMapGenerated.Invoke();
    }

    public void OpenDoors()
    {

    }


}
