using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject LeftBubble, RightBubble, UpBubble, DownBubble;

    public GameObject Fire, Water, Rock, Thunder, Block;

    private SpriteRenderer BubbleSr;

    [SerializeField]
    private KeyCode Left, Right, Up, Down;

    private Color red = new Color(1f, 0f, 0f);
    private Color white = new Color(1f, 1f, 1f);

    public static List<int> AttackBuffer = new List<int>();
    public static List<int> Attack1 = new List<int>(){1,2,3,4};
    public static List<int> Attack2 = new List<int>(){2,3,4,1};
    public static List<int> Attack3 = new List<int>(){3,4,1,2};
    public static List<int> Attack4 = new List<int>(){4,1,2,3};
    public static List<int> Block1 = new List<int>(){1,4,3};
    public static List<int> Block2 = new List<int>(){2,1,4};
    public static List<int> Block3 = new List<int>(){3,2,1};
    public static List<int> Block4 = new List<int>(){4,3,2};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Left))
        {
            BubbleSr = LeftBubble.GetComponent<SpriteRenderer>();
            BubbleSr.color = red;
            StartCoroutine(ColorDelay(BubbleSr));
            Attack(1);

        }
        else if (Input.GetKeyDown(Right))
        {
            BubbleSr = RightBubble.GetComponent<SpriteRenderer>();
            BubbleSr.color = red;
            StartCoroutine(ColorDelay(BubbleSr));
            Attack(3);
        }
        else if (Input.GetKeyDown(Up))
        {
            BubbleSr = UpBubble.GetComponent<SpriteRenderer>();
            BubbleSr.color = red;
            StartCoroutine(ColorDelay(BubbleSr));
            Attack(2);
        }
        else if (Input.GetKeyDown(Down))
        {
            BubbleSr = DownBubble.GetComponent<SpriteRenderer>();
            BubbleSr.color = red;
            StartCoroutine(ColorDelay(BubbleSr));
            Attack(4);
        }
    }

    void Attack(int InputNumber)
    {
        if (AttackBuffer.Count < 4)
        {
            AttackBuffer.Add(InputNumber);
            foreach(var input in AttackBuffer)
            {
                Debug.Log(input);
            }
            if (DoListsMatch(AttackBuffer, Block1))
            {
                AttackBuffer.Clear();
                DisplayAttackSprite(Fire, -6.0f);
                DisplayAttackSprite(Block, -6.1f);
            }
            else if (DoListsMatch(AttackBuffer, Block2))
            {
                AttackBuffer.Clear();
                DisplayAttackSprite(Water, -6.0f);
                DisplayAttackSprite(Block, -6.1f);
            }
            else if (DoListsMatch(AttackBuffer, Block3))
            {
                AttackBuffer.Clear();
                DisplayAttackSprite(Rock, -6.0f);
                DisplayAttackSprite(Block, -6.1f);
            }
            else if (DoListsMatch(AttackBuffer, Block4))
            {
                AttackBuffer.Clear();
                DisplayAttackSprite(Thunder, -6.0f);
                DisplayAttackSprite(Block, -6.1f);
            }
            else if (DoListsMatch(AttackBuffer, Attack1))
            {
                Debug.Log("Attack1!");
                AttackBuffer.Clear();
                DisplayAttackSprite(Fire, -6.0f);
            }
            else if (DoListsMatch(AttackBuffer, Attack2))
            {
                Debug.Log("Attack2!");
                AttackBuffer.Clear();
                DisplayAttackSprite(Water, -6.0f);
            }
            else if (DoListsMatch(AttackBuffer, Attack3))
            {
                Debug.Log("Attack3!");
                AttackBuffer.Clear();
                DisplayAttackSprite(Rock, -6.0f);
            }
            else if (DoListsMatch(AttackBuffer, Attack4))
            {
                Debug.Log("Attack4!");
                AttackBuffer.Clear();
                DisplayAttackSprite(Thunder, -6.0f);
                
            }
            else if (AttackBuffer.Count == 4){
                Debug.Log("Invalid");
                AttackBuffer.Clear();
            }
        }
    }

    void DisplayAttackSprite(GameObject Element, float distance)
    {
        GameObject SpawnedElement = Instantiate(Element, new Vector3(0, 0, distance), Quaternion.identity);
        StartCoroutine(SpriteDelay(SpawnedElement));
    }

    private bool DoListsMatch(List<int> list1, List<int> list2)
    {
        var areListsEqual = true;

        if (list1.Count != list2.Count)
            return false;

        for (var i = 0; i < list1.Count; i++)
        {
            if (list2[i] != list1[i])
            {
                areListsEqual = false;
            }
        }

        return areListsEqual;
    }

    IEnumerator ColorDelay(SpriteRenderer BubbleSr)
    {
        yield return new WaitForSeconds(0.2f);
        BubbleSr.color = white;
    }

    IEnumerator SpriteDelay(GameObject SpawnedElement)
    {
        yield return new WaitForSeconds(1f);
        if(SpawnedElement)
        {
            Destroy(SpawnedElement);
        }
    }
}
