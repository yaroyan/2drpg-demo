using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using Yaroyan.SeedWork.Dialogue;
using Yaroyan.SeedWork.Dialogue.Editor;

public class DialogueEditorTests
{
    DialogueEditorWindow _window;
    string _assetPath = "Assets/UnitTest.asset";

    [OneTimeSetUp]
    public void SetUp()
    {
        _window = DialogueEditorWindow.ShowWindow();
        _window.New(_assetPath);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        if (_window is null) return;
        _window.Delete();
        _window.Close();
    }


    // A Test behaves as an ordinary method
    [Test]
    public void Test()
    {

    }

    //// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    //// `yield return null;` to skip a frame.
    //[UnityTest]
    //public IEnumerator DialogueEditorTestsWithEnumeratorPasses()
    //{
    //    // Use the Assert class to test conditions.
    //    // Use yield to skip a frame.
    //    yield return null;
    //}

    VisualElement findToolBarElement()
    {
        var toolbar = _window.rootVisualElement.Query<Toolbar>().Where(e => e.name == "toolbar").First();
        Button button = null;
        foreach (var child in toolbar.Children())
        {
            if (child is not Button) continue;
            button = child as Button;
            if (button.text != "Save") button = null;
            break;
        }
        return button;
    }
}
