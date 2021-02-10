using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace zhdx.General
{
    public class Sticker2D : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
    {
        [SerializeField]
        private Canvas canvas = null;

        private RectTransform rectTransform;
        private Image image;
        private Color color;

        public void SetCanvas(Canvas canvas) => this.canvas = canvas;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
            color = image.color;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            Debug.Log("Mouse Dragging");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            color.a = 0.4f;
            image.color = color;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            color.a = 1.0f;
            image.color = color;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            transform.SetAsLastSibling();
        }
    }
}

