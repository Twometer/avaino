using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avaino.Code
{
    public class LibraryFinder
    {
        private IEnumerable<string> systemSearchPaths;

        private IEnumerable<string> libraries;

        public LibraryFinder(string arduinoInstallPath, string projectPath)
        {
            var list = new List<string>();
            list.Add(Path.Combine(arduinoInstallPath, "libraries"));
            list.Add(Path.Combine(arduinoInstallPath, "hardware\\tools"));
            list.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Arduino\\libraries"));
            list.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Arduino15\\packages"));
            list.Add(projectPath);
            systemSearchPaths = list;
        }

        public string FindLibrary(string name)
        {
            if (libraries == null)
                RefreshIndex();

            return libraries.Where(n => n.EndsWith(name))
                .FirstOrDefault();
        }

        public void RefreshIndex()
        {
            var list = new HashSet<string>();
            foreach (var searchPath in systemSearchPaths)
            {
                var di = new DirectoryInfo(searchPath);
                foreach(var lib in di.EnumerateFiles("*.h", SearchOption.AllDirectories))
                {
                    list.Add(lib.FullName);
                }
            }
            this.libraries = list;
        }

    }
}
