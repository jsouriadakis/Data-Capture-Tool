using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DansCSharpLibrary.Serialization;
using DataRecordClasses;
using MindFusion.Charting;
using MindFusion.Charting.WinForms;
using CsvHelper;
using Accord.Video;
using Accord.Video.FFMPEG;
using Accord.Video.VFW;
using System.Diagnostics;

namespace Camera_Record
{
    public partial class Form2 : Form
    {
        private string filePath;
        private WholeStream loadStream;
        private int comboPackets;
        private int imuPacketSize;
        private int maximumIMUPackets;
        private List<StartEndIndex> actions = new List<StartEndIndex>();
        private bool playVideo = false;
        private PlotData plotData;
        private string outputDirectory;
        private string fileName;
        int minimumComboPackIndex;
        int maximumComboPackIndex;
        public Form2()
        {
            InitializeComponent();
            InitliazeLineChart();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            if (textBox6.Text == "")
            {
                openFileDialog1.ShowDialog();
                while (openFileDialog1.FileName == "" || openFileDialog1.FileName == "*.bin")
                {
                    MessageBox.Show("Choose File");
                    openFileDialog1.ShowDialog();
                }
                filePath = openFileDialog1.FileName;

                //outputDirectory += "\\";
                MessageBox.Show(filePath);
            }
            
            else
            {
                filePath = textBox6.Text;
                if (!(filePath[filePath.Length -1] == 'n' && filePath[filePath.Length - 2] == 'i' && filePath[filePath.Length - 3] == 'b'))
                {
                    MessageBox.Show("Choose Correct File");
                    return;
                }
            }*/
            openFileDialog1.ShowDialog();
            while (openFileDialog1.FileName == "" || openFileDialog1.FileName == "*.bin")
            {
                MessageBox.Show("Choose File");
                openFileDialog1.ShowDialog();
            }
            filePath = openFileDialog1.FileName;

            //outputDirectory += "\\";
            MessageBox.Show(filePath);
            string[] names = filePath.Split('\\');
            fileName = names[names.Length - 1];
            textBox6.Text = fileName;
            names[names.Length - 1] = "";
            outputDirectory = string.Join("\\", names);
            LoadFile();
            ShowUserDetails();
        }

        private void LoadFile()
        {
            loadStream = new WholeStream();
            loadStream = BinarySerialization.ReadFromBinaryFile<WholeStream>(filePath);
            comboPackets = loadStream.comboDataPerPackList.Count;
            imuPacketSize = loadStream.comboDataPerPackList[0].iMUdataClass.packetSize;
            maximumIMUPackets = comboPackets * imuPacketSize;
            minimumComboPackIndex = 0;
            maximumComboPackIndex = comboPackets - 1;
            InitliazeTaskBars();
            DetectActions();
            LoadPlotData(0, (comboPackets - 1));
            PlotData();
            ShowImage(0, 0);
            PopulateActions();
        }

        private void InitliazeTaskBars()
        {
            trackBar1.Minimum = 0;
            trackBar1.Maximum = maximumIMUPackets - 1;
            selectionRangeSlider2.Min = 0;
            selectionRangeSlider2.Max = maximumIMUPackets - 1;
            selectionRangeSlider2.SelectedMin = 0;
            selectionRangeSlider2.SelectedMax = maximumIMUPackets - 1;
        }
        private void ShowUserDetails()
        {
            textBox2.Text = loadStream.userHeight.ToString();
            textBox5.Text = loadStream.gender;
            textBox4.Text = loadStream.userId.ToString();
            textBox3.Text = loadStream.userWeight.ToString();
        }
        
