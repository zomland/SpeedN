using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Transaction
{
    public static class LanguageDict
    {
        public static Dictionary<string, List<string>> HomeLanguageDict = new Dictionary<string, List<string>>()
        {
            {"EnergyMonitorTitle",new List<string>(){"Energy","Năng lượng","Énergie","活力"}},
            {"textStartButton",new List<string>(){"Start","Bắt đầu","Début","开始"}},
            {"textGuideButton",new List<string>(){"How to play?","Hướng dẫn","Guide","指导"}}
        };

        public static Dictionary<string, List<string>> AccountLanguageDict
            = new Dictionary<string, List<string>>()
        {
            {"",new List<string>(){}},
        };

    }
}
