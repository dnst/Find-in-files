using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using GemBox.Document;


namespace find_in_files
{
    public partial class FindInFiles : Form
    {
        #region 

        private List<FileInfo> txtFiles = new List<FileInfo>();
        private List<FileInfo> logFiles = new List<FileInfo>();
        private List<FileInfo> htmlFiles = new List<FileInfo>();
        private List<FileInfo> xmlFiles = new List<FileInfo>();
        private List<FileInfo> docFiles = new List<FileInfo>();
        private List<FileInfo> docxFiles = new List<FileInfo>();
        private List<FileInfo> xlsFiles = new List<FileInfo>();
        private List<FileInfo> xlsxFiles = new List<FileInfo>();
        private float fullSize;
        private float completeSize;
        private string[] keywords = {};
        String skeleton = "<!DOCTYPE html>\r\n\r\n<html lang=\"ru\" xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n    <meta charset=\"utf-8\" />\r\n    <title>Report</title>\r\n</head>\r\n<body>\r\n$lable$\r\n</body>\r\n</html>";
        List<String> spoiler = new List<string>();


        #endregion
        
        public FindInFiles()
        {
            InitializeComponent();
            for (int i = 0; i < extensionCheckedListBox.Items.Count; i++)
            {
                extensionCheckedListBox.SetItemChecked(i, true);
            }
            backgroundWorker.WorkerSupportsCancellation = true;
        }

        #region ReportGeneration

        public void ReportGeneration(String[] keywords, List<String> spoiler)
        {
            spoiler.Clear();
            foreach (var kw in keywords)
            {
                spoiler.Add("<details>\r\n   <summary>" + kw + "</summary>\r\n   <p>$lable$</p>\r\n</details>\r\n");
            }
        }

        public void ReportGeneration(String filename, int num)
        {
            spoiler[num] = spoiler[num].Replace("<p>$lable$</p>", "<p>" + filename + "</p>\r\n   <p>$lable$</p>");
        }

        public String ReportGeneration(String skeleton)
        {
            String report = "";
            foreach (var sp in spoiler)
            {
                report = skeleton.Replace("$lable$", sp.Replace("\r\n   <p>$lable$</p>", "") + "\r\n$lable$");
            }
            report = report.Replace("$lable$", "");
            String reportName = "Report_" + DateTime.UtcNow.ToString("d/M/yyyy-hh.mm.ss.ffffff ") + ".html";
            File.WriteAllText(reportName, report);
            return reportName;
        }

        #endregion

        public string[] stringToArray(string kwords)
        {
            string[] kws = {};
            char[] separator = {','};
            kwords = kwords.Replace(", ", ",");
            kwords = kwords.Replace(" ,", ",");
            kws = kwords.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (registerCheckBox.Checked) 
                return kws;
            for (int i = 0; i < kws.Length; i++)
            {
                kws[i] = kws[i].ToLower();
            }
            return kws;
        }

