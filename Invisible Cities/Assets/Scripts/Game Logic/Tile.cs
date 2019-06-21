using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    [SerializeField] bool occupied;

    public bool Occupied { get => this.occupied; set => this.occupied = value; }
}
