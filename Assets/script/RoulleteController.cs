using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoulleteController : MonoBehaviour {

    //this class should be given a set of symbols, from which 
    //the symbols are generated. 
    List<symbolController> mySymbols = new List<symbolController>();
    public GameObject StartingSymbolPool;

    public GameObject Prefab_Dojhu;
    public GameObject Prefab_Yog;
    public GameObject Prefab_Kholw;
    public GameObject Prefab_Xoytil;

    public float MAX_SymbolTimer = 3.0f;
    protected float newSymbolTimer = 3.0f;
    public float symbolStartDelay = 0;

    public float speed = -1.5f;

    // Use this for initialization
    void Start () {

        newSymbolTimer = MAX_SymbolTimer;

        if (symbolStartDelay == 0)
            symbolStartDelay = Random.Range(1.0f, 3.0f);

        if( StartingSymbolPool == null)
        {
            Debug.Log("NO Starting Symobl pool given destroying Roullette");
            Destroy(this.gameObject);
        }

        int counter = 0;
        //create 3 of each kind of symbol and put them into the pool
        foreach( string s in symbolController.symbolTypes)
        {
            symbolController tempSymbol = null;
            if (s.ToLower() == "dojhu")
            {
                setUpSymbol(s, 0.0f);
                counter++;
                setUpSymbol(s, 0.0f);
                counter++;
                setUpSymbol(s, 0.0f);
                counter++;
            }
            else if (s.ToLower() == "yog")
            {
                setUpSymbol(s, 0.0f);
                counter++;
                setUpSymbol(s, 0.0f);
                counter++;
                setUpSymbol(s, 0.0f);
                counter++;
            }
            else if (s.ToLower() == "kholw")
            {
                setUpSymbol(s, 0.0f);
                counter++;
                setUpSymbol(s, 0.0f);
                counter++;
                setUpSymbol(s, 0.0f);
                counter++;
            }
            else if (s.ToLower() == "xoytil")
            {
                setUpSymbol(s, 0.0f);
                counter++;
                setUpSymbol(s, 0.0f);
                counter++;
                setUpSymbol(s, 0.0f);
                counter++;
            }
        }
	}
	
    private void setUpSymbol( string sym, float speed)
    {
        var tempSymbol = GameObject.Instantiate(Prefab_Xoytil, StartingSymbolPool.transform, true).GetComponent<symbolController>();
        tempSymbol.symbol = sym;
        tempSymbol.wheel = this;
        tempSymbol.m_SymbolPool = StartingSymbolPool;
        mySymbols.Add(tempSymbol);
        tempSymbol.speed = speed;
        tempSymbol.transform.position = new Vector3(StartingSymbolPool.transform.position.x, StartingSymbolPool.transform.position.y);
        
    }

	// Update is called once per frame
	void Update () {
		
        if( symbolStartDelay > 0)
        {
            symbolStartDelay -= Time.deltaTime;

        }
        else
        {
            if(newSymbolTimer > 0)
            {
                newSymbolTimer -= Time.deltaTime;
                
            }
            else
            {
                //set a random symobl to move and reset timer
                bool symbolfound = false;
                while (symbolfound == false)
                {
                    int rand = Random.Range(0, mySymbols.Count);
                    if (mySymbols[rand].speed == 0)
                    {
                        mySymbols[rand].speed = speed;
                        symbolfound = true;
                    }

                }

                newSymbolTimer = MAX_SymbolTimer;
            }
        }

	}
}
