using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace Sam
{
    public partial class frmSam : Form
    {
        CanberraDeviceAccessLib.DeviceAccess objCamDevice;// = new CanberraDeviceAccessLib.DeviceAccess();
                    //objCamDevice = new CanberraDeviceAccessLib.DeviceAccess();
        Labo_NAATableAdapters.DetectorsSelectTableAdapter taDetectors = new Sam.Labo_NAATableAdapters.DetectorsSelectTableAdapter();
        Labo_NAA.DetectorsSelectDataTable dtDetectors = new Labo_NAA.DetectorsSelectDataTable();

//        NAATableAdapters.DetectorsRequestsSelectTableAdapter taReq = new Sam.NAATableAdapters.DetectorsRequestsSelectTableAdapter();
//        NAA.DetectorsRequestsSelectDataTable dtReq = new NAA.DetectorsRequestsSelectDataTable();

        SqlConnection m_Connection;// = new SqlConnection();
        SqlCommand m_Command;// = new SqlCommand("select top(1)* from DetectorsRequests where IsDone = 0 order by RequestDateTime asc", m_Connection);

        int Counter;

        //CanberraDeviceAccessLib.DeviceAccess objDET20;
        //CanberraDeviceAccessLib.DeviceAccess objDET21;
        //CanberraDeviceAccessLib.DeviceAccess objDET22;
        //CanberraDeviceAccessLib.DeviceAccess objDET23;

                        
        public frmSam()
        {
            InitializeComponent();
        }

        private void frmSam_Load(object sender, EventArgs e)
        {
            Counter = 0;
            taDetectors.Fill(dtDetectors, null);

            m_Connection = new SqlConnection("Data Source=PC1533;Initial Catalog=Labo_NAA;Integrated Security=True");
            m_Connection.Open();
            m_Command = new SqlCommand("select top(1)* from DetectorsRequests where IsDone = 0 order by RequestDateTime asc", m_Connection);
            
            //objDET20 = new CanberraDeviceAccessLib.DeviceAccess();
            //objDET21 = new CanberraDeviceAccessLib.DeviceAccess();
            //objDET22 = new CanberraDeviceAccessLib.DeviceAccess();
            //objDET23 = new CanberraDeviceAccessLib.DeviceAccess();

            //objDET20.Connect("DET-20", CanberraDeviceAccessLib.ConnectOptions.aReadOnly, CanberraDeviceAccessLib.AnalyzerType.aSpectralDetector, "", CanberraDeviceAccessLib.BaudRate.aUseSystemSettings);
            //objDET21.Connect("DET-21", CanberraDeviceAccessLib.ConnectOptions.aReadOnly, CanberraDeviceAccessLib.AnalyzerType.aSpectralDetector, "", CanberraDeviceAccessLib.BaudRate.aUseSystemSettings);
            //objDET22.Connect("DET-22", CanberraDeviceAccessLib.ConnectOptions.aReadOnly, CanberraDeviceAccessLib.AnalyzerType.aSpectralDetector, "", CanberraDeviceAccessLib.BaudRate.aUseSystemSettings);
            //objDET23.Connect("DET-23", CanberraDeviceAccessLib.ConnectOptions.aReadOnly, CanberraDeviceAccessLib.AnalyzerType.aSpectralDetector, "", CanberraDeviceAccessLib.BaudRate.aUseSystemSettings);
            
            //tmrVerify.Enabled = true;
        }


        //private float[] DetectorSpectrum(string Detector)
        //{
        //    string _T = null;// new string("");
        //    double _ET = new double();
        //    object _D = new object();


        //    objCamDevice = new CanberraDeviceAccessLib.DeviceAccess();
        //    objCamDevice.Connect(Detector, CanberraDeviceAccessLib.ConnectOptions.aReadOnly, CanberraDeviceAccessLib.AnalyzerType.aSpectralDetector, "", CanberraDeviceAccessLib.BaudRate.aUseSystemSettings);
            
        //    objCamDevice.SpectroscopyMeasurements(ref _T, ref _ET, ref _D);

        //    float[] spec;
        //    spec = (float[])_D;

        //    clsImage objI = new clsImage();
        //    byte[] spectrum;
        //    spectrum = objI.ConvertToByteArray(spec);

        //    NAATableAdapters.QueriesTableAdapter detUpdate = new Sam.NAATableAdapters.QueriesTableAdapter();
        //    detUpdate.DetectorsUpdate(Detector, null, null, null, null, null, null, null, null, null, null, null, spectrum,   null, null,null,null);

        //    return (spec);
            

        //}

        private void DetectorInfoUpdate(string detector, Boolean IsStartMCA)
        {
            string _T = null;// new string("");
            double _ET = new double();
            object _D = new object();
            float[] spec;
            float m_ElapsedLiveTime;
            float m_ElapsedRealTime;
            float m_PresetLiveTime;
            float m_PresetRealTime;
            float m_DT;
            DateTime m_AcqStartTime;
            clsImage objI = new clsImage();
            byte[] spectrum;
            int _mcaStatus;
            string _mcaStatusDescr;

            System.Windows.Forms.Application.DoEvents();

            CanberraDeviceAccessLib.DeviceAccess objCamDevice = new CanberraDeviceAccessLib.DeviceAccess();
            objCamDevice.Connect(detector, CanberraDeviceAccessLib.ConnectOptions.aReadOnly, CanberraDeviceAccessLib.AnalyzerType.aSpectralDetector, "", CanberraDeviceAccessLib.BaudRate.aUseSystemSettings);

            m_ElapsedLiveTime = System.Convert.ToSingle(objCamDevice.get_Param(CanberraDeviceAccessLib.ParamCodes.CAM_X_ELIVE, 1, 1));
            m_ElapsedRealTime = System.Convert.ToSingle(objCamDevice.get_Param(CanberraDeviceAccessLib.ParamCodes.CAM_X_EREAL, 1, 1));
            m_PresetLiveTime = System.Convert.ToSingle(objCamDevice.get_Param(CanberraDeviceAccessLib.ParamCodes.CAM_X_PLIVE,1,1));
            m_PresetRealTime = System.Convert.ToSingle(objCamDevice.get_Param(CanberraDeviceAccessLib.ParamCodes.CAM_X_PREAL, 0, 0));
            if (m_ElapsedRealTime < 0.001)
            {
                m_DT = 0.0f; //(1 - (ELiveTime / ERealTime)) * 100;
            }
            else
            {
                m_DT = (1 - (m_ElapsedLiveTime / m_ElapsedRealTime)) * 100;
            }

            lblEva_PREAL.Text  = objCamDevice.get_Param(CanberraDeviceAccessLib.ParamCodes.CAM_X_PREAL,0,0).ToString();
            if (objCamDevice.get_Param(CanberraDeviceAccessLib.ParamCodes.CAM_X_ASTIME, 1, 1).ToString() == " ")
            {
                m_AcqStartTime = System.DateTime.Now;
            }
            else
            {
                m_AcqStartTime = System.Convert.ToDateTime(objCamDevice.get_Param(CanberraDeviceAccessLib.ParamCodes.CAM_X_ASTIME, 1, 1));
            }
            objCamDevice.SpectroscopyMeasurements(ref _T, ref _ET, ref _D);

            spec = (float[])_D;

            spectrum = objI.ConvertToByteArray(spec);

            
            //string mcaStatus = objCamDevice.AnalyzerStatus.ToString();
            _mcaStatus = (int)objCamDevice.AnalyzerStatus;
            _mcaStatusDescr="";
            
            switch (_mcaStatus)
            {
                case 2054:
                    _mcaStatusDescr = "Acquiring_2054";
                    break;
                case 2080:
                    _mcaStatusDescr = "Idle";
                    //if ((m_ElapsedLiveTime <= m_PresetLiveTime) && (m_ElapsedRealTime <= m_PresetRealTime))
                    //{
                    //    _mcaStatusDescr = "Idle";
                    //}
                    //else
                    //{
                    //    _mcaStatusDescr = "Preset Reached";
                    //}
                    break;
                case 2084:
                    _mcaStatusDescr = "Acquiring";
                    break;
                case 2052:
                    _mcaStatusDescr = "Acquiring";
                    break;
                case 2048:
                    _mcaStatusDescr = "Idle";
                    break;
                //case 2168:
                //    _mcaStatusDescr = "Paused";
                //    break;
                //case 2224:
                //    _mcaStatusDescr = "Acquisition complete";
                //    break;

                default:
                    _mcaStatusDescr = "?????";
                    break;
            }


            Labo_NAATableAdapters.QueriesTableAdapter det = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();
            
            //det.DetectorsUpdate(detector, null, null, null, null, m_ElapsedRealTime, m_ElapsedLiveTime, null, null, null, null, null, spectrum, null, null, null);
            //det.DetectorsUpdate(detector, null, null, m_PresetRealTime, m_PresetLiveTime, m_ElapsedRealTime, m_ElapsedLiveTime, null, null, spectrum, null, null, null, _mcaStatus , _mcaStatusDescr , null, null, null, null, null, null, m_AcqStartTime, System.DateTime.Now, null, null, null);
            if (IsStartMCA)
            {
                det.MCAUpdate(detector, m_ElapsedRealTime, m_ElapsedLiveTime, null, null, spectrum, m_DT, _mcaStatus, _mcaStatusDescr, m_AcqStartTime, System.DateTime.Now);
            }
            else
            {
                det.MCAUpdate(detector, m_ElapsedRealTime, m_ElapsedLiveTime, null, null, spectrum, m_DT, _mcaStatus, _mcaStatusDescr, m_AcqStartTime, null);
            }
            

            objCamDevice.Disconnect();
            objCamDevice = null;

        
        }

        private Boolean DetectorStart(string detector, ref StringBuilder  _Message)
        {
            Boolean IsDetectorStart = false;
            try
            {
                objCamDevice = new CanberraDeviceAccessLib.DeviceAccess();
                objCamDevice.Connect(detector, CanberraDeviceAccessLib.ConnectOptions.aReadWrite, CanberraDeviceAccessLib.AnalyzerType.aSpectralDetector, "", CanberraDeviceAccessLib.BaudRate.aUseSystemSettings);
                objCamDevice.AcquireStart();
                IsDetectorStart = true;
                _Message.Append("");
                //objCamDevice.Disconnect();
                //objCamDevice = null;

            }
            catch (Exception err)
            {
                //MessageBox.Show(err.Message);
                IsDetectorStart = false;
                _Message.Append(err.Message);
                //objCamDevice.Connect(detector, CanberraDeviceAccessLib.ConnectOptions.aReadWrite, CanberraDeviceAccessLib.AnalyzerType.aSpectralDetector, "", CanberraDeviceAccessLib.BaudRate.aUseSystemSettings);
                //objCamDevice.VerifyInputDevicesEx();
                //objCamDevice.AcquireStart();

            }
            finally
            {
                //objCamDevice.AcquireStart();
                if (objCamDevice.IsConnected)
                {
                    objCamDevice.Disconnect();
                }
                objCamDevice = null;
            }

            return (IsDetectorStart);
        }


        private void DetectorSave(string detector, string AcqPath, string AcqFileName)
        {
            objCamDevice = new CanberraDeviceAccessLib.DeviceAccess();
            //objCamDevice.Connect(detector, CanberraDeviceAccessLib.ConnectOptions.aReadWrite, CanberraDeviceAccessLib.AnalyzerType.aSpectralDetector, "", CanberraDeviceAccessLib.BaudRate.aUseSystemSettings);
            objCamDevice.Connect(detector, CanberraDeviceAccessLib.ConnectOptions.aReadOnly, CanberraDeviceAccessLib.AnalyzerType.aSpectralDetector, "", CanberraDeviceAccessLib.BaudRate.aUseSystemSettings);

            objCamDevice.Save(AcqPath + AcqFileName, true);//  .AcquireStop(CanberraDeviceAccessLib.StopOptions.aNormalStop);
            objCamDevice.Disconnect();
            objCamDevice = null;
        
            // insert new acquisitionID in table ...
            ///int? _AcqID = new Int32();
            //NAATableAdapters.QueriesTableAdapter taAcq = new Sam.NAATableAdapters.QueriesTableAdapter();
            //taAcq.uspAc

        }



        private void DetectorStop(string detector)
        {
            objCamDevice = new CanberraDeviceAccessLib.DeviceAccess();
            objCamDevice.Connect(detector, CanberraDeviceAccessLib.ConnectOptions.aReadWrite, CanberraDeviceAccessLib.AnalyzerType.aSpectralDetector, "", CanberraDeviceAccessLib.BaudRate.aUseSystemSettings);
            objCamDevice.AcquireStop(CanberraDeviceAccessLib.StopOptions.aNormalStop);
            objCamDevice.Disconnect();
            objCamDevice = null;
        }

        private void DetectorClear(string detector)
        {
            objCamDevice = new CanberraDeviceAccessLib.DeviceAccess();
            objCamDevice.Connect(detector, CanberraDeviceAccessLib.ConnectOptions.aReadWrite, CanberraDeviceAccessLib.AnalyzerType.aSpectralDetector, "", CanberraDeviceAccessLib.BaudRate.aUseSystemSettings);
            objCamDevice.Clear();// AcquireStop(CanberraDeviceAccessLib.StopOptions.aNormalStop);
            objCamDevice.Disconnect();
            objCamDevice = null;

        }

        private void DetectorPause(string detector)
        {
            objCamDevice = new CanberraDeviceAccessLib.DeviceAccess();
            objCamDevice.Connect(detector, CanberraDeviceAccessLib.ConnectOptions.aReadWrite, CanberraDeviceAccessLib.AnalyzerType.aSpectralDetector, "", CanberraDeviceAccessLib.BaudRate.aUseSystemSettings);
            objCamDevice.AcquirePause(CanberraDeviceAccessLib.StopOptions.aNormalStop);
            objCamDevice.Disconnect();
            objCamDevice = null;


        }

        private void DetectorPreset(string detector, float PresetTime, Boolean IsRealTime)
        {
            objCamDevice = new CanberraDeviceAccessLib.DeviceAccess();
            objCamDevice.Connect(detector, CanberraDeviceAccessLib.ConnectOptions.aReadWrite, CanberraDeviceAccessLib.AnalyzerType.aSpectralDetector, "", CanberraDeviceAccessLib.BaudRate.aUseSystemSettings);

            if (IsRealTime)
            {
                objCamDevice.Clear();
                objCamDevice.SpectroscopyAcquireSetup(CanberraDeviceAccessLib.AcquisitionModes.aCountToRealTime , PresetTime, 0, 0, 0, 0);
                objCamDevice.Save(null,true);
                //objCamDevice.set_Param(CanberraDeviceAccessLib.ParamCodes.CAM_X_PREAL, 0, 0, PresetTime);
                //objCamDevice.Save("eva.xxx", true);
            }
            else
            {
                objCamDevice.SpectroscopyAcquireSetup(CanberraDeviceAccessLib.AcquisitionModes.aCountToLiveTime , PresetTime, 0, 0, 0, 0);
                objCamDevice.Save(detector, true);
            }


            //objCamDevice.AcquirePause(CanberraDeviceAccessLib.StopOptions.aNormalStop);
            //objCamDevice.sa
            objCamDevice.Disconnect();
            objCamDevice = null;

        }



        private void DoSomeAction(int RequestID, String DetectorName, float PresetRealTime, float PresetLiveTime, string ReqCode, string AcqPath, string AcqFileName, string AcqPathBackup, string AcqFileNameBackup, int SampleID, string SampleName, int Position, string IrradiationCode, int NrOfAcq)
        {
            lblInfo.Text = "Please Wait ...";
            DateTime start = DateTime.Now;

            if (ReqCode == RequestCode.mcaSave)
            {

                Labo_NAATableAdapters.DetectorsSelectTableAdapter taDet = new Sam.Labo_NAATableAdapters.DetectorsSelectTableAdapter();
                Labo_NAA.DetectorsSelectDataTable dtDet = new Labo_NAA.DetectorsSelectDataTable();

                taDet.Fill(dtDet, DetectorName);

                string _AcqPath;
                string _AcqFileName;
                string _AcqPathBackup;
                string _AcqFileNameBackup;
                int _SSID;
                float _PRealTime;
                DateTime _AcqStart;
                float _ERealTime;
                float _DT;
                int _Pos;
                int _NrOfAcq;



                _AcqPath = dtDet[0]["AcquisitionPath"].ToString() + "\\";
                _AcqFileName = dtDet[0]["AcquisitionFileName"].ToString() + ".cnf";
                _AcqPathBackup = dtDet[0]["AcquisitionPathBackup"].ToString() + "\\";
                _AcqFileNameBackup = dtDet[0]["AcquisitionFileNameBackup"].ToString() + ".cnf";
                _SSID = (int)dtDet[0]["SampleID"];
                _PRealTime = (float)dtDet[0]["PresetRealTime"];
                _AcqStart = (DateTime)dtDet[0]["AcquisitionStartTime"];
                _ERealTime = (float)dtDet[0]["ElapsedRealTime"];
                _DT = (float)dtDet[0]["DeadTimePercent"];
                _Pos = (int)dtDet[0]["Position"];
                _NrOfAcq = (int)dtDet[0]["NumberOfAcq"];
                                
                DetectorSave(DetectorName, _AcqPath, _AcqFileName);


                int _NrOfNextAcq = _NrOfAcq + 1;
                string _NextFileName;
                clsFileNaming objF = new clsFileNaming();
                objF.DetectorName = dtDet[0]["DetectorName"].ToString();
                objF.SubSampleName = dtDet[0]["SampleName"].ToString();
                objF.Position = _Pos;
                objF.NumberCurrentAcquisition = _NrOfNextAcq;
                _NextFileName = objF.FileNaming();



                Labo_NAATableAdapters.QueriesTableAdapter taRequest = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();
                taRequest.DetectorsRequestsUpdate(RequestID);

                Labo_NAATableAdapters.QueriesTableAdapter taSample = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();
                taSample.DetectorsUpdate(DetectorName, null, null, true, null, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, _NextFileName, null, _NextFileName, null, null, null, null, _NrOfNextAcq);


                // make copy to N drive

                // check if directory exists ...
                if (System.IO.Directory.Exists(_AcqPathBackup + "\\")) //"\\\\SCKSRV1\\GKD\\DOKUMENT\\SNM\\Labo_NAA\\Metingen\\Z987\\"))  //"c:\\Genie2k\\CAMFILES\\" + stripIrrCode.Text))
                {
                    // directory exists .. do nothing
                }
                else
                {
                    // create directory
                    System.IO.Directory.CreateDirectory(_AcqPathBackup + "\\"); //\\\\SCKSRV1\\GKD\\DOKUMENT\\SNM\\Labo_NAA\\Metingen\\Z987\\"); //c:\\Genie2k\\CAMFILES\\" + stripIrrCode.Text);
                }

                System.IO.File.Copy(_AcqPath + _AcqFileName, _AcqPathBackup + "\\" + _AcqFileName, true);//"\\\\SCKSRV1\\GKD\\DOKUMENT\\SNM\\Labo_NAA\\Metingen\\Z987\\" +AcqFileName,true);

                // insert new AcquisitionsID in table
                // must be done in SAM
                int? AcqID = new Int32();
                NAATableAdapters.QueriesTableAdapter taAcq = new Sam.NAATableAdapters.QueriesTableAdapter();
                taAcq.uspAcquisitionsInsert(_SSID, (int)_PRealTime, DetectorName, _AcqStart, _ERealTime, _DT, _Pos,
                    _AcqPath, _AcqFileName, _AcqPathBackup, _AcqFileName,ref AcqID);

                //Labo_NAATableAdapters.QueriesTableAdapter taDetUp = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();
                //taDetUp.DetectorsUpdate(DetectorName, null, null, null, null,null, null, null, null, null, null,
                //            null, null, null, null, null,null,null,null, null, null, null, null, null, null, 



                //////////int? _AcqID = new Int32();
                //////////NAATableAdapters.QueriesTableAdapter taAcq = new GaST.NAATableAdapters.QueriesTableAdapter();

                //////////taAcq.uspAcquisitionsInsert(m_SubSampleID, (int)m_PresetRealTime, m_detector, m_AcqStartDateTime, m_ElapsedRealTime, m_DeadTime, m_Position, ref _AcqID);



                //////////NAATableAdapters.uspSubSamplesSelectTableAdapter taSub = new GaST.NAATableAdapters.uspSubSamplesSelectTableAdapter();
                //////////NAA.uspSubSamplesSelectDataTable dtSub = new NAA.uspSubSamplesSelectDataTable();

                //////////taSub.Fill(dtSub, null, m_SubSampleID);
                //////////m_NrOfExistingAcq = (int)dtSub[0]["NrOfAcq"];





            }



            if (ReqCode == RequestCode.mcaClear)
            {
                DetectorClear(DetectorName);

                //NAATableAdapters.QueriesTableAdapter taDetStatus = new Sam.NAATableAdapters.QueriesTableAdapter();
//                taDetStatus.DetectorsUpdate(DetectorName, null, null, null, null, null, null, null, null, null, null, null, "Cleared", null, null, null, null, null, null, null, null, null, null, null, null, null);

                Labo_NAATableAdapters.QueriesTableAdapter taRequest = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();
                
                taRequest.DetectorsRequestsUpdate(RequestID);

                DetectorInfoUpdate(DetectorName, false);
            }

            if (ReqCode == RequestCode.mcaStart)
            {
                StringBuilder t = new StringBuilder();
                Boolean test = DetectorStart(DetectorName, ref t);

                if (test)
                {

                    //NAATableAdapters.QueriesTableAdapter taDetStatus = new Sam.NAATableAdapters.QueriesTableAdapter();
                    //taDetStatus.DetectorsUpdate(DetectorName, null, null, null, null, null, null, null, null, null, null, null, "Started", null, null, null, null, null, null, null, null, null, null, null, null, null);

                    Labo_NAATableAdapters.QueriesTableAdapter taRequest = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();

                    taRequest.DetectorsRequestsUpdate(RequestID);

                    DetectorInfoUpdate(DetectorName, true);
                }
                else
                {
                    MessageBox.Show("Could not start Acquisition on Detector : " + DetectorName);
                }
            }
            if (ReqCode == RequestCode.mcaStop)
            {
                // stop detector
                DetectorStop(DetectorName);

                //NAATableAdapters.QueriesTableAdapter taDetStatus = new Sam.NAATableAdapters.QueriesTableAdapter();

                //taDetStatus.DetectorsUpdate(DetectorName, null, null, null, null, null, null, null, null, null, null, null, "Stopped", null, null, null, null, null, null, null, null, null, null, null, null, null);


                Labo_NAATableAdapters.QueriesTableAdapter taRequest = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();

                taRequest.DetectorsRequestsUpdate(RequestID);

                DetectorInfoUpdate(DetectorName, false);
            }
            if (ReqCode == RequestCode.mcaPause)
            {
                // clear detector
                DetectorPause(DetectorName);
                //NAATableAdapters.QueriesTableAdapter taDetStatus = new Sam.NAATableAdapters.QueriesTableAdapter();
                //taDetStatus.DetectorsUpdate(DetectorName, null, null, null, null, null, null, null, null, null, null, null, "Paused", null, null, null, null, null, null, null, null, null, null, null, null, null);

                Labo_NAATableAdapters.QueriesTableAdapter taRequest = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();

                taRequest.DetectorsRequestsUpdate(RequestID);

                DetectorInfoUpdate(DetectorName, false);
            }
            if (ReqCode == RequestCode.mcaPREAL)   // settings for Preset Real Time
            {
                DetectorPreset(DetectorName, PresetRealTime, true);

                Labo_NAATableAdapters.QueriesTableAdapter taSample = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();

                taSample.DetectorsUpdate(DetectorName, null, null, false, PresetRealTime, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

                Labo_NAATableAdapters.QueriesTableAdapter taRequest = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();

                taRequest.DetectorsRequestsUpdate(RequestID);

                DetectorInfoUpdate(DetectorName, false);
            }

            if (ReqCode == RequestCode.mcaPLIVE)
            {
                DetectorPreset(DetectorName, PresetLiveTime, true);

                Labo_NAATableAdapters.QueriesTableAdapter taSample = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();
                
                taSample.DetectorsUpdate(DetectorName, null, null, false, null,  PresetLiveTime, null, null, null, null, null, null, null,
                    null, null, null, null, null, null, null, null, null, null, null, null, null, null);

                Labo_NAATableAdapters.QueriesTableAdapter taRequest = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();
                
                taRequest.DetectorsRequestsUpdate(RequestID);

                DetectorInfoUpdate(DetectorName, false);
            }

            if (ReqCode == RequestCode.setSample)
            {

                Labo_NAATableAdapters.QueriesTableAdapter taSample = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();

                taSample.DetectorsUpdate(DetectorName, null, null, false ,null, null, null, null, null, null, null, null, null,
                    null, null, null, SampleID, SampleName, null, null, null, null, null, null, null, IrradiationCode, NrOfAcq);

                Labo_NAATableAdapters.QueriesTableAdapter taRequest = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();
                
                taRequest.DetectorsRequestsUpdate(RequestID);

                DetectorInfoUpdate(DetectorName, false);
            }

            if (ReqCode == RequestCode.setSampleAll)
            {
                Labo_NAATableAdapters.QueriesTableAdapter taSample = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();

                taSample.DetectorsUpdate(DetectorName, null, null,false, null, null, null, null, null, null, null, null, Position,
                    null, null, null, SampleID, SampleName, AcqPath, AcqFileName, AcqPathBackup, AcqFileNameBackup, null, null, null, IrradiationCode, NrOfAcq);

                Labo_NAATableAdapters.QueriesTableAdapter taRequest = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();

                taRequest.DetectorsRequestsUpdate(RequestID);

                DetectorInfoUpdate(DetectorName, false);


            }



            if (ReqCode == RequestCode.setFileNaming)
            {
                Labo_NAATableAdapters.QueriesTableAdapter taSample = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();

                taSample.DetectorsUpdate(DetectorName, null, null, false, null, null, null, null, null, null, null, null, Position,
                    null, null, null, null, null, AcqPath, AcqFileName, AcqPathBackup, AcqFileNameBackup, null, null, null, null, null);

                Labo_NAATableAdapters.QueriesTableAdapter taRequest = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();

                taRequest.DetectorsRequestsUpdate(RequestID);

                DetectorInfoUpdate(DetectorName, false);

            }


            if (ReqCode == RequestCode.setPosition)
            {

                Labo_NAATableAdapters.QueriesTableAdapter taSample = new Sam.Labo_NAATableAdapters.QueriesTableAdapter(); 
                
                taSample.DetectorsUpdate(DetectorName, null, null, false, null, null, null, null, null, null, null, null,  Position ,
                    null, null, null, null, null, null, null, null, null, null, null, null, null, null);

                Labo_NAATableAdapters.QueriesTableAdapter taRequest = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();
                
                taRequest.DetectorsRequestsUpdate(RequestID);

                DetectorInfoUpdate(DetectorName, false);

            }

            if (ReqCode == RequestCode.setPathFile) // and backup
            {
                Labo_NAATableAdapters.QueriesTableAdapter taSample = new Sam.Labo_NAATableAdapters.QueriesTableAdapter(); 
                
                taSample.DetectorsUpdate(DetectorName, null, null, false, null, null, null, null, null, null, null, null, null,
                    null, null, null, null, null, AcqPath, AcqFileName, AcqPathBackup, AcqFileNameBackup, null, null, null, null, null);

                Labo_NAATableAdapters.QueriesTableAdapter taRequest = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();
                
                taRequest.DetectorsRequestsUpdate(RequestID);

                DetectorInfoUpdate(DetectorName, false);

            }


            if (ReqCode == RequestCode.setComments)
            {
                DetectorInfoUpdate(DetectorName, false);
            }

            if (ReqCode == RequestCode.setIrrCode)
            {
                DetectorInfoUpdate(DetectorName, false);
            }

            if (ReqCode == "SetNumberOfAcq")
            {
                DetectorInfoUpdate(DetectorName, false);
            }



            if (ReqCode == RequestCode.setLockOn) // lock current settings so nobody can change detector
            {

                Labo_NAATableAdapters.QueriesTableAdapter taDetStatus = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();

                taDetStatus.DetectorsUpdate(DetectorName, null, true, null,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null,null,null);

                Labo_NAATableAdapters.QueriesTableAdapter taRequest = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();
                
                taRequest.DetectorsRequestsUpdate(RequestID);

                DetectorInfoUpdate(DetectorName, false);
            }

            if (ReqCode == RequestCode.setLockOff) // lock current settings so nobody can change detector
            {

                Labo_NAATableAdapters.QueriesTableAdapter taDetStatus = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();

                taDetStatus.DetectorsUpdate(DetectorName, null, false,null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

                Labo_NAATableAdapters.QueriesTableAdapter taRequest = new Sam.Labo_NAATableAdapters.QueriesTableAdapter();

                taRequest.DetectorsRequestsUpdate(RequestID);

                DetectorInfoUpdate(DetectorName, false);
            }


            lblInfo.Text = "";
            lblTotalTime.Text = DateTime.Now.Subtract(start).TotalMilliseconds.ToString();
            //DetectorInfoUpdate(DetectorName, false);
            tmrWatchDog.Enabled = true;


        }




        private void tmrMonitor_Tick(object sender, EventArgs e)
        {
            tmrMonitor.Enabled = false;
            tmrWatchDog.Enabled = false;
          
            DoMonitorRefresh();
            DateTime start = DateTime.Now;
            lblMonitor.Text = DateTime.Now.Subtract(start).TotalMilliseconds.ToString();

            tmrMonitor.Enabled = true;
            tmrWatchDog.Enabled = true;

        }

        private void DoMonitorRefresh()
        {

           
            
            string detName;
            foreach (DataRow r in dtDetectors)
            {
                detName = r["DetectorName"].ToString();
                
                DetectorInfoUpdate(detName, false);
                
            }

        }


        private void frmSam_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_Connection.Close();
            m_Connection = null;

            //objDET20.Disconnect();
            //objDET21.Disconnect();
            //objDET22.Disconnect();
            //objDET23.Disconnect();

            //objDET20 = null;
            //objDET21 = null;
            //objDET22 = null;
            //objDET23 = null;

        }

        private void tmrWatchDog_Tick(object sender, EventArgs e)
        {
            tmrWatchDog.Enabled = false;
            
            int RequestID;
            string DetectorName;
            float PresetRealTime;
            float PresetLiveTime;
            string ReqCode;
            string AcqPath;
            string AcqFileName;
            string AcqPathBackup;
            string AcqFileNameBackup;
            int SampleID;
            string SampleName;
            string Comments;
            int Position;
            string IrradiationCode;
            int NumberOfAcq;
            DateTime RequestDateTime;
            Boolean IsDone;

            Boolean IsTrigger = false;

            //taReq.Fill(dtReq);

            
            //SqlConnection m_Connnection;
            //SqlCommand m_Command;
            SqlDataReader m_DataReader;
            //m_Connnection = new SqlConnection("Data Source=PC1533;Initial Catalog=Labo_NAA;Integrated Security=True");
            //m_Connection.Open();
            //m_Command = new SqlCommand("select top(1)* from DetectorsRequests where IsDone = 0 order by RequestDateTime asc", m_Connection);
            m_DataReader = m_Command.ExecuteReader();

            lblWatchDog.Text = System.DateTime.Now.ToLocalTime().ToString();

            if (m_DataReader.HasRows)
            //if (dtReq.Rows.Count > 0)
            {
                m_DataReader.Read();
                lblWatchDog.Text = "action "  ;
                IsTrigger = true;
                tmrWatchDog.Enabled = false;
                RequestID = (int)m_DataReader.GetValue(0);
                DetectorName = m_DataReader.GetValue(1).ToString();
                if (m_DataReader.IsDBNull(2))
                {
                    PresetRealTime = -1;
                }
                else
                {
                    PresetRealTime = (float)m_DataReader.GetValue(2);
                }
                if (m_DataReader.IsDBNull(3))
                {
                    PresetLiveTime = -1;
                }
                else
                {
                    PresetLiveTime = (float)m_DataReader.GetValue(3);
                }
                ReqCode = m_DataReader.GetValue(4).ToString();
                if (m_DataReader.IsDBNull(5))
                {
                    AcqPath = null;// "";
                }
                else
                {
                    AcqPath = m_DataReader.GetValue(5).ToString();
                }
                if (m_DataReader.IsDBNull(6))
                {
                    AcqFileName = null; // "";
                }
                else
                {
                    AcqFileName = m_DataReader.GetValue(6).ToString();
                }
                if (m_DataReader.IsDBNull(7))
                {
                    AcqPathBackup = null;
                }
                else
                {
                    AcqPathBackup = m_DataReader.GetValue(7).ToString();
                }

                if (m_DataReader.IsDBNull(8))
                {
                    AcqFileNameBackup = null;
                }
                else
                {
                    AcqFileNameBackup = m_DataReader.GetValue(8).ToString();
                }
                

                if (m_DataReader.IsDBNull(9))
                {
                    SampleID = -1;
                }
                else
                {
                    SampleID = (int)m_DataReader.GetValue(9);
                }
                if (m_DataReader.IsDBNull(10))
                {
                    SampleName = null; // "";
                }
                else
                {
                    SampleName = m_DataReader.GetValue(10).ToString(); 
                }
                if (m_DataReader.IsDBNull(11))
                {
                    Comments = null; // "";
                }
                else
                {
                    Comments = m_DataReader.GetValue(11).ToString();
                }

                if (m_DataReader.IsDBNull(12))
                {
                    Position = -1; // "";
                }
                else
                {
                    Position = (int)m_DataReader.GetValue(12);
                }

                if (m_DataReader.IsDBNull(13))
                {
                    IrradiationCode = null; // "";
                }
                else
                {
                    IrradiationCode = m_DataReader.GetValue(13).ToString();
                }

                if (m_DataReader.IsDBNull(14))
                {
                    NumberOfAcq = -1; // "";
                }
                else
                {
                    NumberOfAcq = (int)m_DataReader.GetValue(14);
                }
                
                RequestDateTime = (DateTime)m_DataReader.GetValue(15);
                m_DataReader.Close();
                //m_Connection.Close();
                DoSomeAction(RequestID, DetectorName, PresetRealTime, PresetLiveTime, ReqCode, AcqPath, AcqFileName, AcqPathBackup, AcqFileNameBackup, SampleID, SampleName, Position, IrradiationCode, NumberOfAcq);
                //DoMonitorRefresh();

                DoMonitorRefresh();
            }
            else
            {

                m_DataReader.Close();
                //m_Connection.Close();
                //m_Connnection = null;

            }


            if (m_DataReader.IsClosed)
            {
                // do nothing
            }
            else
            {
                m_DataReader.Close();
                //m_Connection.Close();
                //m_Connnection = null;

            }


            //Counter++;

            //if (Counter > 20)
            //{
            //    Counter = 0;
            //    DoMonitorRefresh();
            //}
            tmrWatchDog.Enabled = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }



    }
}
