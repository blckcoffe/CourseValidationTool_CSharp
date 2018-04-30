using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CourseValidationTool_CSharp
{
    public partial class FirstPage : Form
    {
        private string logPath = "C:\\TMP\\CheckLog.txt";
        public FirstPage()
        {
            InitializeComponent();
        }

        private void OpenFileFolderBtn_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.ShowDialog();
            fileFolderText.Text = folderBrowserDialog.SelectedPath;
        }

        private int validateCourse(string FileFolder, string enCoding)
        {
            JsonFileProcessor jsonFileProcessor = new JsonFileProcessor(logPath);        
            int result = jsonFileProcessor.ProcessJsonFileFolder(fileFolderText.Text, enCoding);
            jsonFileProcessor.CloseStream();
            return result;
        }

        private void validateCourseBtn_Click(object sender, EventArgs e)
        {
            string enCodeCode = enCodingList.GetItemText(enCodingList.SelectedItem);
            if (enCodeCode == "")
            {
                enCodeCode = "GB2312";
            }

            if ( fileFolderText.Text != "" )
            {
                int result = validateCourse( fileFolderText.Text, enCodeCode);
                if(result == 0)
                {
                    MessageBox.Show("没有发现问题", "成功", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("发现问题", "错误", MessageBoxButtons.OK);
                }

                string filecontent;
                FileStream fs = new FileStream(logPath, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                filecontent = sr.ReadToEnd();
                sr.Close();
                fs.Close();
                richTextBox.Text = filecontent;
            }
            else
            {
                MessageBox.Show("请选择课程所在目录", "错误", MessageBoxButtons.OK);
            }

        }

        private void TestEncodingBtn_Click(object sender, EventArgs e)
        {
            string enCodeCode = enCodingList.GetItemText(enCodingList.SelectedItem);
            if((enCodeCode == "")||(fileFolderText.Text == "")){

                MessageBox.Show("请选择课程所在目录或编码", "错误", MessageBoxButtons.OK);
                return;
            }
            string jsonFile = JsonFileProcessor.ReadJsonFile(fileFolderText.Text, enCodeCode);
            richTextBox.Text = jsonFile;
        }
    }
}
