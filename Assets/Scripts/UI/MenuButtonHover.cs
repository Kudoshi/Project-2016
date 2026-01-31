using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private Color hoverColour = Color.red;
    [SerializeField] private Color normalColour = new Color(0, 0, 0, 0);
    [SerializeField] private float exitTransitionSpeed = 2f;

    private Image _image;
    private Color _targetColour;
    private bool _hovered;

    void Awake()
    {
        _image = GetComponent<Image>();
        _targetColour = normalColour;

        if (_image != null)
            _image.color = normalColour;
    }
    
    void Update()
    {
        if (_image == null) return;

        if (_hovered)
            _image.color = _targetColour;
        else
        {
            _image.color = Color.Lerp(_image.color, _targetColour, exitTransitionSpeed * Time.deltaTime);
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hovered = true;
        _targetColour = hoverColour;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hovered = false;
        _targetColour = normalColour;
    }
}
