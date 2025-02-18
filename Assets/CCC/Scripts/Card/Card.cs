using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Card : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    private Camera mainCam => VisualCardsHandler.Instance.MainCam;
    private Vector3 offset;
    
    private Canvas canvas;
    private Image imageComponent;


    public bool Dragable;
    [Header("States")]
    public bool isDragging;
    public bool isHovering;
    public bool wasDragged;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeedLimit = 50;
    
    [Header("Selection")]
    [SerializeField] bool selected;
    public float SelectionOffset = 50;
    private float pointerDownTime;
    private float pointerUpTime;

    [Header("Visual")]
    [SerializeField] private bool instantiateVisual = true;
    [SerializeField] private GameObject cardVisualPrefab;
    public CardVisual cardVisual;
    public RectTransform CardHolder;
    public Action<Card> PointerEnterAction;
    public Action<Card> PointerExitAction;
    public Action<Card, bool> PointerUpAction;
    public Action<Card> PointerDownAction;
    public Action<Card> BeginDragAction;
    public Action<Card> EndDragAction;
    public Action<Card, bool> SelectAction;
    
    public Text CardName;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        imageComponent = GetComponent<Image>();
        
        if (!instantiateVisual || CardHolder == null)
            return;
        cardVisual = Instantiate(cardVisualPrefab, CardHolder).GetComponent<CardVisual>();
        cardVisual.Init(this);

        
        
    }

    public void Init(RectTransform rectTransform)
    {
        if (!instantiateVisual)
            return;
        cardVisual = Instantiate(cardVisualPrefab, rectTransform).GetComponent<CardVisual>();
        cardVisual.Init(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClampPosition();
        UpdatePosition();
    }

    /// <summary>
    /// 限制Card位置在屏幕中
    /// </summary>
    void ClampPosition()
    {
        Vector2 screenBounds = mainCam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCam.transform.position.z));
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x, screenBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y, screenBounds.y);
        transform.position = new Vector3(clampedPosition.x, clampedPosition.y, 0);
    }

    /// <summary>
    /// 更新Card位置
    /// </summary>
    void UpdatePosition()
    {
        if (isDragging)
        {
            Vector2 targetPosition = mainCam.ScreenToWorldPoint(Input.mousePosition) - offset;
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            Vector2 velocity = direction * Mathf.Min(moveSpeedLimit, Vector2.Distance(transform.position, targetPosition) / Time.deltaTime);
            transform.Translate(velocity * Time.deltaTime);
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!Dragable)
            return;
        BeginDragAction?.Invoke(this);
        //关闭射线检测与交互，使EventSystem失焦
        canvas.GetComponent<GraphicRaycaster>().enabled = false;
        imageComponent.raycastTarget = false;
        isDragging = true;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = mousePosition - (Vector2)transform.position;
        wasDragged = true;

    }
    public void OnDrag(PointerEventData eventData)
    {
        
    }

    

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDragAction?.Invoke(this);
        isDragging = false;
        //恢复射线检测与交互
        canvas.GetComponent<GraphicRaycaster>().enabled = true;
        imageComponent.raycastTarget = true;
        StartCoroutine(FrameWait());

        IEnumerator FrameWait()
        {
            yield return new WaitForEndOfFrame();
            wasDragged = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerEnterAction?.Invoke(this);
        isHovering = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerExitAction?.Invoke(this);
        isHovering = false;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        pointerUpTime = Time.time;

        PointerUpAction?.Invoke(this, pointerUpTime - pointerDownTime > .2f);

        if (pointerUpTime - pointerDownTime > .2f)
            return;
        if (wasDragged)
            return;
        selected = !selected;
        SelectAction?.Invoke(this, selected);
        if (selected)
            transform.localPosition += (cardVisual.transform.up * SelectionOffset);
        else
            transform.localPosition = Vector3.zero;
        Debug.Log("鼠标左键释放");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!Dragable)
            return;
        if (eventData.button != PointerEventData.InputButton.Left)
            return;
        
        PointerDownAction?.Invoke(this);
        pointerDownTime = Time.time;
        Debug.Log("鼠标左键按下");

    }

    public bool IsSelected()
    {
        return selected;
    }
    public void Deselect()
    {
        if (selected)
        {
            selected = false;
            if (selected)
                transform.localPosition += (cardVisual.transform.up *  SelectionOffset);
            else
                transform.localPosition = Vector3.zero;
        }
    }
    
    public int SiblingAmount()
    {
        return transform.parent.CompareTag("Slot") ? transform.parent.parent.childCount - 1 : 0;
    }
    public int ParentIndex()
    {
        return transform.parent.CompareTag("Slot") ? transform.parent.GetSiblingIndex() : 0;
    }
    public float NormalizedPosition()
    {
        return transform.parent.CompareTag("Slot") ? ExtensionMethods.Remap((float)ParentIndex(), 0, (float)(transform.parent.parent.childCount - 1), 0, 1) : 0;
    }
    private void OnDestroy()
    {
        canvas.GetComponent<GraphicRaycaster>().enabled = true;
        imageComponent.raycastTarget = true;
        if(cardVisual != null)
            Destroy(cardVisual.gameObject);
    }
}
