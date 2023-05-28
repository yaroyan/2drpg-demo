using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Linq;
using Cysharp.Threading.Tasks;
using Yaroyan.SeedWork.Dialogue.Editor.Infrastructure;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    public class DialogueEditorWindow : EditorWindow
    {
        // Current open dialouge container in dialogue editor window.
        DialogueContainerSO _dialogueContainer;
        string _dialoguePath = null;
        DialogueGraphView graphView;
        DialogueSaveAndLoad saveAndLoad;
        DialogueGraphViewRepository _dialogueGraphViewRepository;
        GraphViewInspector _graphViewInspector;
        DialogueNodeFactory _nodeFactory;
        NodeDataStore _nodeDataStore;

        // Current selected language in the dialogue editor window.
        LanguageType selectedLanguage = LanguageType.Japanese;
        // Languages toolbar menu in the top of dialogue editor window.
        ToolbarMenu languagesDropdownMenu;
        // Name of the current open dialouge container.
        Label nameOfDialougeContainer;
        // Name of the graph view style sheet.
        static readonly string graphViewStyleSheet = "USS/EditorWindow/EditorWindowStyleSheet";

        /// <summary>
        /// Current selected language in the dialogue editor window.
        /// </summary>
        public LanguageType SelectedLanguage { get => selectedLanguage; set => selectedLanguage = value; }

        // Callback attribute for opening an asset in Unity (e.g the callback is fired when double clicking an asset in the Project Browser).
        // Read More https://docs.unity3d.com/ScriptReference/Callbacks.OnOpenAssetAttribute.html
        [OnOpenAsset(0)]
        public static bool ShowWindow(int instanceId, int line)
        {
            // Find Unity Object with this instanceId and load it in.
            UnityEngine.Object item = EditorUtility.InstanceIDToObject(instanceId);

            // Check if item is a DialogueContainerSO Object.
            if (item is DialogueContainerSO) CreateNewWindow(item as DialogueContainerSO);

            // we did not handle the open.
            return false;
        }

        [MenuItem("Window/Editor Extension/Dialogue Editor")]
        public static DialogueEditorWindow ShowWindow() => CreateNewWindow(null);


        static DialogueEditorWindow CreateNewWindow(DialogueContainerSO container)
        {
            // Make a unity editor window of type DialogueEditorWindow.
            DialogueEditorWindow window = GetWindow<DialogueEditorWindow>();
            // Name of editor window.
            window.titleContent = new GUIContent("Dialogue Editor");
            // The DialogueContainerSO we will load in to editor window.
            window._dialogueContainer = container;
            // Starter size of the editor window.
            window.minSize = new Vector2(500, 250);
            // Load in DialogueContainerSO data in to the editor window.
            window.Reload();
            return window;
        }

        void OnEnable()
        {
            ConstructGraphView();

            // Add the styleSheet for graph view.
            rootVisualElement.styleSheets.Add(Resources.Load<StyleSheet>(graphViewStyleSheet));
            rootVisualElement.Add(GenerateToolbar());

            Reload();
        }

        void OnDisable()
        {
            rootVisualElement.Remove(graphView);
        }

        /// <summary>
        /// Construct graph view 
        /// </summary>
        void ConstructGraphView()
        {
            // Make the DialogueGraphView and Stretch it to the same size as the Parent.
            // Add it to the DialogueEditorWindow.
            graphView = new DialogueGraphView(this);
            _nodeFactory = graphView.NodeFactory;
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
            saveAndLoad = new DialogueSaveAndLoad(graphView, _nodeFactory);
            _dialogueGraphViewRepository = new DialogueGraphViewRepository(_nodeDataStore, _nodeFactory);
            _graphViewInspector = new GraphViewInspector(graphView);
        }

        /// <summary>
        /// Generate the toolbar you will see in the top left of the dialogue editor window.
        /// </summary>
        Toolbar GenerateToolbar()
        {
            Toolbar toolbar = new();
            toolbar.name = "toolbar";

            // Save button.
            {
                Button saveBtn = new Button() { text = "Save" };
                saveBtn.clicked += () =>
                {
                    Save();
                    Debug.Log("<color=green>Dialog saved successfully.</color>");
                };
                toolbar.Add(saveBtn);
            }

            // Load button.
            {
                Button loadBtn = new Button() { text = "Load" };
                loadBtn.clicked += () =>
                {
                    string filePath = EditorUtility.OpenFilePanelWithFilters("Load asset", Application.dataPath, new string[] { "ScriptableObject", "asset" });
                    Load(filePath);
                    Debug.Log("<color=green>Dialog loaded successfully.</color>");
                };
                toolbar.Add(loadBtn);
            }

            // Reload button.
            {
                Button reloadBtn = new Button() { text = "Reload" };
                reloadBtn.clicked += () =>
                {
                    Reload();
                    Debug.Log("<color=green>Dialog reloaded successfully.</color>");
                };
                toolbar.Add(reloadBtn);
            }

            // New button.
            {
                Button newButton = new Button() { text = "New" };
                newButton.clicked += () =>
                {
                    string filePath = EditorUtility.SaveFilePanelInProject("Save new asset", "New Dialogue", "asset", "Please enter a file name to save the dialogue to");
                    New(filePath);
                    Debug.Log("<color=green>New dialogue created.</color>");
                };
                toolbar.Add(newButton);
            }

            // New button.
            {
                Button newButton = new Button() { text = "Delete" };
                newButton.clicked += () =>
                {
                    Delete();
                };
                toolbar.Add(newButton);
            }

            // Inspect button.
            {
                Button button = new Button { text = "Inspect" };
                button.clicked += () =>
                {
                    Inspect();
                    Debug.Log("Node integrity inspected.");
                };
                toolbar.Add(button);
            }

            {
                Button button = new() { text = "MiniMap" };
                button.clicked += () =>
                {
                    graphView.ToggleVisibilityForMinimap();
                };
                toolbar.Add(button);
            }

            // Dropdown menu for languages.
            {
                languagesDropdownMenu = new ToolbarMenu();

                // Here we go through each language and make a button with that language.
                // When you click on the language in the dropdown menu we tell it to run Language(language) method.
                foreach (LanguageType language in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
                {
                    languagesDropdownMenu.menu.AppendAction(language.ToString(), new Action<DropdownMenuAction>(x => Language(language)));
                }
                toolbar.Add(languagesDropdownMenu);
            }

            // Name of current DialigueContainer you have open.
            {
                nameOfDialougeContainer = new Label("");
                toolbar.Add(nameOfDialougeContainer);
                nameOfDialougeContainer.AddToClassList("nameOfDialougeContainer");
            }

            return toolbar;
        }

        /// <summary>
        /// Will load in current selected dialogue container.
        /// </summary>
        public void Reload()
        {
            if (this.graphView is not null)
            {
                Language(LanguageType.Japanese);
            }
            if (_dialogueContainer is not null)
            {
                nameOfDialougeContainer.text = "Name:   " + _dialogueContainer.name;
                saveAndLoad.Load(_dialogueContainer);
            }
        }

        /// <summary>
        /// Load the selected asset.
        /// </summary>
        public void Load(string filePath)
        {
            ;
            if (string.IsNullOrEmpty(filePath))
            {
                Debug.LogError("Please enter a valid file name or path.");
                return;
            }
            this._dialoguePath = filePath;
            _dialogueContainer = AssetDatabase.LoadAssetAtPath<DialogueContainerSO>(filePath.Replace(Application.dataPath, "Assets"));
            Language(LanguageType.Japanese);
            nameOfDialougeContainer.text = "Name:   " + _dialogueContainer.name;
            saveAndLoad.Load(_dialogueContainer);
        }

        /// <summary>
        /// Will save the current changes to dialogue container.
        /// </summary>
        public void Save()
        {
            if (_dialogueContainer is null)
            {
                Debug.LogWarning("No Dialogue.");
                return;
            }
            saveAndLoad.Save(_dialogueContainer);
        }

        /// <summary>
        /// Create a new asset.
        /// </summary>
        public DialogueContainerSO New(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                Debug.LogError("Please enter a valid file name or path.");
                return null;
            }

            // save new dialogue as asset.
            this._dialogueContainer = CreateInstance<DialogueContainerSO>();
            this._dialoguePath = filePath;
            AssetDatabase.CreateAsset(this._dialogueContainer, filePath);
            Reload();
            return this._dialogueContainer;
        }

        public void Delete()
        {
            if (_dialoguePath is null) return;
            AssetDatabase.DeleteAsset(_dialoguePath);
            this._dialogueContainer = null;
            this._dialoguePath = null;
            Debug.Log("<color=green>dialogue delted.</color>");
            nameOfDialougeContainer.text = "Name:   ";
        }

        /// <summary>
        /// Inspect node consistency.
        /// </summary>
        public async void Inspect()
        {
            foreach (var info in await graphView.nodes
                .Where(node => node is BaseNode)
                .Cast<BaseNode>()
                .Select(async node => await UniTask.RunOnThreadPool(() => node.InspectConsistency()))
                .WhenAll())
            {
                foreach (var err in info.NodeErrors)
                {
                    Debug.Log(err.Message.ComposeMessage());
                }
            }
        }

        /// <summary>
        /// Will change the language in the dialogue editor window.
        /// </summary>
        /// <param name="language">Language that you want to change to</param>
        void Language(LanguageType language)
        {
            languagesDropdownMenu.text = "Language: " + language.ToString();
            selectedLanguage = language;
            graphView.ReloadLanguage();
        }
    }
}