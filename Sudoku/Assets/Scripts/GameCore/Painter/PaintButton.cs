using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    public class PaintButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _numberLabel;
        [SerializeField] private GameObject _eraserView;

        public event Action<PaintButton> OnPaintClicked;
        public PaintNumber Paint => _paint;
        
        private PaintNumber _paint;
        
        public void Init(int paintNumber)
        {
            _button.onClick.AddListener(OnClick);

            if (paintNumber == 10) // TODO: Temporary to marc "Eraser" paint
            {
                paintNumber = -1;
                EnableEraserView();
            }
            
            _paint = new PaintNumber(paintNumber);
            
            InitView();
        }
        private void EnableEraserView()
        {
            _eraserView.SetActive(true);
        }

        private void InitView()
        {
            _numberLabel.text = _paint.Paint.ToString();
        }

        private void OnClick()
        {
            OnPaintClicked?.Invoke(this);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }
    }
}