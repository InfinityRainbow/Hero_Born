using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CustomExtensions;
using System.Linq;
public class GameBehavior : MonoBehaviour,IManager
{
    //interface
    private string _state;
    public string State
    {
        get { return _state; }
        set { _state = value; }
    }
   
    // Start is called before the first frame update

    //realize UI
    public int MaxItems = 4;
    public TMP_Text HealthText;
    public TMP_Text ItemText;
    public TMP_Text ProgressText;


    public Canvas PlayCanvas;
    //victory condition
    public Button WinButton;
    //lose condition
    public Button LossButton;
    //return to menu 
    public Button ReturnMenuButton;

    //Menu UI
    public Canvas MenuCanvas;
    public Button BeginButton;
    public Button SettingButton;
    public Button QuitButton;

    //Setting UI
    public Canvas SettingCanvas;
    public Button BackButton;

    //委托
    public delegate void DebugDelegate(string newText);
    public DebugDelegate debug = Print;
    //事件订阅
    public PlayerBehavior playerBehavior;

    private void OnEnable()
    {
        GameObject player = GameObject.Find("Player");
        playerBehavior = player.GetComponent<PlayerBehavior>();

        playerBehavior.playerJump += HandlePlayerJump;
        debug("Jump event subscribed..");
    }
    //清理事件订阅
    private void OnDisable()
    {
        playerBehavior.playerJump -= HandlePlayerJump;
        debug("Jump event unsubscribed..");
    }
    public void HandlePlayerJump()
    {
        debug("Player has jumped..");
    }


    void Start()
    {
        //UI
        ItemText.text += _itemsCollected;
        HealthText.text += _playerHP;
        
        Initialize();
        UpdateScene();


    }

    //委托测试
    public static void Print(string newText)
    {
        Debug.Log(newText);
    }
    public void LogWithDelegate(DebugDelegate del)
    {
        del("Delegating the debug task");
    }

    //Play Button
    public void HideMenu()
    {
        MenuCanvas.gameObject.SetActive(false);
        PlayCanvas.gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    //Setting Button
    public void ShowSettings()
    {
        MenuCanvas.gameObject.SetActive(false);
        SettingCanvas.gameObject.SetActive(true);
    }

    //Back Button
    public void BackMenu()
    {
        SettingCanvas.gameObject.SetActive(false);
        MenuCanvas.gameObject.SetActive(true);
    }


    //You won/You lose Button
    public void TryAgain()
    {
        //Time.timeScale = 0f;
        //LossButton.gameObject.SetActive(false);
        //WinButton.gameObject.SetActive(true);

        //MenuCanvas.gameObject.SetActive(true);
        //PlayCanvas.gameObject.SetActive(false);
        Utilities.RestartLevel();
    }

    //interface
    public Stack<Loot> LootStack = new Stack<Loot>();
    public void Initialize()
    {
        _state = "Game Manager initialized..";
        
        _state.FancyDebug();
       // Debug.Log(_state);
        //委托测试
        debug(_state);
        LogWithDelegate(debug);




        LootStack.Push(new Loot("Sword of Doom",5));
        LootStack.Push(new Loot("HP Boost",1));
        LootStack.Push(new Loot("Golden Key",3));
        LootStack.Push(new Loot("Pair of Winged Boots",2));
        LootStack.Push(new Loot("Mythril Bracer",4));

        FilterLoot();

        //泛型试验
        var itemShop = new Shop<Collectable>();
        itemShop.AddItem(new Potion());
        itemShop.AddItem(new Antidote());
        Debug.Log("Item for sale:" + itemShop.GetStockCount<Potion>());



    }
    public void UpdateScene(string updateText)
    {
        ProgressText.text = updateText;
        Time.timeScale = 0f;
    }
    public void UpdateScene()
    {
        Time.timeScale = 0f;
    }
    private int _playerHP = 2;
    public int HP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;
            Debug.LogFormat("Lives:{0}", _playerHP);

            HealthText.text = "Health:" + HP;
            Debug.LogFormat("Lives:{0}", _playerHP);
            if(_playerHP<=0)
            {
                UpdateScene("You want to try again?");
                //ProgressText.text = "You want to try again?";
                LossButton.gameObject.SetActive(true);
                ReturnMenuButton.gameObject.SetActive(true);
                //Time.timeScale = 0;
            }
            else
            {
                ProgressText.text = "Ouch...It's hurt..";
            }
        }
    }


    private int _itemsCollected = 0;
    public int Items
    {
        get
        {
            return _itemsCollected;
        }
        set
        {
            _itemsCollected = value;
            Debug.LogFormat("Items:{0}", _itemsCollected);
            ItemText.text = "Items Collected:" + Items;
            if(_itemsCollected>=MaxItems)
            {
                UpdateScene("You've found all the items!!");
                //ProgressText.text = "You've found all the items!!";
                WinButton.gameObject.SetActive(true);
                ReturnMenuButton.gameObject.SetActive(true);
                //Time.timeScale = 0f;
            }
            else
            {
                ProgressText.text = "Item found,only " + (MaxItems - _itemsCollected) + " more!!";
            }
        }
      
    }
    public void RestartScene()
    {
        try
        {
            Utilities.RestartLevel(-1);
            //-1测试异常处理
            debug("Level successfully restarted..");
        }
       catch(System.ArgumentException exception)
        {
            Utilities.RestartLevel(0);
            debug("Reverting to scene 0: " + exception.ToString());
        }
        finally
        {
            debug("Level restart has completed..");
        }
        //SceneManager.LoadScene(0);
        //Time.timeScale = 1f;
    }
    public void PrintLootReport()
    {
        var currentItem = LootStack.Pop();
        var nextItem = LootStack.Peek();
        Debug.LogFormat("You got a {0} ! You've got a good chance of " +
            "finding of finding a {1} next!", currentItem.name, nextItem.name);
        Debug.LogFormat("There are{0} random loot items waiting for you !", LootStack.Count);
    }

    public void FilterLoot()
    {
        //var rareLoot = LootStack.Where(LootPredicate);
        //var rareLoot = LootStack
        //    .Where(item => item.rarity >= 3)//LINQ查询方法
        //    .OrderBy(item => item.rarity)
        //    .Select(item => new
        //    {
        //        item.name
        //    });//lamda表达式,用匿名的方法节省代码
        var rareLoot = from item in LootStack //LINQ解析式查询语法化简代码
                       where item.rarity >= 3 
                       orderby item.rarity 
                       select new 
                       { 
                           item.name
                       };
        foreach (var item in rareLoot)
        {
            Debug.LogFormat("Rare item:{0}!", item.name);
        }
    }
    public bool LootPredicate(Loot loot)
    {
        return loot.rarity >= 3;
    }
    // Update is called once per frame


  
    void Update()
    {
        
    }
}
