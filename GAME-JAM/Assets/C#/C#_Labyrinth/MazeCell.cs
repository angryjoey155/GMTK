using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField] GameObject[] _walls;
    [SerializeField] GameObject _ground;
    public void RemoveWall(int wallIndex)
    {
        _walls[wallIndex].gameObject.SetActive(false);
    }
    public bool HasFloor(int wallIndex)
    {
        if (_walls[wallIndex].activeInHierarchy) return true;
        else return false;
    }
}

