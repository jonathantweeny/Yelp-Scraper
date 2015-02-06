using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ComparePro.Classes;
using GlobusLib;



namespace ComparePro.WebSites
{
    public class TheFunkyGroup
    {
        private WebBrowser wbMain;
        Methods methods = new Methods();

        Logger logger = new Logger();
        FileLogger filelogger = new FileLogger();
 
        public TheFunkyGroup(ref WebBrowser wb)
        {
            wbMain = wb;
            wbMain.ScriptErrorsSuppressed = true;
            wbMain.DocumentCompleted +=new WebBrowserDocumentCompletedEventHandler(wbMain_DocumentCompleted);
            Start();
            logger.LogStatus("Starting1");
        }

        private void wbMain_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            switch (methods)
            {
                case Methods.ExtractData:
                    ExtractDatas();
                    break;
            }
        }

        private void Start()
        {
            wbMain.Navigate(Urls.TheFunkyGroupUrl);
            methods = Methods.ExtractData;
        }

        private void ExtractDatas()
        {
            string aa = wbMain.DocumentText;

            GlobusRegex globusRegex = new GlobusRegex();

            string name = globusRegex.GetH1Tag(aa);
            string Price = globusRegex.GetH3Tag(aa);


        }

        private enum Methods
        {
            Home, ExtractData
        }
       

    }
}
