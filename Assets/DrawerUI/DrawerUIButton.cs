using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace DrawerUI
{
    public class DrawerUIButton : MonoBehaviour
    {
        public AnimationCurve m_AnimCurve;
        public RectTransform m_Content;
        public Vector3 m_MoveRange;
        public float m_HorizontalX;
        public Button m_Button;

        DrawerUIScript m_Parent;
        RectTransform m_Self;

        int m_Index;
        bool m_Actived;
        Vector2 m_DefaultPos = Vector2.zero;

        void Start()
        {
            m_Actived = false;
            m_Self = GetComponent<RectTransform>();
            m_Button.onClick.AddListener(OnClick);
        }

        public void Init(int index, DrawerUIScript parent)
        {
            m_Index = index;
            m_Parent = parent;
        }

        public void SetActived(bool actived)
        {
            if (m_Actived != actived)
            {
                m_Actived = actived;
                if (!m_Actived)
                    m_HorizontalX = 220.0f;
                else
                    m_HorizontalX = -220.0f;
            }
            else
            {
                m_HorizontalX = 0.0f;
            }
        }

        public void SetTargetY(float posY)
        {
            StartCoroutine(MoveVerticalCoroutine(posY));
        }

        void OnClick()
        {
            m_Parent.ChildClick(m_Index);
            Debug.Log("点击了按钮 : " + m_Index);
        }

        IEnumerator ContentFadeCoroutine(bool direction)
        {
            RawImage img = m_Content.GetComponent<RawImage>();
            Color sCol = direction ? Color.clear : Color.white;
            Color eCol = direction ? Color.white : Color.clear;
            Vector2 pos1 = new Vector2(m_MoveRange.x, m_MoveRange.z);
            Vector2 pos2 = new Vector2(m_MoveRange.y, m_MoveRange.z);
            Vector2 sPos = direction ? pos1 : pos2;
            Vector2 ePos = direction ? pos2 : pos1;
            float stime = Time.time;
            float ratio = 0.0f;
            float curve = 0.0f;
            while (ratio < 1.0f)
            {
                m_Content.anchoredPosition = Vector2.Lerp(sPos, ePos, curve);
                img.color = Color.Lerp(sCol, eCol, curve);
                yield return null;
                ratio = (Time.time - stime) * 2.0f;
                curve = m_AnimCurve.Evaluate(ratio);
            }
            m_Content.anchoredPosition = ePos;
            img.color = eCol;
        }

        IEnumerator MoveVerticalCoroutine(float posY)
        {
            if (m_HorizontalX < 0.0f)
            {
                StartCoroutine(ContentFadeCoroutine(true));
            }
            else if (m_HorizontalX > 0.0f)
            {
                StartCoroutine(ContentFadeCoroutine(false));
            }
            Vector2 sPos = m_Self.anchoredPosition;
            Vector2 ePos = new Vector2(m_Self.anchoredPosition.x + m_HorizontalX, posY);
            if (sPos == ePos)
            {
                yield break;
            }
            float stime = Time.time;
            float ratio = 0.0f;
            float curve = 0.0f;
            while (ratio < 1.0f)
            {
                m_Self.anchoredPosition = Vector2.Lerp(sPos, ePos, curve);
                yield return null;
                ratio = (Time.time - stime) * 2.0f;
                curve = m_AnimCurve.Evaluate(ratio);
            }
            m_Self.anchoredPosition = ePos;
        }

    }
}
