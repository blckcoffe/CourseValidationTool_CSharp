using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Text.RegularExpressions;

namespace CourseValidationTool_CSharp
{
    class LogCache
    {
        private StringBuilder logBuffer;
        public LogCache()
        {
            logBuffer = new StringBuilder();
        }

        public void Write(string log)
        {
            logBuffer.Append(log);
        }

        public string GetLog()
        {
            return logBuffer.ToString();
        }
    }

    class JsonFileProcessor
    {
        private string CourseName;
        private List<string> CourseVideoInFolder;
        private List<string> CourseVideoInJsonOfSecton;
        private List<string> CourseVideoInJsonOfLinkedVideo;
        private LogCache swLog;
        //private StreamWriter swLog;

        public JsonFileProcessor(string log )
        {
            CourseVideoInFolder = new List<string>();
            CourseVideoInJsonOfSecton = new List<string>();
            CourseVideoInJsonOfLinkedVideo = new List<string>();
            swLog = new LogCache();
            //fsLog = new FileStream( log, FileMode.Create, FileAccess.ReadWrite);
            //swLog = new StreamWriter(fsLog);
        }

        ~JsonFileProcessor()
        {
            //swLog.Flush();
            //swLog.Close();
            //fsLog.Close();
        }

        public string GetLog()
        {
            return swLog.GetLog();
            //swLog.Flush();
            //swLog.Close();
            //fsLog.Close();
        }

        static public string ReadJsonFile(string FilePath, string enCoding )
        {
            string jsonFile = "";
            DirectoryInfo folder = new DirectoryInfo(FilePath);
            foreach (FileInfo file in folder.GetFiles("*.json"))
            {
                if ( file.FullName.IndexOf("_Extend.json") < 0 )
                {
                    jsonFile = file.FullName;
                }               
            }

            string filecontent;
            FileStream fs       = new FileStream(jsonFile, FileMode.Open, FileAccess.Read);
            StreamReader sr     = new StreamReader(fs, Encoding.GetEncoding(enCoding));
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            filecontent = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            return filecontent;
        }

        public Boolean ValidateFolderFiles(string FilePath)
        {
            Boolean result = true;
            Boolean tmpResult = true;
            string localFileFolder = FilePath;
            string[] strArray = localFileFolder.Split('\\');
            this.CourseName = strArray[strArray.Length - 1];

            string regEpx = "^" + strArray[strArray.Length - 1] + "[_][0-9]*";
            DirectoryInfo folder = new DirectoryInfo(localFileFolder);
            foreach ( FileInfo file in folder.GetFiles("*.mp4"))
            {
                this.CourseVideoInFolder.Add(file.Name);
                tmpResult = Regex.IsMatch(file.Name, regEpx);
                if (!tmpResult)
                {
                    _WriteLogWithEnter("文件夹中课程名称和视频不匹配\r\n   课程名称：《" + strArray[strArray.Length - 1] + "》  视频名称：" + file.Name, 1);
                    result = false;
                }
            }
            return result;
        }