        Exercise previousExercise = null;
        private void ShowCurrentAction(int comboPackIndex)
        {
            if (loadStream.comboDataPerPackList[comboPackIndex].exerciseType == previousExercise)
            {
                return;
            }
            textBox1.Clear();
            textBox1.AppendText(loadStream.comboDataPerPackList[comboPackIndex].exerciseType.action.ToString() + "\r\n");
            textBox1.AppendText("\r\n");
            if (loadStream.comboDataPerPackList[comboPackIndex].exerciseType.action == action.Exercising)
            {
                textBox1.AppendText("Exercise Name : " + loadStream.comboDataPerPackList[comboPackIndex].exerciseType.name + "\r\n");
                textBox1.AppendText("Repetitions : " + loadStream.comboDataPerPackList[comboPackIndex].exerciseType.repetitions + "\r\n");
                textBox1.AppendText("Applied Weight : " + loadStream.comboDataPerPackList[comboPackIndex].exerciseType.weight + " Kg" + "\r\n");
            }
            previousExercise = loadStream.comboDataPerPackList[comboPackIndex].exerciseType;
        }

        private int previousValue = 0;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (loadStream == null)
            {
                return;
            }
            if (trackBar1.Value == previousValue)
            {
                return;
            }
            if (trackBar1.Value > maximumIMUPackets - 1)
            {
                trackBar1.Value = maximumIMUPackets;
            }
            if (trackBar1.Value < 0)
            {
                trackBar1.Value = 0;
            }
            if (playVideo)
            {

                button2_Click(sender, e);
            }
            if (trackBar1.Value > selectionRangeSlider2.SelectedMax)
            {
                selectionRangeSlider2.SelectedMax = trackBar1.Value;
            }
            if (trackBar1.Value < selectionRangeSlider2.SelectedMin)
            {
                selectionRangeSlider2.SelectedMin = trackBar1.Value;
            }
            int tempComboPacketIndex = trackBar1.Value / imuPacketSize;
            int imuPacketIndex = (trackBar1.Value % imuPacketSize);
            ShowImage(tempComboPacketIndex, imuPacketIndex);
            selectionRangeSlider2.Value = trackBar1.Value;
            ShowCurrentAction(tempComboPacketIndex);
            previousValue = trackBar1.Value;
        }


        private void selectionRangeSlider2_SelectionChanged(object sender, EventArgs e)
        {
            if (loadStream == null)
            {
                return;
            }
            if (selectionRangeSlider2.SelectedMax > maximumIMUPackets - 1)
            {
                selectionRangeSlider2.SelectedMax = maximumIMUPackets - 1;
            }
                   
            if (selectionRangeSlider2.SelectedMin < 0)
            {
                selectionRangeSlider2.SelectedMin = 0;
            }

            minimumComboPackIndex = (int)(selectionRangeSlider2.SelectedMin / imuPacketSize);
            maximumComboPackIndex = (int)(selectionRangeSlider2.SelectedMax / imuPacketSize);
            /*
            if (plotData == null)
            {
                LoadPlotData(minimumComboPackIndex, maximumComboPackIndex);
            }
            else
            {
                ModifyPlotDataMax(maximumComboPackIndex);
                ModifyPlotDataMin(minimumComboPackIndex);
            }*/
            LoadPlotData(minimumComboPackIndex, maximumComboPackIndex);
            PlotData();
        }
        private void ShowImage(int comboPacketIndex, int imuPacketIndex)
        {
            byte[] tempImage = loadStream.comboDataPerPackList[comboPacketIndex].bitmaps[imuPacketIndex];
            MemoryStream memoryStream = new MemoryStream(tempImage);
            Bitmap bitmap = new Bitmap(memoryStream);
            pictureBox3.Image = bitmap;
        }
        private void ShowLine(int comboPacketIndex, int imuPacketIndex)
        {

        }

