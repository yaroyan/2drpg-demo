using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor.UIElements;
#endif

namespace Yaroyan.SeedWork.Dialogue
{
    public class DialogueContainerValues { }

    [System.Serializable]
    public class LanguageGeneric<T>
    {
        public LanguageType LanguageType;
        public T LanguageGenericType;
    }

    //[System.Serializable]
    //public class Container_DialogueEventSO
    //{
    //    public DialogueEventSO DialogueEventSO;
    //}

    // Values --------------------------------------

    [System.Serializable]
    public class Ref<T> : IEquatable<Ref<T>>
    {
        public T Value { get; set; }

        public static Ref<T> Default() => new Ref<T>() { Value = default };

        public bool MaybeDefault => Equals(Default());

        public override bool Equals(object obj)
        {
            return Equals(obj as Ref<T>);
        }

        public bool Equals(Ref<T> other)
        {
            return other is not null &&
                   EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public static bool operator ==(Ref<T> left, Ref<T> right)
        {
            return EqualityComparer<Ref<T>>.Default.Equals(left, right);
        }

        public static bool operator !=(Ref<T> left, Ref<T> right)
        {
            return !(left == right);
        }
    }

    // Enums --------------------------------------

    //    [System.Serializable]
    //    public class Container_ChoiceStateType
    //    {
    //#if UNITY_EDITOR
    //        public EnumField EnumField;
    //#endif
    //        public ChoiceStateType Value = ChoiceStateType.Hide;
    //    }

    //    [System.Serializable]
    //    public class Container_EndNodeType
    //    {
    //#if UNITY_EDITOR
    //        public EnumField EnumField;
    //#endif
    //        public EndNodeType Value = EndNodeType.End;
    //    }

    //    [System.Serializable]
    //    public class Container_StringEventModifierType
    //    {
    //#if UNITY_EDITOR
    //        public EnumField EnumField;
    //#endif
    //        public StringEventModifierType Value = StringEventModifierType.SetTrue;
    //    }

    //    [System.Serializable]
    //    public class Container_StringEventConditionType
    //    {
    //#if UNITY_EDITOR
    //        public EnumField EnumField;
    //#endif
    //        public StringEventConditionType Value = StringEventConditionType.True;
    //    }


    // Event --------------------------------------

    [System.Serializable]
    public class EventDataStringModifier
    {
        public Ref<string> StringEventText { get; set; } = new();
        public Ref<float> Number { get; set; } = new();

        //public Container_StringEventModifierType StringEventModifierType = new Container_StringEventModifierType();
        public Ref<StringEventModifierType> StringEventModifierType { get; set; } = new Ref<StringEventModifierType> { Value = Dialogue.StringEventModifierType.SetTrue };
    }

    [System.Serializable]
    public class EventDataStringCondition
    {
        public Ref<string> StringEventText { get; set; } = new();
        public Ref<float> Number { get; set; } = new();

        //public Container_StringEventConditionType StringEventConditionType = new Container_StringEventConditionType();
        public Ref<StringEventConditionType> StringEventConditionType { get; set; } = new Ref<StringEventConditionType>() { Value = Dialogue.StringEventConditionType.True };
    }
}

