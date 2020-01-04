
#if UNITY_5 || UNITY_2017_1_OR_NEWER
using JetBrains.Annotations;
#endif
using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Polyglot
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedTextMeshProUGUI : MonoBehaviour, ILocalize
    {

        [Tooltip("The TextMesh component to localize")]
        [SerializeField]
        private TextMeshProUGUI text;

        [Tooltip("The key to localize with")]
        [SerializeField]
        private string key;

        public string Key { get { return key; } }

        public List<object> Parameters { get { return parameters; } }

        private List<object> parameters = new List<object>();

#if UNITY_5 || UNITY_2017_1_OR_NEWER
        [UsedImplicitly]
#endif
        public void Reset()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

#if UNITY_5 || UNITY_2017_1_OR_NEWER
        [UsedImplicitly]
#endif
        public void Start()
        {
            Localization.Instance.AddOnLocalizeEvent(this);
        }
        protected void SetText(TextMeshProUGUI text, string value)
        {
            text.text = value;
        }
        public void OnLocalize()
        {
            var flags = text.hideFlags;
            text.hideFlags = HideFlags.DontSave;
            if (parameters != null && parameters.Count > 0)
            {
                SetText(text, Localization.GetFormat(key, parameters.ToArray()));
            }
            else
            {
                SetText(text, Localization.Get(key));
            }

            var direction = Localization.Instance.SelectedLanguageDirection;

            if (IsOppositeDirection(text.alignment, direction))
            {
                switch (text.alignment)
                {
                    case TextAlignmentOptions.Left:
                        text.alignment = TextAlignmentOptions.Right;
                        break;
                    case TextAlignmentOptions.Right:
                        text.alignment = TextAlignmentOptions.Left;
                        break;
                }
            }
            text.hideFlags = flags;
        }

        private bool IsOppositeDirection(TextAlignmentOptions alignment, LanguageDirection direction)
        {
            return (direction == LanguageDirection.LeftToRight && IsAlignmentRight(alignment)) || (direction == LanguageDirection.RightToLeft && IsAlignmentLeft(alignment));
        }

        private bool IsAlignmentRight(TextAlignmentOptions alignment)
        {
            return alignment == TextAlignmentOptions.Right;
        }
        private bool IsAlignmentLeft(TextAlignmentOptions alignment)
        {
            return alignment == TextAlignmentOptions.Left;
        }

        public void ClearParameters()
        {
            parameters.Clear();
        }

        public void AddParameter(object parameter)
        {
            parameters.Add(parameter);
            OnLocalize();
        }
        public void AddParameter(int parameter)
        {
            AddParameter((object)parameter);
        }
        public void AddParameter(float parameter)
        {
            AddParameter((object)parameter);
        }
        public void AddParameter(string parameter)
        {
            AddParameter((object)parameter);
        }
    }
}