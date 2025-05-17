using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGuideController : MonoBehaviour
{
    public RectTransform handTransform;  // Asignar el objeto UI de la mano
    public float distance = 150f;        // Qué tan lejos se mueve
    public float duration = 0.8f;        // Duración del movimiento
    public int tapsToHide = 2;

    private int tapCount = 0;
    private Vector2 startPos;
    private Vector2 endPos;

    void Start()
    {
        startPos = handTransform.anchoredPosition;
        endPos = startPos + new Vector2(-distance, -distance); // diagonal hacia abajo izquierda
        AnimateLoop();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            tapCount++;
            if (tapCount >= tapsToHide)
                HideHand();
        }

        // Para pruebas en PC
        if (Application.isEditor && Input.GetMouseButtonDown(0))
        {
            tapCount++;
            if (tapCount >= tapsToHide)
                HideHand();
        }
    }

    void AnimateLoop()
    {
        handTransform.DOAnchorPos(endPos, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    void HideHand()
    {
        handTransform.DOKill(); // Detiene la animación
        handTransform.gameObject.SetActive(false);
    }
}
