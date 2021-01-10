using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool m_CanMove;

    float h;
    float v;
    // Start is called before the first frame update

    private void Awake()
    {
        GameManager.Instance.OnGameOver += Instance_OnGameOver;
        GameManager.Instance.OnGameStart += Instance_OnGameStart;
        GameManager.Instance.OnGameReset += Instance_OnGameStart;
    }

    private void Instance_OnGameStart()
    {
        m_CanMove = true;
    }

    private void Instance_OnGameOver()
    {
        m_CanMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");

        if (!m_CanMove)
            return;

        //transform.position += new Vector3(h * Time.deltaTime * speed, v * Time.deltaTime * speed, 0);
    }

    private void FixedUpdate()
    {
        if (!m_CanMove)
            return;

        transform.position += new Vector3(h * Time.fixedDeltaTime * speed, v * Time.fixedDeltaTime * speed, 0);
        if(h > 0.1f)
            transform.DORotate(new Vector3(0, 90, 0), 0.3f);
        else if(h < -0.1f)
            transform.DORotate(new Vector3(0, -90, 0), 0.3f);
    }
}
