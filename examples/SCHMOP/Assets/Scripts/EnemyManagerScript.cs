using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerScript : MonoBehaviour
{
    public GameObject enemy;
    public int rows, columns;

    public float enemySpacing_h;
    public float enemySpacing_v;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < columns+1; i++)
        {
            for(int j = 0; j < rows; j++)
            {
                
                float xPos = Mathf.Lerp(-columns*enemySpacing_h, columns*enemySpacing_h, ((float)i/((float)columns+1)));
                Debug.Log(xPos);
                float yPos = rows - (j * enemySpacing_v);
                Instantiate(enemy, new Vector3(xPos, yPos, 0f), Quaternion.identity, this.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
