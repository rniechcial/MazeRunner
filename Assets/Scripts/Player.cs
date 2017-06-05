using UnityEngine;

public class Player: MonoBehaviour
{

    private MazeCell currentCell;
    private MazeDirection currentDirection;
    public float _Radius = 2.0f;

    public void SetLocation(MazeCell cell) {
        currentCell = cell;
        transform.localPosition = cell.transform.localPosition;
    }

    public void PickUpItem(TGK.Communication.Messages.KeyAction keyAction)
    {
        Debug.Log("Player has "+ keyAction.key.keyName);
    }

    private void Move(MazeDirection direction) {
        MazeCellEdge edge = currentCell.GetEdge(direction);
        if (edge is MazePassage)
        {
            SetLocation(edge.otherCell);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(currentDirection);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(currentDirection.GetNextClockwise());
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(currentDirection.GetOpposite());
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(currentDirection.GetNextCounterclockwise());
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Rotate(currentDirection.GetNextCounterclockwise());
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Rotate(currentDirection.GetNextClockwise());
        }
        //else if (Input.GetKeyDown(KeyCode.Space))
        //{
        //
         //   Debug.Log("Pick up item action");
        //}
    }

    private void Rotate(MazeDirection direction)
    {
        transform.localRotation = direction.ToRotation();
        currentDirection = direction;
    }

    private void OnDrawGizmos()
    {
        // Draw Action message send radius
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _Radius);
    }
}
