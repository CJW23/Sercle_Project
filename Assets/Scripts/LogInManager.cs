/*
 * 작성자 : 백성진
 * 
 * 로그인 화면을 관리하는 매니저 스크립트 입니다.
 */ 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogInManager : MonoBehaviour
{
    [Header("InputFields")]
    public InputField idInput;
    public InputField pwInput;

    [Header("Buttons")]
    public Button signInButton;
    public Button signUpButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    
    
    // 로그인 버튼 클릭시 이벤트 발생 함수
    public void SignInBtn()
    {
        // DB 연결 후 질의 후, 존재하면 다음 씬으로 이동.
        // sample codes :
        if (idInput.text == "" && pwInput.text == "")
        {
            SceneManager.LoadScene("SJ_Test");
        }
        else
        {
            // 해당 id, pw가 존재하지 않았을 경우 다른 이벤트 실행.
        }
    }

    // 회원가입 버튼 클릭시 이벤트 발생 함수
    public void SignUpBtn()
    {
        // 회원가입 페이지 or 씬으로 이동.
    }
}
