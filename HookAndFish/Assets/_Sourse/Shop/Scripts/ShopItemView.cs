using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Image))]
public class ShopItemView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ShopItemView> OnClick;
    
    [SerializeField] private Sprite _standartBackground;
    [SerializeField] private Sprite _highlightBackground;
    
    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _lockImage;
    
    [SerializeField] private IntValueView _priceView;
    
    [SerializeField] private Image _selectionText;
    
    private Image _backgroundImage;
    
    public PlayerSkinItem Item { get; private set; }
    
    public bool IsLock {get; private set;}

    public int Price => Item.RequiredLevel;
    public GameObject Model => Item.PlayerPrefab;

    public void Initialize(PlayerSkinItem item)
    {
        _backgroundImage = _contentImage.GetComponent<Image>();
        _backgroundImage.sprite = _standartBackground;
        
        Item = item;

        _contentImage.sprite = item.Icon;
        _priceView.Show(Price);
    }
    
    public void OnPointerClick(PointerEventData eventData) => OnClick(this);

    public void Lock()
    {
        IsLock = true;
        _lockImage.gameObject.SetActive(IsLock);
        _priceView.Show(Price);
    }
    
    public void Unlock()
    {
        IsLock = false;
        _lockImage.gameObject.SetActive(IsLock);
        _priceView.Hide();
    }

    public void Select() => _selectionText.gameObject.SetActive(true);
    public void UnSelect() => _selectionText.gameObject.SetActive(false);
    
    public void Highlight() => _backgroundImage.sprite = _highlightBackground;
    public void UnHighlight() => _backgroundImage.sprite = _standartBackground;
}
