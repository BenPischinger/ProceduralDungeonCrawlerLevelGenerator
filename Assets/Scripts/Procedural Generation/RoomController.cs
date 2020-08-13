using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInformation
{
    public string name;
    public int x, y, z;
    public int roomNumber;
}

// Script to generate the dungeon layout
public class RoomController : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;

    public static RoomController roomControllerInstance;
     
    public RoomInformation currentRoomData;

    Queue<RoomInformation> roomQueue = new Queue<RoomInformation>();

    public List<Room> loadedRooms = new List<Room>();

    string currentLevelName = "Dungeon";

    int roomCounter = 0;

    int itemRoomsAmount;

    bool loadingRoom = false;

    bool specialRoomsSpawned = false;

    bool roomsUpdated = false;

    void Awake()
    {
        roomControllerInstance = this;
    }

    // Test method to load rooms manually
    private void Start()
    {
        //LoadRoom("StartRoom",  0,  0,  0);
        //LoadRoom("StandardRoom",  1,  0,  0);
        //LoadRoom("StandardRoom", -1,  0,  0);
        //LoadRoom("StandardRoom",  0,  0,  1);
        //LoadRoom("StandardRoom",  0,  0, -1);
    }

    // Updates the room queue and checks for boss room
    // Calls the method to place doors once a boss room was spawned
    public void UpdateRoomQueue() 
    {
        if (loadingRoom) 
        {
            return;
        }

        if (roomQueue.Count == 0)
        {
            if (!specialRoomsSpawned)
            {
                StartCoroutine(SpawnSpecialRooms());
            } 

            else if(specialRoomsSpawned && !roomsUpdated)
            {
                foreach (Room room in loadedRooms)
                {
                    room.PlaceDoors();
                }

                roomsUpdated = true;
            }

            else if (roomsUpdated)
            {
                for(int i = 1; i < dungeonGenerationData.numberOfItemRooms + 1; ++i)
                {
                    loadedRooms[loadedRooms.Count - i].PlaceDoors();
                }      
            }
            return;
        }
            currentRoomData = roomQueue.Dequeue();
                 
            loadingRoom = true;

            StartCoroutine(LoadRoomRoutine(currentRoomData));
    }

    // Creates the queue for the rooms that have to be loaded
    public void LoadRoomQueue(string name, int x, int y, int z)
    {
        if (DoesRoomExist(x, y, z)) 
        {
            return;
        }

        ++roomCounter;

        RoomInformation newRoomData = new RoomInformation();
        newRoomData.name = name;
        newRoomData.x = x;
        newRoomData.y = y;
        newRoomData.z = z;
        newRoomData.roomNumber = roomCounter;

        roomQueue.Enqueue(newRoomData);
    }

    // Coroutine that adds the scenes of the rooms to the main scene
    IEnumerator LoadRoomRoutine(RoomInformation roomData)
    {
        string roomName = currentLevelName + roomData.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    // Checks if a room already exists at a position and destroys it if necessary
    public void CheckForOverlap(Room room) 
    {
        if(!DoesRoomExist(currentRoomData.x, 0, currentRoomData.z))
        {
            room.transform.position = new Vector3(
            currentRoomData.x * room.width,
            0,
            currentRoomData.z * room.depth);

            room.roomX = currentRoomData.x;
            room.roomY = currentRoomData.y;
            room.roomZ = currentRoomData.z;

            room.name = currentLevelName + "-" + currentRoomData.name + "-" + currentRoomData.roomNumber + " - " + room.roomX + ", " + room.roomY + ", " + room.roomZ;

            room.transform.parent = transform;

            loadingRoom = false;

            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            loadingRoom = false;
        } 
    }

    // Coroutine to spawn special rooms
    IEnumerator SpawnSpecialRooms()
    {
        specialRoomsSpawned = true;

        yield return new WaitForSeconds(1.0f);

        if (roomQueue.Count == 0)
        {
            SpawnBossRoom();

            SpawnItemRoom();
        }       
    }

    // Method to spawn the boss room
    public void SpawnBossRoom() 
    {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];

            Room tempRoom = new Room(bossRoom.roomX, bossRoom.roomY, bossRoom.roomZ);

            Destroy(bossRoom.gameObject);

            var roomToDelete = loadedRooms.Single(room => room.roomX == tempRoom.roomX && room.roomY == tempRoom.roomY && room.roomZ == tempRoom.roomZ);

            loadedRooms.Remove(roomToDelete);

            LoadRoomQueue("BossRoom", tempRoom.roomX, tempRoom.roomY, tempRoom.roomZ);

            loadedRooms.Add(tempRoom);
    }

    // Method to replace random rooms with item rooms
    public void SpawnItemRoom()
    {
        for (int i = 0; i < dungeonGenerationData.numberOfItemRooms; ++i)
        {
            int randomIndex = Random.Range(1, loadedRooms.Count - 3);

            while (loadedRooms[randomIndex].name.Contains("ItemRoom"))
            {
                randomIndex = Random.Range(1, loadedRooms.Count - 3);
            }

            Room itemRoom = loadedRooms[randomIndex];

            Room tempRoom = new Room(itemRoom.roomX, itemRoom.roomY, itemRoom.roomZ);

            Destroy(itemRoom.gameObject);

            var roomToDelete = loadedRooms.Single(room => room.roomX == tempRoom.roomX && room.roomY == tempRoom.roomY && room.roomZ == tempRoom.roomZ);

            loadedRooms.Remove(roomToDelete);

            LoadRoomQueue("ItemRoom", tempRoom.roomX, tempRoom.roomY, tempRoom.roomZ);

            loadedRooms.Add(tempRoom);
        }
    }

    // Returns a random room from a list of available scenes
    public string GetRandomRoom()
    {
        string[] possibleRooms = new string[]
        {
            "StandardRoom",
            "MonsterRoom"
        };

        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }

    // Returns a specific room
    public Room FindRoom(int x, int y, int z)
    {
        return loadedRooms.Find(item => item.roomX == x && item.roomY == y && item.roomZ == z);
    }

    // Searches through the loaded rooms to see if a room already exists at a certain position
    public bool DoesRoomExist(int x, int y, int z) 
    {
        return loadedRooms.Find(item => item.roomX == x && item.roomY == y && item.roomZ == z) != null;
    }

    // Update the room queue every frame
    void Update()
    {
        UpdateRoomQueue();
    }

}
