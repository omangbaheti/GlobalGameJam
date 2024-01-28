using System;
using System.Collections;
using System.Collections.Generic;
using Array2DEditor;
using UnityEngine;

public class TerritoryCalculator : Singleton<TerritoryCalculator>
{
    public float Player1Percent => player1Percentage;
    public float Player2Percent => player2Percentage;
    
    [Range(0.01f, 100f)]
    [SerializeField] private float scale;
    [SerializeField] private Transform originTransform;
    [SerializeField] private Vector2Int numberOfCells;
    [SerializeField] private float player1Percentage;
    [SerializeField] private float player2Percentage;
    
    [SerializeField] private Array2DInt territoryTracker ;
    [SerializeField] int player1Cells = 0;
    [SerializeField] int player2Cells = 0;
    
    
    public bool drawGizmo = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (drawGizmo)
        {
            for (int i = 0; i <= numberOfCells.x; i++)
            {
                for (int j = 0; j <= numberOfCells.y; j++)
                {
                    Debug.DrawRay(originTransform.position + new Vector3(i * scale, 0f, j * scale),
                        Vector3.forward * scale, Color.red);
                    Debug.DrawRay(originTransform.position + new Vector3(i * scale, 0f, j * scale),
                        Vector3.right * scale, Color.red);
                }
            }
        }
    }

    public void UpdateTerritory(Vector3 pos, float radius, int playerIndex)
    {
        Debug.Log($"Player index : {playerIndex}");
        pos -= originTransform.position; //reverse offset
        int scaledRadius = Mathf.RoundToInt(radius / scale);
        Vector2Int territory = new Vector2Int(Mathf.RoundToInt(pos.x/scale), Mathf.RoundToInt(pos.z/scale));
        int iterations = 0;
        for (int x = -scaledRadius; x < scaledRadius; x++)
        {
            
            for (int y = -scaledRadius; y < scaledRadius; y++)
            {
                if(territory.x + x < 0 || territory.x + x > numberOfCells.x) continue;
                if(territory.y + y < 0 || territory.y + y > numberOfCells.y) continue;
                territoryTracker.SetCell(territory.x + x, territory.y + y, playerIndex);
            }
        }
        
    }

    public void CalculateCapturedTerritory()
    {
        player1Cells = 0;
        player2Cells = 0;
        for (int i = 0; i < numberOfCells.x; i++)
        {
            for (int j = 0; j < numberOfCells.y; j++)
            {
                switch (territoryTracker.GetCell(i,j))
                {
                    case 1:
                        player1Cells++;
                        break;
                    case 2:
                        player2Cells++;
                        break;
                }
            }
        }
        if (player1Cells + player2Cells == 0) return;

        player1Percentage = (float) player1Cells / (player1Cells + player2Cells);
        player2Percentage = (float) player2Cells / (player1Cells + player2Cells);
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmo) return;
        
    }

    private void DrawGridGizmo()
    {
        
    }

    private void OnValidate()
    {
        for (int i = 0; i <= numberOfCells.x; i++)
        {
            for (int j = 0; j <= numberOfCells.y; j++)
            {
                Debug.DrawRay(originTransform.position + new Vector3(i * scale, 0f, j * scale),
                    Vector3.forward * scale * numberOfCells.y, Color.red);
                Debug.DrawRay(originTransform.position + new Vector3(i * scale, 0f, j * scale),
                    Vector3.right * scale * numberOfCells.y, Color.red);
            }
        }
        
    }

    IEnumerator ToggleBool()
    {
        yield return new WaitForSeconds(0.01f);
        drawGizmo = false;
    }
}