        public void serchFiles(DirectoryInfo dirPath,  List<FileInfo> files, string pattern)
        {
            try
            {
                foreach (var subdir in dirPath.GetDirectories())
                {
                    serchFiles(subdir, files, pattern);
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
            try
            {
                FileInfo[] temp = dirPath.GetFiles(pattern);
                foreach (var f in temp)
                {
                    files.Add(f);
                    fullSize += f.Length;
                }
                //dirPath.GetFiles(pattern).ToList().ForEach(s => files.Add(s));
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        public void findFiles(DoWorkEventArgs e)
        {
            DirectoryInfo filesPath = new DirectoryInfo("\\");

            if (keywordsTextBox.Text == "")
            {
                MessageBox.Show("Задайте ключевые слова", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            keywords = stringToArray(keywordsTextBox.Text);
            ReportGeneration(keywords, spoiler);

            try
            {
                filesPath = new DirectoryInfo(pathTextBox.Text);
                if (!filesPath.Exists)
                {
                    MessageBox.Show("Выбранная папка не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Не допустимый путь", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (extensionCheckedListBox.CheckedIndices.Count == 0)
            {
                MessageBox.Show("Выберите расширения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            #region Очистка списка файлов

            txtFiles.Clear();
            logFiles.Clear();
            xmlFiles.Clear();
            htmlFiles.Clear();
            xlsFiles.Clear();
            xlsxFiles.Clear();
            docFiles.Clear();
            docxFiles.Clear();

            #endregion


            foreach (int check in extensionCheckedListBox.CheckedIndices)
            {
                #region 

                switch (check)
                {
                    case 0:
                    {
                        serchFiles(filesPath,  txtFiles, "*.txt");
                        //txtFiles = filesPath.GetFiles("*.txt", recursive);
                        break;
                    }
                    case 1:
                    {
                        serchFiles(filesPath, logFiles, "*.log");
                        //logFiles = filesPath.GetFiles("*.log", recursive);
                        break;
                    }
                    case 2:
                    {
                        serchFiles(filesPath, htmlFiles, "*.html");
                        //htmlFiles = filesPath.GetFiles("*.html", recursive);
                        break;
                    }
                    case 3:
                    {
                        serchFiles(filesPath, xmlFiles, "*.xml");
                        //xmlFiles = filesPath.GetFiles("*.xml", recursive);
                        break;
                    }
                    case 4:
                    {
                        serchFiles(filesPath, docFiles, "*.doc");
                        //docFiles = filesPath.GetFiles("*.doc", recursive);
                        break;
                    }
                    case 5:
                    {
                        serchFiles(filesPath, docxFiles, "*.docx");
                        //docxFiles = filesPath.GetFiles("*.docx", recursive);
                        break;
                    }
                    case 6:
                    {
                        serchFiles(filesPath, xlsFiles, "*.xls");
                        //xlsFiles = filesPath.GetFiles("*.xls", recursive);
                        break;
                    }
                    case 7:
                    {
                        serchFiles(filesPath, xlsxFiles, "*.xlsx");
                        //xlsxFiles = filesPath.GetFiles("*.xlsx", recursive);
                        break;
                    }
                }

                #endregion
            }

            completeSize = fullSize;

            foreach (int check in extensionCheckedListBox.CheckedIndices)
            {
                #region

                switch (check)
                {
                    case 0:
                        {
                            OtherFilesToString(txtFiles, e);
                            break;
                        }
                    case 1:
                        {
                            OtherFilesToString(logFiles, e);
                            break;
                        }
                    case 2:
                        {
                            OtherFilesToString(htmlFiles, e);
                            break;
                        }
                    case 3:
                        {
                            OtherFilesToString(xmlFiles, e);
                            break;
                        }
                    case 4:
                        {
                            DocToString(e);
                            break;
                        }
                    case 5:
                        {
                            DocxToString(e);
                            break;
                        }
                    case 6:
                        {
                            XlsToString(e);
                            break;
                        }
                    case 7:
                        {
                            XlsxToString(e);
                            break;
                        }
                }

                #endregion
            }

            backgroundWorker.ReportProgress(100);
            String reportName = ReportGeneration(skeleton);
            var result = MessageBox.Show("Поиск в файлах завершен. Открыть отчет?", "Выполнено", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                Process.Start(reportName);
        }

        public void findInString(string textFile, string filename, DoWorkEventArgs e)
        {
            if (backgroundWorker.CancellationPending)
            {
                e.Cancel = true;
            }
            int num = 0;
            if (!registerCheckBox.Checked)
                textFile = textFile.ToLower();
            foreach (var keyword in keywords)
            {
                int index = textFile.IndexOf(" " + keyword + " ");
                if (index != -1)
                {
                    ReportGeneration(filename, num);
                    //break;
                }
                num++;
            }
            int percentComplete = (int) (100 - completeSize/fullSize*100);
            backgroundWorker.ReportProgress(percentComplete);
        }

        public void DocToString(DoWorkEventArgs e)
        {
            ComponentInfo.SetLicense("DPQT-XUJY-PE7T-OUFA");
            foreach (var docFile in docFiles)
            {
                FileStream fs = new FileStream(docFile.FullName, FileMode.Open);
                byte[] magicNumberFile = new byte[8];
                byte[] magicNumber = {208, 207, 17, 224, 161, 171, 26, 225};
                fs.Read(magicNumberFile, 0, 8);
                fs.Close();
                if (ByteArrayCompareWithSimplest(magicNumberFile, magicNumber))
                {
                    var docText = new StringBuilder();
                    try
                    {
                        var doc = DocumentModel.Load(docFile.FullName);
                        foreach (var paragraph in doc.GetChildElements(true, ElementType.Paragraph))
                        {
                            foreach (Run run in paragraph.GetChildElements(true, ElementType.Run))
                                docText.Append(run.Text);
                            docText.Append('\n');
                        }
                        completeSize -= docFile.Length;
                        findInString(docText.ToString(), docFile.FullName, e);
                    }
                    catch (Exception ex)
                    {
                        completeSize -= docFile.Length;
                        //MessageBox.Show(docFile.Name + "\n" + e.ToString());
                    }
                }
                else
                {
                    completeSize -= docFile.Length;
                }
            }
        }

        public void DocxToString(DoWorkEventArgs e)
        {
            foreach (var docxFile in docxFiles)
            {
                FileStream fs = new FileStream(docxFile.FullName, FileMode.Open);
                byte[] magicNumberFile = new byte[8];
                byte[] magicNumber = {80, 75, 3, 4, 20, 0, 6, 0};
                fs.Read(magicNumberFile, 0, 8);
                fs.Close();
                if (ByteArrayCompareWithSimplest(magicNumberFile, magicNumber))
                {
                    var docxText = new StringBuilder();
                    try
                    {
                        FileStream stream = File.OpenRead(docxFile.FullName);
                        XWPFDocument docx = new XWPFDocument(stream);
                        foreach (var paragraph in docx.Paragraphs)
                        {
                            docxText.Append(paragraph.ParagraphText + "\n");
                        }
                        foreach (var table in docx.Tables)
                        {
                            docxText.Append(table.Text);
                        }
                        completeSize -= docxFile.Length;
                        findInString(docxText.ToString(), docxFile.FullName, e);
                    }
                    catch (Exception)
                    {
                        completeSize -= docxFile.Length;
                        //MessageBox.Show(docxFile.Name);
                    }
                }
                else
                {
                    completeSize -= docxFile.Length;
                }

            }
        }

        public void XlsxToString(DoWorkEventArgs e)
        {
            foreach (var xlsxFile in xlsxFiles)
            {
                FileStream fs = new FileStream(xlsxFile.FullName, FileMode.Open);
                byte[] magicNumberFile = new byte[8];
                byte[] magicNumber = {80, 75, 3, 4, 20, 0, 6, 0};
                fs.Read(magicNumberFile, 0, 8);
                fs.Close();
                if (ByteArrayCompareWithSimplest(magicNumberFile, magicNumber))
                {
                    string xlsxText = "";
                    try
                    {
                        FileStream stream = File.OpenRead(xlsxFile.FullName);
                        XSSFWorkbook workbook = new XSSFWorkbook(stream);
                        for (int i = 0; i < workbook.NumberOfSheets; i++)
                        {
                            var sheet = workbook.GetSheetAt(i);
                            if (sheet == null)
                                continue;
                            for (int j = sheet.FirstRowNum; j < sheet.LastRowNum; j++)
                            {
                                var row = sheet.GetRow(j);
                                if (row == null)
                                    continue;
                                xlsxText += "\n";
                                for (int k = row.FirstCellNum; k < row.PhysicalNumberOfCells; k++)
                                {
                                    var cell = row.Cells[k];
                                    xlsxText += cell.ToString() + " ";
                                }
                            }
                        }
                        completeSize -= xlsxFile.Length;
                        findInString(xlsxText, xlsxFile.FullName, e);
                    }
                    catch (Exception exception)
                    {
                        completeSize -= xlsxFile.Length;
                        MessageBox.Show(exception.ToString());
                    }
                }
                else
                {
                    completeSize -= xlsxFile.Length;
                }
            }
        }

        public void XlsToString(DoWorkEventArgs e)
        {
            foreach (var xlsFile in xlsFiles)
            {

                FileStream fs = new FileStream(xlsFile.FullName, FileMode.Open);
                byte[] magicNumberFile = new byte[8];
                byte[] magicNumber = {208, 207, 17, 224, 161, 171, 26, 225};
                fs.Read(magicNumberFile, 0, 8);
                fs.Close();
                if (ByteArrayCompareWithSimplest(magicNumberFile, magicNumber))
                {
                    string xlsText = "";
                    try
                    {
                        FileStream stream = File.OpenRead(xlsFile.FullName);
                        HSSFWorkbook workbook = new HSSFWorkbook(stream);
                        for (int i = 0; i < workbook.Count; i++)
                        {
                            var sheet = (HSSFSheet)workbook.GetSheetAt(i);
                            if (sheet == null)
                                continue;
                            for (int j = sheet.FirstRowNum; j < sheet.PhysicalNumberOfRows; j++)
                            {
                                var row = (HSSFRow)sheet.GetRow(j);
                                if (row == null)
                                    continue;
                                for (int k = row.FirstCellNum; k < row.LastCellNum; k++)
                                {
                                    var cell = (HSSFCell)row.Cells[k];
                                    xlsText += cell.ToString();
                                }
                            }
                        }
                        completeSize -= xlsFile.Length;
                        findInString(xlsText, xlsFile.FullName, e);
                    }
                    catch (Exception)
                    {
                        completeSize -= xlsFile.Length;
                    }

                }
                else
                {
                    completeSize -= xlsFile.Length;   
                }
            }
        }

        public void OtherFilesToString(List<FileInfo> files, DoWorkEventArgs e)
        {
            foreach (var file in files)
            {
                String text = File.ReadAllText(file.FullName);
                completeSize -= file.Length;
                findInString(text, file.FullName, e);
            }
        }

        private static bool ByteArrayCompareWithSimplest(byte[] p_BytesLeft, byte[] p_BytesRight)
        {
            if (p_BytesLeft.Length != p_BytesRight.Length)
                return false;

            var length = p_BytesLeft.Length;

            for (int i = 0; i < length; i++)
            {
                if (p_BytesLeft[i] != p_BytesRight[i])
                    return false;
            }

            return true;
        }

        private void selectDirButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.ShowDialog();
            pathTextBox.Text = folderBrowserDialog.SelectedPath;
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            pathTextBox.ReadOnly = true;
            findButton.Enabled = false;
            cancelButton.Enabled = true;
            selectDirButton.Enabled = false;
            keywordsTextBox.ReadOnly = true;

            //findFiles();
            if (backgroundWorker.IsBusy != true)
            {
                backgroundWorker.RunWorkerAsync();
            }
            /*
            pathTextBox.ReadOnly = false;
            selectDirButton.Enabled = true;
            keywordsTextBox.ReadOnly = false;*/
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            findFiles(e);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //debugTextBox.Text = e.ProgressPercentage.ToString();
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pathTextBox.ReadOnly = false;
            findButton.Enabled = true;
            selectDirButton.Enabled = true;
            keywordsTextBox.ReadOnly = false;
            cancelButton.Enabled = false;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
            /*
            if (backgroundWorker.WorkerSupportsCancellation == true)
            {
                backgroundWorker.CancelAsync();
            }*/
        } 

    }
}
