using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class symbolController : MonoBehaviour {

    public static string[] symbolTypes =
    {
        "Dojhu",
        "Yog",
        "Kholw",
        "Xoytil",
    };

    public string color;
    public string symbol;//this is the value of the symbol it should be set by the roullete controller when its created.
    public float speed = 0.0f;
    MatchManager match;//refrence to the match
    public RoulleteController wheel { get; set; } //refrence to the roullete controller
    public int breakCount = 0;//how many times this needs to be touched
    public bool touched = false;//if the player has touched this
    public GameObject m_SymbolPool; //the starting point of for the symobl set by the roulette controller again

    void Awake()
    {
        if(match == null)
        {
            match = MatchManager.instance;
        }
    }

    void OnEnable()
    {
        MatchManager.OnSymbolUsed += CastSymol;
    }


    void OnDisale()
    {
        MatchManager.OnSymbolUsed -= CastSymol;
    }

    void CastSymol()
    {
        
    }

	// Use this for initialization
	void Start () {
		
        switch( symbol )
        {
            case "Dojhu":
                GetComponent<SpriteRenderer>().color = Color.green;
                color = "green";
                break;
            case "Yog":
                GetComponent<SpriteRenderer>().color = Color.blue;
                color = "blue";
                break;
            case "Kholw":
                GetComponent<SpriteRenderer>().color = Color.red;
                color = "red";
                break;
            case "Xoytil":
                GetComponent<SpriteRenderer>().color = Color.yellow;
                color = "yellow";
                break;
        }

	}
	
	// Update is called once per frame
	void Update () {

        if(touched == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touchPos = new Vector2(wp.x, wp.y);
                if (this.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                {
                    //Destroy(this.gameObject);
                    UseSymbol();
                }
            }

            if (Input.touchCount == 1)
            {
                Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Vector2 touchPos = new Vector2(wp.x, wp.y);
                if (this.GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                {
                    //Destroy(this.gameObject);
                    UseSymbol();
                }
            }
        }
        

        //transform.Translate(transform.position.x, transform.position.y - 0.01f, transform.position.z);
        transform.position = new Vector3(transform.position.x, transform.position.y - speed);

    }

    void UseSymbol()
    {
        //this needs to hide the symbol and prevent it from responding to touch events
        if (breakCount > 0)
        {
            breakCount--;
        }
        else
        {
            touched = true;
            GetComponent<SpriteRenderer>().enabled = false;
            MatchManager.castSymbol( this );
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag=="EndSymbol")
        {
            //move this symbol back to the symbol pool
            //and reset its flags.
            speed = 0;
            touched = false;
            transform.position = m_SymbolPool.transform.position;
            GetComponent<SpriteRenderer>().enabled = true;
        }

    }

}
