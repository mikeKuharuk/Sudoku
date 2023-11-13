using System;
using UnityEngine;

namespace GameCore
{
    public abstract class Cell : MonoBehaviour
    {
        public CellData Data { get; protected set; }
        
        public bool IsImmutable { get; protected set; }

        public bool IsSolved => Data.PlacedValue == Data.TrueValue;

        public abstract event Action<Cell> OnCellClicked;

        public void Init(int fieldBlockIndex, int indexInBlock)
        {
            Data = new CellData(fieldBlockIndex, indexInBlock);
            OnInit();
        }

        /// <summary> This method designed to fill values after board generation </summary>
        public void SetData(int trueValue, int shownValue) 
        {
            Data.TrueValue = trueValue;
            Data.PlacedValue = shownValue;
            UpdateView();
        }
        
        public void SetImmutable(bool isImmutable)
        {
            IsImmutable = isImmutable;
        }
        public abstract void SelectCell(bool isSelected);
        
        /// <summary> This method designed to fill values with user inserted data </summary>
        public abstract void PaintCell(IPaint data);
        public abstract void PlaceNoteInCell(IPaint data);
        public abstract void HighlightInBlock(bool isHighlighted);
        public abstract void HighlightSameValue(bool isHighlighted);
        public abstract void ClearCell();
        public abstract void ClearHighlights();
        protected abstract void OnInit();
        protected abstract void UpdateView();
        protected abstract void HighlightCorrect();
        protected abstract void HighlightWrong();
    }
}