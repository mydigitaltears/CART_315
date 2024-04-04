using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    [SerializeField]
    private GameObject CardPrefab;
    // private bool Restart = false;
    // private bool Down = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnCard()
    {
        //Down = true;
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(CardPrefab, new Vector3(position.x, position.y-1.5f, 0), Quaternion.identity);
        //StartCoroutine(RestartDelay());
    }


    // IEnumerator RestartDelay(){
    //     yield return new WaitForSeconds(5f);
    //     if (Down == true){
    //         GameObject[] Cards = GameObject.FindGameObjectsWithTag("Card");
    //         foreach(GameObject Card in Cards)
    //             Destroy(Card);
    //     }
    // }
}
