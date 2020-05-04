using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avaino.Code.Parser
{
    public class Parser
    {
        private LibraryFinder libraryFinder;

        public Parser(LibraryFinder libraryFinder)
        {
            this.libraryFinder = libraryFinder;
        }

        public IEnumerable<ICodeEntity> FindDeclaredEntities(string content, List<string> scanned)
        {
            var entities = new List<ICodeEntity>();
            var reader = new StringReader(content);

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLine().Trim();
                if (line.StartsWith("#include"))
                {
                    var libName = FindBetween(line, "<", ">") ?? FindBetween(line, "\"", "\"");
                    if (libName == null) continue;
                    var libPath = libraryFinder.FindLibrary(libName);
                    if (libPath == null)
                        continue;
                    if (scanned.Contains(libPath))
                        continue;
                    scanned.Add(libPath);
                    var libData = File.ReadAllText(libPath);
                    entities.AddRange(FindDeclaredEntities(libData, scanned));
                }
                else
                {
                    var lineEntities = ParseLine(line);
                    if (lineEntities == null) continue;
                    entities.AddRange(lineEntities);
                }
            }

            return entities;
        }

        private IEnumerable<ICodeEntity> ParseLine(string line)
        {
            var entities = new List<ICodeEntity>();
            if (line.StartsWith("#define"))
            {
                var parts = line.Substring("#define".Length).Split(' ');
                if (parts.Length == 1)
                {
                    var defName = parts.First();
                    entities.Add(new PreprocessorDef(defName, string.Empty));
                }
                else if (parts.Length > 1)
                {
                    var defName = parts.First();
                    var defVal = string.Join(" ", parts.Skip(1));
                    entities.Add(new PreprocessorDef(defName, defVal));
                }
            }

            FindDefinitions(line, "class", d =>
            {
                entities.Add(new ClassDef(d));
            });

            FindDefinitions(line, "struct", d =>
            {
                entities.Add(new StructDef(d));
            });

            ForEachOccurrence(line, "typedef", d =>
            {
                var name = FindBetween(d, "typedef", ";");
                if (name == null)
                    return;

                var parts = name.Split(' ');
                if (parts.Length < 2)
                    return;

                var defName = parts.Last();
                var defSource = string.Join(" ", parts.Take(parts.Length - 1));
                entities.Add(new Typedef(defSource, defName));
            });

            if (entities.Count == 0) return null;
            else return entities;
        }

        private void FindDefinitions(string line, string deftype, Action<string> dataCb)
        {
            ForEachOccurrence(line, deftype, p =>
            {
                var data = FindBetween(p, deftype, "{") ?? FindBetween(p, deftype, ";"); // Get data
                if (data == null)
                    return;

                data = data.Trim(':'); // Drop namespace indicators
                if (data.Contains(":"))  // Remove all inheritances
                    data = data.Remove(data.IndexOf(':'));

                dataCb(data.Trim());
            });
        }

        private void ForEachOccurrence(string line, string val, Action<string> partCb)
        {
            int idx;
            while ((idx = line.IndexOf(val)) != -1)
            {
                partCb(line);
                line = line.Substring(idx + val.Length); // Move to next
            }
        }

        private string FindBetween(string data, string start, string end)
        {
            var startIdx = data.IndexOf(start);
            if (startIdx < 0) return null;
            var tmp = data.Substring(startIdx + start.Length);
            var endIdx = tmp.IndexOf(end);
            if (endIdx < 0) return null;
            return tmp.Remove(endIdx);
        }

    }
}
