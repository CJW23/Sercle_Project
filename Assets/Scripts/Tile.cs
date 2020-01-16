using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileState { Idle, MouseOver, Clicked, CanMove }

public class Tile : MonoBehaviour
{
    private TileManager tileManager;
    private Vector2 myPos;
    [SerializeField] private TileState myState;
    private Unit myUnit;

    public Unit Unit { get { return myUnit; } }

    private void Update()
    {
        StateShower();
    }

    public void Initialize(float x, float y, TileManager tileManager)
    {
        myPos = new Vector2(x, y);
        myState = TileState.Idle;
        this.tileManager = tileManager;
    }

    public void AssignUnit(Unit unit)
    {
        if (myUnit == null)
            myUnit = unit;
    }

    private void OnMouseEnter()
    {
        myState = TileState.MouseOver;
    }

    private void OnMouseDown()
    {
        myState = TileState.Clicked;
        print(myUnit);
    }

    private void OnMouseExit()
    {
        myState = TileState.Idle;
    }

    private void StateShower()
    {
        switch (myState)
        {
            case TileState.Idle:
                GetComponent<Renderer>().material.color = Color.white;
                break;
            case TileState.MouseOver:
                GetComponent<Renderer>().material.color = Color.yellow;
                break;
            case TileState.Clicked:
                GetComponent<Renderer>().material.color = Color.red;
                break;
            case TileState.CanMove:
                GetComponent<Renderer>().material.color = Color.blue;
                break;
            default:
                Debug.LogError("Something crucial things happen during State Shower of " + myPos + " tile");
                break;
        }
    }
}
