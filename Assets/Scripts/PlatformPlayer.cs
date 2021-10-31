using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayer : MonoBehaviour
{
    public float speed = 4.5f;

    public float jumpForce = 12.0f;

    private Rigidbody2D _rigidBody;

    private Animator _anim;

    private BoxCollider2D _box;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, _rigidBody.velocity.y);
        _rigidBody.velocity = movement;

        _anim.SetFloat("speed", Mathf.Abs(deltaX));

        // reverse X Axle direction
        if (!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1, 1);
        }

        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;

        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);

        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = false;
        if(hit != null)
        {
            grounded = true;
        }

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
