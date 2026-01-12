using UnityEngine;

public class PlaceableGrid : MonoBehaviour
{
    public bool IsOccupied { get; private set; }

    [SerializeField] private GameObject gridHover;
    
    public void AddTowerToGrid(GameObject tower)
    {
        if (IsOccupied) return;
        
        Instantiate(tower, transform.position, Quaternion.identity);
        UnhoverGrid();
        IsOccupied = true;
    }

    public void HoverGrid()
    {
        gridHover.SetActive(true);
    }

    public void UnhoverGrid()
    {
        gridHover.SetActive(false);
    }
}
