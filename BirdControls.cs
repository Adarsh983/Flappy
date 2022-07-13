using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdControls : MonoBehaviour
{
    private Rigidbody2D rb;
    public KeyCode jumpButton;
    public float jumpSpeed;
    private bool control;
    private float angle;
    [Range(0,10)]
    [SerializeField] private float multiplier;
    [Range(0, 10)]
    [SerializeField] private float smoothFactor;
    private float xPos;
    private GameManager gm;
    public GameObject jump;
    public GameObject die;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        control = true;
        xPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(jumpButton) && control)
        {
            rb.velocity = new Vector2(0f, jumpSpeed);
            GameObject j = Instantiate(jump, Vector2.zero, Quaternion.identity);
            Destroy(j, 1f);
        }
        angle = Mathf.Clamp(rb.velocity.y * multiplier, -90f, 90f);
        transform.rotation = Quaternion.Slerp(transform.rotation , Quaternion.Euler(0f , 0f , angle) , smoothFactor) ;
        transform.position = new Vector2(xPos, transform.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   /*
        if (collision.collider.CompareTag("pipe"))
        {
            StopControls();
            gm.EndGame();
        }
        */
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("pipe"))
        {
            StopControls();
            GameObject d = Instantiate(die, Vector2.zero, Quaternion.identity);
            Destroy(d, 1f);
            gm.EndGame();
        }
        if (collision.CompareTag("1up"))
        {
            gm.UpdateScore();
            Debug.Log(gm.GetScore());
        }
    }

    public void StopControls()
    {
        control = false;
    }
}
