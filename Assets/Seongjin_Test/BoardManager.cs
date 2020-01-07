using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    private readonly float TILE_SIZE = 5f;
    private readonly float TILE_OFFSET = 5f;

    // 마우스 위치
    private int selectionX = -1;
    private int selectionY = -1;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50f, LayerMask.GetMask("ChessBoard")))
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
