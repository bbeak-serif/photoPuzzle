using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Text_SliderUpdater : MonoBehaviour
{
    [SerializeField] Slider slider;    
    [SerializeField] TMP_Text valueText;  
    void Start() {
        
        slider.onValueChanged.AddListener(OnSliderValueChanged);

        
        UpdateText(slider.value);
    }

    void OnSliderValueChanged(float value) {
        UpdateText(value);
    }

    void UpdateText(float value) {
        
        valueText.text = value.ToString();
    }
}
