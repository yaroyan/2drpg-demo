using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yaroyan.SproutWork.Domain.Model.SaveData
{
    public class Preferences
    {
        private SaveDataId saveDataId;
        private float walkSpeed;
        private float soundEffectVolume;
        private float backgroundMusicVolume;
        private Language language;
    }

    public enum Language
    {
        Japanese
    }
}
