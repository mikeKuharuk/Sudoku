using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace GameCore
{
    public class GameFieldView : MonoBehaviour, IGameFieldView
    {
        [SerializeField] private FieldBlock _fieldBlockPrefab;
        public Cell LastSelectedCell => _lastSelectedCell;
        public Cell[,] ViewCells => _viewCells;

        private List<FieldBlock> _blocks;
        private Cell _lastSelectedCell;
        
        private FieldBlock _lastLightedBlock;
        private List<Cell> _lastLightedLines;
        private List<Cell> _lastLightedSameValues;

        private Cell[,] _viewCells = new Cell[9,9];
        public void Init()
        {
            _blocks = new List<FieldBlock>();
            _lastLightedSameValues = new List<Cell>();
            _lastLightedLines = new List<Cell>();
            
            InstantiateBlocks();
            InitCells();
        }
        public void OnCellClicked(Cell cell)
        {
            ClearAllHighlights();

            if (cell != _lastSelectedCell)
            {
                HighlightField(cell);
            }
            else
            {
                _lastSelectedCell = null;
            }
        }
        private void HighlightSameValue(int value, Cell cell = null)
        {
            foreach (var cellView in _lastLightedSameValues)
            {
                cellView.HighlightSameValue(false);
            }
            
            if(value == 0) return;

            foreach (var viewCell in _viewCells)
            {
                if (viewCell.Data.PlacedValue == value)
                {
                    viewCell.HighlightSameValue(true);
                    _lastLightedSameValues.Add(viewCell);
                }
            }
        }
        private void HighlightField(Cell cell)
        {
            HighLightBlock(cell);
            HighlightLines(cell);
            HighlightSameValue(cell.Data.PlacedValue, cell);

            if (_lastSelectedCell != null && _lastSelectedCell != cell)
            {
                _lastSelectedCell.SelectCell(false);
            }
            cell.SelectCell(true);
            _lastSelectedCell = cell;

        }
        private void ClearAllHighlights()
        {
            foreach (var viewCell in _viewCells)
            {
                viewCell.ClearHighlights();
            }

            _lastLightedBlock = null;
            _lastLightedLines.Clear();
            _lastLightedSameValues.Clear();
        }

        private void InitCells()
        {
            if (_blocks == null) throw new ArgumentException("Field blocks list must be filled before start", nameof(_blocks));

            for (var blockIndex = 0; blockIndex < _blocks.Count; blockIndex++)
            {
                var block = _blocks[blockIndex];
                block.Init(blockIndex);
                
                for (var cellIndex = 0; cellIndex < block.Cells.Count; cellIndex++)
                {
                    var cell = block.Cells[cellIndex];
                    cell.Init(blockIndex, cellIndex);
                    _viewCells[cell.Data.XInMatrix, cell.Data.YInMatrix] = cell;
                }
            }
        }
        private void InstantiateBlocks()
        {
            for (int i = 0; i < TemporaryProjectConstants.BlocksCountInField; i++)
            {
                var block = Instantiate(_fieldBlockPrefab, transform);
                _blocks.Add(block);
            }
        }
        private void HighLightBlock(Cell cell)
        {
            var block = _blocks[cell.Data.FieldBlockIndex];

            if (_lastLightedBlock != null && _lastLightedBlock.BlockIndex != cell.Data.FieldBlockIndex)
            {
                _lastLightedBlock.HighlightPlacingHint(false);
            }

            block.HighlightPlacingHint(true);
            _lastLightedBlock = block;
        }
        private void HighlightLines(Cell cell)
        {
            foreach (var lastSelected in _lastLightedLines)
            {
                lastSelected.HighlightInBlock(false);
            }
            _lastLightedLines.Clear();
            
            var line = new List<Cell>();
            var xIndex = cell.Data.XInMatrix;
            var yIndex = cell.Data.YInMatrix;

            for (int i = 0; i < _viewCells.GetLength(0); i++)
            {
                _viewCells[xIndex, i].HighlightInBlock(true);
                _viewCells[i, yIndex].HighlightInBlock(true);

                line.Add(_viewCells[xIndex, i]);
                line.Add(_viewCells[i, yIndex]);
            }

            _lastLightedLines = line;
        }
    }
}