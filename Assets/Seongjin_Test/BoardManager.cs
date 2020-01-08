using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private readonly float TILE_SIZE = 5f;
    private readonly float TILE_OFFSET = 5f;
    private readonly float TILE_CENTER_OFFSET = 2.5f;

    // 마우스 위치
    private int selectionX = -1;
    private int selectionY = -1;

    // 체스 말 관리 리스트
    public List<GameObject> chessmanPrefabs;

    private List<GameObject> activeChessman;

    // Start is called before the first frame update
    void Start()
    {
        SpawnAllChessman();
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
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, LayerMask.GetMask("ChessBoard")))
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

    // 전체 체스말 생성
    private void SpawnAllChessman()
    {
        activeChessman = new List<GameObject>();

        // pin Index 는 list에서의 체스말의 번호. 열거형으로 바꿀 수 있음.

        // White team 생성
        // King
        SpawnIntendedChessman(0, 3, 0);

        // Queen
        SpawnIntendedChessman(1, 4, 0);

        // Rooks
        SpawnIntendedChessman(2, 0, 0);
        SpawnIntendedChessman(2, 7, 0);

        // Bishops
        SpawnIntendedChessman(3, 2, 0);
        SpawnIntendedChessman(3, 5, 0);

        // Knights
        SpawnIntendedChessman(4, 1, 0);
        SpawnIntendedChessman(4, 6, 0);

        // Pawns
        for(int i = 0; i < 8; i++)
        {
            SpawnIntendedChessman(5, i, 1);
        }

        // Black team 생성
        // King
        SpawnIntendedChessman(6, 3, 7);

        // Queen
        SpawnIntendedChessman(7, 4, 7);

        // Rooks
        SpawnIntendedChessman(8, 0, 7);
        SpawnIntendedChessman(8, 7, 7);

        // Bishops
        SpawnIntendedChessman(9, 2, 7);
        SpawnIntendedChessman(9, 5, 7);

        // Knights
        SpawnIntendedChessman(10, 1, 7);
        SpawnIntendedChessman(10, 6, 7);

        // Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnIntendedChessman(11, i, 6);
        }

    }


    // 체스말 4개단위 생성
    private void SpawnIntendedChessman(int pinIndex, int x, int y)
    {
        Vector3 origin = Vector3.zero;
        // 2, 1 => 10, 5
        origin.x += (TILE_SIZE * x);
        origin.z += (TILE_SIZE * y);

        // King, Queen은 1개 유닛만 생성.
        if(pinIndex == 0 || pinIndex == 1 || pinIndex == 6 || pinIndex == 7)
        {
            SpawnChessman(pinIndex, new Vector3(origin.x + TILE_CENTER_OFFSET, 0, origin.z + TILE_CENTER_OFFSET));
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
                    SpawnChessman(pinIndex, spawnPos);
                }
            }
        }

    
    }

  

    // 체스말 1개를 생성
    private void SpawnChessman(int index, Vector3 position)
    {
        Debug.Log("상대 플레이어의 팀(white or black)에 따라서 쿼터니언 회전시켜주어야 함. 현재는 그냥 identity."); 
        GameObject go = (GameObject)Instantiate(chessmanPrefabs[index], position, Quaternion.identity);
        go.transform.SetParent(transform);
        activeChessman.Add(go);
    }



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
        }
    }
}
