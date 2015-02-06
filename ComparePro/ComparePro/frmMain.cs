using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ComparePro.WebSites;
using GlobusLib;
using System.IO;
using ComparePro.Classes;
using System.Linq;
using System.Text.RegularExpressions;


namespace ComparePro
{
    public partial class frmMain : Form
    {
        FileLogger fileLogger = null;
        GlobussLogger globussLogger = null;
        DataManager dataManager = null;
        Logger logger = null;
        List<string> lstCsvData = new List<string>();
        WaitCallback callback;
        string FileName = string.Empty;

        public frmMain()
        {
            dataManager = new DataManager();
            globussLogger = new GlobussLogger();
            fileLogger = new FileLogger();
            logger = new Logger();
            FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) , "ComparePro\\Result.csv");
            FileLogger.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ComparePro\\Log\\Application.log");
            GlobussLogger.addToLogger += new EventHandler(GlobussLogger_addToLogger);
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            logger.LogStatus("Starting");
            CreateResultCsv();
        }

        private void StartCrawlingData(List<string> lst)
        {
            callback = new WaitCallback(GettingData);
            foreach (string str in lst)
            {
                if (!str.Contains("stock_extract_start"))
                {
                    ThreadPool.QueueUserWorkItem(callback,str);
                    //GettingData(str); 
                }       
            }
        }

        private void GettingData(object obj)
        {
            try
            {
                GlobusHttpHelper httpHelper = new GlobusHttpHelper();
                string str = (string)obj;
                string[] RowData = Regex.Split(str, ",");
                
                try
                {
                    string aa = RowData[1].ToString().Replace("\"", string.Empty);
                    Uri uri = new Uri(aa);
                    string Response = httpHelper.getHtmlfromUrl(uri);

                    if (!string.IsNullOrEmpty(Response))
                    {
                        string Idetifier = string.Empty;
                        string Stock = "ERR";
                        string Price = "ERR";

                        try
                        {
                            //string aa = GetIdentifier(Response, RowData);
                            Idetifier = GetValues(Response, RowData[0], RowData[2], RowData[3]);
                            logger.LogStatus("RowNumber --" + RowData[0] + " -- Idetifier -- " + Idetifier);
                        }
                        catch (Exception ex)
                        {
                            logger.LogDebug("RowNumber --" + RowData[0] + "--" + ex.Message);
                        }
                        try
                        {
                            //string bb = GetStock(Response, RowData);
                            Stock = GetValues(Response, RowData[0], RowData[5], RowData[6]);
                            logger.LogStatus("RowNumber --" + RowData[0] + " -- Stock -- " + Stock);
                        }
                        catch (Exception ex)
                        {
                            logger.LogDebug("RowNumber --" + RowData[0] + "--" + ex.Message);
                        }
                        try
                        {
                            //string CC = GetPrice(Response, RowData);
                             Price = GetValues(Response, RowData[0], RowData[8], RowData[9]).Replace("&pound;", "Amount"+" ");
                             if (!Price.Contains("Amount") && !Price.Contains("ERR"))
                             {
                                 Price = "Amout" + " " + Price;
                             }
                            logger.LogStatus("RowNumber --" + RowData[0] + " -- Price -- " + Price);
                        }
                        catch (Exception ex)
                        {
                            logger.LogDebug("RowNumber --" + RowData[0] + "--" + ex.Message);
                        }

                        string Data = RowData[0] + "," + "1" + "," + Stock + "," + Price;
                        logger.LogStatus("RowNumber --" + RowData[0] + " -- Data -- " + Data);
                        WriteData(Data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);

                    if (ex.Message.Contains("Invalid URI"))
                    {
                        string Data = RowData[0] + "," + "0" + "," + string.Empty + "," + string.Empty;
                        WriteData(Data);
                        logger.LogStatus("RowNumber --" + RowData[0] + "-- Invalid Url Setting Url Value 0");
                    }
                }

                
            }
            catch (Exception ex)
            {
                logger.LogDebug("Error --" + ex.Message);
            }


        }

        private string GetValues(string Response,string RowNumber ,string First, string End)
        {
            GlobusRegex g = new GlobusRegex();

            ///Checking Values 
            if (!string.IsNullOrEmpty(First) && !string.IsNullOrEmpty(End))
            {
                //Replacing (") from Resonse
                string FormattedResponse = Response.Replace("\"", string.Empty);

                //Getting Start Set String
                string strStart = First.ToString().Replace("\"", string.Empty).Trim();

                // Getting First Point from FormattedResponse
                int FirstPoint = FormattedResponse.IndexOf(strStart);

                if (FirstPoint > 0)
                {
                    // Creating NewResponse Start from FirstPoint
                    string NewResponse = FormattedResponse.Substring(FirstPoint);

                    //Getting End Set String
                    string strEnd = End.ToString().Replace("\"", string.Empty).Trim();

                    //Getting Second Point of NewResponse
                    int SecondPoint = NewResponse.IndexOf(strEnd);

                    if (SecondPoint > 0)
                    {
                        //Getting Result
                        string Result = NewResponse.Substring(0, SecondPoint).Replace(strStart, string.Empty);

                        //Stripping Html Tag For Final Result
                        string FinalResult = g.StripTagsRegex(Result).Replace(",", string.Empty).Replace("\r\n", string.Empty).Replace("\n", string.Empty);

                        return FinalResult;
                    }
                    else
                    {
                        logger.LogStatus("RowNumber --" + RowNumber + "-- End Tag is not in well Format -- " + strEnd);
                        return "ERR";
                    }
                }
                else
                {
                    logger.LogStatus("RowNumber --" + RowNumber + "-- Start Tag is not in well Format -- " + strStart);
                    return "ERR";
                }
            }
            else
            {
                return "ERR";
            }
        }

        //private string GetIdentifier(string Response, string[] RowData)
        //{
        //    GlobusRegex g = new GlobusRegex();

        //   ///Checking Values 
        //    if(!string.IsNullOrEmpty(RowData[2]) && !string.IsNullOrEmpty(RowData[3]))
        //    {
        //        //Replacing (") from Resonse
        //        string FormattedResponse = Response.Replace("\"", string.Empty);

        //        //Getting Start Set String
        //        string StartSet = RowData[2].ToString().Replace("\"", string.Empty);

        //        // Getting First Point from FormattedResponse
        //        int FirstPoint = FormattedResponse.IndexOf(StartSet);

        //        // Creating NewResponse Start from FirstPoint
        //        string NewResponse = FormattedResponse.Substring(FirstPoint);

        //        //Getting End Set String
        //        string EndSet = RowData[3].ToString().Replace("\"", string.Empty).Replace(" ", string.Empty);

        //        //Getting Second Point of NewResponse
        //        int SecondPoint = NewResponse.IndexOf(EndSet);

        //        //Getting Result
        //        string Result = NewResponse.Substring(0, SecondPoint);

        //        //Stripping Html Tag For Final Result
        //        string FinalResult = g.StripTagsRegex(Result);

        //        return FinalResult;
        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        //private string GetStock(string Response, string[] RowData)
        //{
        //    GlobusRegex g = new GlobusRegex();

        //    if (!string.IsNullOrEmpty(RowData[5]) && !string.IsNullOrEmpty(RowData[6]))
        //    {
        //        //Replacing (") from Resonse
        //        string FormattedResponse = Response.Replace("\"", string.Empty);

        //        //Getting Stock Extract Start String
        //        string StockExractStart = RowData[5].ToString().Replace("\"", string.Empty);

        //        // Getting First Point from FormattedResponse
        //        int FirstPoint = FormattedResponse.IndexOf(StockExractStart);

        //        // Creating NewResponse Start from FirstPoint
        //        string NewResponse = FormattedResponse.Substring(FirstPoint);

        //        //Getting Stock Extract End String
        //        string StockExractEnd = RowData[6].ToString().Replace("\"", string.Empty).Replace(" ", string.Empty);

        //        //Getting Second Point of NewResponse
        //        int SecondPoint = NewResponse.IndexOf(StockExractEnd);

        //        //Getting Result
        //        string Result = NewResponse.Substring(0, SecondPoint);

        //        //Stripping Html Tag For Final Result
        //        string FinalResult = g.StripTagsRegex(Result);

        //        return FinalResult;

        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        //private string GetPrice(string Response, string[] RowData)
        //{
        //    GlobusRegex g = new GlobusRegex();

        //    if (!string.IsNullOrEmpty(RowData[8]) && !string.IsNullOrEmpty(RowData[9]))
        //    {
        //        //Replacing (") from Resonse
        //        string FormattedResponse = Response.Replace("\"", string.Empty);

        //        //Getting Price Extarct Start String
        //        string PriceExtarctStart = RowData[8].ToString().Replace("\"", string.Empty);

        //        // Getting First Point from FormattedResponse
        //        int FirstPoint = FormattedResponse.IndexOf(PriceExtarctStart);

        //        // Creating NewResponse Start from FirstPoint
        //        string NewResponse = FormattedResponse.Substring(FirstPoint);

        //        //Getting Price Extarct End String
        //        string PriceExtarctEnd = RowData[9].ToString().Replace("\"", string.Empty).Replace(" ", string.Empty);

        //        //Getting Second Point of NewResponse
        //        int SecondPoint = NewResponse.IndexOf(PriceExtarctEnd);

        //        //Getting Result
        //        string Result = NewResponse.Substring(0, SecondPoint);

        //        //Stripping Html Tag For Final Result
        //        string FinalResult = g.StripTagsRegex(Result);

        //        return FinalResult;

        //    }
        //    else
        //    {
        //        return string.Empty;
        //    }
        //}

        private void CreateResultCsv()
        {
            
            if (!File.Exists(FileName))
            {
                string Data = "ID" + "," + "Valid URL" + "," + "StocK Status" + "," + "Price";
                WriteData(Data);
            }
            else
            { 
            
            }
        }

        private void WriteData(string Data)
        {
            Monitor.Enter(this);
            GlobusFileHelper.AppendStringToTextFileNewLine(Data, FileName);
            Monitor.Exit(this);
        }

        #region Logger
        public void LogMessage(string log)
        {
            GlobussLogger.LoggerEventArgs loggerEventArgs = new GlobussLogger.LoggerEventArgs(log);
            globussLogger.LogText(loggerEventArgs);

        }
        void GlobussLogger_addToLogger(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                if (e is GlobussLogger.LoggerEventArgs)
                {
                    GlobussLogger.LoggerEventArgs loggerEventArgs = e as GlobussLogger.LoggerEventArgs;
                    lstLog.Items.Add(loggerEventArgs.log);
                    lstLog.SelectedItem = lstLog.Items.Count - 1;
                    
                }
            }));
        }
        public void Log(string log)
        {
            GlobussLogger.LoggerEventArgs loggerEventArgs = new GlobussLogger.LoggerEventArgs(log);
            globussLogger.LogText(loggerEventArgs);

        } 
        #endregion

        private void btnUpload_Click(object sender, EventArgs e)
        {
            DialogResult Result = ofdFile.ShowDialog();

            if (Result == DialogResult.OK)
            {
                this.txtFile.BackColor = System.Drawing.SystemColors.Control;
                this.txtFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                txtFile.Text = ofdFile.FileName.ToString();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtFile.Text))
            {
                lstCsvData = GlobusFileHelper.readcsvfile(txtFile.Text.Trim()); //(Path.Combine(Application.StartupPath, "sample_feed.csv"));
                StartCrawlingData(lstCsvData);
            }
            else
            {
                this.txtFile.BackColor = System.Drawing.Color.Red;
                txtFile.Text = "Please Select File";
            }
        }


       

        private void Get(object obj)
        {
            List<string> lst = (List<string>)obj;
            foreach (string str1 in lst)
            {
                Monitor.Enter(this);
                Console.WriteLine(str1);
                GlobusFileHelper.AppendStringToTextFileNewLine(str1, Path.Combine(Application.StartupPath, "Log\\sample_feed1.csv"));
                Monitor.Exit(this);
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            Application.ExitThread();
        }
    }
}
