using System;
using System.Collections.Generic;
using System.Linq;

namespace DataRecordClasses
{
    [Serializable]
    public class IMUdataClass : IDisposable
    {
        public long index;
        public long userID;
        public long deviceID;
        public long startTime;
        public long endTime;
        public int weight;
        public int distance;
        public double[][] accelerometer;
        public double[][] gyroscope;
        public DateTime[] timePerValue;
        public double timePerStep;
        public int packetSize;
        public IMUdataClass(long _index, long _userID, long _deviceID, long _startTime, long _endTime, int _weight, int _distance, double[][] _accelerometer, double[][] _gyroscope)
        {
            index = _index;
            userID = _userID;
            deviceID = _deviceID;
            startTime = _startTime;
            endTime = _endTime;
            weight = _weight;
            distance = _distance;
            accelerometer = _accelerometer;
            gyroscope = _gyroscope;
            packetSize = accelerometer.Length;
            timePerValue = new DateTime[accelerometer.Length];
            FillTimePerValue();
           
        }

        public void FillTimePerValue()
        {
            long timeDif = endTime - startTime;
            timePerStep = (double)timeDif / (double)(timePerValue.Length - 1);
            timePerValue[0] = DateTime.Now;
            for (int i = 1; i < timePerValue.Length; i++)
            {
                timePerValue[i] = timePerValue[0].AddMilliseconds(timePerStep * i);
            }
        }

        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        ~IMUdataClass()
        {
            Dispose(false);
        }
    }

    [Serializable]
    public class ComboDataPerPack
    {
        public long index;
        public int packetLength;
        public IMUdataClass iMUdataClass;
        public List<byte[]> bitmaps = new List<byte[]>();
        public Exercise exerciseType;
        public bool badForm;
        public ComboDataPerPack(long _index, IMUdataClass _iMUdataClass, List<byte[]> _bitmaps, Exercise _exerciseType, bool _badForm = false)
        {
            index = _index;
            iMUdataClass = _iMUdataClass;
            bitmaps = _bitmaps;
            packetLength = _bitmaps.Count;
            exerciseType = _exerciseType;
            badForm = _badForm;

        }

    }

    [Serializable]
    public enum action
    {
        DoNothing,
        Walking,
        Exercising
    };

    [Serializable]
    public class Exercise
    {
        public action action = action.DoNothing;
        public int id;
        public int type;
        public string name;
        public int repetitions;
        public float weight;

        public Exercise(action _action)
        {
            action = _action;
        }

    }

    [Serializable]
    public class WholeStream
    {
        public string userId;
        public DateTime startTime;
        public DateTime endTime;
        public float userHeight;
        public float userWeight;
        public string gender;
        public List<ComboDataPerPack> comboDataPerPackList = new List<ComboDataPerPack>();

        public void OffloadToFile()
        {

        }
        public void AppendComboDataPack(ComboDataPerPack comboDataPerPack)
        {
            comboDataPerPackList.Add(comboDataPerPack);
        }


    }
    public class PlotData
    {
        public List<ThreeAxisData> accelerometer = new List<ThreeAxisData>();
        public List<double> accelerometerX = new List<double>();
        public List<double> accelerometerY = new List<double>();
        public List<double> accelerometerZ = new List<double>();
        public List<double> time = new List<double>();
        public List<ThreeAxisData> gyroscope = new List<ThreeAxisData>();
        public List<DateTime> dateTime = new List<DateTime>();
        public int rollingWindowLength = 112;
        public List<IMUdataClass> imuList = new List<IMUdataClass>();
        private int packetSize = 0;

