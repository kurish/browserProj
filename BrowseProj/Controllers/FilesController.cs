using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BrowseProj.Controllers
{
    public class FilesController : ApiController
    {
       
        public string[] Post([FromBody]string currentpath)
        {

            string[] response;

            try
            {
                response = System.IO.Directory.GetFiles(currentpath, "*.*");
                return response;
            }


            catch
            {
                response = new string[] { };
                return response;
            }
        }

       
    }
}
