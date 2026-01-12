using UnityEngine;
using UnityEngine.UI;


public class BuildingManager : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector2 _mousePosition;
    private PlaceableGrid _currentGrid;
    private bool _isMenuOpen;
    
    [Header("References")]
    [SerializeField] private LayerMask gridLayer;
    [SerializeField] private GameObject buildMenu;
    [SerializeField] private Button archerTowerButton;
    [SerializeField] private Button magicTowerButton;
    [SerializeField] private Button exitBuildMenuButton;
    [SerializeField] private GameObject archerTowerPrefab;
    [SerializeField] private GameObject magicTowerPrefab;

    private void Start()
    {
        _mainCamera = Camera.main;
        
        archerTowerButton.onClick.AddListener(TryBuildArcherTower);
        magicTowerButton.onClick.AddListener(TryBuildMagicTower);
        exitBuildMenuButton.onClick.AddListener(DeactivateBuildMenu);
    }

    public void OnMouseClickBehaviour()
    {
        CheckGrid();
    }
    
    private void CheckGrid()
    {
        Ray ray = _mainCamera.ScreenPointToRay(_mousePosition);

        if (!_isMenuOpen)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, gridLayer))
            {
                _currentGrid = hit.transform.gameObject.GetComponent<PlaceableGrid>();
                if (_currentGrid.IsOccupied) return;
                ActivateBuildMenuOnGrid(hit.transform.position);
            }
        }
    }

    public void GetMousePosition(Vector2 mousePosition)
    {
        _mousePosition = mousePosition;
    }

    private void ActivateBuildMenuOnGrid(Vector3 position)
    {
        buildMenu.SetActive(true);
        _isMenuOpen = true;
        _currentGrid.HoverGrid();
        buildMenu.transform.position = position;
    }

    private void TryBuildMagicTower()
    {
        Debug.Log("button ");
        _currentGrid.AddTowerToGrid(magicTowerPrefab);
        DeactivateBuildMenu();
    }

    private void TryBuildArcherTower()
    {
        Debug.Log("button ");
        _currentGrid.AddTowerToGrid(archerTowerPrefab);
        DeactivateBuildMenu();
    }

    private void DeactivateBuildMenu()
    {
        buildMenu.SetActive(false);
        _isMenuOpen = false;
        _currentGrid.UnhoverGrid();
    }
}
