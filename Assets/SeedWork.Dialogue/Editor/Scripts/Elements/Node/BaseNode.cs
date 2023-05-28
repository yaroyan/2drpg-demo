using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Yaroyan.SeedWork.Dialogue.Editor
{
    internal abstract class GenericBaseNode<T> : BaseNode, IGnericDialogueNode<T> where T : BaseData
    {
        public GenericBaseNode(NodeData<T> nodeData) : base(nodeData) { }
        public GenericBaseNode(Ulid nodeId, Vector2 position) : base(nodeId, position) { }

        public abstract NodeData<T> Export();

        public NodeMetadata ExportMetadata()
        {
            Rect position = GetPosition();
            NodeMetadata metadata = new NodeMetadata(NodeId) { NodePosition = position.position, NodeSize = position.size };
            return metadata;
        }
    }

    internal abstract class BaseNode : Node, IDialogueNode
    {
        public Ulid NodeId { get; }
        private DialogueGraphView _cachedGraphView;
        private protected DialogueGraphView graphView
        {
            get
            {
                _cachedGraphView ??= GetFirstAncestorOfType<DialogueGraphView>();
                return _cachedGraphView;
            }
        }
        private protected Vector2 defaultNodeSize = new Vector2(200, 250);

        private List<LanguageGenericText> languageGenericsList_Texts = new List<LanguageGenericText>();
        private List<LanguageGenericAudioClip> languageGenericsList_AudioClips = new List<LanguageGenericAudioClip>();

        private BaseNode(Ulid nodeId)
        {
            NodeId = nodeId;
            styleSheets.Add(Resources.Load<StyleSheet>("USS/Nodes/NodeStyleSheet"));
        }

        public BaseNode(BaseNodeData nodeData) : this(nodeData.Metadata.NodeId)
        {
            SetPosition(new Rect(nodeData.Metadata.NodePosition, nodeData.Metadata.NodeSize));
        }

        public BaseNode(Ulid nodeId, Vector2 position) : this(nodeId)
        {
            SetPosition(new Rect(position, defaultNodeSize));
        }

        abstract public void MutateOnLoad();

        /// <summary>
        /// ノードの整合性を検証する
        /// </summary>
        abstract public NodeErrorInfo InspectConsistency();

        #region Get New Field ------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Get a new Label
        /// </summary>
        /// <param name="labelName">Text in the label</param>
        /// <param name="USS01">USS class add to the UI element</param>
        /// <param name="USS02">USS class add to the UI element</param>
        /// <returns></returns>
        protected Label GetNewLabel(string labelName, params string[] styles)
        {
            Label label_texts = new Label(labelName);
            label_texts.AddAllToClassList(styles);
            return label_texts;
        }

        /// <summary>
        /// Get a new Button
        /// </summary>
        /// <param name="btnText">Text in the button</param>
        /// <param name="USS01">USS class add to the UI element</param>
        /// <param name="USS02">USS class add to the UI element</param>
        /// <returns></returns>
        protected Button GetNewButton(string btnText, params string[] styles)
        {
            Button btn = new Button { text = btnText };
            btn.AddAllToClassList(styles);
            return btn;
        }

        /// <summary>
        /// Get a new Image.
        /// </summary>
        /// <param name="USS01">USS class add to the UI element</param>
        /// <param name="USS02">USS class add to the UI element</param>
        /// <returns></returns>
        protected Image GetNewImage(params string[] styles)
        {
            Image imagePreview = new Image();
            imagePreview.AddAllToClassList(styles);
            return imagePreview;
        }

        // Value's --------------------------------------------------------------------------

        protected T CreateField<T, U>(Ref<U> inputValue, params string[] styles) where T : BaseField<U>, new()
        {
            T field = new T();
            // When we change the variable from graph view.
            field.RegisterValueChangedCallback(value =>
            {
                inputValue.Value = value.newValue;
            });
            field.SetValueWithoutNotify(inputValue.Value);

            // Set uss class for stylesheet.
            field.AddAllToClassList(styles);
            return field;
        }

        protected FloatField CreateFloatField(Ref<float> inputValue, params string[] styles)
        {
            FloatField field = CreateField<FloatField, float>(inputValue, styles);
            return field;
        }

        protected IntegerField CreateIntegerField(Ref<int> inputValue, params string[] styles)
        {
            IntegerField field = CreateField<IntegerField, int>(inputValue, styles);
            return field;
        }

        protected TextField CreateTextField(Ref<string> inputValue, string placeholder, params string[] styles)
        {
            TextField field = CreateField<TextField, string>(inputValue, styles);
            SetPlaceholderText(field, placeholder);
            return field;
        }

        /// <summary>
        /// Get a new ObjectField with a Sprite as the Object.
        /// </summary>
        /// <param name="inputSprite">Container_Sprite that need to be set in to the ObjectField</param>
        /// <param name="imagePreview">Image that need to be set as preview image</param>
        /// <param name="USS01">USS class add to the UI element</param>
        /// <param name="USS02">USS class add to the UI element</param>
        /// <returns></returns>
        protected ObjectField CreateObjectField(Ref<Sprite> inputSprite, Image imagePreview, params string[] styles)
        {
            ObjectField objectField = new ObjectField()
            {
                objectType = typeof(Sprite),
                allowSceneObjects = false,
                value = inputSprite.Value,
            };

            // When we change the variable from graph view.
            objectField.RegisterValueChangedCallback(value =>
            {
                inputSprite.Value = value.newValue as Sprite;

                imagePreview.image = (inputSprite.Value != null ? inputSprite.Value.texture : null);
            });
            imagePreview.image = (inputSprite.Value != null ? inputSprite.Value.texture : null);

            // Set uss class for stylesheet.
            objectField.AddAllToClassList(styles);

            return objectField;
        }

        /// <summary>
        /// Get a new ObjectField with a Container_DialogueEventSO as the Object.
        /// </summary>
        /// <param name="inputDialogueEventSO">Container_DialogueEventSO that need to be set in to the ObjectField</param>
        /// <param name="USS01">USS class add to the UI element</param>
        /// <param name="USS02">USS class add to the UI element</param>
        /// <returns></returns>
        protected ObjectField CreateObjectField(Ref<DialogueEventSO> inputDialogueEventSO, params string[] styles)
        {
            ObjectField objectField = new ObjectField()
            {
                objectType = typeof(DialogueEventSO),
                allowSceneObjects = false,
                value = inputDialogueEventSO.Value,
            };

            // When we change the variable from graph view.
            objectField.RegisterValueChangedCallback(value =>
            {
                inputDialogueEventSO.Value = value.newValue as DialogueEventSO;
            });
            objectField.SetValueWithoutNotify(inputDialogueEventSO.Value);

            // Set uss class for stylesheet.
            objectField.AddAllToClassList(styles);

            return objectField;
        }

        // Enum's --------------------------------------------------------------------------

        protected EnumField CreateEnumField<T>(Ref<T> enumType, params string[] styles) where T : Enum
        {
            EnumField enumField = new EnumField { value = enumType.Value };
            enumField.Init(enumType.Value);

            // When we change the variable from graph view.
            enumField.RegisterValueChangedCallback((value) =>
            {
                enumType.Value = (T)value.newValue;
            });
            enumField.SetValueWithoutNotify(enumType.Value);

            // Set uss class for stylesheet.
            enumField.AddAllToClassList(styles);

            //enumType.EnumField = enumField;
            return enumField;
        }

        protected EnumField CreateEnumField<T>(Ref<T> enumType, EventCallback<ChangeEvent<Enum>> callback, params string[] styles) where T : Enum
        {
            EnumField enumField = new EnumField { value = enumType.Value };
            enumField.Init(enumType.Value);

            // When we change the variable from graph view.
            enumField.RegisterValueChangedCallback(callback);
            enumField.SetValueWithoutNotify(enumType.Value);

            // Set uss class for stylesheet.
            enumField.AddAllToClassList(styles);

            //enumType.EnumField = enumField;
            return enumField;
        }

        /// <summary>
        /// Get a new EnumField where the emum is ChoiceStateType.
        /// </summary>
        /// <param name="enumType">Container_ChoiceStateType that need to be set in to the EnumField</param>
        /// <param name="USS01">USS class add to the UI element</param>
        /// <param name="USS02">USS class add to the UI element</param>
        /// <returns></returns>
        //protected EnumField GetNewEnumField_ChoiceStateType(Container_ChoiceStateType enumType, string USS01 = "", string USS02 = "")
        protected EnumField GetNewEnumField_ChoiceStateType(Ref<ChoiceStateType> enumType, string USS01 = "", string USS02 = "")
        {
            EnumField field = CreateEnumField(enumType, USS01, USS02);
            //enumType.EnumField = field;
            return field;
        }

        /// <summary>
        /// Get a new EnumField where the emum is EndNodeType.
        /// </summary>
        /// <param name="enumType">Container_EndNodeType that need to be set in to the EnumField</param>
        /// <param name="USS01">USS class add to the UI element</param>
        /// <param name="USS02">USS class add to the UI element</param>
        /// <returns></returns>
        //protected EnumField GetNewEnumField_EndNodeType(Container_EndNodeType enumType, string USS01 = "", string USS02 = "")
        protected EnumField GetNewEnumField_EndNodeType(Ref<EndNodeType> enumType, string USS01 = "", string USS02 = "")
        {
            EnumField field = CreateEnumField(enumType, USS01, USS02);

            //enumType.EnumField = field;
            return field;
        }

        /// <summary>
        /// Get a new EnumField where the emum is StringEventModifierType.
        /// </summary>
        /// <param name="enumType">Container_StringEventModifierType that need to be set in to the EnumField</param>
        /// <param name="action"></param>
        /// <param name="USS01">USS class add to the UI element</param>
        /// <param name="USS02">USS class add to the UI element</param>
        /// <returns></returns>
        //protected EnumField GetNewEnumField_StringEventModifierType(Container_StringEventModifierType enumType, Action action, string USS01 = "", string USS02 = "")
        protected EnumField GetNewEnumField_StringEventModifierType(Ref<StringEventModifierType> enumType, Action action, params string[] styles)
        {
            EnumField field = CreateEnumField(enumType, styles);
            field.RegisterValueChangedCallback(value => action?.Invoke());
            return field;
        }

        /// <summary>
        /// Get a new EnumField where the emum is StringEventConditionType.
        /// </summary>
        /// <param name="enumType">Container_StringEventConditionType that need to be set in to the EnumField</param>
        /// <param name="action">A Action that is use to hide/show depending on if a FloatField is needed</param>
        /// <param name="USS01">USS class add to the UI element</param>
        /// <param name="USS02">USS class add to the UI element</param>
        /// <returns></returns>
        //protected EnumField GetNewEnumField_StringEventConditionType(Container_StringEventConditionType enumType, Action action, string USS01 = "", string USS02 = "")
        protected EnumField GetNewEnumField_StringEventConditionType(Ref<StringEventConditionType> enumType, Action action, params string[] styles)
        {
            EnumField field = CreateEnumField(enumType, styles);
            field.RegisterValueChangedCallback(value => action?.Invoke());
            return field;
        }

        // Custom-made's --------------------------------------------------------------------------

        /// <summary>
        /// Get a new TextField that use a List<LanguageGeneric<string>> text.
        /// </summary>
        /// <param name="Text">List of LanguageGeneric<string> Text</param>
        /// <param name="placeholderText">The text that will be displayed if the text field is empty</param>
        /// <param name="USS01">USS class add to the UI element</param>
        /// <param name="USS02">USS class add to the UI element</param>
        /// <returns></returns>
        protected TextField GetNewTextField_TextLanguage(List<LanguageGeneric<string>> Text, string placeholderText = "", params string[] styles)
        {
            // Add languages
            foreach (LanguageType language in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
            {
                Text.Add(new LanguageGeneric<string>
                {
                    LanguageType = language,
                    LanguageGenericType = ""
                });
            }

            // Make TextField.
            TextField textField = new TextField("");

            // Add it to the reaload current language list.
            languageGenericsList_Texts.Add(new LanguageGenericText(Text, textField, placeholderText));

            // When we change the variable from graph view.
            textField.RegisterValueChangedCallback(value =>
            {
                Text.Find(text => text.LanguageType == graphView.Language).LanguageGenericType = value.newValue;
            });
            textField.SetValueWithoutNotify(Text.Find(text => text.LanguageType == graphView.Language).LanguageGenericType);

            // Text field is set to be multiline.
            textField.multiline = true;

            // Set uss class for stylesheet.
            textField.AddAllToClassList(styles);

            return textField;
        }


        /// <summary>
        /// Get a new ObjectField that use List<LanguageGeneric<AudioClip>>.
        /// </summary>
        /// <param name="audioClips"></param>
        /// <param name="USS01">USS class add to the UI element</param>
        /// <param name="USS02">USS class add to the UI element</param>
        /// <returns></returns>
        protected ObjectField GetNewObjectField_AudioClipsLanguage(List<LanguageGeneric<AudioClip>> audioClips, params string[] styles)
        {
            // Add languages.
            foreach (LanguageType language in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
            {
                audioClips.Add(new LanguageGeneric<AudioClip>
                {
                    LanguageType = language,
                    LanguageGenericType = null
                });
            }

            // Make ObjectField.
            ObjectField objectField = new ObjectField()
            {
                objectType = typeof(AudioClip),
                allowSceneObjects = false,
                value = audioClips.Find(audioClip => audioClip.LanguageType == graphView.Language).LanguageGenericType,
            };

            // Add it to the reaload current language list.
            languageGenericsList_AudioClips.Add(new LanguageGenericAudioClip(audioClips, objectField));

            // When we change the variable from graph view.
            objectField.RegisterValueChangedCallback(value =>
            {
                audioClips.Find(audioClip => audioClip.LanguageType == graphView.Language).LanguageGenericType = value.newValue as AudioClip;
            });
            objectField.SetValueWithoutNotify(audioClips.Find(audioClip => audioClip.LanguageType == graphView.Language).LanguageGenericType);

            // Set uss class for stylesheet.
            objectField.AddAllToClassList(styles);

            return objectField;
        }

        #endregion

        #region Methods ------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Add a port to the outputContainer.
        /// </summary>
        /// <param name="name">The name of port.</param>
        /// <param name="capacity">Can it accept multiple or a single one.</param>
        /// <returns>Get the port that was just added to the outputContainer.</returns>
        public Port AddOutputPort(string name, Port.Capacity capacity = Port.Capacity.Single)
        {
            Port outputPort = GetPortInstance(Direction.Output, capacity);
            outputPort.portName = name;
            outputContainer.Add(outputPort);
            return outputPort;
        }

        /// <summary>
        /// Add a port to the inputContainer.
        /// </summary>
        /// <param name="name">The name of port.</param>
        /// <param name="capacity">Can it accept multiple or a single one.</param>
        /// <returns>Get the port that was just added to the inputContainer.</returns>
        public Port AddInputPort(string name, Port.Capacity capacity = Port.Capacity.Multi)
        {
            Port inputPort = GetPortInstance(Direction.Input, capacity);
            inputPort.portName = name;
            inputContainer.Add(inputPort);
            return inputPort;
        }

        /// <summary>
        /// Make a new port and return it.
        /// </summary>
        /// <param name="nodeDirection">What direction the port is input or output.</param>
        /// <param name="capacity">Can it accept multiple or a single one.</param>
        /// <returns>Get new port</returns>
        public Port GetPortInstance(Direction nodeDirection, Port.Capacity capacity = Port.Capacity.Single)
        {
            return InstantiatePort(Orientation.Horizontal, nodeDirection, capacity, typeof(float));
        }

        public virtual void LoadValueInToField() { }

        /// <summary>
        /// Reload languages to the current selected language.
        /// </summary>
        public virtual void ReloadLanguage()
        {
            foreach (LanguageGenericText textHolder in languageGenericsList_Texts)
            {
                Reload_TextLanguage(textHolder.inputText, textHolder.textField, textHolder.placeholderText);
            }
            foreach (LanguageGenericAudioClip audioHolder in languageGenericsList_AudioClips)
            {
                Reload_AudioClipLanguage(audioHolder.inputAudioClip, audioHolder.objectField);
            }
        }

        /// <summary>
        /// Add String Modifier Event to UI element.
        /// </summary>
        /// <param name="stringEventModifier">The List<EventData_StringModifier> that EventData_StringModifier should be added to.</param>
        /// <param name="stringEvent">EventData_StringModifier that should be use.</param>
        protected void AddStringModifierEventBuild(List<EventDataStringModifier> stringEventModifier, EventDataStringModifier stringEvent = null)
        {
            EventDataStringModifier tmpStringEventModifier = new EventDataStringModifier();

            // If we paramida value is not null we load in values.
            if (stringEvent != null)
            {
                tmpStringEventModifier.StringEventText.Value = stringEvent.StringEventText.Value;
                tmpStringEventModifier.Number.Value = stringEvent.Number.Value;
                tmpStringEventModifier.StringEventModifierType.Value = stringEvent.StringEventModifierType.Value;
            }

            stringEventModifier.Add(tmpStringEventModifier);

            // Container of all object.
            Box boxContainer = new Box();
            Box boxfloatField = new Box();
            boxContainer.AddToClassList("StringEventBox");
            boxfloatField.AddToClassList("StringEventBoxfloatField");

            // Text.
            TextField textField = CreateTextField(tmpStringEventModifier.StringEventText, "String Event", "StringEventText");

            // ID number.
            FloatField floatField = CreateFloatField(tmpStringEventModifier.Number, "StringEventInt");

            // TODO: Delete maby?
            // Check for StringEventType and add the proper one.
            //EnumField enumField = null;

            // String Event Modifier
            Action tmp = () => ShowHide_StringEventModifierType(tmpStringEventModifier.StringEventModifierType.Value, boxfloatField);
            // EnumField String Event Modifier
            EnumField enumField = GetNewEnumField_StringEventModifierType(tmpStringEventModifier.StringEventModifierType, tmp, "StringEventEnum");
            // Run the show and hide.
            ShowHide_StringEventModifierType(tmpStringEventModifier.StringEventModifierType.Value, boxfloatField);

            // Remove button.
            Button btn = GetNewButton("X", "removeBtn");
            btn.clicked += () =>
            {
                stringEventModifier.Remove(tmpStringEventModifier);
                DeleteBox(boxContainer);
            };

            // Add it to the box
            boxContainer.Add(textField);
            boxContainer.Add(enumField);
            boxfloatField.Add(floatField);
            boxContainer.Add(boxfloatField);
            boxContainer.Add(btn);

            mainContainer.Add(boxContainer);
            RefreshExpandedState();
        }

        /// <summary>
        /// Add String Condition Event to UI element.
        /// </summary>
        /// <param name="stringEventCondition">The List<EventData_StringCondition> that EventData_StringCondition should be added to.</param>
        /// <param name="stringEvent">EventData_StringCondition that should be use.</param>
        protected void AddStringConditionEventBuild(List<EventDataStringCondition> stringEventCondition, EventDataStringCondition stringEvent = null)
        {
            EventDataStringCondition tmpStringEventCondition = new EventDataStringCondition();

            // If we paramida value is not null we load in values.
            if (stringEvent != null)
            {
                tmpStringEventCondition.StringEventText.Value = stringEvent.StringEventText.Value;
                tmpStringEventCondition.Number.Value = stringEvent.Number.Value;
                tmpStringEventCondition.StringEventConditionType.Value = stringEvent.StringEventConditionType.Value;
            }

            stringEventCondition.Add(tmpStringEventCondition);

            // Container of all object.
            Box boxContainer = new Box();
            Box boxfloatField = new Box();
            boxContainer.AddToClassList("StringEventBox");
            boxfloatField.AddToClassList("StringEventBoxfloatField");

            // Text.
            TextField textField = CreateTextField(tmpStringEventCondition.StringEventText, "String Event", "StringEventText");

            // ID number.
            FloatField floatField = CreateFloatField(tmpStringEventCondition.Number, "StringEventInt");

            // Check for StringEventType and add the proper one.
            EnumField enumField = null;
            // String Event Condition
            Action tmp = () => ShowHide_StringEventConditionType(tmpStringEventCondition.StringEventConditionType.Value, boxfloatField);
            // EnumField String Event Condition
            enumField = GetNewEnumField_StringEventConditionType(tmpStringEventCondition.StringEventConditionType, tmp, "StringEventEnum");
            // Run the show and hide.
            ShowHide_StringEventConditionType(tmpStringEventCondition.StringEventConditionType.Value, boxfloatField);

            // Remove button.
            Button btn = GetNewButton("X", "removeBtn");
            btn.clicked += () =>
            {
                stringEventCondition.Remove(tmpStringEventCondition);
                DeleteBox(boxContainer);
            };

            // Add it to the box
            boxContainer.Add(textField);
            boxContainer.Add(enumField);
            boxfloatField.Add(floatField);
            boxContainer.Add(boxfloatField);
            boxContainer.Add(btn);

            mainContainer.Add(boxContainer);
            RefreshExpandedState();
        }

        /// <summary>
        /// hid and show the UI element
        /// </summary>
        /// <param name="value">StringEventModifierType</param>
        /// <param name="boxContainer">The Box that will be hidden or shown</param>
        private void ShowHide_StringEventModifierType(StringEventModifierType value, Box boxContainer)
        {
            var isShow = !(value == StringEventModifierType.SetTrue || value == StringEventModifierType.SetFalse);
            ShowContainer(isShow, boxContainer);
        }

        /// <summary>
        /// hid and show the UI element
        /// </summary>
        /// <param name="value">StringEventConditionType</param>
        /// <param name="boxContainer">The Box that will be hidden or shown</param>
        private void ShowHide_StringEventConditionType(StringEventConditionType value, Box boxContainer)
        {
            var isShow = !(value == StringEventConditionType.True || value == StringEventConditionType.False);
            ShowContainer(isShow, boxContainer);
        }

        /// <summary>
        /// Set a placeholder text on a TextField.
        /// </summary>
        /// <param name="textField">TextField that need a placeholder</param>
        /// <param name="placeholder">The text that will be displayed if the text field is empty</param>
        protected void SetPlaceholderText(TextField textField, string placeholder)
        {
            string placeholderClass = TextField.ussClassName + "__placeholder";

            CheckForText();
            onFocusOut();
            textField.RegisterCallback<FocusInEvent>(evt => onFocusIn());
            textField.RegisterCallback<FocusOutEvent>(evt => onFocusOut());

            void onFocusIn()
            {
                if (textField.ClassListContains(placeholderClass))
                {
                    textField.value = string.Empty;
                    textField.RemoveFromClassList(placeholderClass);
                }
            }

            void onFocusOut()
            {
                if (string.IsNullOrEmpty(textField.text))
                {
                    textField.SetValueWithoutNotify(placeholder);
                    textField.AddToClassList(placeholderClass);
                }
            }

            void CheckForText()
            {
                if (!string.IsNullOrEmpty(textField.text))
                {
                    textField.RemoveFromClassList(placeholderClass);
                }
            }
        }

        /// <summary>
        /// Reload all the text in the TextField to the current selected language.
        /// </summary>
        /// <param name="inputText">List of LanguageGeneric<string></param>
        /// <param name="textField">The TextField that is to be reload</param>
        /// <param name="placeholderText">The text that will be displayed if the text field is empty</param>
        protected void Reload_TextLanguage(List<LanguageGeneric<string>> inputText, TextField textField, string placeholderText = "")
        {
            // Reload Text
            textField.RegisterValueChangedCallback(value =>
            {
                inputText.Find(text => text.LanguageType == graphView.Language).LanguageGenericType = value.newValue;
            });
            textField.SetValueWithoutNotify(inputText.Find(text => text.LanguageType == graphView.Language).LanguageGenericType);

            SetPlaceholderText(textField, placeholderText);
        }

        /// <summary>
        /// Reload all the AudioClip in the ObjectField to the current selected language.
        /// </summary>
        /// <param name="inputAudioClip">List of LanguageGeneric<AudioClip></param>
        /// <param name="objectField">The ObjectField that is to be reload</param>
        protected void Reload_AudioClipLanguage(List<LanguageGeneric<AudioClip>> inputAudioClip, ObjectField objectField)
        {
            // Reload Text
            objectField.RegisterValueChangedCallback(value =>
            {
                inputAudioClip.Find(text => text.LanguageType == graphView.Language).LanguageGenericType = value.newValue as AudioClip;
            });
            objectField.SetValueWithoutNotify(inputAudioClip.Find(text => text.LanguageType == graphView.Language).LanguageGenericType);
        }

        /// <summary>
        /// Add or remove the USS Hide tag.
        /// </summary>
        /// <param name="show">true = show - flase = hide</param>
        /// <param name="boxContainer">which container box to add the desired USS tag to</param>
        protected void ShowContainer(bool show, Box boxContainer)
        {
            string hideUssClass = "Hide";
            if (show)
            {
                boxContainer.RemoveFromClassList(hideUssClass);
            }
            else
            {
                boxContainer.AddToClassList(hideUssClass);
            }
        }

        /// <summary>
        /// Remove box container.
        /// </summary>
        /// <param name="boxContainer">desired box to delete and remove</param>
        protected virtual void DeleteBox(Box boxContainer)
        {
            mainContainer.Remove(boxContainer);
            RefreshExpandedState();
        }

        #endregion

        #region LanguageGenericHolder Class ------------------------------------------------------------------------------------------------------------------------------------------------

        class LanguageGenericText
        {
            public LanguageGenericText(List<LanguageGeneric<string>> inputText, TextField textField, string placeholderText = "placeholderText")
            {
                this.inputText = inputText;
                this.textField = textField;
                this.placeholderText = placeholderText;
            }
            public List<LanguageGeneric<string>> inputText;
            public TextField textField;
            public string placeholderText;
        }

        class LanguageGenericAudioClip
        {
            public LanguageGenericAudioClip(List<LanguageGeneric<AudioClip>> inputAudioClip, ObjectField objectField)
            {
                this.inputAudioClip = inputAudioClip;
                this.objectField = objectField;
            }
            public List<LanguageGeneric<AudioClip>> inputAudioClip;
            public ObjectField objectField;
        }

        #endregion
    }
}