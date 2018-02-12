using UnityEngine;
using UnityEngine.UI;

namespace Util.Component.UI
{
    public class SliderField : MonoBehaviour
    {
        private void Start()
        {
            Value = slider.value;
        }
        
        public void ValueOnChange()
        {
            Value = slider.value;
        }

        #region Property

        public float Value
        {
            get { return slider.value; }
            set
            {
                slider.value = value;
                valueLabel.text = value.ToString();
            }
        }

        #endregion

        #region Attributes

        [SerializeField] private Slider slider;

        [SerializeField]  private Text valueLabel;

        #endregion
    }
}