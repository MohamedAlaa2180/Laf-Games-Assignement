using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] int rows, cols;
    [SerializeField] GameObject block, arrow;
    float padding = 1.0f;
    float cellSize;
    int[,] gridArray;
    float gridWidth, gridHeight;
    [HideInInspector] public float unitSize = 1;

    public static GridController Instance;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gridWidth = padding * (cols + 1);
        gridHeight = padding * (rows +1);
        GenerateGrid();
    }

    public void UnitSize(float unitSize)
    {
        this.unitSize = unitSize;
    }

    void GenerateGrid()
    {
        GameObject block;
        gridArray = new int[cols, rows];

        for (int i=0; i < cols + 2; i++)
        {
            for(int j=0; j<rows + 2; j++)
            {
               
                if( j == 0 || i == 0 || j == rows + 1 || i == cols + 1)
                {
                    if(j == 0 && i == 0)
                    {
                        continue;
                    }
                    else if(j == 0 & i == cols +1)
                    {
                        continue;
                    }
                    else if(j == rows + 1 && i == 0)
                    {
                        continue;
                    }
                    else if(j == rows +1 && i == cols +1)
                    {
                        continue;
                    }
                    else
                    {
                        block = Instantiate(arrow, transform);
                    }
                    
                }
                else
                {
                    block = Instantiate(this.block, transform);
                }
                
                float posX = i * padding;
                float posY = j * -padding;

                block.transform.position = new Vector3(posX, posY, 0);
                
            }
        }
        transform.position = new Vector3(-cols, rows);
    }

    public float GetWidth()
    {
        return gridWidth;
    }

    public float GetHeight()
    {
        return gridHeight;
    }
}