        public PlotData (int _rollingWindowLength)
        {
            rollingWindowLength = _rollingWindowLength;
        }
        public void AddDataFuture(IMUdataClass iMUdata)
        {
            packetSize = iMUdata.accelerometer.Length;
            if (accelerometer.Count > rollingWindowLength)
            {
                accelerometer.RemoveRange(0, packetSize);
                accelerometerX.RemoveRange(0, packetSize);
                accelerometerY.RemoveRange(0, packetSize);
                accelerometerZ.RemoveRange(0, packetSize);
                gyroscope.RemoveRange(0, packetSize);
                dateTime.RemoveRange(0, packetSize);
                time.RemoveRange(0, packetSize);
                imuList.RemoveAt(0);
            }
            for (int i = 0; i < packetSize; i++)
            {
                accelerometer.Add(new ThreeAxisData(iMUdata.accelerometer[i][0], iMUdata.accelerometer[i][1], iMUdata.accelerometer[i][2]));
                accelerometerX.Add(iMUdata.accelerometer[i][0]);
                accelerometerY.Add(iMUdata.accelerometer[i][1]);
                accelerometerZ.Add(iMUdata.accelerometer[i][2]);
                gyroscope.Add(new ThreeAxisData(iMUdata.gyroscope[i][0], iMUdata.gyroscope[i][1], iMUdata.gyroscope[i][2]));
                dateTime.Add(iMUdata.timePerValue[i]);
                time.Add(iMUdata.timePerValue[i].ToOADate());
            }
            imuList.Add(iMUdata);
        }
        public void AddDataPast(IMUdataClass iMUdata)
        {
            packetSize = iMUdata.accelerometer.Length;
            int amountToRemove = packetSize;
            int index = accelerometer.Count - amountToRemove;
            if (accelerometer.Count > rollingWindowLength)
            {

                accelerometer.RemoveRange(index, amountToRemove);
                accelerometerX.RemoveRange(index, amountToRemove);
                accelerometerY.RemoveRange(index, amountToRemove);
                accelerometerZ.RemoveRange(index, amountToRemove);
                gyroscope.RemoveRange(index, amountToRemove);
                dateTime.RemoveRange(index, amountToRemove);
                time.RemoveRange(index, amountToRemove);
                imuList.RemoveAt(imuList.Count - 1);

            }
            for (int i = packetSize - 1; i >= 0; i--)
            {
                accelerometer.Insert(0, (new ThreeAxisData(iMUdata.accelerometer[i][0], iMUdata.accelerometer[i][1], iMUdata.accelerometer[i][2])));
                accelerometerX.Insert(0, iMUdata.accelerometer[i][0]);
                accelerometerY.Insert(0, iMUdata.accelerometer[i][1]);
                accelerometerZ.Insert(0, iMUdata.accelerometer[i][2]);
                gyroscope.Insert(0, (new ThreeAxisData(iMUdata.gyroscope[i][0], iMUdata.gyroscope[i][1], iMUdata.gyroscope[i][2])));
                dateTime.Insert(0, (iMUdata.timePerValue[i]));
                time.Insert(0, iMUdata.timePerValue[i].ToOADate());
                imuList.Insert(0, iMUdata);
            }
       
        }
        public void RemoveDataPast(int comboPacksToRemove)
        {
            if (accelerometer.Count == 0)
            {
                return;
            }
            int amountToRemove = comboPacksToRemove * packetSize;
            if (accelerometer.Count >= rollingWindowLength)
            {
                accelerometer.RemoveRange(0, amountToRemove);
                accelerometerX.RemoveRange(0, amountToRemove);
                accelerometerY.RemoveRange(0, amountToRemove);
                accelerometerZ.RemoveRange(0, amountToRemove);
                gyroscope.RemoveRange(0, amountToRemove);
                dateTime.RemoveRange(0, amountToRemove);
                time.RemoveRange(0, amountToRemove);
                imuList.RemoveRange(0, comboPacksToRemove);

            }
            rollingWindowLength = accelerometer.Count;
        }
        public void RemoveDataFuture(int comboPacksToRemove)
        {
            if (accelerometer.Count == 0)
            {
                return;
            }
            int amountToRemove = (comboPacksToRemove * packetSize);
            int index = accelerometer.Count - amountToRemove;
            if (accelerometer.Count >= rollingWindowLength)
            {

                accelerometer.RemoveRange(index, amountToRemove);
                accelerometerX.RemoveRange(index, amountToRemove);
                accelerometerY.RemoveRange(index, amountToRemove);
                accelerometerZ.RemoveRange(index, amountToRemove);
                gyroscope.RemoveRange(index, amountToRemove);
                dateTime.RemoveRange(index, amountToRemove);
                time.RemoveRange(index, amountToRemove);
                imuList.RemoveRange(index/packetSize, comboPacksToRemove);

            }
            rollingWindowLength = accelerometer.Count;
        }
        public void ChangeRollingWindows(int comboPacksAmount)
        {
            rollingWindowLength += comboPacksAmount * packetSize;
        }
        public bool IsReadyToPlot()
        {
            if (accelerometer.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
    public class ThreeAxisData
    {
        public double x;
        public double y;
        public double z;

        public ThreeAxisData(double _x, double _y, double _z)
        {
            x = _x;
            y = _y;
            z = _z;

        }
    }

    public class StartEndIndex
    {
        public int startIndex = 0;
        public int endIndex = 0;
        public action action = action.DoNothing;

        public StartEndIndex(int _startIndex, int _endIndex, action _action)
        {
            startIndex = _startIndex;
            endIndex = _endIndex;
            action = _action;
        }
    }

}