        private int currentStartIndexPlot;
        private int currentEndIndexPlot;
        private void LoadPlotData(int startIndex, int endIndex)
        {
            lineChart3.XAxis.MinValue = null;
            lineChart3.XAxis.MaxValue = null;
            plotData = new PlotData(maximumIMUPackets);
            currentStartIndexPlot = startIndex;
            currentEndIndexPlot = endIndex;
            for (int i = startIndex; i <= endIndex; i++)
            {
                IMUdataClass tempIMU = loadStream.comboDataPerPackList[i].iMUdataClass;
                plotData.AddDataFuture(tempIMU);
            }
        }

        private void ModifyPlotDataMax(int modifyUntilIndex)
        {
            if (modifyUntilIndex == currentEndIndexPlot)
            {
                return;
            }
            if (modifyUntilIndex < currentEndIndexPlot)
            {
                plotData.RemoveDataFuture(currentEndIndexPlot - modifyUntilIndex);
            }
            else
            {
                plotData.ChangeRollingWindows(modifyUntilIndex - currentEndIndexPlot);
                for (int i = currentEndIndexPlot; i < modifyUntilIndex; i++)
                {
                    IMUdataClass tempIMU = loadStream.comboDataPerPackList[i].iMUdataClass;
                    plotData.AddDataFuture(tempIMU);
                }
            }
            currentEndIndexPlot = modifyUntilIndex;

        }
        private void ModifyPlotDataMin(int modifyUntilIndex)
        {
            if (modifyUntilIndex == currentStartIndexPlot)
            {
                return;
            } 
            if (modifyUntilIndex > currentStartIndexPlot)
            {
            
                plotData.RemoveDataPast(modifyUntilIndex - currentStartIndexPlot);
            }
            else
            {
                plotData.ChangeRollingWindows(currentStartIndexPlot - modifyUntilIndex);
                for (int i = currentStartIndexPlot; i >= modifyUntilIndex; i--)
                {
                    IMUdataClass tempIMU = loadStream.comboDataPerPackList[i].iMUdataClass;
                    plotData.AddDataPast(tempIMU);
                }
            }
            currentStartIndexPlot = modifyUntilIndex;
        }
        private void InitliazeLineChart()
        {
            lineChart3.Title = "";
            lineChart3.XAxis.Title = "";
            lineChart3.YAxis.Title = "";
            lineChart3.ShowXCoordinates = false;
            lineChart3.ShowYCoordinates = true;
            lineChart3.YAxis.MinValue = -2.4;
            lineChart3.YAxis.MaxValue = 2.4;
            lineChart3.Theme = new Theme();
            lineChart3.Theme.CommonSeriesFills = lineChart3.Theme.CommonSeriesStrokes =
                new List<MindFusion.Drawing.Brush>
                {
                    new MindFusion.Drawing.SolidBrush(Color.Red),
                    new MindFusion.Drawing.SolidBrush(Color.Blue),
                    new MindFusion.Drawing.SolidBrush(Color.Green)


                };
            lineChart3.Theme.AxisLabelsBrush = new MindFusion.Drawing.SolidBrush(Color.White);
            lineChart3.Theme.LegendBorderStroke = new MindFusion.Drawing.SolidBrush(Color.Gray);
            lineChart3.Theme.LegendBackground = new MindFusion.Drawing.SolidBrush(Color.FromArgb(120, 230, 230, 230));

        }

