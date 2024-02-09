using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public int Lives = 4;
    public int Score = 0;

    public static GameManagerScript S;

    // Start is called before the first frame update
    void Awake()
    {
        S = this;
    }

    void Start()
    {
        Lives = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseLife()
    {
        Lives -= 1;
    }

    public void AddPoint(int numPoints)
    {
        Score += numPoints;
    }
}
