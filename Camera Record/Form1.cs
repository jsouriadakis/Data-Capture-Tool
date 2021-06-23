using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;
using AForge.Video;
using System.Diagnostics;
using AForge.Video.DirectShow;
using System.Collections;
using System.IO;
using System.Drawing.Imaging;
using System.IO.Ports;
using System.Globalization;
using System.Net;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MindFusion.Charting;
using MindFusion.Charting.WinForms;
using System.Collections.Concurrent;
using DansCSharpLibrary.Serialization;
using DataRecordClasses;

namespace Camera_Record
{
    public partial class Form1 : Form
    {

        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoDevice;
        private VideoCapabilities[] snapshotCapabilities;
        private ArrayList listCamera = new ArrayList();
        public string pathFolder = Application.StartupPath + @"\ImageCapture\";

        private Stopwatch stopWatch = null;
        private static bool needSnapshot = false;


        private MySqlConnection mySqlConnection;
        private string serverIP;
        private string serverPort;
        private string databaseName;
        private string userID;
        private string password;
        private ConcurrentQueue<IMUdataClass> imuDataQueue = new ConcurrentQueue<IMUdataClass>();
        private List<byte[]> tempCamShotList = new List<byte[]>();
        private int imuPackSize = 0;
        private double imuPackTimeStep;
        private WholeStream wholeStream;
        private PlotData plotData;
        private Exercise tempExercise;
        private bool exerciseStarted = false;
        private bool walkingStarted = false;
        private bool badForm = false;
        private bool firstPackReceived = false;
        private DateTime DateTimeAtStart;
        private DateTime timeForCamShots;
        private long comboIndex = 0;
        private bool startRecording = false;
        private string outputDirectory = "";

        private Exercise refNothing;
        private Exercise refWalking;
        private Exercise exercise;
        private Color defaultButtonColor;
        private Color activeButtonColor;
        private action currentAction;

        private bool loopSelectThread = false;
        private bool fillDataThread = false;
        
        private void InitializeDatabase()
        {
            serverIP = "127.0.0.1";
            serverPort = "3306";
            databaseName = "automatic_gym";
            userID = "root";
            password = "";
            string connectionString;
            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}", serverIP, serverPort, userID, password, databaseName);

