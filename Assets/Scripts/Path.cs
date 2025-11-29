using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private List<Transform> path;

    public Transform GetPositionByIndex(int index)
    {
        if(index >= path.Count) return null;
        
        return path[index];
    }
}
