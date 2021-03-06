

using System;
using System.Collections.Generic;
using UnityEngine;
using static surfm.tool.i18n.I18nDB;

namespace surfm.tool.i18n {
    public class I18n : MonoBehaviour {

        public static readonly string KEY_I18N = "KEY_I18N";
        public static I18n instance = null;
        internal SystemLanguage language { get; private set; }

        public string category = "I18nDB";

        void Awake() {
            if (instance == null) {
                instance = this;
                setupLanguage();
            }
        }


        internal void setupLanguage() {
            language = loadAssignLanguage();
        }


        private SystemLanguage loadAssignLanguage() {
            try {
                if (!PlayerPrefs.HasKey(KEY_I18N)) return Application.systemLanguage;
                string ui = PlayerPrefs.GetString(KEY_I18N);
                SystemLanguage lg = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), ui);
                return lg;
            } catch (Exception e) {
                return SystemLanguage.English;
            }
        }

        internal static string get(object i18N_PREFIX) {
            throw new NotImplementedException();
        }

        public void saveAssignLanguage(SystemLanguage lg) {
            PlayerPrefs.SetString(KEY_I18N, lg.ToString());
        }

        void OnDestroy() {
            instance = null;
        }

        public string _get(I18nKey k) {
            if (string.IsNullOrEmpty(k.category)) {
                return _get(k.key);
            } else {
                return _get(k.category, k.key);
            }
        }

        public string _get(string key) {
            return _get(category, key);
        }

        public string _get(string cat, string key) {
            return I18nDB.get(cat, key, language);
        }

        public static string get(string key) {
            return getInstance()._get(key);
        }

        public List<string> listKeys() {
            return new List<string>(I18nDB.getInstance(category).getMap().Keys);
        }

        public Term getTerm(string key) {
            return I18nDB.getInstance(category).getMap()[key];
        }

        public static string get(I18nKey key) {
            return getInstance()._get(key);
        }

        public static string get(string cat, string key) {
            return getInstance()._get(cat, key);
        }

        public static I18n getInstance() {
            initLanguageIniter();
            return instance;
        }

        private static void initLanguageIniter() {
            if (instance == null) {
                instance = FindObjectOfType<I18n>();
                instance.setupLanguage();
            }
        }

    }
}

