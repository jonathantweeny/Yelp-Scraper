using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using GlobusLib;

namespace ComparePro.Classes
{
    public class DataManager
    {
        Logger logger = null;

        public DataManager()
        {
            logger = new Logger();
            try
            {
                if (!File.Exists(Environment.GetFolderPath
                        (Environment.SpecialFolder.LocalApplicationData) + "\\ComparePro"))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath
                    (Environment.SpecialFolder.LocalApplicationData) + "\\ComparePro");


                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