        public Boolean ValidateJsonContent(JsonResult JsonContent )
        {
            Boolean result = true;
            string coursNameInJson = JsonContent.notetitle;
            if (coursNameInJson != this.CourseName)
            {
                _WriteLogWithEnter("NoteTile中的课程名《" + coursNameInJson +  "》与文件夹名称不一致", 1 );
                result = false;
            }

            int indexStart, indexEnd;
            string videoInJsonOfSection;
            CourseVideoInJsonOfLinkedVideo = JsonContent.linkedvideo;

            //匹配视频名称： 课程名_数字.mp4 或者 课程名.obb
            string regEpx = "^((" + coursNameInJson + "_)?\\d *\\.mp4)|(" + coursNameInJson + ".obb)";
            foreach (string videoName in CourseVideoInJsonOfLinkedVideo)
            {
                if ( !Regex.IsMatch(videoName, regEpx) )
                {
                    _WriteLogWithEnter("LinkedVideo中的视频名称与notetile不一致：" + videoName, 1);
                    result = false;
                }
            }

            foreach (CourseValidationTool_CSharp.Notecontent noteContent in JsonContent.notecontent )
            {
                if (noteContent.sectioncontent != null)
                {
                    foreach (CourseValidationTool_CSharp.Sectioncontent sectionContent in noteContent.sectioncontent )
                    {
                        foreach (string section in sectionContent.section)
                        {
                            indexStart = section.IndexOf("《");
                            if ( indexStart != -1)
                            {
                                indexEnd = section.IndexOf("》");
                                if (indexEnd != -1 )
                                {
                                    videoInJsonOfSection = section.Substring(indexStart + 1, indexEnd - indexStart-1) + ".mp4";
                                    if (CourseVideoInJsonOfLinkedVideo.IndexOf(videoInJsonOfSection) == -1)
                                    {
                                        _WriteLogWithEnter("Json sectioncontent中的视频在linkedvideo 中不存在\r\n  视频：" + videoInJsonOfSection, 1);
                                        result = false;
                                    }
                                    CourseVideoInJsonOfSecton.Add(videoInJsonOfSection);
                                }
                            }
                        }
                    }
                }
            }

            if ( CourseVideoInJsonOfSecton.Count != CourseVideoInFolder.Count )
            {
                _WriteLogWithEnter("Json sectioncontent 包含的视频个数和文件夹中不一致", 1 );
                result = false;
            }

            if (CourseVideoInJsonOfSecton.Count != CourseVideoInJsonOfLinkedVideo.Count - 1)
            {
                _WriteLogWithEnter("Json sectioncontent 包含的视频个数和 linkedvideo 中不一致", 1);
                result = false;
            }

            if (CourseVideoInJsonOfLinkedVideo.IndexOf(CourseName + ".obb") == -1)
            {
                _WriteLogWithEnter("《" + CourseName + ".obb 》在linkedvideo 中不存在", 1);
                result = false;
            }

            foreach ( string courseName in CourseVideoInFolder)
            {
                if(CourseVideoInJsonOfLinkedVideo.IndexOf(courseName) == -1 )
                {
                    _WriteLogWithEnter("文件夹中的视频《" + courseName + "》在Json文件linkedvideo中未找到 ", 1 );
                    result = false;
                }else if (CourseVideoInJsonOfSecton.IndexOf(courseName) == -1)
                {
                    _WriteLogWithEnter("文件夹中的视频《" + courseName + "》在Json文件sectioncontent中未找到 ", 1);
                    result = false;
                }
            }

            return result;
        }

        public int ProcessJsonFileFolder(string FileFolder, string enCoding)
        {
            int result = 0;
            System.DateTime currentTime = new System.DateTime();
            currentTime = System.DateTime.Now;
            string strY = currentTime.ToString("f");
            _WriteLogWithEnter(strY, 0);
            _WriteLogWithEnter("开始检查，文件路径：" + FileFolder, 0 );
            Boolean folderResult = ValidateFolderFiles(FileFolder);
            if ( true == folderResult)
            {
                _WriteLogWithEnter("文件夹检查成功", 0);
            }
            else
            {
                _WriteLogWithEnter("文件夹检查失败", 1);
                result = 1;
            }

            string fileContent = ReadJsonFile(FileFolder, enCoding);

            _WriteLogWithEnter("开始检查Json文件： ", 0);
            JsonResult deserializedProduct = JsonConvert.DeserializeObject<JsonResult>(fileContent);
            if( deserializedProduct == null)
            {
                _WriteLogWithEnter("Json文件解析失败，请先检查json文件格式是否正确", 1);
                return 1;
            }
            Boolean jsonResult = ValidateJsonContent(deserializedProduct);
            if (true == jsonResult)
            {
                _WriteLogWithEnter("Json文件检查成功", 0);
            }
            else
            {
                _WriteLogWithEnter("Json文件检查失败", 1);
                result = 1;
            }

            return result;
        }

        private void _WriteLogWithEnter(string log, int type ) // Type 0: Information, 1: Error
        {
            switch( type )
            {
                case 0:
                    swLog.Write(log + "\r\n"); 
                    break;
                case 1:
                    swLog.Write( "ERROR: " + log + "\r\n");
                    break;
                default:
                    swLog.Write(log + "\r\n");
                    break;
            }
        }
    }
}
