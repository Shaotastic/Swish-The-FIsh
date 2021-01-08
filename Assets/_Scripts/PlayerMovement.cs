using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if(h > 0.1f)
        {
            spriteRenderer.flipX = true;
        }
        else if (h < -0.1f)
            spriteRenderer.flipX = false;

        transform.position += new Vector3(h * Time.deltaTime * speed, v * Time.deltaTime * speed, 0);
    }
}
