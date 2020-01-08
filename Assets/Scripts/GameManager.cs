using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Tile selectedTile { get; set; }
    public Tile targetTile { get; set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }


}
