using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGridManager : UnitySingleton<GameGridManager>
{
    private const int ROW = 40;
    private const int COLUMN = 40;
    
    private GameGrid[,] m_gameMapGrid;

    private GameObject m_letTopPoint;
    private GameObject m_rightBottomPoint;
    
    public override void Init()
    {
        InitGirdMap();
    }

    public void InitGirdMap()
    {
        GameObject leftBottomPoint = CommonFunc.GetChild(gameObject, "BottomLeft");
        m_letTopPoint = CommonFunc.GetChild(gameObject, "TopLeft");
        m_rightBottomPoint = CommonFunc.GetChild(gameObject, "BottomRight");

        Vector3 leftBottomPosition = leftBottomPoint.transform.position;
        m_gameMapGrid = new GameGrid[ROW, COLUMN];
        float width, height;
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COLUMN; j++)
            {
                GetWidthAndHeight(leftBottomPoint, m_letTopPoint, m_rightBottomPoint, out width, out height);
                Vector3 origin = new Vector3(leftBottomPosition.x +  width * i, leftBottomPosition.y + height * j, leftBottomPosition.z);
                m_gameMapGrid[i, j] = new GameGrid(i, j, origin, width, height);
            }
        }
    }

    public bool CheckIsOutOfMap(Vector3 position)
    {
        return position.x < m_letTopPoint.transform.position.x ||
               position.x > m_rightBottomPoint.transform.position.x ||
               position.y < m_rightBottomPoint.transform.position.y ||
               position.y > m_letTopPoint.transform.position.y;
    }

    public void Dispose()
    {
        if(m_gameMapGrid == null){return;}
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COLUMN; j++)
            {
                m_gameMapGrid[i, j].Dispose();
            }
        }

        m_gameMapGrid = null;
    }

    private void Update()
    {
        DrawLine();
    }

    private void GetWidthAndHeight(GameObject leftBottom, GameObject leftTop, GameObject rightBottom, out float width,
        out float height)
    {
        float y = leftTop.transform.position.y - leftBottom.transform.position.y;
        float x = rightBottom.transform.position.x - leftBottom.transform.position.x;
        height = y / ROW;
        width = x / COLUMN;
    }
    
    private void CalculateGridIndexByPosition(Vector2 position, out int x, out int y)
    {
        x = 0;
        y = 0;
        if (m_gameMapGrid != null)
        {
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUMN; j++)
                {
                    if (m_gameMapGrid[i, j].CheckIsInRect(position))
                    {
                        x = m_gameMapGrid[i, j].X;
                        y = m_gameMapGrid[i, j].Y;
                        return;
                    }
                }
            }
        }
    }

    public EGameGridAreaType GetCurAreaTypeByPosition(Vector2 position)
    {
        if (m_gameMapGrid == null)
        {
            return EGameGridAreaType.None;
        }
        int x, y;
        CalculateGridIndexByPosition(position, out x, out y);
        GameGrid grid = m_gameMapGrid[x, y];
        if (grid != null)
        {
            return grid.AreaType;
        }

        return EGameGridAreaType.None;
    }

    public void DrawLine()
    {
        if(m_gameMapGrid == null){return;}
        for (int i = 0; i < ROW; i++)
        {
            for (int j = 0; j < COLUMN; j++)
            {
                m_gameMapGrid[i,j].DrawLine();
            }
        }
    }

//    private void AddNeiborhoodGrid(GameGrid grid)
//    {
//        AddNeiborhoodGrid(grid.X - 1, grid.Y - 1);
//        AddNeiborhoodGrid(grid.X - 1, grid.Y);
//        AddNeiborhoodGrid(grid.X - 1, grid.Y + 1);
//        AddNeiborhoodGrid(grid.X, grid.Y - 1);
//        AddNeiborhoodGrid(grid.X, grid.Y + 1);
//        AddNeiborhoodGrid(grid.X + 1, grid.Y - 1);
//        AddNeiborhoodGrid(grid.X + 1, grid.Y);
//        AddNeiborhoodGrid(grid.X + 1, grid.Y + 1);
//    }
//
//    private void AddNeiborhoodGrid(int x, int y)
//    {
//        if (x >= 0 && x < ROW && y >= 0 && y < COLUMN)
//        {
//            GameGrid grid = m_gameMapGrid[x, y];
//            if (m_checkedGridList.Contains(grid))
//            {
//                return;
//            }
//
//            if (m_findGridList.Contains(grid))
//            {
//                return;
//            }
//
//            m_findGridList.Enqueue(m_gameMapGrid[x, y]);
//        }
//    }
}
