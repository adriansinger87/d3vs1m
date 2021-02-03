using D3vS1m.Application.Scene.Geometries;
using D3vS1m.Domain.Data.Scene;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TeleScope.Logging;
using TeleScope.Logging.Extensions;

namespace D3vS1m.Application.Scene
{

    /*
     * possible Source: https://github.com/helix-toolkit/helix-toolkit/blob/develop/Source/HelixToolkit.Wpf/Importers/ObjReader.cs
     * online access: 08.01.2019
     * License of Original: MIT
     */

    public class ObjAdapter
    {
        // -- fields

        private readonly ILogger<ObjAdapter> _log;

        private Geometry _root;
        private Geometry _current;

        private List<Vertex> _vertices;
        private List<Vertex> _normales;

		// -- methods

		public ObjAdapter()
		{
            _log = LoggingProvider.CreateLogger<ObjAdapter>();

        }

        public Tout Adapt<Tin, Tout>(Tin input) where Tout : new()
        {
            // validation
            Type inType = typeof(Tin);
            Type outType = typeof(Tout);

            if (!typeof(string).IsAssignableFrom(inType) ||
                !typeof(Geometry).IsAssignableFrom(outType))
            {
                _log.Error($"The casting from '{inType.Name}' to '{outType.Name}' is not supported by the {this.GetType().Name}.");
                return new Tout();
            }

            // instantiate global objects
            _root = new Geometry();
            _current = _root;
            _vertices = new List<Vertex>();
            _normales = new List<Vertex>();

            try
            {
                using (StringReader reader = new StringReader(input as string))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        ReadLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Critical(ex);
            }
            finally
            {
                _vertices = null;
                _normales = null;
            }

            // final return
            return (Tout)Convert.ChangeType(_root, outType);
        }

        /// <summary>
        /// Reads the line string and decides what tag is in front of it and
        /// starts other specific read methods
        /// </summary>
        /// <param name="line">the current line string</param>
        private void ReadLine(string line)
        {
            if (line.Length == 0)
            {
                return;
            }

            string[] lineData = line.Split(' ');

            if (lineData[0] == "#")         // a comment in obj
            {
                // TODO: make something meaningful with a comment
                return;
            }
            else if (lineData[0] == "g")        // group
            {
                _current = ReadGroup(lineData);
            }
            else if (lineData[0] == "v")        // vertex
            {
                _vertices.Add(ReadVertex(lineData));
                _current.Vertices.Add(_vertices.Last());
            }
            else if (lineData[0] == "vt")       // vertex-texture
            {
                // not implemented
            }
            else if (lineData[0] == "vn")       // vertex-normale
            {
                _normales.Add(ReadVertex(lineData));
            }
            else if (lineData[0] == "f")        // face
            {
                _current.Faces.Add(
                    ReadFace(lineData));
            }
        }

        /// <summary>
        /// Reads the data of a group line. 
        /// </summary>
        /// <param name="groupData">splitted line of a group line</param>
        /// <returns>Latest geometry child object for further data lines</returns>
        private Geometry ReadGroup(string[] groupData)
        {
            Geometry current = _root;        // search for group names always beginning at root  
            Geometry next = null;

            /*
             * start index = 1 !!!
             * skip first elemenet in the the line data, because its always a "g" and
             * not part of the name hierarchy
             */
            for (int i = 1; i < groupData.Length; i++)
            {
                var name = groupData[i];
                next = current.Children.FirstOrDefault(g => g.Name == name);

                if (next == null)
                {
                    next = new Geometry(name);  // create next geometry
                    current.Children.Add(next);
                    current = next;
                }
                else
                {
                    current = next;             // move to next
                }
            }

            // return latest object
            return next;
        }

        private Vertex ReadVertex(string[] vertexData)
        {
            /*
	         * aktuelle Zeilendaten auslesen:
	         * Die Zeile sieht folgendermaßen aus "v 1.0 2.1 0.5"
	         * Sie wurde in ein Array anhand der Leerzeichen gesplittet
	         * und sieht so aus:	[0]		[1]		[2]		[3]			: Länge 4
	         *						v		1.0		2.1		0.5
	         * ---
	         * manchmal stehen auch zwei Leerzeichen zwischen v und dem Rest,
	         * also wie folgt:		[0]		[1]		[2]		[3]		[4]	: Länge 5
	         *						v		""		1.0		2.1		0.5
	         * -> darum wird von hinten rückwärts gezählt
	         */
            int len = vertexData.Length;

            if (len < 4 || len > 5)
            {
                throw new InvalidDataException($"Could not load obj-file. The length of the vertices line '{len}' is invalid.");
            }

            float x = ParseF(vertexData[len - 3]);
            float y = ParseF(vertexData[len - 2]);
            float z = ParseF(vertexData[len - 1]);

            return new Vertex(x, y, z);
        }

        private float ParseF(string s)
        {
            return float.Parse(s.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
        }

        private Face ReadFace(string[] faceData)
        {
            Face f = new Face();

            // start at index 1 because of 'f' tag that is the first element
            for (int i = 1; i < faceData.Length; i++)
            {
                string data = faceData[i];
                if (string.IsNullOrEmpty(data)) continue;

                string[] triple = data.Split('/');

                int vex = SetIndex(triple[0]);      // vertex index
                int vtx = -1;                       // vertex texture index
                int vnx = -1;                       // vertex normale

                // read triple information as one piece of line data
                if (triple.Length == 3)
                {
                    if (!string.IsNullOrEmpty(triple[1]))
                    {
                        vtx = SetIndex(triple[1]);
                    }
                    vnx = SetIndex(triple[2]);
                }
                else if (triple.Length == 2)
                {
                    vtx = SetIndex(triple[1]);
                }

                // push vertices 
                f.Vertices.Add(_vertices[vex]);

                // push normals
                if (vnx > -1)
                {
                    f.Normals.Add(_vertices[vnx]);
                }
            }

            return NormalizeFace(f);
        }

        private Face NormalizeFace(Face f)
        {
            try
            {
                if (f.Normals.Count > 0) return f;

                Vertex n = Vertex.Normalize(f.A, f.B, f.C);
                _normales.Add(n);
                f.Normals.Add(n);
            }
            catch (Exception ex)
            {
                _log.Error("Could not normalize face");
            }

            return f;
        }

        /// <summary>
        /// Sets the index of the face information with the input string for
        /// vertex index, vertex texture index or vertex normale index.
        /// </summary>
        /// <param name="s">input input string that represent a index</param>
        private int SetIndex(string s)
        {
            /*
             * The face index value s is a one-based index out of the obj file
             * the minus one results in zero-based indices for vertex lists
             */
            return (int.Parse(s) - 1);
        }


    }
}
