using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] private List<Unit> selectedUnitList;
    
    [SerializeField] private List<Unit> availableUnitList;
    
    [SerializeField] private RectTransform selectionBox;

    private Vector2 clickStartPos;

    private bool isDragSelect;

    // Update is called once per frame
    void Update()
    {
        MouseClickListener();
        KeyBoardInputListener();
    }

    private void MouseClickListener()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseClicked();
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            clickStartPos = Input.mousePosition;
            RightMouseClicked();
        }
       
        if (Input.GetMouseButton(1))
        {
            UpdateSelectionBox(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(1))
        {
            var dist = Vector2.Distance(clickStartPos, Input.mousePosition);

            if (dist > 40)
                isDragSelect = true;
            else
                isDragSelect = false;

            if (isDragSelect)
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    DeselectAllUnits();
                }

                ReleaseSelectionBox();
            }
        }
    }

    private void RightMouseClicked()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.layer == 9)
            {
                Unit u = hit.transform.GetComponent<Unit>();
                if (u.isSelected)
                {
                    Debug.Log("Unit already selected!");
                }
                else
                {
                    if (!Input.GetKey(KeyCode.LeftShift))
                    {
                        DeselectAllUnits();
                    }
                    
                    u.OnUnitSelected();
                    selectedUnitList.Add(u);
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    DeselectAllUnits();
                }
            }
        }
    }

    private void LeftMouseClicked()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.layer == 8)
            {
                ChangeUnitTarget(hit.transform.GetComponent<Target>());
            }
        }
    }

    private void ChangeUnitTarget(Target t)
    {
        foreach (var unit in selectedUnitList)
        {
            unit.SetCurrentTarget(t);
            Debug.Log("Set unit target!");
        }
    }

    private void UpdateSelectionBox(Vector2 currentMousePos)
    {
        if(!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);

        float width = currentMousePos.x - clickStartPos.x;
        float height = currentMousePos.y - clickStartPos.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = clickStartPos + new Vector2(width / 2, height / 2);
    }

    private void ReleaseSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);

        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        foreach (var unit in availableUnitList)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);

            if (screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectedUnitList.Add(unit);
                unit.OnUnitSelected();
            }
        }
    }

    private void KeyBoardInputListener()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DeselectAllUnits();
        }
    }

    private void DeselectAllUnits()
    {
        foreach (var unit in selectedUnitList)
        {
            unit.OnUnitDeselected();
        }
        selectedUnitList = new List<Unit>();
    }

    public void AddUnitToSelected(Unit u)
    {
        if (!u.isSelected)
        {
            selectedUnitList.Add(u);
            u.OnUnitSelected();
        }
    }

    public void CacheAvailableUnits(List<Unit> list)
    {
        availableUnitList = list;
    }
}
