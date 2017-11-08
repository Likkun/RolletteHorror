using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spell
{
    //A spell is a seise of symbols that  must be met
    //These can be tjhe spells needed to beat the match
    //or they can be spells needed to kill monsters
    //or they could be spells attached to allies 
    //to gain benificial effects
    public List<KeyValuePair<string, string>> spellString { get; set; }
    public bool cast { get; set; }
    public string element { get; set; }
    public string monsterID { get; set; }
    public string allyID { get; set; }

}

public class PlayerAlly
{
    //A list of allies should be given to the match manager when the match begins
    //the match manager should refrence a list to gather all the required spell
    //and effects from the allies.
    public List<KeyValuePair<string, string>> spellString1 { get; set; }
    public List<KeyValuePair<string, string>> spellString2 { get; set; }
    public string element { get; set; }
    public string allyID { get; set; }
    public float cooldownSpell1 { get; set; }
    public float cooldownSpell2 { get; set; }
}

public class MatchManager : MonoBehaviour {

    public delegate void UseSymbol();
    public static event UseSymbol OnSymbolUsed;

    public Text SealText;
    //this class will register itself with the game manager so the game 
    //manager can access it for various information
    //this should have a set of routlette wheels that is keeps track of
    //the roulette wheels should be autonomous, and only
    //need to be given a set of symbols it can show.

    GameManager gm;

    public static MatchManager instance = null;

    public int section_num;
    public int world_num;

    public int difficulty;

    static List<spell> _mseals;
    static spell restoreSpell;

    void Awake()
    {
        if( instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        //we are not destroyed so create the list of combinations that must be met
        _mseals = new List<spell>();

        spell temp = new spell
        {
            spellString = new List<KeyValuePair<string, string>>()
        };

        temp.spellString.Add(new KeyValuePair<string, string>("blue","Yog"));
        _mseals.Add(temp);

        temp = new spell
        {
            spellString = new List<KeyValuePair<string, string>>()
        };

        temp.spellString.Add(new KeyValuePair<string, string>("blue", "Yog"));
        temp.spellString.Add(new KeyValuePair<string, string>("red", "Kholw"));
        _mseals.Add(temp);

        temp = new spell
        {
            spellString = new List<KeyValuePair<string, string>>()
        };

        temp.spellString.Add(new KeyValuePair<string, string>("blue", "Yog"));
        temp.spellString.Add(new KeyValuePair<string, string>("red", "Kholw"));
        temp.spellString.Add(new KeyValuePair<string, string>("green", "Dojhu"));
        _mseals.Add(temp);

        temp = new spell
        {
            spellString = new List<KeyValuePair<string, string>>()
        };

        temp.spellString.Add(new KeyValuePair<string, string>("blue", "Yog"));
        temp.spellString.Add(new KeyValuePair<string, string>("red", "Kholw"));
        temp.spellString.Add(new KeyValuePair<string, string>("green", "Dojhu"));
        temp.spellString.Add(new KeyValuePair<string, string>("blue", "Yog"));
        _mseals.Add(temp);


        //Here we should look for a list of allies in the GameManager
        if( GameManager.SelectedAllies != null &&
            GameManager.SelectedAllies.Count > 0)
        {
            //list of allies should be strings that will match 
            //allies ids in a list
            //using the ID from the game manager
            //create a new insatnce of the ally with proper
            //stats/spells

        }


    }

    void OnEnable()
    {
        //OnSymbolUsed += castSymbol;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        FillText();

    }

    void FillText()
    {
        if (SealText == null)
            return;
        else
            SealText.text = string.Empty;
       
        int counter = 1;
        foreach (spell entry in _mseals)
        {
            string currentStr = string.Empty;
            foreach (KeyValuePair<string, string> s in entry.spellString)
            {
                currentStr += s.Key + " " + s.Value + " ";
            }
            SealText.text += "Spell "+counter+": "+currentStr + "\n";
            counter++;
        }
        
        if( restoreSpell != null)
        {

            SealText.text += "\n\n";
            foreach(KeyValuePair<string, string> s in restoreSpell.spellString)
            {
                SealText.text += s.Key + " " + s.Value + " ";
            }

        }
    }

    public static void castSymbol( symbolController symbol )
    {
        Debug.Log("Cast Symbol");
        Debug.Log(symbol.color);
        Debug.Log(symbol.symbol);

        //get the top spell off the seal list
        spell currentSpell = _mseals[0];

        //restore spell is referring to a backup copy of the target spell
        //if the player does not put it in, in the correct order
        ///then the curernt spell will revert to the restore spell.
        if( restoreSpell == null)
        {
            restoreSpell = new spell()
            {
                spellString = new List<KeyValuePair<string, string>>()
            };
            foreach(KeyValuePair<string,string> s in currentSpell.spellString)
            {
                restoreSpell.spellString.Add(new KeyValuePair<string, string>( s.Key.ToString(), s.Value.ToString()));
            }
           // restoreSpell.spellString = currentSpell.spellString;
        }
        else
        {
            Debug.Log("ITS been SET");
        }

        //loop through current allies
        //for each ally loop through their spell and see if 
        //this symbol matches any of them


        KeyValuePair<string,string> currentSymbol = currentSpell.spellString[0];

        bool correctSymbol = false;

        if( currentSymbol.Key == symbol.color)
        {
            if( currentSymbol.Value == symbol.symbol)
            {
                correctSymbol = true;
            }
        }

        if( correctSymbol)
        {
            //remove the symbol 
            currentSpell.spellString.Remove(currentSymbol);
            if( currentSpell.spellString.Count <= 0)
            {
                _mseals.Remove(currentSpell);
                restoreSpell.spellString.Clear();
                // restoreSpell = _mseals[0];
                foreach (KeyValuePair<string, string> s in _mseals[0].spellString)
                {
                    restoreSpell.spellString.Add(new KeyValuePair<string, string>(s.Key.ToString(), s.Value.ToString()));
                }
            }
        }
        else
        {
            _mseals[0].spellString.Clear();
            //restore the spell
            foreach( KeyValuePair<string, string> s in restoreSpell.spellString)
            {
                _mseals[0].spellString.Add(new KeyValuePair<string, string>(s.Key, s.Value));
            }

        }
    }

}
