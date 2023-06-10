using CenterRegisterCard.DAL;
using CenterRegisterCard.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
namespace CenterRegisterCard.Service
{
    public class WordDocumentation
    {
        private FileInfo _fileInfo;
        public WordDocumentation(string fineName)
        {
            if (File.Exists(fineName))
            {
                _fileInfo = new FileInfo(fineName);
            }
            else
            {
                throw new ArgumentException("File not found");
            }
        }

        public bool Process(Dictionary<string, string> items, string path)
        {
            Word.Application app = null;
            try
            {
                app = new Word.Application();
                Object file = _fileInfo.FullName;
                object missing = Type.Missing;
                app.Documents.Open(file);
                foreach (var item in items)
                {
                    Word.Find find = app.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;

                    find.Execute(FindText: Type.Missing,
                        MatchCase: false,
                        MatchWholeWord: false,
                        MatchWildcards: false,
                        MatchSoundsLike: missing,
                        MatchAllWordForms: false,
                        Forward: true,
                        Wrap: wrap,
                        Format: false,
                        ReplaceWith: missing, Replace: replace);
                }
                if (CenterRegisterCardContext.userAccountreg != null)
                {
                    Object newFileName = Path.Combine(@$"{path}\UserDocumantation\", $"{CenterRegisterCardContext.userAccountreg}");
                    app.ActiveDocument.SaveAs2(newFileName);
                    app.ActiveDocument.Close();
                    app.Quit();
                    CenterRegisterCardContext.userAccountreg = null;
                }
                else
                {
                    Object newFileName = Path.Combine(@$"{path}\EmployeeDocumantation\", $"{CenterRegisterCardContext.userActiveView.PassportSeriesNumber}");
                    app.ActiveDocument.SaveAs2(newFileName);
                    app.ActiveDocument.Close();
                    app.Quit();
                }

                return true;
            }
            catch (Exception ex)

            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (app != null)
                {
                    app.Quit();
                }
            }
            return false;
        }
    }
}
