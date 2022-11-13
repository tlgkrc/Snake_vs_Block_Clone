using System;
using Data.ValueObject;
using Managers;
using UnityEngine;

namespace Controllers.Obstacle
{
    public class ObstacleMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private ObstacleManager manager;
        [SerializeField] private Renderer renderer;

        #endregion

        #region Private Variables

        private ushort _health;
        private ColorData _colorData;

        #endregion

        #endregion

        private void Start()
        {
            SetColor();
        }

        public void SetHealthData(ColorData colorData,ushort health)
        {
            _colorData = colorData;
            _health = health;
        }

        private void SetColor()
        {
            ushort value = (ushort)((_health / _colorData.color.Count)%(_colorData.color.Count));
            renderer.material.color = _colorData.color[value];
        }
    }
}