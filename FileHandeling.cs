using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows;
using System.Net.NetworkInformation;
using System.Windows.Shapes;
using System.Windows.Media;

namespace CToDo
{
    internal class FileHandeling
    {
        public void SaveUsername(string username)
        {
            FileStream fs = File.Create(@"tempUsername.txt");
            fs.Flush();
            fs.Close();

            FileInfo fileinfo = new FileInfo(@"tempUsername.txt");
            fileinfo.Attributes = FileAttributes.Temporary;

            StreamWriter streamWriter = File.AppendText(@"tempUsername.txt");
            streamWriter.WriteLine(username);
            streamWriter.Flush();
            streamWriter.Close();
        }
        public void CreateTempUsers()
        {
            FileStream fs = File.Create(@"tempUsers.txt");
            fs.Flush();
            fs.Close();

            FileInfo fileinfo = new FileInfo(@"tempUsers.txt");
            fileinfo.Attributes = FileAttributes.Temporary;
        }
        public void SaveUsers(int empID, string empUsername, string empName, string empSurname, int empStatus)
        {
            StreamWriter streamWriter = File.AppendText(@"tempUsers.txt");
            streamWriter.WriteLine("------------------");
            streamWriter.WriteLine("#{0}", empUsername);
            streamWriter.WriteLine("${0}", empID.ToString());
            streamWriter.WriteLine("%{0}", empName);
            streamWriter.WriteLine("&{0}", empSurname);
            streamWriter.WriteLine("*{0}", empStatus.ToString());
            streamWriter.WriteLine("__________________");
            streamWriter.Flush();
            streamWriter.Close();
        }
        public int EmpID(string nameAndSurname)
        {
            string tempID = "";

            StreamReader reader = File.OpenText(@"tempUsers.txt");
            try
            {
                while (!reader.EndOfStream)
                {
                    if (reader.ReadLine()[0] == '#')
                    {
                        tempID = reader.ReadLine();

                        if (nameAndSurname == reader.ReadLine().Trim('%', '\n', '\r') + " " + reader.ReadLine().Trim('&', '\n', '\r'))
                        {
                            break;
                        }
                    }
                }
            }
            catch { }
            reader.Close();
            return Convert.ToInt32(tempID.Trim('$'));
        }
        public string  EmpUsernameCheck(string nameAndSurname)
        {
            string tempUsername = "";

            StreamReader reader = File.OpenText(@"tempUsers.txt");
            try
            {
                while (!reader.EndOfStream)
                {
                    if (reader.ReadLine()[0] == '-')
                    {
                        tempUsername = reader.ReadLine();
                        reader.ReadLine();

                        if (nameAndSurname == reader.ReadLine().Trim('%', '\n', '\r') + " " + reader.ReadLine().Trim('&', '\n', '\r'))
                        {
                            break;
                        }
                    }
                }
            }
            catch { }
            reader.Close();
            return tempUsername.Trim('#');
        }
        public string RetrieveUsername()
        {
            StreamReader reader = File.OpenText(@"tempUsername.txt");
            string username = reader.ReadToEnd().Trim('\n', '\r');
            reader.Close();
            return username;
        }
        public void DeleteTempFileUsers()
        {
            if (File.Exists(@"tempUsers.txt"))
            {
                File.Delete(@"tempUsers.txt");
            }
        }
        public void DeleteTempFileUsername()
        {
            if (File.Exists(@"tempUsername.txt"))
            {
                File.Delete(@"tempUsername.txt");
            }
        }
        public int CheckEmpStatus()
        {
            StreamReader reader = File.OpenText(@"tempUsername.txt");
            string username = reader.ReadToEnd();
            username = username.Trim('\r', '\n');
            reader.Close();

            StreamReader readerUser = File.OpenText(@"tempUsers.txt");
            username = "#" + username;
            string empStatus = "";

            while (!readerUser.EndOfStream)
            {
                if(readerUser.ReadLine() == username)
                {
                    readerUser.ReadLine();
                    readerUser.ReadLine();
                    readerUser.ReadLine();
                    empStatus = readerUser.ReadLine().Trim('*');
                }
            }
            readerUser.Close();
            return Int32.Parse(empStatus);
        }
        public string ShowLoggedIn()
        { 
            string name = "";
            string surname = "";
            StreamReader reader = File.OpenText(@"tempUsername.txt");
            string username = reader.ReadToEnd();
            username = username.Trim('\r', '\n');
            username = "#" + username;
            reader.Close();

            StreamReader readerUser = File.OpenText(@"tempUsers.txt");

            while (!readerUser.EndOfStream)
            {
                if (readerUser.ReadLine() == username)
                {
                    readerUser.ReadLine();
                    name = readerUser.ReadLine().Trim('%');
                    surname = readerUser.ReadLine().Trim('&');
                }
            }
            readerUser.Close();
            return (name + " " + surname);
        }
        public List<string> RetrieveUserNames()
        {
            List<string> name = new List<string>();

            StreamReader readerUser = File.OpenText(@"tempUsers.txt");

            while (!readerUser.EndOfStream)
            {
                if (readerUser.ReadLine()[0] == '$')
                {
                    name.Add(readerUser.ReadLine().Trim('%'));
                }
            }
            readerUser.Close();
            return name;
        }
        public List<string> RetrieveUserSurnames()
        {
            List<string> surname = new List<string>();

            StreamReader readerUser = File.OpenText(@"tempUsers.txt");

            while (!readerUser.EndOfStream)
            {
                if (readerUser.ReadLine()[0] == '%')
                {
                    surname.Add(readerUser.ReadLine().Trim('&'));
                }
            }
            readerUser.Close();
            return surname;
        }
        public void ClearFileUsers()
        {
            File.WriteAllText(@"tempUsers.txt", string.Empty);
        }
        #region projectFile
        public void CreateProjectFile()
        {
            if (!File.Exists(@"projectWork.txt"))
            {
                FileStream fs = File.Create(@"projectWork.txt");
                fs.Flush();
                fs.Close();
            }
        }
        public void AddToProjectFile(string person, string title, string description, string date)
        {
            int i = CheckTotalProject();

            if (i == -1)
            {
                i = 0;
            }

            StreamWriter streamWriter = File.AppendText(@"projectWork.txt");
            streamWriter.WriteLine("{0}", i.ToString().Trim('\n', '\r'));
            streamWriter.WriteLine("------------------");
            streamWriter.WriteLine("#{0}", date);
            streamWriter.WriteLine("${0}", title);
            streamWriter.WriteLine("%{0}", description);
            streamWriter.WriteLine("&{0}", person);
            streamWriter.WriteLine("------------------");
            streamWriter.Flush();
            streamWriter.Close();
        }
        public DateTime DateTaskFinished(int i)
        {
            /*string d = "";
            string m = "";
            string y = "";

            string h = "";
            string min = "";
            string sec = "";

            DateTime date;*/

            string s = "";
            StreamReader reader = File.OpenText(@"projectWork.txt");
            while (!reader.EndOfStream)
            {
                if (reader.ReadLine() == i.ToString())
                {
                    for (int j = 0; j < 7; j++)
                    {
                        try
                        {
                            if (reader.ReadLine()[0] == '-')
                            {
                                s = reader.ReadLine();
                                if (s[0] == '#')
                                {
                                    s = s.Trim('#');
                                    break;
                                }
                            }
                        }
                        catch { }
                    }
                }
            }


            /*if (s[1] == '/')
            {
                if (s[3] == '/')
                {
                    m = ("0" + s[0].ToString());
                    d = ("0" + s[2].ToString());
                    y = (s[4].ToString() + s[5].ToString() + s[6].ToString() + s[7].ToString());

                    h = (s[9].ToString() + s[10].ToString());
                    min = (s[12].ToString() + s[13].ToString());
                    sec = (s[15].ToString() + s[16].ToString());
                }
                else
                {
                    m = ("0" + s[0].ToString());
                    d = (s[2].ToString() + s[3].ToString());
                    y = (s[5].ToString() + s[6].ToString() + s[7].ToString() + s[8].ToString());

                    h = (s[10].ToString() + s[11].ToString());
                    min = (s[13].ToString() + s[14].ToString());
                    sec = (s[16].ToString() + s[17].ToString());
                }
            }
            else if(s[2] == '/')
            {
                if (s[4] == '/')
                {
                    m = (s[0].ToString() + s[1].ToString());
                    d = ("0" + s[3].ToString());
                    y = (s[5].ToString() + s[6].ToString() + s[7].ToString() + s[8].ToString());

                    h = (s[10].ToString() + s[11].ToString());
                    min = (s[13].ToString() + s[14].ToString());
                    sec = (s[16].ToString() + s[17].ToString());
                }
                else
                {
                    m = (s[0].ToString() + s[1].ToString());
                    d = (s[3].ToString() + s[4].ToString());
                    y = (s[6].ToString() + s[7].ToString() + s[8].ToString() + s[9].ToString());

                    h = (s[11].ToString() + s[12].ToString());
                    min = (s[14].ToString() + s[15].ToString());
                    sec = (s[17].ToString() + s[18].ToString());
                }
            }

            date = new DateTime((Int32.Parse(y)), (Int32.Parse(m)), (Int32.Parse(d)), (Int32.Parse(h)), (Int32.Parse(min)), (Int32.Parse(sec)));*/

            reader.Close();
            return Convert.ToDateTime(s);
        }
        public SolidColorBrush Priority(int i)
        {
            FileHandeling fh = new FileHandeling();
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            int timeLeft = 0;
            try
            {
                timeLeft = Int32.Parse(fh.TimeLeft(i));
            }
            catch { timeLeft = 0; }
            if (timeLeft < 0)
            {
                solidColorBrush.Color = Color.FromRgb(0, 0, 0);
                return solidColorBrush;
            }
            else if (timeLeft >= 0 && timeLeft <= 5)
            {
                solidColorBrush.Color = Color.FromRgb(255, 0, 0);
                return solidColorBrush;
            }
            else if (timeLeft > 5 && timeLeft <= 10)
            {
                solidColorBrush.Color = Color.FromRgb(255, 255, 0);
                return solidColorBrush;
            }

            solidColorBrush.Color = Color.FromRgb(0, 255, 0);
            return solidColorBrush;

        }
        public string TimeLeft(int i)
        {
            DateTime date = DateTaskFinished(i);
            TimeSpan timeLeft = date - DateTime.Now;

            return (timeLeft.Days).ToString();

        }
        public string RetrieveProjectEmpName(int i)
        {
            string name = "";
            StreamReader reader = File.OpenText(@"projectWork.txt");
            while (!reader.EndOfStream)
            {
                if (reader.ReadLine() == i.ToString())
                {

                    for (int j = 0; j < 7; j++)
                    {
                        try
                        {
                            if (reader.ReadLine()[0] == '%')
                            {
                                name = reader.ReadLine().Trim('&');
                            }
                        }catch { }
                    }
                }
            }
            reader.Close();
            return name;
        }
        public string RetrieveProjectWorkTitle(int i)
        {
            string title = "";
            StreamReader reader = File.OpenText(@"projectWork.txt");
            while (!reader.EndOfStream)
            {
                if (reader.ReadLine() == i.ToString())
                {
                    for (int j = 0; j < 7; j++)
                    {
                        try
                        {
                            if (reader.ReadLine()[0] == '#')
                            {
                                title = reader.ReadLine().Trim('$');
                            }
                        }
                        catch { }
                    }
                }
            }
            reader.Close();
            return title;
        }
        public string RetrieveProjectDescription(int i)
        {
            string description = "";
            StreamReader reader = File.OpenText(@"projectWork.txt");
            while (!reader.EndOfStream)
            {
                if (reader.ReadLine() == i.ToString())
                {
                    for (int j = 0; j < 7; j++)
                    {
                        try
                        {
                            if (reader.ReadLine()[0] == '$')
                            {
                                description = reader.ReadLine().Trim('%');
                            }
                        }
                        catch { }
                    }
                }
            }
            reader.Close();
            return description;
        }
        public int CheckTotalProject()
        {
            StreamReader reader = File.OpenText(@"projectWork.txt");
            int i = 0;
            int j = 0;
            while (!reader.EndOfStream)
            {
                try
                {
                    if (reader.ReadLine()[0] == '-')
                    {
                        j += 1;
                        if (j == 2)
                        {
                            i++;
                            j = 0;
                        }
                    }
                } catch { }
            }
            reader.Close();
            return i;
        }
        public List<int> projectOrder()
        {
            int i = CheckTotalProject();

            List<int> projectNums = new List<int>();
            List<DateTime> date = new List<DateTime>();
            DateTime tempDate;
            int tempInt;

            for (int j = 0; j < i; j++)
            {
                date.Add(DateTaskFinished(j));
                projectNums.Add(j);
            }

            for (int bubJ = 0; bubJ <= i - 2; bubJ++)
            {
                for (int bubI = 0; bubI <= i - 2; bubI++)
                {
                    if (date[bubI] > date[bubI + 1])
                    {
                        tempDate = date[bubI + 1];
                        date[bubI + 1] = date[bubI];
                        date[bubI] = tempDate;

                        tempInt = projectNums[bubI + 1];
                        projectNums[bubI + 1] = projectNums[bubI];
                        projectNums[bubI] = tempInt;
                    }
                }
            }

            return projectNums;
        }
        #endregion

