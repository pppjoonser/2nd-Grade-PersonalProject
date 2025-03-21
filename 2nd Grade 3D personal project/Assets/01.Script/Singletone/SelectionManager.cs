using System.Collections.Generic;
using UnityEngine;

public class SelectionManager
{
    private static SelectionManager _instance;

    public static SelectionManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new SelectionManager();
            }

            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    public HashSet<SelectableUnit> SelectedUnits = new HashSet<SelectableUnit>();
    public List<SelectableUnit> AvilableUnits = new List<SelectableUnit>();

    private SelectionManager() { }

    public void Select(SelectableUnit unit)
    {
        SelectedUnits.Add(unit);
        unit.OnSelected();
    }

    public void Deselect(SelectableUnit unit)
    {
        SelectedUnits.Remove(unit);
        unit.OnDeselectied();
    }

    public void DeselectAll()
    {
        foreach (SelectableUnit unit in SelectedUnits)
        {
            unit.OnDeselectied();
        }
        SelectedUnits.Clear();
    }

    public bool IsSelected(SelectableUnit unit)
    {
        return SelectedUnits.Contains(unit);
    }
}
