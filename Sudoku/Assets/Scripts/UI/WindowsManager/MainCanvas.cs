using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Logger;

namespace PlayborschUI.WindowsManager
{
    public class MainCanvas : MonoBehaviour
    {
        [SerializeField] private GameObject _layerRootPrefab;
        
        public Canvas GameCanvas;

        private Dictionary<CanvasLayers, Transform> _layerRoots;

        public void Init()
        {
            _layerRoots = new Dictionary<CanvasLayers, Transform>();
            foreach (CanvasLayers layer in (CanvasLayers[]) Enum.GetValues(typeof(CanvasLayers)))
            {
                var layerRoot = Instantiate(_layerRootPrefab, transform);
                layerRoot.name = layer.ToString();
                
                _layerRoots.Add(layer, layerRoot.transform);
            }
        }

        public Transform GetRootForLayer(CanvasLayers layer)
        {
            if (!_layerRoots.ContainsKey(layer))
            {
                LogHandler.LogError($"[{nameof(MainCanvas)}] There is no root for layer: {layer}");
                return null;
            }

            return _layerRoots[layer];
        }
    }
}