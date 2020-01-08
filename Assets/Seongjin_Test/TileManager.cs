/*
 * 작성자 : 백성진
 * 
 * 게임 시작시 맵, 유닛 생성, 게임 내 타일 관리 등을 담당하는 클래스 입니다.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private readonly float TILE_SIZE = 5f;
    private readonly float TILE_OFFSET = 5f;
    private readonly float TILE_CENTER_OFFSET = 2.5f;

    // 마우스 위치
    private int selectionX = -1;
    private int selectionY = -1;

    // 유닛 프리팹 리스트
    public List<GameObject> unitPrefabs;
    // 타일 프리팹 리스트
    public List<GameObject> tilePrefabs;

    private List<GameObject> activeUnits;

    // 타일 관리 리스트
    public List<List<Tile>> tiles;

    // Start is called before the first frame update
    void Start()
    {
        CreateTiles();
        CreateUnits();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSelection();
        DrawBoard();
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, LayerMask.GetMask("Tile")))
        {
            //Debug.Log(hit.point);
            selectionX = (int)(hit.point.x);
            selectionY = (int)(hit.point.z);
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    // 전체 타일 생성
    private void CreateTiles()
    {
        tiles = new List<List<Tile>>();

        for (int i = 0; i < 8; i++)
        {
            Vector3 spawnPos = Vector3.forward * i * TILE_OFFSET;
            spawnPos.z += TILE_CENTER_OFFSET;
            spawnPos.x += (TILE_CENTER_OFFSET - TILE_OFFSET);

            // 타일 리스트 할당
            tiles.Add(new List<Tile>());

            for (int j = 0; j < 8; j++)
            {
                spawnPos += Vector3.right * TILE_OFFSET; 
                GameObject go = (GameObject)Instantiate(tilePrefabs[0], spawnPos, Quaternion.identity);
                go.transform.SetParent(transform);

                // 일정 확률로 풀 생성
                if (Random.Range(0, 10) >= 7)
                {
                    GameObject grass = (GameObject)Instantiate(tilePrefabs[1], spawnPos, Quaternion.identity);
                    grass.transform.SetParent(transform);
                }

                // 타일 관리 리스트에 등록
                tiles[i].Add(go.GetComponent<Tile>());
                tiles[i][j].TilePos = new Vector2(i, j);
            }
        }
    }

    // 타일 선택시, 이벤트에 맞는 
    private void SelectTile()
    {

    }

    // 이동, 공격, 특수효과 등의 범위 표시를 지시하는 명령 함수,  추후 마지막 매개변수는 enum으로.
    private void DoEffect(int x, int y, int effectType)
    {
        // 해당 타일에게 특수효과를 지시한다.
        tiles[x][y].Effect();
    }

    // 전체 체스말 생성
    private void CreateUnits()
    {
        activeUnits = new List<GameObject>();

        // pin Index 는 list에서의 체스말의 번호. 열거형으로 바꿀 수 있음.

        // White team 생성
        // King
        Spawn4SetUnit(0, 3, 0);

        // Queen
        Spawn4SetUnit(1, 4, 0);

        // Rooks
        Spawn4SetUnit(2, 0, 0);
        Spawn4SetUnit(2, 7, 0);

        // Bishops
        Spawn4SetUnit(3, 2, 0);
        Spawn4SetUnit(3, 5, 0);

        // Knights
        Spawn4SetUnit(4, 1, 0);
        Spawn4SetUnit(4, 6, 0);

        // Pawns
        for(int i = 0; i < 8; i++)
        {
            Spawn4SetUnit(5, i, 1);
        }

        // Black team 생성
        // King
        Spawn4SetUnit(6, 3, 7);

        // Queen
        Spawn4SetUnit(7, 4, 7);

        // Rooks
        Spawn4SetUnit(8, 0, 7);
        Spawn4SetUnit(8, 7, 7);

        // Bishops
        Spawn4SetUnit(9, 2, 7);
        Spawn4SetUnit(9, 5, 7);

        // Knights
        Spawn4SetUnit(10, 1, 7);
        Spawn4SetUnit(10, 6, 7);

        // Pawns
        for (int i = 0; i < 8; i++)
        {
            Spawn4SetUnit(11, i, 6);
        }

    }


    // 체스말 4개단위 생성
    private void Spawn4SetUnit(int pinIndex, int x, int y)
    {
        Vector3 origin = Vector3.zero;
        // 2, 1 => 10, 5
        origin.x += (TILE_SIZE * x);
        origin.z += (TILE_SIZE * y);

        // King, Queen은 1개 유닛만 생성.
        if(pinIndex == 0 || pinIndex == 1 || pinIndex == 6 || pinIndex == 7)
        {
            SpawnUnit(pinIndex, new Vector3(origin.x + TILE_CENTER_OFFSET, 0, origin.z + TILE_CENTER_OFFSET));
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Vector3 spawnPos = origin;
                    spawnPos.x += (TILE_CENTER_OFFSET / 2) * (2 * i + 1);
                    spawnPos.z += (TILE_CENTER_OFFSET / 2) * (2 * j + 1);
                    SpawnUnit(pinIndex, spawnPos);
                }
            }
        }

    
    }

  

    // 체스말 1개를 생성
    private void SpawnUnit(int index, Vector3 position)
    {
        Debug.Log("상대 플레이어의 팀(white or black)에 따라서 쿼터니언 회전시켜주어야 함. 현재는 그냥 identity.");
        Debug.Log("현재 Boardmanager의 자식오브젝트가 되는데, 이를 새로운 whitePawn1 등의 이름을 가진 부모 오브젝트의 자식으로 생성한다. 이때 whitePawn1의 오브젝트에 Unit script 부착.");
        Debug.Log("즉, 타일 클릭시, 그 타일에 존재하는 오브젝트는 whitePawn1이 될 것이고 이를 통해 그 유닛의 공격,체력등 파악, UI뿌리기.");

        GameObject go = (GameObject)Instantiate(unitPrefabs[index], position, Quaternion.identity);
        go.transform.SetParent(transform);
        activeUnits.Add(go);
    }

    // Debug용 Gizmos
    private void DrawBoard()
    {
        Vector3 widthLine = Vector3.right * TILE_OFFSET * 8;
        Vector3 heightLine = Vector3.forward * TILE_OFFSET * 8;

        // 체스보드 
        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i * TILE_OFFSET;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j * TILE_OFFSET;
                Debug.DrawLine(start, start + heightLine);
            }
        }

        // 마우스 위치를 Scene view에 표시. 단, 8x8 중 마우스 위치에 맞는 한개의 정사각형의 대각선 모양을 그린다.
        if(selectionX >= 0 && selectionY >= 0)
        {
            int posX = (int)((int)(selectionX / TILE_OFFSET) * TILE_OFFSET);
            int posY = (int)((int)(selectionY / TILE_OFFSET) * TILE_OFFSET);
          
            // posX~posX + 1, posY~posY+1이 실제 타일의 정사각형 모양임.

            Debug.DrawLine(Vector3.forward * posY + Vector3.right * posX, Vector3.forward * (posY  + TILE_OFFSET) + Vector3.right * (posX + TILE_OFFSET));
            Debug.DrawLine(Vector3.forward * (posY + TILE_OFFSET) + Vector3.right * posX, Vector3.forward * posY + Vector3.right * (posX + TILE_OFFSET));

            // GameManager에게 Selected unit 정보를 넘기자.
            if (Input.GetMouseButtonDown(0))
            {
                // TestCode
                // Debug.Log(selectionX/5 + " " + selectionY/5);
                // DoEffect(selectionX / 5, selectionY / 5, 1);
                // SelectionX/5는 0~7 scale.

                /*
                if (GameManager.Phase == Phase.ChooseUnit)
                {
                    GameManager.curSelectedUnit = tiles[selectionX][selectionY];
                    return;
                }
                if(GameManager.Phase == Phase.ChooseTarget)
                {
                    GameManager.curSelectedTarget = tiles[selectionX][selectionY];
                }
                */

            }
            
        }
    }
}
