using System.Collections.Generic;
using UnityEngine;
namespace DrawerUI
{
    public class DrawerUIScript : MonoBehaviour
    {
        readonly float m_TopY = 320.0f;
        readonly float m_BottomY = -600.0f;
        readonly float m_IntervalY = 160.0f;
        public DrawerUIButton[] m_Buttons;

        void Start()
        {
            for (int i = 0, length = m_Buttons.Length; i < length; i++)
            {
                m_Buttons[i].Init(i, this);
            }
        }

        public void ChildClick(int index)
        {
            List<DrawerUIButton> list1 = new List<DrawerUIButton>();
            List<DrawerUIButton> list2 = new List<DrawerUIButton>();
            for (int i = 0, length = m_Buttons.Length; i < length; i++)
            {
                m_Buttons[i].SetActived(i == index);
                if (i <= index)
                    list1.Add(m_Buttons[i]);
                else
                    list2.Add(m_Buttons[i]);
            }
            for (int i = 0, count = list1.Count; i < count; i++)
            {
                list1[i].SetTargetY(m_TopY - m_IntervalY * i);
            }
            int _index = 0;
            for (int i = list2.Count - 1; i >= 0; i--)
            {
                list2[i].SetTargetY(m_BottomY + m_IntervalY * _index);
                _index++;
            }
        }
    }
}
