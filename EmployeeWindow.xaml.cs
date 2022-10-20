using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CToDo
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            InitializeComponent();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)     //Removes the temp files on program exit
        {
            FileHandeling fh = new FileHandeling();
            //initiates the deletion process
            fh.DeleteTempFileUsername();
            fh.DeleteTempFileUsers();
        }
        private void UpdateWindow()
        {
            FileHandeling fh = new FileHandeling();
            txtUser.Text = fh.ShowLoggedIn();
            //returns to lists containing the users names and suranems respectavely
            List<string> names = new List<string>(fh.RetrieveUserNames());
            List<string> surnames = new List<string>(fh.RetrieveUserSurnames());
            SolidColorBrush mySolidColorBrush;

            try
            {
                pnlShowUsers.Children.Clear();
                //Creates a new TextBlock that has the name and surname of the users as it's Text

                mySolidColorBrush = new SolidColorBrush();
                mySolidColorBrush.Color = Color.FromArgb(206, 206, 206, 206);

                TextBlock txtTempUsers = new TextBlock();
                txtTempUsers.Text = "Users";
                txtTempUsers.MinWidth = 50;
                txtTempUsers.MinHeight = 20;
                txtTempUsers.FontSize = 20;
                txtTempUsers.TextDecorations = TextDecorations.Underline;
                txtTempUsers.HorizontalAlignment = HorizontalAlignment.Center;
                txtTempUsers.Foreground = mySolidColorBrush;
                pnlShowUsers.Children.Add(txtTempUsers);

                int i = 0;
                while (true)
                {
                    txtTempUsers = new TextBlock();
                    string user = " " + names[i] + " " + surnames[i];

                    if (user.Length >= 19)
                    {
                        for (int j = 0; j < 18; j++)
                        {
                            txtTempUsers.Text += user[j];
                        }
                        txtTempUsers.Text += "...";
                    }
                    else
                    {
                        txtTempUsers.Text = user;
                    }
                    txtTempUsers.MinWidth = 50;
                    txtTempUsers.MinHeight = 20;
                    txtTempUsers.FontSize = 20;
                    txtTempUsers.Foreground = mySolidColorBrush;
                    pnlShowUsers.Children.Add(txtTempUsers);
                    i++;
                }
            }
            catch { }
        }
        private void Window_Activated(object sender, EventArgs e)
        {

            //Catches an exception when the lists run out of items and ends the loop
            UpdateWindow();
            CreateWorkMenus();
            CreateWorkMenus_FinishedWork();

        }        // Fills out the Panel that displays all the users with the names and surnames of the users
        #region Panel population
        private void CreateWorkMenus_FinishedWork()
        {
            myCanvascomplete.Children.Clear();
            myCanvascomplete.Width = 0;
            myCanvascomplete.Width = window.ActualWidth - 446;
            pnlWorkComplete.Height = pnlWork.Height;

            FileHandeling fh = new FileHandeling();
            SolidColorBrush colTransparent = new SolidColorBrush();
            colTransparent.Color = Color.FromArgb(0, 0, 0, 0);

            int paneldistance = 10;
            int paneldistance2 = 10;
            int paneldistance3 = 10;
            int z = 0;
            int canvasWidth = 215;

            int lim = fh.CheckTotalProject_FinishedWork();


            if (lim > 0)
            {
                List<int> projectNum = fh.projectOrder_FinishedWork();
                for (int i = 0; i < projectNum.Count(); i++)
                {
                    Style style = this.FindResource("backgroundsTextblocks") as Style;
                    myCanvascomplete.Width += canvasWidth;

                    TextBlock textblockNameTitle = new TextBlock()
                    {
                        FontSize = 15,
                        Text = "Employee responsible",
                        Margin = new Thickness(5, 5, 0, 0),
                        Style = style,
                    };
                    TextBlock textblockName = new TextBlock()
                    {
                        FontSize = 13,
                        Text = fh.RetrieveProjectEmpName_FinishedWork(projectNum[i]),
                        Margin = new Thickness(10, 0, 0, 5),
                        Style = style,
                    };

                    TextBlock textblockTaskTitle = new TextBlock()
                    {
                        FontSize = 15,
                        Text = "Task",
                        Margin = new Thickness(5, 0, 0, 0),
                        Style = style,
                    };
                    string title = fh.RetrieveProjectWorkTitle_FinishedWork(projectNum[i]);
                    string s = "";
                    if (title.Length > 25)
                    {
                        for (int j = 0; j < 25; j++)
                        {
                            s += title[j];
                        }
                        s += "...";
                    }
                    else
                    {
                        s = title;
                    }
                    TextBlock textblockTask = new TextBlock()
                    {
                        FontSize = 13,
                        Text = s,
                        Margin = new Thickness(10, 0, 0, 5),
                        Style = style,
                    };

                    TextBlock textblockTimeTitle = new TextBlock()
                    {
                        FontSize = 15,
                        Text = "Timeframe",
                        Margin = new Thickness(5, 0, 0, 0),
                        Style = style,
                    };
                    TextBlock textblockOriginalDate = new TextBlock()
                    {
                        Text = "Date required " + fh.DateMeantToBeFinished_FinishedWork(projectNum[i]).Day.ToString() + "/" + fh.DateMeantToBeFinished_FinishedWork(projectNum[i]).Month.ToString() + "/" + fh.DateMeantToBeFinished_FinishedWork(projectNum[i]).Year.ToString(),
                        FontSize = 13,
                        Margin = new Thickness(10, 0, 0, 5),
                        Style = style,
                    };
                    TextBlock textblockNewDate = new TextBlock()
                    {
                        Text = "Date Finished " + fh.DateTaskFinished_FinishedWork(projectNum[i]).Day.ToString() + "/" + fh.DateTaskFinished_FinishedWork(projectNum[i]).Month.ToString() + "/" + fh.DateTaskFinished_FinishedWork(projectNum[i]).Year.ToString(),
                        FontSize = 13,
                        Margin = new Thickness(10, 0, 0, 5),
                        Style = style,
                    };
                    TextBlock textblockTime = new TextBlock()
                    {
                        FontSize = 13,
                        Margin = new Thickness(10, 0, 0, 5),
                        Style = style,
                    };
                    switch (fh.TimeLeft_FinishedWork(projectNum[i]))
                    {
                        case "0":
                            textblockTime.Text = "Finished today";
                            break;
                        case "-1":
                            textblockTime.Text = "Finished yesterday";
                            break;
                        default:
                            textblockTime.Text = "Finished " + fh.TimeLeft_FinishedWork(projectNum[i]).Trim('-') + " days ago";
                            break;
                    }
                    TextBlock textblockSeeWorkTitle = new TextBlock()
                    {
                        FontSize = 15,
                        Text = "See more details",
                        Margin = new Thickness(5, 0, 0, 0),
                        Style = style,
                    };
                    style = this.FindResource("frontPanelButtons") as Style;
                    Button buttonSeeWork = new Button()
                    {
                        Height = 20,
                        Width = 50,
                        Content = "Details",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(10, 0, 0, 5),
                        Name = String.Format("btn{0}", projectNum[i]),
                        Style = style,
                    };
                    Line line = new Line()
                    {
                        X1 = 0,
                        Y1 = 5,
                        X2 = 200,
                        Y2 = 5,
                        Stroke = fh.Priority_FinishedWork(projectNum[i]),
                        StrokeThickness = 10,
                    };
                    buttonSeeWork.Click += new RoutedEventHandler(SeeWork);
                    StackPanel buttonPanel = new StackPanel()
                    {
                        Width = 200,
                        Height = 30,
                        Background = colTransparent,
                        Orientation = Orientation.Horizontal,
                    };


                    style = this.FindResource("frontPanel") as Style;
                    switch (myCanvascomplete.Height)
                    {
                        case 300:

                            StackPanel panel = new StackPanel()
                            {
                                Width = 200,
                                Height = 230,
                                Margin = new Thickness(paneldistance, 10, 0, 0),
                                Style = style,
                            };
                            myCanvascomplete.Children.Add(panel);

                            panel.Children.Add(textblockNameTitle);
                            panel.Children.Add(textblockName);
                            panel.Children.Add(textblockTaskTitle);
                            panel.Children.Add(textblockTask);
                            panel.Children.Add(textblockTimeTitle);
                            panel.Children.Add(textblockTime);
                            panel.Children.Add(textblockOriginalDate);
                            panel.Children.Add(textblockNewDate);
                            panel.Children.Add(textblockSeeWorkTitle);
                            panel.Children.Add(buttonSeeWork);
                            panel.Children.Add(line);

                            paneldistance += 210;

                            myCanvascomplete.Width += 210;
                            pnlWorkComplete.Width += 210;

                            break;
                        case 600:
                            if (z == 0)
                            {
                                panel = new StackPanel()
                                {
                                    Width = 200,
                                    Height = 230,
                                    Margin = new Thickness(paneldistance, 10, 0, 0),
                                    Style = style,
                                };
                                myCanvascomplete.Children.Add(panel);

                                panel.Children.Add(textblockNameTitle);
                                panel.Children.Add(textblockName);
                                panel.Children.Add(textblockTaskTitle);
                                panel.Children.Add(textblockTask);
                                panel.Children.Add(textblockTimeTitle);
                                panel.Children.Add(textblockTime);
                                panel.Children.Add(textblockOriginalDate);
                                panel.Children.Add(textblockNewDate);
                                panel.Children.Add(textblockSeeWorkTitle);
                                panel.Children.Add(buttonSeeWork);
                                panel.Children.Add(line);

                                paneldistance += 210;
                                z += 1;
                            }
                            else if (z == 1)
                            {
                                panel = new StackPanel()
                                {
                                    Width = 200,
                                    Height = 230,
                                    Margin = new Thickness(paneldistance2, 260, 0, 0),
                                    Style = style,
                                };
                                myCanvascomplete.Children.Add(panel);

                                panel.Children.Add(textblockNameTitle);
                                panel.Children.Add(textblockName);
                                panel.Children.Add(textblockTaskTitle);
                                panel.Children.Add(textblockTask);
                                panel.Children.Add(textblockTimeTitle);
                                panel.Children.Add(textblockTime);
                                panel.Children.Add(textblockOriginalDate);
                                panel.Children.Add(textblockNewDate);
                                panel.Children.Add(textblockSeeWorkTitle);
                                panel.Children.Add(buttonSeeWork);
                                panel.Children.Add(line);

                                paneldistance2 += 210;
                                z = 0;

                                myCanvascomplete.Width += 210;
                                pnlWorkComplete.Width += 210;
                            }

                            break;
                        case 900:
                            if (z == 0)
                            {
                                panel = new StackPanel()
                                {
                                    Width = 200,
                                    Height = 230,
                                    Margin = new Thickness(paneldistance, 10, 0, 0),
                                    Style = style,
                                };
                                myCanvascomplete.Children.Add(panel);

                                panel.Children.Add(textblockNameTitle);
                                panel.Children.Add(textblockName);
                                panel.Children.Add(textblockTaskTitle);
                                panel.Children.Add(textblockTask);
                                panel.Children.Add(textblockTimeTitle);
                                panel.Children.Add(textblockTime);
                                panel.Children.Add(textblockOriginalDate);
                                panel.Children.Add(textblockNewDate);
                                panel.Children.Add(textblockSeeWorkTitle);
                                panel.Children.Add(buttonSeeWork);
                                panel.Children.Add(line);

                                paneldistance += 210;
                                z += 1;
                            }
                            else if (z == 1)
                            {
                                panel = new StackPanel()
                                {
                                    Width = 200,
                                    Height = 230,
                                    Margin = new Thickness(paneldistance2, 260, 0, 0),
                                    Style = style,
                                };
                                myCanvascomplete.Children.Add(panel);

                                panel.Children.Add(textblockNameTitle);
                                panel.Children.Add(textblockName);
                                panel.Children.Add(textblockTaskTitle);
                                panel.Children.Add(textblockTask);
                                panel.Children.Add(textblockTimeTitle);
                                panel.Children.Add(textblockTime);
                                panel.Children.Add(textblockOriginalDate);
                                panel.Children.Add(textblockNewDate);
                                panel.Children.Add(textblockSeeWorkTitle);
                                panel.Children.Add(buttonSeeWork);
                                panel.Children.Add(line);

                                paneldistance2 += 210;
                                z += 1;
                            }
                            else if (z == 2)
                            {
                                panel = new StackPanel()
                                {
                                    Width = 200,
                                    Height = 230,
                                    Margin = new Thickness(paneldistance3, 520, 0, 0),
                                    Style = style,
                                };
                                myCanvascomplete.Children.Add(panel);

                                panel.Children.Add(textblockNameTitle);
                                panel.Children.Add(textblockName);
                                panel.Children.Add(textblockTaskTitle);
                                panel.Children.Add(textblockTask);
                                panel.Children.Add(textblockTimeTitle);
                                panel.Children.Add(textblockTime);
                                panel.Children.Add(textblockOriginalDate);
                                panel.Children.Add(textblockNewDate);
                                panel.Children.Add(textblockSeeWorkTitle);
                                panel.Children.Add(buttonSeeWork);
                                panel.Children.Add(line);

                                paneldistance3 += 210;
                                z = 0;

                                myCanvascomplete.Width += 210;
                                pnlWorkComplete.Width += 210;
                            }
                            break;
                    }
                    if (z != 0)
                    {
                        myCanvascomplete.Width += 215;
                        pnlWorkComplete.Width += 225;
                    }
                }
                pnlWorkComplete.Width += 10;
            }

            
        }
        private void CreateWorkMenus()
        {
            myCanvas.Children.Clear();
            myCanvas.Width = 0;
            myCanvas.Width = window.ActualWidth - 446;
            cldShowDate.SelectedDates.Clear();

            FileHandeling fh = new FileHandeling();
            SolidColorBrush colTransparent = new SolidColorBrush();
            colTransparent.Color = Color.FromArgb(0, 0, 0, 0);

            int paneldistance = 10;
            int paneldistance2 = 10;
            int paneldistance3 = 10;
            int canvasWidth = 210;
            int z = 0;
            int y = 0;

            int lim = fh.CheckTotalProject();


            if (lim > 0)
            {
                List<int> projectNum = fh.projectOrder();
                for (int i = 0; i < projectNum.Count(); i++)
                {
                    Style style = this.FindResource("backgroundsTextblocks") as Style;

                    TextBlock textblockNameTitle = new TextBlock()
                    {
                        FontSize = 15,
                        Text = "Employee responsible",
                        Margin = new Thickness(5, 5, 0, 0),
                        Style = style,
                    };
                    TextBlock textblockName = new TextBlock()
                    {
                        FontSize = 13,
                        Text = fh.RetrieveProjectEmpName(projectNum[i]),
                        Margin = new Thickness(10, 0, 0, 5),
                        Style = style,
                    };

                    TextBlock textblockTaskTitle = new TextBlock()
                    {
                        FontSize = 15,
                        Text = "Task",
                        Margin = new Thickness(5, 0, 0, 0),
                        Style = style,
                    };
                    string title = fh.RetrieveProjectWorkTitle(projectNum[i]);
                    string s = "";
                    if (title.Length > 25)
                    {
                        for (int j = 0; j < 25; j++)
                        {
                            s += title[j];
                        }
                        s += "...";
                    }
                    else
                    {
                        s = title;
                    }
                    TextBlock textblockTask = new TextBlock()
                    {
                        FontSize = 13,
                        Text = s,
                        Margin = new Thickness(10, 0, 0, 5),
                        Style = style,
                    };

                    TextBlock textblockTimeTitle = new TextBlock()
                    {
                        FontSize = 15,
                        Text = "Time left",
                        Margin = new Thickness(5, 0, 0, 0),
                        Style = style,
                    };
                    TextBlock textblockTime = new TextBlock()
                    {
                        FontSize = 13,
                        Margin = new Thickness(10, 0, 0, 5),
                        Style = style,
                    };
                    cldShowDate.SelectedDates.Add(fh.DateTaskFinished(projectNum[i]));
                    switch (fh.TimeLeft(projectNum[i]))
                    {
                        case "0":
                            textblockTime.Text = "Required today!";
                            break;
                        case "1":
                            textblockTime.Text = "Required tomorrow!";
                            break;
                        default:
                            textblockTime.Text = fh.TimeLeft(projectNum[i]) + " days left";
                            break;
                    }
                    TextBlock textblockSeeWorkTitle = new TextBlock()
                    {
                        FontSize = 15,
                        Text = "See more details",
                        Margin = new Thickness(5, 0, 0, 0),
                        Style = style,
                    };

                    Style stylebtn = this.FindResource("frontPanelButtons") as Style;
                    Button buttonSeeWork = new Button()
                    {
                        Height = 20,
                        Width = 50,
                        Content = "Details",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(10, 0, 0, 5),
                        Name = String.Format("btn{0}", projectNum[i]),
                        Style = stylebtn,
                    };
                    buttonSeeWork.Click += new RoutedEventHandler(SeeWork);

                    StackPanel buttonPanel = new StackPanel()
                    {
                        Width = 200,
                        Height = 30,
                        Background = colTransparent,
                        Orientation = Orientation.Horizontal,
                    };
                    Line line = new Line()
                    {
                        X1 = 0,
                        Y1 = 5,
                        X2 = 200,
                        Y2 = 5,
                        Stroke = fh.Priority(projectNum[i]),
                        StrokeThickness = 10,
                    };


                    style = this.FindResource("frontPanel") as Style;
                    switch (myCanvas.ActualHeight)
                    {
                        case 300:
                            y += 1;

                            StackPanel panel = new StackPanel()
                            {
                                Width = 200,
                                Height = 215,
                                Margin = new Thickness(paneldistance, 10, 0, 0),
                                VerticalAlignment = VerticalAlignment.Center,
                                Style = style,
                            };
                            myCanvas.Children.Add(panel);

                            panel.Children.Add(textblockNameTitle);
                            panel.Children.Add(textblockName);
                            panel.Children.Add(textblockTaskTitle);
                            panel.Children.Add(textblockTask);
                            panel.Children.Add(textblockTimeTitle);
                            panel.Children.Add(textblockTime);
                            panel.Children.Add(textblockSeeWorkTitle);
                            panel.Children.Add(buttonSeeWork);
                            panel.Children.Add(buttonPanel);

                            paneldistance += 210;

                            myCanvas.Width += 210;
                            pnlWork.Width += 210;

                            if (txtUser.Text == fh.RetrieveProjectEmpName(projectNum[i]))
                            {
                                Button buttonFinish = new Button()
                                {
                                    Height = 20,
                                    Width = 50,
                                    Content = "Finish",
                                    HorizontalAlignment = HorizontalAlignment.Left,
                                    VerticalAlignment = VerticalAlignment.Bottom,
                                    Tag = i,
                                    Name = String.Format("btn{0}", projectNum[i]),
                                    Margin = new Thickness(5, 0, 45, 5),
                                    Style = stylebtn,
                                };
                                buttonFinish.Click += new RoutedEventHandler(FinishWork);
                                buttonPanel.Children.Add(buttonFinish);
                            }
                            panel.Children.Add(line);
                            break;
                        case 600:
                            if (z == 0)
                            {
                                panel = new StackPanel()
                                {
                                    Width = 200,
                                    Height = 215,
                                    Margin = new Thickness(paneldistance, 10, 0, 0),
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Style = style,
                                };
                                myCanvas.Children.Add(panel);

                                panel.Children.Add(textblockNameTitle);
                                panel.Children.Add(textblockName);
                                panel.Children.Add(textblockTaskTitle);
                                panel.Children.Add(textblockTask);
                                panel.Children.Add(textblockTimeTitle);
                                panel.Children.Add(textblockTime);
                                panel.Children.Add(textblockSeeWorkTitle);
                                panel.Children.Add(buttonSeeWork);
                                panel.Children.Add(buttonPanel);

                                paneldistance += 210;
                                z += 1;
                                if (txtUser.Text == fh.RetrieveProjectEmpName(projectNum[i]))
                                {
                                    Button buttonFinish = new Button()
                                    {
                                        Height = 20,
                                        Width = 50,
                                        Content = "Finish",
                                        HorizontalAlignment = HorizontalAlignment.Left,
                                        VerticalAlignment = VerticalAlignment.Bottom,
                                        Tag = i,
                                        Name = String.Format("btn{0}", projectNum[i]),
                                        Margin = new Thickness(5, 0, 45, 5),
                                        Style = stylebtn,
                                    };
                                    buttonFinish.Click += new RoutedEventHandler(FinishWork);
                                    buttonPanel.Children.Add(buttonFinish);
                                }
                                panel.Children.Add(line);
                            }
                            else if (z == 1)
                            {
                                y += 1;
                                panel = new StackPanel()
                                {
                                    Width = 200,
                                    Height = 215,
                                    Margin = new Thickness(paneldistance2, 260, 0, 0),
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Style = style,
                                };
                                myCanvas.Children.Add(panel);

                                panel.Children.Add(textblockNameTitle);
                                panel.Children.Add(textblockName);
                                panel.Children.Add(textblockTaskTitle);
                                panel.Children.Add(textblockTask);
                                panel.Children.Add(textblockTimeTitle);
                                panel.Children.Add(textblockTime);
                                panel.Children.Add(textblockSeeWorkTitle);
                                panel.Children.Add(buttonSeeWork);
                                panel.Children.Add(buttonPanel);

                                paneldistance2 += 210;
                                z = 0;

                                myCanvas.Width += 210;
                                pnlWork.Width += 210;

                                if (txtUser.Text == fh.RetrieveProjectEmpName(projectNum[i]))
                                {
                                    Button buttonFinish = new Button()
                                    {
                                        Height = 20,
                                        Width = 50,
                                        Content = "Finish",
                                        HorizontalAlignment = HorizontalAlignment.Left,
                                        VerticalAlignment = VerticalAlignment.Bottom,
                                        Tag = i,
                                        Name = String.Format("btn{0}", projectNum[i]),
                                        Margin = new Thickness(5, 0, 45, 5),
                                        Style = stylebtn,
                                    };
                                    buttonFinish.Click += new RoutedEventHandler(FinishWork);
                                    buttonPanel.Children.Add(buttonFinish);
                                }
                                panel.Children.Add(line);
                            }

                            break;
                        case 900:
                            if (z == 0)
                            {
                                panel = new StackPanel()
                                {
                                    Width = 200,
                                    Height = 215,
                                    Margin = new Thickness(paneldistance, 10, 0, 0),
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Style = style,
                                };
                                myCanvas.Children.Add(panel);

                                panel.Children.Add(textblockNameTitle);
                                panel.Children.Add(textblockName);
                                panel.Children.Add(textblockTaskTitle);
                                panel.Children.Add(textblockTask);
                                panel.Children.Add(textblockTimeTitle);
                                panel.Children.Add(textblockTime);
                                panel.Children.Add(textblockSeeWorkTitle);
                                panel.Children.Add(buttonSeeWork);
                                panel.Children.Add(buttonPanel);

                                paneldistance += 210;
                                z += 1;
                                if (txtUser.Text == fh.RetrieveProjectEmpName(projectNum[i]))
                                {
                                    Button buttonFinish = new Button()
                                    {
                                        Height = 20,
                                        Width = 50,
                                        Content = "Finish",
                                        HorizontalAlignment = HorizontalAlignment.Left,
                                        VerticalAlignment = VerticalAlignment.Bottom,
                                        Tag = i,
                                        Name = String.Format("btn{0}", projectNum[i]),
                                        Margin = new Thickness(5, 0, 45, 5),
                                        Style = stylebtn,
                                    };
                                    buttonFinish.Click += new RoutedEventHandler(FinishWork);
                                    buttonPanel.Children.Add(buttonFinish);
                                }
                                panel.Children.Add(line);
                            }
                            else if (z == 1)
                            {
                                panel = new StackPanel()
                                {
                                    Width = 200,
                                    Height = 215,
                                    Margin = new Thickness(paneldistance2, 260, 0, 0),
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Style = style,
                                };
                                myCanvas.Children.Add(panel);

                                panel.Children.Add(textblockNameTitle);
                                panel.Children.Add(textblockName);
                                panel.Children.Add(textblockTaskTitle);
                                panel.Children.Add(textblockTask);
                                panel.Children.Add(textblockTimeTitle);
                                panel.Children.Add(textblockTime);
                                panel.Children.Add(textblockSeeWorkTitle);
                                panel.Children.Add(buttonSeeWork);
                                panel.Children.Add(buttonPanel);

                                paneldistance2 += 210;
                                z += 1;
                                if (txtUser.Text == fh.RetrieveProjectEmpName(projectNum[i]))
                                {
                                    Button buttonFinish = new Button()
                                    {
                                        Height = 20,
                                        Width = 50,
                                        Content = "Finish",
                                        HorizontalAlignment = HorizontalAlignment.Left,
                                        VerticalAlignment = VerticalAlignment.Bottom,
                                        Tag = i,
                                        Name = String.Format("btn{0}", projectNum[i]),
                                        Margin = new Thickness(5, 0, 45, 5),
                                        Style = stylebtn,
                                    };
                                    buttonFinish.Click += new RoutedEventHandler(FinishWork);
                                    buttonPanel.Children.Add(buttonFinish);
                                }
                                panel.Children.Add(line);
                            }
                            else if (z == 2)
                            {
                                y += 1;
                                panel = new StackPanel()
                                {
                                    Width = 200,
                                    Height = 215,
                                    Margin = new Thickness(paneldistance3, 520, 0, 0),
                                    VerticalAlignment = VerticalAlignment.Center,
                                    Style = style,
                                };
                                myCanvas.Children.Add(panel);

                                panel.Children.Add(textblockNameTitle);
                                panel.Children.Add(textblockName);
                                panel.Children.Add(textblockTaskTitle);
                                panel.Children.Add(textblockTask);
                                panel.Children.Add(textblockTimeTitle);
                                panel.Children.Add(textblockTime);
                                panel.Children.Add(textblockSeeWorkTitle);
                                panel.Children.Add(buttonSeeWork);
                                panel.Children.Add(buttonPanel);

                                paneldistance3 += 210;
                                z = 0;

                                myCanvas.Width += 210;
                                pnlWork.Width += 210;

                                if (txtUser.Text == fh.RetrieveProjectEmpName(projectNum[i]))
                                {
                                    Button buttonFinish = new Button()
                                    {
                                        Height = 20,
                                        Width = 50,
                                        Content = "Finish",
                                        HorizontalAlignment = HorizontalAlignment.Left,
                                        VerticalAlignment = VerticalAlignment.Bottom,
                                        Tag = i,
                                        Name = String.Format("btn{0}", projectNum[i]),
                                        Margin = new Thickness(5, 0, 45, 5),
                                        Style = stylebtn,
                                    };
                                    buttonFinish.Click += new RoutedEventHandler(FinishWork);
                                    buttonPanel.Children.Add(buttonFinish);
                                }
                                panel.Children.Add(line);
                            }
                            break;
                    }
                    if (z != 0)
                    {
                        myCanvas.Width += 210;
                        pnlWork.Width += 210;
                    }
                }
                pnlWork.Width += 10;
            }
        }
        #endregion
        #region Procedural buttons
        public void FinishWork(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string s = btn.Name.Trim('b', 'n', 't');
            int i = Convert.ToInt32(s);

            FinishWork finishWork = new FinishWork();
            finishWork.I = i;
            finishWork.Show();
        }
        public void DeleteWork(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string s = btn.Name.Trim('b', 'n', 't');
            int i = Convert.ToInt32(s);

            RemoveWork removeWork = new RemoveWork();
            removeWork.I = i;
            removeWork.Show();
        }
        public void SeeWork(object sender, RoutedEventArgs e)
        {
            WorkInformation workInformation = new WorkInformation();
            FileHandeling fh = new FileHandeling();
            int i;
            Button btn = (Button)sender;
            string s = btn.Name.Trim('b', 'n', 't');
            try
            {
                i = Convert.ToInt32(s);
                workInformation.Date = Convert.ToDateTime(fh.DateTaskFinished(i));
            }
            catch
            {
                i = Convert.ToInt32(s.Trim('c'));
                workInformation.Date = Convert.ToDateTime(fh.DateMeantToBeFinished_FinishedWork(i));
            }

            workInformation.I = i;
            workInformation.Show();
        }
        #endregion
        #region Panel Switch
        private void pnlWorkCompBtn_Click(object sender, RoutedEventArgs e)
        {
            scrollboxBusy.Visibility = Visibility.Collapsed;
            scrollboxComplete.Visibility = Visibility.Visible;

            pnlWorkCompBtn.IsEnabled = false;
            pnlWorkBusyBtn.IsEnabled = true;

            CreateWorkMenus();
            CreateWorkMenus_FinishedWork();
        }

        private void pnlWorkBusyBtn_Click(object sender, RoutedEventArgs e)
        {
            scrollboxBusy.Visibility = Visibility.Visible;
            scrollboxComplete.Visibility = Visibility.Collapsed;

            pnlWorkCompBtn.IsEnabled = true;
            pnlWorkBusyBtn.IsEnabled = false;

            CreateWorkMenus();
            CreateWorkMenus_FinishedWork();
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void btnNewProject_Click(object sender, RoutedEventArgs e)
        {
            NewProjectConfirmation newProjectConfirmation = new NewProjectConfirmation();
            newProjectConfirmation.Show();
        }

        private void Window_StateChanged_1(object sender, EventArgs e)
        {
            dckpnlShowBtn.Width = window.ActualWidth;
            pnlShowUsers.Height = window.ActualHeight - 350;
            scrlpnlUsers.Height = window.ActualHeight - 350;

            {
                switch (this.WindowState)
                {
                    case WindowState.Maximized:
                        myCanvas.Height = 600;
                        myCanvas.Width = 600;
                        myCanvascomplete.Height = 600;
                        myCanvascomplete.Width = 600;
                        break;
                    default:
                        if (RestoreBounds.Height >= 900)
                        {
                            myCanvas.Height = 900;
                            myCanvascomplete.Height = 900;

                            pnlWork.MaxHeight = 900;
                        }
                        else if (RestoreBounds.Height >= 700 && RestoreBounds.Height < 900)
                        {
                            myCanvas.Height = 600;
                            myCanvascomplete.Height = 600;

                            pnlWork.MaxHeight = 900;
                        }
                        else if (RestoreBounds.Height < 700)
                        {
                            myCanvas.Height = 300;
                            myCanvascomplete.Height = 300;

                            pnlWork.MaxHeight = 900;
                        }
                        break;
                }

                CreateWorkMenus();
                CreateWorkMenus_FinishedWork();
            }
        }

        private void window_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {
            dckpnlShowBtn.Width = window.ActualWidth;
            pnlWork.Height = window.ActualHeight - 200;
            pnlShowUsers.Height = window.ActualHeight - 350;
            scrlpnlUsers.Height = window.ActualHeight - 350;


            if (pnlWork.Visibility == Visibility.Visible)
            {
                pnlWork.Height = window.ActualHeight - 200;
                if (pnlWork.Height > 800)
                {
                    myCanvas.Height = 900;
                }
                else if (pnlWork.Height > 500 && pnlWork.Height < 800)
                {
                    myCanvas.Height = 600;
                }
                else if (pnlWork.Height < 500)
                {
                    myCanvas.Height = 300;
                }
                CreateWorkMenus();
            }
            else
            {
                pnlWorkComplete.Height = window.ActualHeight - 200;
                if (pnlWorkComplete.Height > 800)
                {
                    myCanvascomplete.Height = 900;
                }
                else if (pnlWorkComplete.Height > 500 && pnlWork.Height < 800)
                {
                    myCanvascomplete.Height = 600;
                }
                else if (pnlWorkComplete.Height < 500)
                {
                    myCanvascomplete.Height = 300;
                }
            }
            CreateWorkMenus_FinishedWork();
        }
    }
    
}
