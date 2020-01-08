/*
 * 작성자 : 백성진
 * 
 * 타일의 정보를 담고있는 클래스 입니다.
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // 이 타일의 위치( 0 ~ 8 scale)
    public Vector2 TilePos { get; set; }

    // 이 타일위에 존재하는 유닛 정보
    public Unit unitInfo { get; set; }

    // 이 타일의 이펙트 효과 타입, 추후 enum 으로 변경.
    private int curEffectType;


    public void Effect()
    {
        Debug.Log(transform.position + " Effect 시작");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
