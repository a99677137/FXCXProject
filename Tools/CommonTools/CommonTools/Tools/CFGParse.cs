using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Game.Tools.CommonTools
{
    public class CFGParse
    {
        const string regexSection = "\\[[\\w\\s\\.]*\\][^\\[]*";
        const string regexSectionTitle = "\\[(.*)\\]";
        const string regexKeyValue = ".*=.*";
        public CFGParse()
        {
 
        }
        public CFGParse(String content, bool ignoreCase = false)
        {
            Parse(content, ignoreCase);
        }
        public void  Parse(String content,bool ignoreCase = false)
        {
            if (ignoreCase)
                content = content.ToLower();

            Clear();
            foreach (Match section in Regex.Matches(content, regexSection))
            {
                String sectionName = Regex.Replace(Regex.Match(section.ToString(), regexSectionTitle).ToString(), regexSectionTitle, "$1").ToString().Trim();
                foreach (Match keyvalue in Regex.Matches(section.ToString(), regexKeyValue))
                {
                    string kv = keyvalue.ToString().Trim();
                    if (kv.ToCharArray()[0] == ';')
                        continue;
                    int index = kv.IndexOf('=');
                    int indexX = kv.IndexOf(';');
                    if (indexX >= 0)
                    {

                        if (indexX < index)
                            continue;
                        AddSection(sectionName, kv.Substring(0, index).Trim(), kv.Substring(index + 1, indexX - index - 1).Trim());
                    }
                    else
                    {
                        AddSection(sectionName, kv.Substring(0, index).Trim(), kv.Substring(index + 1).Trim());
                    }
                }
            }
        }

        public bool SectionExists(String section)
        {
            if(m_SectionDic.ContainsKey(section))
                return true;
            else
                return false;
        }
        public bool KeyExists(String section,String key)
        {
            if (m_SectionDic.ContainsKey(section))
            {
                return m_SectionDic[section].ContainsKey(key);
            }
            else
                return false;
        }
        public bool GetInt(String section, String key, out Int32 value)
        {
            string data = GetSectionValue(section, key);
            if (data == null)
            {
                value = Int32.MinValue;
                return false;
            }
            else
            {
                if (Int32.TryParse(data, out value))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Int32 GetInt(String section, String key)
        {
            string data = GetSectionValue(section, key);
            if (data == null)
            {
                return Int32.MinValue;
            }
            else
            {
                Int32 value;
                if (Int32.TryParse(data, out value))
                {
                    return value;
                }
                else
                {
                    return Int32.MinValue;
                }
            }
        }

        public bool GetSingle(String section, String key, out float value)
        {
            string data = GetSectionValue(section, key);
            if (data == null)
            {
                value = Single.MinValue;
                return false;
            }
            else
            {
                if(Single.TryParse(data,out value))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public float GetSingle(String section, String key)
        {
            string data = GetSectionValue(section, key);
            if (data == null)
            {
                return Single.MinValue;
            }
            else
            {
                float value;
                if (Single.TryParse(data, out value))
                {
                    return value;
                }
                else
                {
                    return Single.MinValue;
                }
            }
        }

        public bool GetString(String section, String key, out String value)
        {
            string data = GetSectionValue(section, key);
            if (data == null)
            {
                value = null;
                return false;
            }
            else
            {
                value = data;
                return true;
            }
        }

        public String GetString(String section, String key)
        {
            return GetSectionValue(section, key);
        }

        protected void Clear()
        {
            m_SectionDic.Clear();
        }

        protected string GetSectionValue(string section, string key)
        {
            if(m_SectionDic.ContainsKey(section))
            {
                if(m_SectionDic[section].ContainsKey(key))
                {
                    return m_SectionDic[section][key];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        protected void AddSection(string section,string key,string value)
        {
            if(m_SectionDic.ContainsKey(section))
            {
                if(m_SectionDic[section].ContainsKey(key))
                {
                    GameLog.Debug("CFG配置文件{0}中key重复", section);
                }
                else
                {
                    m_SectionDic[section][key] = value;
                }
            }
            else
            {
                m_SectionDic.Add(section, new Dictionary<string, string>());
                m_SectionDic[section][key] = value;
            }
        }

        Dictionary<string, Dictionary<string, string>> m_SectionDic = new Dictionary<string, Dictionary<string, string>>();
    }
}
