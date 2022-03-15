using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace Furniture
{
    class WordHelper
    {
        private FileInfo _fileInfo;

        public WordHelper(string filePath)
        {
            if (File.Exists(filePath))
            {
                _fileInfo = new FileInfo(filePath);
            }
            else
            {

            }
        }
        public void CreateDocument(Dictionary<string, string> items,string category)
        {
            var app = new Word.Application();
            string file = _fileInfo.FullName;
            var missing = Type.Missing;
            app.Documents.Open(file);
            foreach(var item in items)
            {
                Word.Find find = app.Selection.Find;
                find.Text = item.Key;
                find.Replacement.Text = item.Value;

                var wrap = Word.WdFindWrap.wdFindContinue;
                var replace = Word.WdReplace.wdReplaceAll;

                find.Execute(FindText: Type.Missing,
                    MatchCase: false,
                    MatchWholeWord: false,
                    MatchWildcards: false,
                    MatchSoundsLike: missing,
                    MatchAllWordForms: false,
                    Forward: true,
                    Wrap: wrap,
                    Format: false,
                    ReplaceWith: missing,
                    Replace: replace);

               

            }
            string newFileName = new DirectoryInfo(@"..\..\Reports\").FullName + DateTime.Now.ToString("ddMMyyyy HHmmss") + _fileInfo.Name;
            app.ActiveDocument.SaveAs2(newFileName);

            app.ActiveDocument.Close();
            app.Quit();
        }
    }
}