        private void PlotData()
        {
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
                if (lineChart3.Series.Count == 3)
                {

                    lineChart3.Series[0] = seriesX;
                    lineChart3.Series[1] = seriesY;
                    lineChart3.Series[2] = seriesZ;
                }
                else
                {
                    lineChart3.Series.Add(seriesX);
                    lineChart3.Series.Add(seriesY);
                    lineChart3.Series.Add(seriesZ);
                }
            }
        }
        private void DetectActions()
        {
            actions = new List<StartEndIndex>();
            StartEndIndex defaultAction = new StartEndIndex(0, comboPackets - 1, action.DoNothing);
            actions.Add(defaultAction);
            action lastAction = action.DoNothing;
            int previousIndex = 0;
            int currentIndex = 0;
            foreach (ComboDataPerPack comboDataPerPack in loadStream.comboDataPerPackList)
            {
                if(comboDataPerPack.exerciseType.action == lastAction)
                {
                    currentIndex = loadStream.comboDataPerPackList.IndexOf(comboDataPerPack);
                }
                if (comboDataPerPack.exerciseType.action != lastAction || currentIndex == comboPackets - 1)
                {
                    StartEndIndex startEndIndex = new StartEndIndex(previousIndex, currentIndex, lastAction);
                    actions.Add(startEndIndex);
                    previousIndex = currentIndex + 1;
                    lastAction = comboDataPerPack.exerciseType.action;
                }

            }
        }

        private Thread playVideoThread;
        private int currentTrackBarValue;
        private void button2_Click(object sender, EventArgs e)
        {
            if (loadStream == null)
            {
                return;
            }
            if (playVideo)
            {
                playVideo = false;
                selectionRangeSlider2.Enabled = true;
                button1.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button2.Text = "Play";
                LoadPlotData(minimumComboPackIndex, maximumComboPackIndex);
                PlotData();
                return;
            }
            if (playVideoThread != null)
            {
                playVideo = false;
                while (playVideoThread.IsAlive) ;
            }
            button1.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            selectionRangeSlider2.Enabled = false;
            button2.Text = "Pause";
            playVideo = true;
            currentTrackBarValue = trackBar1.Value;
            playVideoThread = new Thread(PlayVideo);
            playVideoThread.Start();

        }

        private void PlayVideo()
        {
            if (loadStream == null)
            {
                return;
            }
            int comboPacketIndex = currentTrackBarValue / imuPacketSize;
            int imuPacketIndex = (currentTrackBarValue % imuPacketSize);
            int rollingWindows = (maximumComboPackIndex - minimumComboPackIndex) * imuPacketSize;
            plotData = new PlotData(rollingWindows);
            lineChart3.XAxis.MinValue = loadStream.comboDataPerPackList[comboPacketIndex].iMUdataClass.timePerValue[imuPacketIndex].ToOADate();
            lineChart3.XAxis.MaxValue = loadStream.comboDataPerPackList[maximumComboPackIndex].iMUdataClass.timePerValue[imuPacketSize - 1].ToOADate();
            while (comboPacketIndex < maximumComboPackIndex && playVideo)
            {
                double timePerStep = loadStream.comboDataPerPackList[comboPacketIndex].iMUdataClass.timePerStep;
                TimeSpan timeSpan = new TimeSpan(0,0,0,0, (int) timePerStep);
                plotData.AddDataFuture(loadStream.comboDataPerPackList[comboPacketIndex].iMUdataClass);
                PlotData();
                while (imuPacketIndex < imuPacketSize && playVideo)
                {
                    ShowImage(comboPacketIndex, imuPacketIndex);
                    imuPacketIndex ++;
                    Task.Delay(timeSpan).Wait();
                    //trackBar1.Value = comboPacketIndex * imuPacketSize + imuPacketIndex;
                }
                imuPacketIndex = 0;
                comboPacketIndex++;
            }

        }

        private void ModifySelected()
        {
            if (loadStream == null)
            {
                return;
            }
            int modifySelectionStartingIndex = (int)(selectionRangeSlider2.SelectedMin / imuPacketSize);
            int modifySelectionEndingIndex = (int)(selectionRangeSlider2.SelectedMax / imuPacketSize);
            Exercise currentSelectedExercise = loadStream.comboDataPerPackList[modifySelectionStartingIndex].exerciseType;
            //Prompt.ExerciseDialogData exerciseDialogData = Prompt.ShowExerciseDialog();
            Prompt.ExerciseDialogData exerciseDialogData = new Prompt.ExerciseDialogData(currentSelectedExercise.id, currentSelectedExercise.name, currentSelectedExercise.weight, currentSelectedExercise.repetitions);
            exerciseDialogData = Prompt.ShowExerciseDialog();
            currentSelectedExercise.id = exerciseDialogData.id;
            currentSelectedExercise.name = exerciseDialogData.name;
            currentSelectedExercise.weight = exerciseDialogData.weight;
            currentSelectedExercise.repetitions = exerciseDialogData.repetitions;
            for (int i = modifySelectionStartingIndex; i <= modifySelectionEndingIndex; i++)
            {
                loadStream.comboDataPerPackList[i].exerciseType = currentSelectedExercise;
            }
        }
        

        private void ExportSelectedToCSV()
        {
            if (loadStream == null)
            {
                return;
            }
            string tempFileName = loadStream.userId.ToString() + "_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
            //before your loop
            var csv = new StringBuilder();
            string userInformationID = "UserID,Gender,Height,Weight,Action";
            csv.AppendLine(userInformationID);
            string userInformation = string.Format("{0},{1},{2},{3}", loadStream.userId, loadStream.gender, loadStream.userHeight, loadStream.userWeight);
            csv.AppendLine(userInformation);
            string dataID = "Time(ms),aX,aY,aZ,gX,gY,gZ";
            csv.AppendLine(dataID);
            for (int i = minimumComboPackIndex; i <= maximumComboPackIndex; i++)
            {
                IMUdataClass tempIMU = loadStream.comboDataPerPackList[i].iMUdataClass;
                for (int j = 0; j < imuPacketSize; j++)
                {
                    string data = string.Format("{0},{1},{2},{3},{4},{5},{6}", tempIMU.timePerValue[j].TimeOfDay.TotalMilliseconds, tempIMU.accelerometer[j][0], tempIMU.accelerometer[j][1], tempIMU.accelerometer[j][2], tempIMU.gyroscope[j][0], tempIMU.gyroscope[j][1], tempIMU.gyroscope[j][2]);
                    csv.AppendLine(data);
                }

            }
            string filePath = outputDirectory + tempFileName;
            if (File.Exists(filePath + "_features" + ".csv"))
            {
                for (int i = 1; i < 99999; i++)
                {
                    string tempPath = filePath + "_" + i.ToString();
                    if (!File.Exists(tempPath + "_features" + ".csv"))
                    {
                        filePath = tempPath;
                        break;
                    }
                }
            }

            //after your loop
            File.WriteAllText(filePath + "_features" + ".csv", csv.ToString());

            csv = new StringBuilder();
            string exerciseInformationID = "Action,From(time),To(time),ExerciseID,ExerciseName,Repetitions,AppliedLoad";
            csv.AppendLine(exerciseInformationID);
            action lastAction = loadStream.comboDataPerPackList[minimumComboPackIndex].exerciseType.action;
            double timeStarted = loadStream.comboDataPerPackList[minimumComboPackIndex].iMUdataClass.timePerValue[0].TimeOfDay.TotalMilliseconds;
            double timeFinished = timeStarted;
            for (int i = minimumComboPackIndex; i <= maximumComboPackIndex; i++)
            {
                Exercise currentExercise = loadStream.comboDataPerPackList[i].exerciseType;
                if (currentExercise.action == lastAction)
                {
                    timeFinished = loadStream.comboDataPerPackList[i].iMUdataClass.timePerValue[imuPacketSize - 1].TimeOfDay.TotalMilliseconds;
                }
                if (currentExercise.action != lastAction || i == maximumComboPackIndex)
                {
                    Exercise previousExercise = loadStream.comboDataPerPackList[i - 1].exerciseType;
                    string data = string.Format("{0},{1},{2},{3},{4},{5},{6}", lastAction, timeStarted, timeFinished, previousExercise.id, previousExercise.name, previousExercise.repetitions, previousExercise.weight);
                    csv.AppendLine(data);
                    if (i != maximumComboPackIndex)
                    {
                        timeStarted = loadStream.comboDataPerPackList[i + 1].iMUdataClass.timePerValue[0].TimeOfDay.TotalMilliseconds;

                    }
                    lastAction = currentExercise.action;


                }

            }

            File.WriteAllText(filePath + "_labels" + ".csv", csv.ToString());


            MessageBox.Show("CSV file saved at: " + filePath);
        }

        private void PopulateActions()
        {
            listBox1.Items.Clear();
            foreach (StartEndIndex startEndIndex in actions)
            {
                string action = startEndIndex.action.ToString();
                if (startEndIndex.startIndex == 0 && startEndIndex.endIndex == comboPackets - 1)
                {
                    action = "All Data";
                }

                string timeStarted = loadStream.comboDataPerPackList[startEndIndex.startIndex].iMUdataClass.timePerValue[0].ToLongTimeString();
                string timeFinished= loadStream.comboDataPerPackList[startEndIndex.endIndex].iMUdataClass.timePerValue[imuPacketSize - 1].ToLongTimeString();
                string item = action + " From " + timeStarted + " To " + timeFinished;
                listBox1.Items.Add(item);
            }
        }

        private void SaveModifiedDataToFile()
        {
            WholeStream tempStream = loadStream;
            tempStream.endTime = DateTime.Now;
            string fileName = tempStream.userId;
            string filePath = outputDirectory + fileName;
            if (File.Exists(filePath + ".bin"))
            {
                for (int i = 1; i < 99999; i++)
                {
                    string tempPath = filePath + "_Modified_" + i.ToString() + ".bin";
                    if (!File.Exists(tempPath))
                    {
                        filePath = tempPath;
                        break;
                    }
                }
            }
            else
            {
                filePath = filePath + "_Modified_" + ".bin";
            }

            BinarySerialization.WriteToBinaryFile<WholeStream>(filePath, tempStream);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                return;
            }
            StartEndIndex currentAction = actions[listBox1.SelectedIndex];
            int minimum = currentAction.startIndex * imuPacketSize;
            int maximum = (currentAction.endIndex * imuPacketSize) + imuPacketSize - 1;
            selectionRangeSlider2.SelectedMin = minimum;
            selectionRangeSlider2.SelectedMax = maximum;
            trackBar1.Value = minimum;
            trackBar1_Scroll(sender, e);



        }

        private void button3_Click(object sender, EventArgs e)
        {
            ModifySelected();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExportSelectedToCSV();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveModifiedDataToFile();
        }

        private void ExportSelectionToVideo(int quality)
        {
            ShowProgressBar();
            string tempFileName = loadStream.userId.ToString() + "_" + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
            string filePath = outputDirectory + tempFileName;
            if (File.Exists(filePath + "_video_labels" + ".csv"))
            {
                for (int i = 1; i < 99999; i++)
                {
                    string tempPath = filePath + "_" + i.ToString();
                    if (!File.Exists(tempPath + "_video_labels" + ".csv"))
                    {
                        filePath = tempPath;
                        break;
                    }
                }
            }


            int width = 320;
            int height = 240;
            byte[] image = loadStream.comboDataPerPackList[0].bitmaps[0];
            MemoryStream stream = new MemoryStream(image);
            Bitmap temp = new Bitmap(stream);
            width = temp.Width;
            height = temp.Height;
            temp.Dispose();
            int framePerSecond = 25;
            double videoTime = 0f;
            int videoFrames = 0;
            for (int i = minimumComboPackIndex; i <= maximumComboPackIndex; i++)
            {
                long timeDif = loadStream.comboDataPerPackList[i].iMUdataClass.endTime - loadStream.comboDataPerPackList[i].iMUdataClass.startTime;
                videoTime += timeDif / 1000f;
                videoFrames += imuPacketSize;

            }

            int progressCounter = 0;
            framePerSecond = (int)(videoFrames /(2 *videoTime));
            // create instance of video writer
            VideoFileWriter writer = new VideoFileWriter();
            VideoCodec codec;
            // create new video file
            if (checkBox1.Checked)
            {
                codec = Accord.Video.FFMPEG.VideoCodec.VP9;
            }
            else
            {
                codec = Accord.Video.FFMPEG.VideoCodec.FFV1;
            }
            writer.Open(filePath + "_video" + ".avi", width, height, framePerSecond, codec);
            long pastTime = 0;
            long avgRequiredTime = 0;
            long avgCounter = 0;
            for (int i = minimumComboPackIndex; i <= maximumComboPackIndex; i++)
            {
                for (int j = 0; j < imuPacketSize; j++)
                {
                    avgCounter++;
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    byte[] tempImage = loadStream.comboDataPerPackList[i].bitmaps[j];
                    MemoryStream memoryStream = new MemoryStream(tempImage);
                    Bitmap bitmap = new Bitmap(memoryStream);
                    writer.WriteVideoFrame(bitmap);
                    bitmap.Dispose();
                    progressCounter++;
                    int progressValue = (int)(1000 * progressCounter / videoFrames);
                    ChangeProgressBarValue(progressValue);
                    stopwatch.Stop();
                    pastTime += stopwatch.ElapsedMilliseconds;
                    TimeSpan elapsedTime = TimeSpan.FromMilliseconds(pastTime);
                    string elapsedTimeString = elapsedTime.ToString();
                    avgRequiredTime += videoFrames * stopwatch.ElapsedMilliseconds;
                    TimeSpan remainingTime = TimeSpan.FromMilliseconds(avgRequiredTime/avgCounter);
                    string remainingTimeString = remainingTime.ToString();
                    label6.Text = elapsedTimeString + "/" + remainingTimeString;
                    label6.Refresh();
                }

            }
            writer.Close();


            var csv = new StringBuilder();
            string exerciseInformationID = "Action,From(Frame),To(Frame),ExerciseID,ExerciseName,Repetitions,AppliedLoad";
            csv.AppendLine(exerciseInformationID);
            action lastAction = loadStream.comboDataPerPackList[minimumComboPackIndex].exerciseType.action;
            int frameStarted = minimumComboPackIndex * imuPacketSize;
            int frameFinished = frameStarted;
            for (int i = minimumComboPackIndex; i <= maximumComboPackIndex; i++)
            {
                Exercise currentExercise = loadStream.comboDataPerPackList[i].exerciseType;
                if (currentExercise.action == lastAction)
                {
                    frameFinished = (i + 1) * imuPacketSize;
                }
                if (currentExercise.action != lastAction || i == maximumComboPackIndex)
                {
                    Exercise previousExercise = loadStream.comboDataPerPackList[i - 1].exerciseType;
                    string data = string.Format("{0},{1},{2},{3},{4},{5},{6}", lastAction, frameStarted, frameFinished, previousExercise.id, previousExercise.name, previousExercise.repetitions, previousExercise.weight);
                    csv.AppendLine(data);
                    if (i != maximumComboPackIndex)
                    {
                        frameStarted = frameFinished + 1;

                    }
                    lastAction = currentExercise.action;


                }

            }
            File.WriteAllText(filePath + "_video_labels" + ".csv", csv.ToString());


            MessageBox.Show("Video file saved at: " + filePath);
            HideProgressBar();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ExportSelectionToVideo(0);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ExportSelectionToVideo(1);
        }

        private void ShowProgressBar()
        {

            progressBar1.Visible = true;
            panel1.Visible = true;
            label6.Visible = true;
            label6.Enabled = true;
            progressBar1.Value = 0;
        }

        private void HideProgressBar()
        {
            progressBar1.Visible = false;
            panel1.Visible = false;
            label6.Visible = false;
            label6.Enabled = false;
        }
        private void ChangeProgressBarValue(int value)
        {
            progressBar1.Value = value;
        }

    }
}
