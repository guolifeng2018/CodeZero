using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameGridAreaType
{
    None = 0,               //无特殊区域
    Playground = 1,         //游乐场
}

public class GameGrid
{
    private int m_x;
    private int m_y;
    private float m_z;
    private Rect m_rect;
    private EGameGridAreaType m_areaType;

    public EGameGridAreaType AreaType
    {
        get { return m_areaType; }
    }

    public int X
    {
        get { return m_x; }
    }

    public int Y
    {
        get { return m_y; }
    }

    public GameGrid(int x, int y, Vector3 leftBottomPoint, float width, float height)
    {
        m_x = x;
        m_y = y;
        m_z = leftBottomPoint.z;
        m_rect = new Rect(leftBottomPoint.x, leftBottomPoint.y, width, height);
        if (x < 15 && y < 15)
        {
            m_areaType = EGameGridAreaType.Playground;
        }
        else
        {
            m_areaType = EGameGridAreaType.None;
        }
    }

    public bool CheckIsInRect(Vector2 position)
    {
        return m_rect.Contains(position);
    }

    public void Dispose()
    {
    }

    public void DrawLine()
    {
        Vector3 leftBottomPoint = new Vector3(m_rect.position.x, m_rect.position.y, m_z);
        Vector3 leftTopPoint = leftBottomPoint + Vector3.up * m_rect.height;
        Vector3 rightBottomPoint = leftBottomPoint + Vector3.right * m_rect.width;
        Vector3 rightTopPoint = leftBottomPoint + new Vector3(m_rect.width, m_rect.height, 0f);
        Debug.DrawLine(leftBottomPoint, leftTopPoint, Color.red);
        Debug.DrawLine(leftTopPoint, rightTopPoint, Color.red);
        Debug.DrawLine(rightTopPoint, rightBottomPoint, Color.red);
        Debug.DrawLine(rightBottomPoint, leftBottomPoint, Color.red);
    }
}
