using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    public class CellButton : Cell
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _label;
        [SerializeField] private GameObject _blockHighlighter;
        [SerializeField] private GameObject _selectedHighlighter;
        [SerializeField] private GameObject _sameValuedHighlighter;
        [SerializeField] private Animator _animator;
        [SerializeField] private List<GameObject> _notes;
        
        public override event Action<Cell> OnCellClicked;

        private List<int> _notesPlaced = new List<int>();
        
        private static readonly int Wrong = Animator.StringToHash("Wrong");
        private static readonly int Correct = Animator.StringToHash("Correct");

        protected override void OnInit()
        {
            _button.onClick.AddListener(OnClick);
            _blockHighlighter.SetActive(false);
            _selectedHighlighter.SetActive(false);
        }

        public override void PaintCell(IPaint data)
        {
            if(IsImmutable) return;

            Data.PlacedValue = data.Paint;
            ClearNotes();
            UpdateView();
            VerifyPlacedValue();
        }

        public override void SelectCell(bool isSelected)
        {
            _selectedHighlighter.SetActive(isSelected);
        }
        public override void HighlightInBlock(bool isHighlighted)
        {
            _blockHighlighter.SetActive(isHighlighted);
        }

        public override void HighlightSameValue(bool isHighlighted)
        {
            _sameValuedHighlighter.SetActive(isHighlighted);
        }

        public override void PlaceNoteInCell(IPaint data)
        {
            if(IsImmutable) return;

            if (_notesPlaced.Contains(data.Paint))
            {
                _notesPlaced.Remove(data.Paint);
                _notes[data.Paint-1].SetActive(false);
            }
            else
            {
                _notesPlaced.Add(data.Paint);
                _notes[data.Paint-1].SetActive(true);
            }
        }

        public override void ClearCell()
        {
            _label.text = "";
        }

        public override void ClearHighlights()
        {
            _blockHighlighter.SetActive(false);
            _selectedHighlighter.SetActive(false);
            _sameValuedHighlighter.SetActive(false);
        }

        protected override void UpdateView()
        {
            if (Data.PlacedValue == 0)
            {
                ClearCell();
            }
            else
            {
                _label.text = Data.PlacedValue.ToString();
            }
        }
        protected override void HighlightCorrect()
        {
            _animator.SetTrigger(Correct);
        }

        protected override void HighlightWrong()
        {
            _animator.SetTrigger(Wrong);
        }
        private void ClearNotes()
        {
            foreach (var note in _notes)
            {
                note.SetActive(false);
            }
            
            _notesPlaced.Clear();
        }

        private void VerifyPlacedValue()
        {
            if (Data.PlacedValue == Data.TrueValue)
            {
                HighlightCorrect();
                SetImmutable(true);
            }
            else
            {
                HighlightWrong();
            }
        }

        private void OnClick()
        {
            OnCellClicked?.Invoke(this);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}