        #region projectCompletedWorkFile
        public void CompleteWork_FinishedWork(int i)
        {
            string oDate = "";
            string fDate = "";
            string title = "" ;
            string description = "";
            string emp = "";

            StreamReader reader = File.OpenText(@"projectWork.txt");
            while (!reader.EndOfStream)
            {
                if (reader.ReadLine() == i.ToString())
                {
                    reader.ReadLine();
                    oDate = reader.ReadLine();
                    fDate = DateTime.Now.ToString();
                    title = reader.ReadLine();
                    description = reader.ReadLine();
                    emp = reader.ReadLine();
                }
            }
            reader.Close();

            int workID = CheckTotalProject_FinishedWork();

            StreamWriter writer = File.AppendText(@"projectFinishedWork.txt");
            writer.WriteLine("{0}", workID.ToString());
            writer.WriteLine("------------------");
            writer.WriteLine("{0}", oDate);
            writer.WriteLine("^{0}", fDate);
            writer.WriteLine("{0}", title);
            writer.WriteLine("{0}", description);
            writer.WriteLine("{0}", emp);
            writer.WriteLine("------------------");

            writer.Close();

            DeleteWork(i);
        }
        public void DeleteWork(int i)
        {
            FileStream fs = File.Create(@"tempmove.txt");
            fs.Flush();
            fs.Close();

            StreamWriter writer = File.AppendText(@"tempmove.txt");
            StreamReader reader = File.OpenText(@"projectWork.txt");

            int j = 0;
            while (!reader.EndOfStream)
            {
                string readline = reader.ReadLine();
                if (readline == i.ToString())
                {
                    reader.ReadLine();
                    reader.ReadLine();
                    reader.ReadLine();
                    reader.ReadLine();
                    reader.ReadLine();
                    readline = reader.ReadLine();
                }
                else
                {
                    if (readline[0] != '-' && readline[0] != '#' && readline[0] != '$' && readline[0] != '%' && readline[0] != '&')
                    {
                        writer.WriteLine(j.ToString());
                        j++;
                    }
                    else
                    {
                        writer.WriteLine(readline);
                    }
                }
            }

            writer.Close();
            reader.Close();

            File.Delete(@"projectWork.txt");

            CreateProjectFile();

            StreamWriter writer2 = File.AppendText(@"projectWork.txt");
            StreamReader reader2 = File.OpenText(@"tempmove.txt");

            while (!reader2.EndOfStream)
            {
                writer2.WriteLine(reader2.ReadLine());
            }

            writer2.Close();
            reader2.Close();

            File.Delete(@"tempmove.txt");
        }
        public void CreateProjectFile_FinishedWork()
        {
            if (!File.Exists(@"projectFinishedWork.txt"))
            {
                FileStream fs = File.Create(@"projectFinishedWork.txt");
                fs.Flush();
                fs.Close();
            }
        }
        public DateTime DateMeantToBeFinished_FinishedWork(int i)
        {
            {
                /*string d = "";
                string m = "";
                string y = "";

                string h = "";
                string min = "";
                string sec = "";
                
                DateTime date;*/

                string s = "";
                StreamReader reader = File.OpenText(@"projectFinishedWork.txt");
                while (!reader.EndOfStream)
                {
                    if (reader.ReadLine() == i.ToString())
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            try
                            {
                                if (reader.ReadLine()[0] == '#')
                                {
                                    s = reader.ReadLine();
                                    if (s[0] == '^')
                                    {
                                        s = s.Trim('^');
                                        break;
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }


                /*if (s[1] == '/')
                {
                    if (s[3] == '/')
                    {
                        m = ("0" + s[0].ToString());
                        d = ("0" + s[2].ToString());
                        y = (s[4].ToString() + s[5].ToString() + s[6].ToString() + s[7].ToString());

                        h = (s[9].ToString() + s[10].ToString());
                        min = (s[12].ToString() + s[13].ToString());
                        sec = (s[15].ToString() + s[16].ToString());
                    }
                    else
                    {
                        m = ("0" + s[0].ToString());
                        d = (s[2].ToString() + s[3].ToString());
                        y = (s[5].ToString() + s[6].ToString() + s[7].ToString() + s[8].ToString());

                        h = (s[10].ToString() + s[11].ToString());
                        min = (s[13].ToString() + s[14].ToString());
                        sec = (s[16].ToString() + s[17].ToString());
                    }
                }
                else if (s[2] == '/')
                {
                    if (s[4] == '/')
                    {
                        m = (s[0].ToString() + s[1].ToString());
                        d = ("0" + s[3].ToString());
                        y = (s[5].ToString() + s[6].ToString() + s[7].ToString() + s[8].ToString());

                        h = (s[10].ToString() + s[11].ToString());
                        min = (s[13].ToString() + s[14].ToString());
                        sec = (s[16].ToString() + s[17].ToString());
                    }
                    else
                    {
                        m = (s[0].ToString() + s[1].ToString());
                        d = (s[3].ToString() + s[4].ToString());
                        y = (s[6].ToString() + s[7].ToString() + s[8].ToString() + s[9].ToString());

                        h = (s[11].ToString() + s[12].ToString());
                        min = (s[14].ToString() + s[15].ToString());
                        sec = (s[17].ToString() + s[18].ToString());
                    }
                }

                date = new DateTime((Int32.Parse(y)), (Int32.Parse(m)), (Int32.Parse(d)), (Int32.Parse(h)), (Int32.Parse(min)), (Int32.Parse(sec)));*/

                reader.Close();
                return Convert.ToDateTime(s);
            }
        }
        public DateTime DateTaskFinished_FinishedWork(int i)
        {
            /*string d = "";
            string m = "";
            string y = "";

            string h = "";
            string min = "";
            string sec = "";
            
            DateTime date;*/

            string s = "";
            StreamReader reader = File.OpenText(@"projectFinishedWork.txt");
            while (!reader.EndOfStream)
            {
                if (reader.ReadLine() == i.ToString())
                {
                    for (int j = 0; j < 7; j++)
                    {
                        try
                        {
                            if (reader.ReadLine()[0] == '-')
                            {
                                s = reader.ReadLine();
                                if (s[0] == '#')
                                {
                                    s = s.Trim('#');
                                    break;
                                }
                            }
                        }
                        catch { }
                    }
                }
            }

            /*if (s[1] == '/') //111
            {
                if (s[3] == '/') //11
                {
                    m = ("0" + s[0].ToString());
                    d = ("0" + s[2].ToString());
                    y = (s[4].ToString() + s[5].ToString() + s[6].ToString() + s[7].ToString());

                    if (s[10] == ':') //1
                    {
                        h = ("0" + s[9].ToString());
                        min = (s[11].ToString() + s[12].ToString());
                        sec = (s[14].ToString() + s[15].ToString());
                    }
                    else //2
                    {
                        h = (s[9].ToString() + s[10].ToString());
                        min = (s[12].ToString() + s[13].ToString());
                        sec = (s[15].ToString() + s[16].ToString());
                    }
                }
                else //22
                {
                    m = ("0" + s[0].ToString());
                    d = (s[2].ToString() + s[3].ToString());
                    y = (s[5].ToString() + s[6].ToString() + s[7].ToString() + s[8].ToString());

                    if (s[11] == ':') //3
                    {
                        h = ("0" + s[10].ToString());
                        min = (s[12].ToString() + s[13].ToString());
                        sec = (s[15].ToString() + s[16].ToString());
                    }
                    else //4
                    {
                        h = (s[10].ToString() + s[11].ToString());
                        min = (s[13].ToString() + s[14].ToString());
                        sec = (s[16].ToString() + s[17].ToString());
                    }
                }
            }
            else //222
            {
                if (s[4] == '/')
                {
                    m = (s[0].ToString() + s[1].ToString());
                    d = ("0" + s[3].ToString());
                    y = (s[5].ToString() + s[6].ToString() + s[7].ToString() + s[8].ToString());

                    if (s[11] == ':') //5
                    {
                        h = ("0" + s[10].ToString());
                        min = (s[12].ToString() + s[13].ToString());
                        sec = (s[15].ToString() + s[16].ToString());
                    }
                    else //6
                    {
                        h = (s[10].ToString() + s[11].ToString());
                        min = (s[13].ToString() + s[14].ToString());
                        sec = (s[16].ToString() + s[17].ToString());
                    }
                }
                else
                {
                    m = (s[0].ToString() + s[1].ToString());
                    d = (s[3].ToString() + s[4].ToString());
                    y = (s[6].ToString() + s[7].ToString() + s[8].ToString() + s[9].ToString());

                    if (s[12] == ':') //7
                    {
                        h = ("0" + s[11].ToString());
                        min = (s[12].ToString() + s[13].ToString());
                        sec = (s[15].ToString() + s[16].ToString());
                    }
                    else // 8
                    {
                        h = (s[11].ToString() + s[12].ToString());
                        min = (s[14].ToString() + s[15].ToString());
                        sec = (s[17].ToString() + s[18].ToString());
                    }
                }
            }

            date = new DateTime((Int32.Parse(y)), (Int32.Parse(m)), (Int32.Parse(d)), (Int32.Parse(h)), (Int32.Parse(min)), (Int32.Parse(sec)));*/

            
            reader.Close();
            return Convert.ToDateTime(s);
        }
        public SolidColorBrush Priority_FinishedWork(int i)
        {
            FileHandeling fh = new FileHandeling();
            SolidColorBrush solidColorBrush = new SolidColorBrush();
            int timeLeft = 0;
            try
            {
                timeLeft = Int32.Parse(fh.DateDif_FinsihedWork(i));
            }
            catch { timeLeft = 0; }
            if (timeLeft < 0)
            {
                solidColorBrush.Color = Color.FromRgb(255, 0, 0);
                return solidColorBrush;
            }
            else if (timeLeft == 0)
            {

                solidColorBrush.Color = Color.FromRgb(255, 255, 0);
                return solidColorBrush;
            }

            solidColorBrush.Color = Color.FromRgb(0, 255, 0);
            return solidColorBrush;

        }
        public string DateDif_FinsihedWork(int i)
        {
            TimeSpan timeLeft = DateMeantToBeFinished_FinishedWork(i) - DateTaskFinished_FinishedWork(i);

            return timeLeft.Days.ToString();
        }
        public string TimeLeft_FinishedWork(int i)
        {
            DateTime date = DateTaskFinished_FinishedWork(i);
            TimeSpan timeLeft = DateTime.Now - date;

            return timeLeft.Days.ToString();

        }
        public string RetrieveProjectEmpName_FinishedWork(int i)
        {
            string name = "";
            StreamReader reader = File.OpenText(@"projectFinishedWork.txt");
            while (!reader.EndOfStream)
            {
                if (reader.ReadLine() == i.ToString())
                {
                    for (int j = 0; j < 7; j++)
                    {
                        try
                        {
                            if (reader.ReadLine()[0] == '%')
                            {
                                name = reader.ReadLine().Trim('&');
                            }
                        }
                        catch { }
                    }
                }
            }
            reader.Close();
            return name;
        }
        public string RetrieveProjectWorkTitle_FinishedWork(int i)
        {
            string title = "";
            StreamReader reader = File.OpenText(@"projectFinishedWork.txt");
            while (!reader.EndOfStream)
            {
                if (reader.ReadLine() == i.ToString())
                {
                    for (int j = 0; j < 7; j++)
                    {
                        try
                        {
                            if (reader.ReadLine()[0] == '^')
                            {
                                title = reader.ReadLine().Trim('$');
                            }
                        }
                        catch { }
                    }
                }
            }
            reader.Close();
            return title;
        }
        public int CheckTotalProject_FinishedWork()
        {
            StreamReader reader = File.OpenText(@"projectFinishedWork.txt");
            int i = 0;
            int j = 0;
            while (!reader.EndOfStream)
            {
                try
                {
                    if (reader.ReadLine()[0] == '-')
                    {
                        j += 1;
                        if (j == 2)
                        {
                            i++;
                            j = 0;
                        }
                    }
                }
                catch { }
            }
            reader.Close();
            return i;
        }
        public List<int> projectOrder_FinishedWork()
        {
            int i = CheckTotalProject_FinishedWork();

            List<int> projectNums = new List<int>();
            List<DateTime> date = new List<DateTime>();
            DateTime tempDate;
            int tempInt;

            for (int j = 0; j < i; j++)
            {
                date.Add(DateTaskFinished_FinishedWork(j));
                projectNums.Add(j);
            }

            for (int bubJ = 0; bubJ <= i - 2; bubJ++)
            {
                for (int bubI = 0; bubI <= i - 2; bubI++)
                {
                    if (date[bubI] > date[bubI + 1])
                    {
                        tempDate = date[bubI + 1];
                        date[bubI + 1] = date[bubI];
                        date[bubI] = tempDate;

                        tempInt = projectNums[bubI + 1];
                        projectNums[bubI + 1] = projectNums[bubI];
                        projectNums[bubI] = tempInt;
                    }
                }
            }

            return projectNums;
        }
        #endregion
        public void NewProject()
        {
            if (File.Exists(@"projectWork.txt"))
            {
                File.Delete(@"projectWork.txt");
            }
            if (File.Exists(@"projectFinishedWork.txt"))
            {
                File.Delete(@"projectFinishedWork.txt");
            }

            FileStream fs = File.Create(@"projectWork.txt");
            fs.Flush();
            fs.Close();

            FileStream fs2 = File.Create(@"projectFinishedWork.txt");
            fs2.Flush();
            fs2.Close();
        }
    }
}
