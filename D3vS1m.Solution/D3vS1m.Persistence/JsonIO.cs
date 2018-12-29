using D3vS1m.Domain.System.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace D3vS1m.Persistence
{
    /// <summary>
    /// Stellt statische Funktionalität für JSON Ex- und Import von beliebigen Objekten bereit.
    /// Sollte ggf. in die konkreten Ex- und Import-Klassen verschoben werden.
    /// </summary>
    static class JsonIO
    {
        #region ToJsonString (2)
        /// <summary>
        /// Erstellt eine Zeichenkette im json-Format aus einem beliebigen Objekt
        /// </summary>
        /// <param name="obj">Das Objekt mit beliebig kaskadierten Eigenschaften</param>
        /// <returns>die resuliterende Zeichenkette</returns>
        public static string ToJsonString(object obj)
        {
            return ToJsonString(obj, Formatting.None);
        }
        /// <summary>
        /// Erstellt eine Zeichenkette im json-Format aus einem beliebigen Objekt
        /// </summary>
        /// <param name="obj">Das Objekt mit beliebig kaskadierten Eigenschaften</param>
        /// <param name="format">Das gewünschte Format. mit Zeilenumbrüchen. oder ohne.</param>
        /// <returns>die resuliterende Zeichenkette</returns>
        public static string ToJsonString(object obj, Formatting format)
        {
            string json = "{ }";
            try
            {
                json = JsonConvert.SerializeObject(obj, format);
            }
            catch (Exception ex)
            {
                json = "{method: 'MagnaPaintMvc.IO.JsonObjectConverter.ToJsonString()'. error: '" + ex.Message + "'}";
            }
            return json;
        } 
        #endregion

        /// <summary>
        /// Lädt aus der angegebenen Datei das generische Objekt T und gibt dieses aus.
        /// </summary>
        /// <typeparam name="T">Das Objekttyp</typeparam>
        /// <param name="file">Die Zieldatei. inkl. Pfadangabe</param>
        /// <returns>Das ausgelesene Objekt vom Typ T.</returns>
        public static T LoadFromJson<T>(string file)
        {
            T obj;
            using (StreamReader r = new StreamReader(file))
            {
                string json = r.ReadToEnd();
                if (typeof(T) != typeof(string))
                {
                    obj = JsonConvert.DeserializeObject<T>(json);
                }
                else
                {
                    obj = (T)Convert.ChangeType(json, typeof(string));
                }
            }

            return obj;
        }

        /// <summary>
        /// Schreibt die Daten eines beliebigen Objektes in die angegebene Datei.
        /// </summary>
        /// <param name="obj">Das Objekt. welches eingelesen wird</param>
        /// <param name="file">Die Zieldatei. inkl. Pfadangabe</param>
        public static bool SaveToJson(object obj, string file)
        {
            bool result = false;
            try
            {
                File.WriteAllText(@file, JsonIO.ToJsonString(obj, Formatting.Indented));
                result = true;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
            }
            
            return result;
        }

      
    }
}