using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private float velocity = 8.0f;
    [SerializeField]
    private float maxVelocity = 12.0f;
    [SerializeField]
    private GameObject topCollider;
    [SerializeField]
    private GameObject bottomCollider;

    private TileCheck topCheck;

    private TileCheck bottomCheck;

    private bool ready;

    public float points = 0;

    private void Start()
    {
        Camera.main.GetComponent<CameraController>().player = this.gameObject.transform;
        sprite.color = Color.white;

        topCheck = topCollider.GetComponent<TileCheck>();
        bottomCheck = bottomCollider.GetComponent<TileCheck>();

        StartCoroutine(ResetPlayer());
    }

    private void Update()
    {
        if (ready)
            points += 2 * Time.deltaTime;

        GameObject.Find("Txt_Points").GetComponent<Text>().text = ""+ (int)points;

        if (!this.transform.GetChild(0).GetComponent<TileCheck>().levelManager)
            this.transform.GetChild(0).GetComponent<TileCheck>().levelManager = GameObject.Find("Level Controller").gameObject.GetComponent<LevelManager>();
    }

    private void LateUpdate()
    {
        if (velocity < maxVelocity)
            velocity += 0.01f * Time.deltaTime;

        if (ready)
            this.transform.Translate(0, velocity * Time.deltaTime, 0);

        if (topCheck.whiteTile && bottomCheck.whiteTile && sprite.color == Color.white || topCheck.blackTile && bottomCheck.blackTile && sprite.color == Color.black)
        {
            ready = false;
            GameObject.Find("Level Controller").GetComponent<LevelManager>().deathScreen();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ResetPlayer());
        }
    }

    public void Switch()
    {
        if (ready)
        {
            if (sprite.color == Color.white)
                sprite.color = Color.black;
            else
                sprite.color = Color.white;
        }      
    }

    public IEnumerator ResetPlayer()
    {
        this.transform.position = Vector3.zero;
        sprite.color = Color.white;
        yield return new WaitForSeconds(3);
        ready = true;
    }
}
