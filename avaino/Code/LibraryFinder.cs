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
        private IEnumerable<string> searchPaths;

        private IEnumerable<string> libraries;

        public LibraryFinder(string arduinoInstallPath)
        {
            var list = new List<string>();
            list.Add(Path.Combine(arduinoInstallPath, "libraries"));
            list.Add(Path.Combine(arduinoInstallPath, "hardware\\tools"));
            list.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Arduino\\libraries"));
            list.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Arduino15\\packages"));
            searchPaths = list;
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
            var list = new List<string>();
            foreach (var searchPath in searchPaths)
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
