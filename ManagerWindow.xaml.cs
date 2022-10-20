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
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
        }
        private void btnAddWork_Click(object sender, RoutedEventArgs e)     //Shows the AddWork.Xaml window used to create new ongoing work tabs
        {
            AddWork addWork = new AddWork();
            addWork.Show();
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
                cmbobxUsers.Items.Clear();
                //Creates a new TextBlock that has the name and surname of the users as it's Text

                Style style = this.FindResource("backgroundsTextblocks") as Style;

                TextBlock txtTempUsers = new TextBlock();
                txtTempUsers.Text = "Users";
                txtTempUsers.MinWidth = 50;
                txtTempUsers.MinHeight = 20;
                txtTempUsers.FontSize = 20;
                txtTempUsers.Style = style;
                txtTempUsers.TextDecorations = TextDecorations.Underline;
                txtTempUsers.HorizontalAlignment = HorizontalAlignment.Center;
                pnlShowUsers.Children.Add(txtTempUsers);

                int i = 0;
                while (true)
                {
                    string user = " " + names[i] + " " + surnames[i];

                    txtTempUsers = new TextBlock();
                    if (user.Length >= 19)
                    {
                        for(int j = 0; j < 18; j++)
                        {
                            txtTempUsers.Text += user[j];
                        }
                        txtTempUsers.Text += "...";
                    }
                    else
                    {
                        txtTempUsers.Text = user;
                    }
                    cmbobxUsers.Items.Add(user);
                    txtTempUsers.MinWidth = 50;
                    txtTempUsers.MinHeight = 20;
                    txtTempUsers.FontSize = 20;
                    txtTempUsers.Style = style;
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
        #region Adding/Removing users
        private void btnAddUsers_Click(object sender, RoutedEventArgs e)    //Opens the AddUsers window
        {
            AddUser addUser = new AddUser();
            addUser.Show();
        }
        private void btnRemoveUsers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatabaseHandeling dbh = new DatabaseHandeling();
                dbh.DeleteUser(cmbobxUsers.SelectedItem.ToString().Trim(' '));

                FileHandeling fh = new FileHandeling();
                fh.ClearFileUsers();
                dbh.RememberUsers();
                UpdateWindow();
            }
            catch { MessageBox.Show("Select a user before trying to remove someone"); }
        }
        #endregion
        #region Panel population
        private void CreateWorkMenus_FinishedWork()
        {
            myCanvascomplete.Children.Clear();
            myCanvascomplete.Width = 0;
            pnlWorkComplete.Width = 0;
            pnlWorkComplete.Height = pnlWork.Height;

            FileHandeling fh = new FileHandeling();
            SolidColorBrush colTransparent = new SolidColorBrush();
            colTransparent.Color = Color.FromArgb(0, 0, 0, 0);

            int paneldistance = 10;
            int paneldistance2 = 10;
            int paneldistance3 = 10;
            int canvasWidth = 215;
            int z = 0;

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
                        for(int j = 0; j < 25; j++)
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
                        Name = String.Format("btnc{0}", projectNum[i]),
                        Style = style,
                    };
                    buttonSeeWork.Click += new RoutedEventHandler(SeeWork);

                    Line line = new Line()
                    {
                        X1 = 0,
                        Y1 = 5,
                        X2 = 200,
                        Y2 = 5,
                        Stroke = fh.Priority_FinishedWork(projectNum[i]),
                        StrokeThickness = 10,
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
                            canvasWidth += 210;
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
                                canvasWidth += 210;
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
                                canvasWidth += 210;
                                pnlWorkComplete.Width += 210;
                            }
                            break;
                    }
                    if (z != 0)
                    {
                        myCanvas.Width += 215;
                        pnlWork.Width += 225;
                    }
                }
                pnlWorkComplete.Width += 10;
            }
        }
        private void CreateWorkMenus()
        {
            myCanvas.Children.Clear();
            myCanvas.Width = 0;
            pnlWork.Width = 0;
            cldShowDate.SelectedDates.Clear();

            FileHandeling fh = new FileHandeling();
            SolidColorBrush colTransparent = new SolidColorBrush();
            colTransparent.Color = Color.FromArgb(0, 0, 0, 0);

            int paneldistance = 10;
            int paneldistance2 = 10;
            int paneldistance3 = 10;

            int lim = fh.CheckTotalProject();
            int z = 0;

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
                        Style = style,
                        Margin = new Thickness(5, 5, 0, 0),
                    };
                    TextBlock textblockName = new TextBlock()
                    {
                        FontSize = 13,
                        Text = fh.RetrieveProjectEmpName(projectNum[i]),
                        Style = style,
                        Margin = new Thickness(10, 0, 0, 5),
                    };

                    TextBlock textblockTaskTitle = new TextBlock()
                    {
                        FontSize = 15,
                        Text = "Task",
                        Style = style,
                        Margin = new Thickness(5, 0, 0, 0),
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
                        Style = style,
                        Margin = new Thickness(5, 0, 0, 0),
                    };
                    TextBlock textblockTime = new TextBlock()
                    {
                        FontSize = 13,
                        Style = style,
                        Margin = new Thickness(10, 0, 0, 5),
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
                            if (fh.TimeLeft(projectNum[i])[0] == '-')
                            {
                                textblockTime.Text = fh.TimeLeft(projectNum[i]).Trim('-') + " days past deadline";
                            }
                            else
                            {
                                textblockTime.Text = fh.TimeLeft(projectNum[i]) + " days left";
                            }
                            break;
                    }
                    TextBlock textblockSeeWorkTitle = new TextBlock()
                    {
                        FontSize = 15,
                        Text = "See more details",
                        Style = style,
                        Margin = new Thickness(5, 0, 0, 0),
                    };
                    style = this.FindResource("frontPanelButtons") as Style;
                    Button buttonSeeWork = new Button()
                    {
                        Height = 20,
                        Width = 50,
                        Content = "Details",
                        Style = style,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(10, 0, 0, 5),
                        Name = String.Format("btn{0}", projectNum[i])
                    };
                    buttonSeeWork.Click += new RoutedEventHandler(SeeWork);
                    Button buttonDelete = new Button()
                    {
                        Height = 20,
                        Width = 50,
                        Content = "Delete",
                        Style = style,
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        Tag = i,
                        Margin = new Thickness(45, 0, 5, 5),
                        Name = String.Format("btn{0}", projectNum[i])
                    };
                    buttonDelete.Click += new RoutedEventHandler(DeleteWork);
                    Button buttonFinish = new Button()
                    {
                        Height = 20,
                        Width = 50,
                        Content = "Finish",
                        Style = style,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        Tag = i,
                        Name = String.Format("btn{0}", projectNum[i]),
                        Margin = new Thickness(5, 0, 45, 5),
                    };
                    buttonFinish.Click += new RoutedEventHandler(FinishWork);
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

                            StackPanel panel = new StackPanel()
                            {
                                Width = 200,
                                Height = 215,
                                Margin = new Thickness(paneldistance, 10, 0, 0),
                                Style = style,
                                VerticalAlignment = VerticalAlignment.Center,
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
                            buttonPanel.Children.Add(buttonFinish);
                            buttonPanel.Children.Add(buttonDelete);
                            panel.Children.Add(line);

                            paneldistance += 210;

                            myCanvas.Width += 215;
                            pnlWork.Width += 225;

                            break;
                        case 600:
                            if (z == 0)
                            {
                                panel = new StackPanel()
                                {
                                    Width = 200,
                                    Height = 215,
                                    Margin = new Thickness(paneldistance, 10, 0, 0),
                                    Style = style,
                                    VerticalAlignment = VerticalAlignment.Center,
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
                                buttonPanel.Children.Add(buttonFinish);
                                buttonPanel.Children.Add(buttonDelete);
                                panel.Children.Add(line);

                                paneldistance += 210;
                                z += 1;
                            }
                            else if (z == 1)
                            {
                                panel = new StackPanel()
                                {
                                    Width = 200,
                                    Height = 215,
                                    Margin = new Thickness(paneldistance2, 260, 0, 0),
                                    Style = style,
                                    VerticalAlignment = VerticalAlignment.Center,
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
                                buttonPanel.Children.Add(buttonFinish);
                                buttonPanel.Children.Add(buttonDelete);
                                panel.Children.Add(line);

                                paneldistance2 += 210;
                                z = 0;

                                myCanvas.Width += 215;
                                pnlWork.Width += 225;
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
                                    Style = style,
                                    VerticalAlignment = VerticalAlignment.Center,
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
                                buttonPanel.Children.Add(buttonFinish);
                                buttonPanel.Children.Add(buttonDelete);
                                panel.Children.Add(line);

                                paneldistance += 210;
                                z += 1;
                            }
                            else if (z == 1)
                            {
                                panel = new StackPanel()
                                {
                                    Width = 200,
                                    Height = 215,
                                    Margin = new Thickness(paneldistance2, 260, 0, 0),
                                    Style = style,
                                    VerticalAlignment = VerticalAlignment.Center,
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
                                buttonPanel.Children.Add(buttonFinish);
                                buttonPanel.Children.Add(buttonDelete);
                                panel.Children.Add(line);

                                paneldistance2 += 210;
                                z += 1;
                            }
                            else if (z == 2)
                            {
                                panel = new StackPanel()
                                {
                                    Width = 200,
                                    Height = 215,
                                    Margin = new Thickness(paneldistance3, 520, 0, 0),
                                    Style = style,
                                    VerticalAlignment = VerticalAlignment.Center,
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
                                buttonPanel.Children.Add(buttonFinish);
                                buttonPanel.Children.Add(buttonDelete);
                                panel.Children.Add(line);

                                paneldistance3 += 210;
                                z = 0;

                                myCanvas.Width += 215;
                                pnlWork.Width += 225;
                            }
                            break;
                    }

                    if (z != 0)
                    {
                        myCanvas.Width += 215;
                        pnlWork.Width += 225;
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

            CreateWorkMenus_FinishedWork();
        }
        private void pnlWorkBusyBtn_Click(object sender, RoutedEventArgs e)
        {
            scrollboxBusy.Visibility = Visibility.Visible;
            scrollboxComplete.Visibility = Visibility.Collapsed;

            pnlWorkCompBtn.IsEnabled = true;
            pnlWorkBusyBtn.IsEnabled = false;

            CreateWorkMenus();
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

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dckpnlShowBtn.Width = window.ActualWidth;
            dckpnlWork.Width = window.ActualWidth;
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

        private void window_StateChanged(object sender, EventArgs e)
        {
            dckpnlShowBtn.Width = window.ActualWidth;
            dckpnlWork.Width = window.ActualWidth;
            pnlShowUsers.Height = window.ActualHeight - 350;
            scrlpnlUsers.Height = window.ActualHeight - 350;

            {
                switch (this.WindowState)
                {
                    case WindowState.Maximized:
                        myCanvas.Height = 600;
                        myCanvascomplete.Height = 600;
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
    }
}
