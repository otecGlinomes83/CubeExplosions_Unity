using UnityEngine;
    public class ColorChanger
    {
        private float _minColorValue = 0f;
        private float _maxColorValue = 1f;

        public void ChangeColor(MeshRenderer cubeRenderer)
        {
            cubeRenderer.material.color = GenerateRandomColor();
        }

        private Color GenerateRandomColor() =>
                new Color
                (
                 Random.Range(_minColorValue, _maxColorValue),
                 Random.Range(_minColorValue, _maxColorValue),
                 Random.Range(_minColorValue, _maxColorValue)
                );
    }