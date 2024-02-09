using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickLayerManager : MonoBehaviour
{
    public GameObject brick;
    public int rows, columns;

    public float brickSpacing_h;
    public float brickSpacing_v;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < columns+1; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                
                float xPos = Mathf.Lerp(-columns*brickSpacing_h, columns*brickSpacing_h, ((float)i/((float)columns+1)));
                Debug.Log(xPos);
                float yPos = rows - (j * brickSpacing_v);
                Instantiate(brick, new Vector3(xPos, yPos, 0f), Quaternion.identity, this.transform);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
