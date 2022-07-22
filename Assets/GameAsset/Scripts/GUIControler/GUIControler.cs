using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GUIHandler
{
    public static class GUIManager
    {
        public static void ResizeScrollRectContent(GameObject Content)
        {
            int numChild = Content.transform.childCount;
            int row, column;
            GridLayoutGroup.Constraint constraint = Content.GetComponent<GridLayoutGroup>().constraint;
            if (numChild > 0)
            {
                float widthElement = Content.GetComponent<GridLayoutGroup>().cellSize.x;
                float width = Mathf.Abs(Content.GetComponent<RectTransform>().sizeDelta.x);
                Debug.Log(width);
                float heightElement = Content.GetComponent<GridLayoutGroup>().cellSize.y;
                float height = Mathf.Abs(Content.GetComponent<RectTransform>().sizeDelta.y);
                Debug.Log(height);
                switch (constraint)
                {
                    case GridLayoutGroup.Constraint.FixedColumnCount:
                        column = Content.GetComponent<GridLayoutGroup>().constraintCount;
                        row = (numChild / column)+ (numChild % column > 0 ? 1 : 0);
                        break;
                    case GridLayoutGroup.Constraint.FixedRowCount:
                        row = Content.GetComponent<GridLayoutGroup>().constraintCount;
                        column = (numChild / row) + (numChild % row > 0 ? 1 : 0);
                        Debug.Log(numChild / row);
                        Debug.Log((numChild / row) + (numChild % row > 0 ? 1 : 0));
                        break;
                    default:
                        row = 0; column = 0;
                        break;
                }
                Debug.Log(column + "|" + row);
                Debug.Log((widthElement * column - width) + "|" + (heightElement * row - height));
                Content.GetComponent<RectTransform>().sizeDelta =
                            new Vector2(widthElement * column - width, heightElement * row - height);
            }
        }
    }
    public enum ResizeDirection
    {
        Horizontal, Vertical
    }
}

