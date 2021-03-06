﻿using UnityEngine;

public class Door : MazeCellEdge
{
    public Transform hinge;

    /*private Door OtherSideOfDoor
    {
        get
        {
            return otherCell.GetEdge(direction.GetOpposite()) as Door;
        }
    } */

    public override void Initialize(MazeCell primary, MazeCell other, MazeDirection direction)
    {
        base.Initialize(primary, other, direction);
        //if (OtherSideOfDoor != null)
        //{
            hinge.localScale = new Vector3(-1f, 1f, 1f);
            Vector3 p = hinge.localPosition;
            p.x = -p.x;
            hinge.localPosition = p;
        //}
    }
    
}