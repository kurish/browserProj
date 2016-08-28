using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;


namespace BrowseProj.Controllers
{
    public class FoldersController : ApiController
    {

        public string[] Get()
        {
            List<string> drives = new List<string>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in allDrives)
            {
                drives.Add(drive.Name.Replace("\\", ""));
                
            }

            return drives.ToArray();
        }
        
        public string[] Post([FromBody]string currentpath)
        {
            string[] response;

            try {
                    response = Directory.GetDirectories(currentpath, "*");
                    return response;
            }
                           
            catch {
                    response = new string[] { };
                    return response;
            }
            
        }

        
        
    }
}
