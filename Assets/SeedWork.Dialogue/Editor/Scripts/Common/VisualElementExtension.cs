using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    public static class VisualElementExtension
    {
        public static void AddAllToClassList(this VisualElement visualElement, IEnumerable<string> classNames)
        {
            foreach (string className in classNames)
            {
                visualElement.AddToClassList(className);
            }
        }
    }
}
