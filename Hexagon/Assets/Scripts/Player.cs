using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if !UNITY_EDITOR && UNITY_WEBGL
using System.Runtime.InteropServices;
#endif

public class Player : MonoBehaviour
{
    public static Player Instance;

    public float moveSpeed = 600f;

    public bool isPlay = false;
    public float startTimer = 2f;
    public CanvasGroup startCanvasGroup;
    public CanvasGroup deathCanvasGroup;
    public GameObject mobileButtons;

    bool gamestarted = false;
    bool closeStartMenu = false;
    bool isDeath = false;
    float movement = 0f;



#if !UNITY_EDITOR && UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern bool IsMobile();
#endif

    public bool isMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
             return IsMobile();
#endif
        return false;
    }


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (!isMobile())
            mobileButtons.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimer >= 0 && !gamestarted)
        {
            startTimer -= Time.deltaTime;
        }
        else
        {
            if (!gamestarted)
                isPlay = true;

            gamestarted = true;

            closeStartMenu = true;
        }

        if (closeStartMenu)
        {
            if (startCanvasGroup.alpha > 0)
                startCanvasGroup.alpha -= Time.deltaTime;
        }

        if (isDeath)
        {
            if (deathCanvasGroup.alpha < 1)
            {
                deathCanvasGroup.alpha += Time.deltaTime;
            }
            deathCanvasGroup.blocksRaycasts = true;
            deathCanvasGroup.interactable = true;
        }

        if (!isMobile())
        {
            movement = Input.GetAxisRaw("Horizontal");
        }
    }

    private void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, movement * Time.fixedDeltaTime * -moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlay = false;
        isDeath = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void MovePos()
    {
        if (!isMobile())
            return;

        movement = 1f;
    }

    public void MoveNeg()
    {
        if (!isMobile())
            return;

        movement = -1f;
    }

    public void NoMove()
    {
        if (!isMobile())
            return;
        
        movement = 0f;
    }
}
