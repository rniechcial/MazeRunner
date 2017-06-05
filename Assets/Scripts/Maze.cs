using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour {

    public IntVector2 size;

    public MazeCell cellPrefab;

    private MazeCell[,] cells;

    public float generationStepDelay;

    public MazePassage passagePrefab;
    public MazeWall[] wallPrefabs;

    public Door doorPrefab;

    private Door doorInstance;

    private bool doorInitialized = false;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public MazeCell GetCell(IntVector2 coordinates) {
        return cells[coordinates.x, coordinates.z];
    }

    public IEnumerator Generate() {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0) {
            yield return delay;
                DoNextGenerationStep(activeCells);
            }
    }

    private void DoFirstGenerationStep(List<MazeCell> activeCells) {
        activeCells.Add(CreateCell(RandomCoordinates));
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (!doorInitialized && isEdge(currentCell.coordinates, direction))
        {
            //Debug.Log(direction);
            //Debug.Log(currentCell.coordinates.ToString());
            CreateDoor(currentCell, null, direction);
            doorInitialized = true;
        }
        else if (ContainsCoordinates(coordinates) && GetCell(coordinates) == null)
        {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
            }
        }
        else
        {
                CreateWall(currentCell, null, direction);
        }
    }

    private bool isEdge(IntVector2 coordinates, MazeDirection direction)
    {
        return ((coordinates.x == 0 && direction.Equals(MazeDirection.West)) ||
            (coordinates.z == 0 && direction.Equals(MazeDirection.South)) ||
            (coordinates.x == size.x && direction.Equals(MazeDirection.East)) ||
            (coordinates.z == size.z && direction.Equals(MazeDirection.North)));
    }

    private MazeCell CreateCell(IntVector2 coordinates) {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }

    public IntVector2 RandomCoordinates {
        get {
            return new IntVector2(UnityEngine.Random.Range(0, size.x), UnityEngine.Random.Range(0, size.z));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate) {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefabs[UnityEngine.Random.Range(0, wallPrefabs.Length)]) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = Instantiate(wallPrefabs[UnityEngine.Random.Range(0, wallPrefabs.Length)]) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    private void CreateDoor(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        Door wall = Instantiate(doorPrefab) as Door;
        wall.Initialize(cell, otherCell, direction);
        //Debug.Log(direction);
        //Debug.Log(cell.coordinates.ToString());
        //Debug.Log(GetCell(cell.coordinates));
        if (otherCell != null)
        {
            wall = Instantiate(doorPrefab) as Door;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }
}