            mySqlConnection = new MySqlConnection(connectionString);
        }
        //open connection to database
        private void OpenConnection()
        {
            try
            {
                mySqlConnection.Open();
                MessageBox.Show("Connection Succesful");
                //return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                //return false;
            }
        }
        /// <summary>
        /// Add ImuDataToList
        /// </summary>
        private void LoopSelect()
        {
            long select = -1;
            long packIndex = 0;
            while(loopSelectThread)
            {
                IMUdataClass imuData = SelectDB(select);
                if (imuData == null)
                {
                    continue;
                }
                if (!firstPackReceived)
                {
                    wholeStream.startTime = imuData.timePerValue[0];
                    DateTimeAtStart = DateTime.Now;
                    timeForCamShots = DateTime.Now;
                    imuPackSize = imuData.accelerometer.Length;
                    imuPackTimeStep = imuData.timePerStep;
                    firstPackReceived = true;
                }
                select = imuData.index;
                imuDataQueue.Enqueue(imuData);
                imuData.Dispose();
                packIndex++;


            }
        }
        private IMUdataClass SelectDB(long selectId)
        {
            if (!ConnectionOpen())
            {
                return null;
            }
            string query = "";
            string queryToFindNewest = "SELECT * FROM sensor_data ORDER BY id DESC LIMIT 1";
            string queryToFindNext = "SELECT * FROM automatic_gym.sensor_data  where id = " + (selectId + 1).ToString();
            IMUdataClass imuData = null;
            if (selectId == -1)
            {
                query = queryToFindNewest;
            }
            else
            {
                query = queryToFindNext;
            }
            MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
            MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                if (mySqlDataReader.GetValue(0) == null)
                {
                    break;
                }
                imuData = new IMUdataClass(mySqlDataReader.GetInt64(0), mySqlDataReader.GetInt64(1), mySqlDataReader.GetInt64(2), mySqlDataReader.GetInt64(3), mySqlDataReader.GetInt64(4), mySqlDataReader.GetInt16(5), mySqlDataReader.GetInt16(6), JsonConvert.DeserializeObject<double[][]>(mySqlDataReader.GetString(7)), JsonConvert.DeserializeObject<double[][]>(mySqlDataReader.GetString(8)));
            }
            mySqlDataReader.Close();
            return imuData;
        }

        private bool ConnectionOpen()
        {
            if (ConnectionState.Open == mySqlConnection.State)
            {
                return true;
            }
            return false;
        }
        //Close connection
        private bool CloseConnection()
        {
            try
            {
                mySqlConnection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public Form1()
        {
            
            InitializeComponent();
            InitializeDatabase();
            getListCameraUSB();
            wholeStream = new WholeStream();
            plotData = new PlotData(112);
            refNothing = new Exercise(action.DoNothing);
            refWalking = new Exercise(action.Walking);
            InitliazeLineChart();
            System.Windows.Forms.Timer dataTimer = new System.Windows.Forms.Timer();
            dataTimer.Tick += new EventHandler(PlotLiveData);
            dataTimer.Interval = 200;
            dataTimer.Start();
            currentAction = action.DoNothing;
            InitializeButtons();


        }
        private void InitializeButtons()
        {
            button3.Enabled = false;
            button2.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            defaultButtonColor = button5.BackColor;
            activeButtonColor = Color.Green;
        }
        private static string _usbcamera;
        public string usbcamera
        {
            get { return _usbcamera; }
            set { _usbcamera = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenCamera();
            if (!ConnectionOpen())
            {
                OpenConnection();
            }
            //button1.Enabled = false;
            button2.Enabled = true;
            
        }

        #region Open Scan Camera
        private void OpenCamera()
        {
            try
            {
                usbcamera = comboBox1.SelectedIndex.ToString();
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count != 0)
                {
                    // add all devices to combo
                    foreach (FilterInfo device in videoDevices)
                    {
                        listCamera.Add(device.Name);

                    }
                }
                else
                {
                    MessageBox.Show("Camera devices found");
                }

                videoDevice = new VideoCaptureDevice(videoDevices[Convert.ToInt32(usbcamera)].MonikerString);
                /*
                snapshotCapabilities = videoDevice.SnapshotCapabilities;
                if (snapshotCapabilities.Length == 0)
                {
                    //MessageBox.Show("Camera Capture Not supported");
                }
                */
                OpenVideoSource(videoDevice);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }
        #endregion


        //Delegate Untuk Capture, insert database, update ke grid 
        public delegate void CaptureSnapshotManifast(Bitmap image);
        public void UpdateCaptureSnapshotManifast(Bitmap image)
        {
            try
            {
                /*
                needSnapshot = false;
                pictureBox2.Image = image;
                pictureBox2.Update();

               
                string namaImage = "sampleImage";
                string nameCapture = namaImage + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".bmp";

                if (Directory.Exists(pathFolder))
                {
                    pictureBox2.Image.Save(pathFolder + nameCapture, ImageFormat.Bmp);
                }
                else
                {
                    Directory.CreateDirectory(pathFolder);
                    pictureBox2.Image.Save(pathFolder + nameCapture, ImageFormat.Bmp);
                }
                */
            }

            catch { }

        }

        public void OpenVideoSource(IVideoSource source)
        {
            try
            {
                // set busy cursor
                this.Cursor = Cursors.WaitCursor;

                // stop current video source
                CloseCurrentVideoSource();

                // start new video source
                videoSourcePlayer1.VideoSource = source;
                videoSourcePlayer1.Start();

                // reset stop watch
                stopWatch = null;


                this.Cursor = Cursors.Default;
            }
            catch { }
        }


        private void getListCameraUSB()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count != 0)
            {
                // add all devices to combo
                foreach (FilterInfo device in videoDevices)
                {
                    comboBox1.Items.Add(device.Name);

                }
            }
            else
            {
                comboBox1.Items.Add("No DirectShow devices found");
            }

            comboBox1.SelectedIndex = 0;

        }

        public void CloseCurrentVideoSource()
        {
            try
            {

                if (videoSourcePlayer1.VideoSource != null)
                {
                    videoSourcePlayer1.SignalToStop();

                    // wait ~ 3 seconds
                    for (int i = 0; i < 30; i++)
                    {
                        if (!videoSourcePlayer1.IsRunning)
                            break;
                        System.Threading.Thread.Sleep(100);
                    }

                    if (videoSourcePlayer1.IsRunning)
                    {
                        videoSourcePlayer1.Stop();
                    }

                    videoSourcePlayer1.VideoSource = null;
                }
            }
            catch { }
        }

        Thread thread1 = null;
        Thread thread2 = null;
        /// <summary>
        /// To Add change to stop recording
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (outputDirectory == "")
            {
                MessageBox.Show("Choose Output Directory");
                return;
            }
            if (!ConnectionOpen() || this.Cursor == Cursors.WaitCursor)
            {
                MessageBox.Show("Need to Connect First");
                return;
            }
            
            if (!CheckUserDetails())
            {
                return;
            }
            

            if (button2.Text == "Start Recording")
            {
                MessageBox.Show("Start Recording");
                button2.Text = "Stop Recording";

                button3.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;

                button4.Enabled = false;
                System.Threading.Thread.Sleep(100);
                thread1 = new Thread(LoopSelect);
                thread2 = new Thread(FillComboDataPack);
                loopSelectThread = true;
                fillDataThread = true;
                thread1.Start();
                thread2.Start();
                startRecording = true;
                DisableUserInputs();
                InsertUserDetails();
                ButtonsColorOnState();
            }
            else
            {
                startRecording = false;
                
                button3.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;

                button4.Enabled = true;

                button5_Click(sender, e);
                loopSelectThread = false;
                fillDataThread = false;
                while (thread1.IsAlive || thread2.IsAlive);
                imuDataQueue = new ConcurrentQueue<IMUdataClass>();
                tempCamShotList.Clear();
                MessageBox.Show("Stop Recording");
                button2.Text = "Start Recording";
                System.Threading.Thread.Sleep(100);
                SaveCurrentDataToFile();
                EnableUserInputs();
               // CloseConnection();
            }



        }

        private void videoSourcePlayer1_NewFrame_1(object sender, ref Bitmap image)
        {
            try
            {
               if(startRecording)
               {
                    Bitmap bitmap = (Bitmap)image.Clone();
                    MemoryStream memoryStream = new MemoryStream();
                    bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    tempCamShotList.Add(memoryStream.ToArray());
                    memoryStream.Dispose();
                    bitmap.Dispose();
               }
                
              
            }
            catch
            { }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="exercise"></param>
        private void ExportExceriseToCSV(Exercise exercise)
        {

        }

        private void FillComboDataPack()
        {
            
            while (fillDataThread)
            {
                if (firstPackReceived)
                {
                    if (imuDataQueue.Count > 0)
                    {
                        int camSize = tempCamShotList.Count;
                        List<byte[]> camShots = new List<byte[]>(imuPackSize);
                        camShots.Add(tempCamShotList[0]);
                        float camStepForPack = (float)camSize / (imuPackSize - 1);
                        for (int i = 1; i < imuPackSize - 1; i++)
                        {
                            camShots.Add(tempCamShotList[(int)(i * camStepForPack)]);
                        }
                        camShots.Add(tempCamShotList[camSize - 1]);
                        tempCamShotList.RemoveRange(0, camSize - 1);

                        IMUdataClass imuData = null;
                        while (!imuDataQueue.TryDequeue(out imuData));

                        if (exerciseStarted)
                        {
                            tempExercise = exercise;
                        }
                        else
                        {
                            tempExercise = refWalking;
                        }
                        switch (currentAction)
                        {
                            case action.DoNothing:
                                tempExercise = refNothing;
                                break;
                            case action.Walking:
                                tempExercise = refWalking;
                                break;
                            case action.Exercising:
                                tempExercise = exercise;
                                break;

                        }

                        ComboDataPerPack comboDataPerPack = new ComboDataPerPack(comboIndex, imuData, camShots, tempExercise, badForm);
                        if (badForm)
                        {
                            badForm = false;
                        }
                        wholeStream.AppendComboDataPack(comboDataPerPack);
                        imuData.Dispose();
                        comboIndex++;

                    }

                }
            }
            
        }

        private void PlotLiveData(object sender, EventArgs e)
        {
            if(!startRecording)
            {
                return;
            }
            if (wholeStream == null)
            {
                return;
            }
            if (!firstPackReceived)
            {
                return;
            }
            int currentDataLength = 0;
            if (currentDataLength < wholeStream.comboDataPerPackList.Count)
            {
                currentDataLength = wholeStream.comboDataPerPackList.Count;
                if (wholeStream.comboDataPerPackList[currentDataLength - 1] == null)
                {
                    return;
                }

                IMUdataClass tempIMU = wholeStream.comboDataPerPackList[currentDataLength - 1].iMUdataClass;
                plotData.AddDataFuture(tempIMU);
                if (plotData.IsReadyToPlot())
                {
                    List<DateTime> dates = new List<DateTime>();
                    List<double> dates2 = new List<double>();
                    dates = plotData.dateTime.ToList();
                    dates2 = plotData.time.ToList();
                    List<double> valuesX = new List<double>();
                    List<double> valuesY = new List<double>();
                    List<double> valuesZ = new List<double>();
                    List<string> labels = new List<string>(dates.Count);
                    valuesX = plotData.accelerometerX.ToList();
                    valuesY = plotData.accelerometerY.ToList();
                    valuesZ = plotData.accelerometerZ.ToList();
                    Series2D seriesX = new Series2D(dates2, valuesX, labels);
                    Series2D seriesY = new Series2D(dates2, valuesY, labels);
                    Series2D seriesZ = new Series2D(dates2, valuesZ, labels);

                    seriesX.Title = "X";
                    seriesY.Title = "Y";
                    seriesZ.Title = "Z";
                    if (lineChart1.Series.Count == 3)
                    {
                        
                        lineChart1.Series[0] = seriesX;
                        lineChart1.Series[1] = seriesY;
                        lineChart1.Series[2] = seriesZ;
                    }
                    else
                    {
                        lineChart1.Series.Add(seriesX);
                        lineChart1.Series.Add(seriesY);
                        lineChart1.Series.Add(seriesZ);
                    }
                    

                } 
            }
        }

        private void InitliazeLineChart()
        {
            lineChart1.Title = "";
            lineChart1.XAxis.Title = "";
            lineChart1.YAxis.Title = "";
            lineChart1.ShowXCoordinates = false;
            lineChart1.ShowYCoordinates = true;
            lineChart1.YAxis.MinValue = -2.4;
            lineChart1.YAxis.MaxValue = 2.4;
            lineChart1.Theme = new Theme();
            lineChart1.Theme.CommonSeriesFills = lineChart1.Theme.CommonSeriesStrokes =
                new List<MindFusion.Drawing.Brush>
                {
                    new MindFusion.Drawing.SolidBrush(Color.Red),
                    new MindFusion.Drawing.SolidBrush(Color.Blue),
                    new MindFusion.Drawing.SolidBrush(Color.Green)

                  
                };
            lineChart1.Theme.AxisLabelsBrush = new MindFusion.Drawing.SolidBrush(Color.White);
            lineChart1.Theme.LegendBorderStroke = new MindFusion.Drawing.SolidBrush(Color.Gray);
            lineChart1.Theme.LegendBackground = new MindFusion.Drawing.SolidBrush(Color.FromArgb(120, 230, 230, 230));
            
        }

        private void SaveCurrentDataToFile()
        {
            WholeStream tempStream = wholeStream;
            tempStream.endTime = DateTime.Now;
            string fileName = tempStream.userId;
            string filePath = outputDirectory + fileName;
            if (File.Exists(filePath + ".bin"))
            {
                for (int i = 1; i < 99999; i++)
                {
                    string tempPath = filePath + "_" + i.ToString() + ".bin";
                    if (!File.Exists(tempPath))
                    {
                        filePath = tempPath;
                        break;
                    }
                }
            }
            else
            {
                filePath = filePath + ".bin";
            }

            BinarySerialization.WriteToBinaryFile<WholeStream>(filePath, tempStream);
        }

        private void InsertUserDetails()
        {

            wholeStream.userId = textBox1.Text;
            wholeStream.userHeight = float.Parse(textBox2.Text);
            wholeStream.userWeight = float.Parse(textBox3.Text);
            wholeStream.gender = comboBox2.Text;

        }

        private bool CheckUserDetails()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("Fill User Details");
                return false;
            }
            if (!Utilities.IsDigitsOnly(textBox2.Text) || !Utilities.IsDigitsOnly(textBox3.Text))
            {
                MessageBox.Show("Weight and Height must me integer numbers, cm and kg");
                return false;
            }
            return true;
        }

        private void DisableUserInputs()
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            comboBox2.Enabled = false;
        }
        private void EnableUserInputs()
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            comboBox2.Enabled = true;
        }
        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (startRecording)
            {
                MessageBox.Show("Stop recording to change directory");
                return;
            }
            folderBrowserDialog1.ShowDialog();
            while (folderBrowserDialog1.SelectedPath == "")
            {
                MessageBox.Show("Choose Directory");
                folderBrowserDialog1.ShowDialog();
            }
            outputDirectory = folderBrowserDialog1.SelectedPath +  "\\";
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (currentAction != action.Exercising)
            {
                button3.Text = "Stop Exercise";
                currentAction = action.Exercising;
                exercise = new Exercise(action.Exercising);
                ButtonsColorOnState();
                button7.Enabled = true;
                button7.BackColor = Color.Red;
            }
            else
            {
                currentAction = action.DoNothing;
                button5_Click(sender, e);
                button7.Enabled = false;
                button7.BackColor = defaultButtonColor;
                button3.Text = "Start Exercise";
                Prompt.ExerciseDialogData exerciseDialogData = Prompt.ShowExerciseDialog();
                exercise.id = exerciseDialogData.id;
                exercise.name = exerciseDialogData.name;
                exercise.weight = exerciseDialogData.weight;
                exercise.repetitions = exerciseDialogData.repetitions;
                
            }


        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (currentAction == action.Exercising)
            {
                button3_Click(sender, e);
            }
            if (currentAction !=  action.Walking)
            {
                button6.Text = "Not Walking";
                currentAction = action.Walking;
                ButtonsColorOnState();
            }
            else
            {
                button6.Text = "Walking";
                button5_Click(sender, e);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (currentAction == action.Exercising)
            {
                button3_Click(sender, e);
            }
            currentAction = action.DoNothing;
            ButtonsColorOnState();


        }

        private void ButtonsColorOnState()
        {
            button3.BackColor = defaultButtonColor;
            button5.BackColor = defaultButtonColor;
            button6.BackColor = defaultButtonColor;
            if (!startRecording)
            {
                return;
            }
            switch (currentAction)
            {
                case action.DoNothing:
                    button5.BackColor = activeButtonColor;
                    break;
                case action.Walking:
                    button6.BackColor = activeButtonColor;
                    break;
                case action.Exercising:
                    button3.BackColor = activeButtonColor;
                    break;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (currentAction != action.Exercising)
            {
                return;
            }
            badForm = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

    }


}
