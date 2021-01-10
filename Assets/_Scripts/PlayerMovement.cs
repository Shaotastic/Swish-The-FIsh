using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool m_CanMove;
    [SerializeField] private Vector3 m_MouseInput;
    [SerializeField] private Vector3 m_MouseDelta;
    [SerializeField] private bool m_Moveable;

    float h;
    float v;
    // Start is called before the first frame update

    private void Awake()
    {
        GameManager.Instance.OnGameOver += Instance_OnGameOver;
        GameManager.Instance.OnGameStart += Instance_OnGameStart;
        GameManager.Instance.OnGameReset += Instance_OnGameStart;
        Input.simulateMouseWithTouches = true;
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

        m_MouseDelta = m_MouseInput;
        m_MouseInput = Input.mousePosition;
        m_MouseDelta = m_MouseInput - m_MouseDelta;

        RaycastToWorldSpace();

        if (!m_CanMove)
            return;

        if(Input.GetMouseButton(0) && m_Moveable)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(m_MouseInput);
            newPosition.z = 0;
            transform.position = newPosition;

            if (m_MouseDelta.x > 0.1f)
                transform.DORotate(new Vector3(0, 90, 0), 0.3f);
            else if (m_MouseDelta.x < -0.1f)
                transform.DORotate(new Vector3(0, -90, 0), 0.3f);
        }
    }

    private void FixedUpdate()
    {
        if (!m_CanMove)
            return;
    }

    void RaycastToWorldSpace()
    {
        RaycastHit hit;

        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(m_MouseInput);

        if (Physics.Raycast(screenToWorld, Vector3.forward, out hit, 50))
        {
            if (hit.transform.CompareTag("Player"))
            {
                m_Moveable = true;
                Debug.Log("Can move");
            }
        }
        else
        {
            m_Moveable = false;
            Debug.Log("Cannnottt move");
        }
    }
}
