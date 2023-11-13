using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace GameCore
{
    public class FieldBlock : MonoBehaviour
    {
        [SerializeField] private Cell _cellPrefab;
        
        private List<Cell> _cells;

        public int BlockIndex { get; private set; }
        public List<Cell> Cells => _cells;

        public void Init(int blockIndex)
        {
            BlockIndex = blockIndex;
            _cells = new List<Cell>();
            InstantiateCells();
        }

        public void HighlightPlacingHint(bool isHighlighted)
        {
            foreach (var cell in _cells)
            {
                cell.HighlightInBlock(isHighlighted);
            }
        }

        private void InstantiateCells()
        {
            for (int i = 0; i < TemporaryProjectConstants.CellsCountInBlock; i++)
            {
                var cell = Instantiate(_cellPrefab, transform);
                _cells.Add(cell);
            }
        }
}
}