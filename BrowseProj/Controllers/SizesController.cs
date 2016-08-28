using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;

namespace BrowseProj.Controllers
{
    public class SizesController : ApiController
    {
        


        public int[] Post([FromBody]string currentpath)
        {
            int[] sizes = { 0, 0, 0 };
            List<string> filelist = new List<string>();
            Search(currentpath, sizes, filelist);
            return sizes;
        }




        private void Search(string dir, int[] sizes, List<string> filelist)
        {
            try
            {

                CountBySize(System.IO.Directory.GetFiles(dir, "*", SearchOption.AllDirectories), sizes);
            }
            catch (Exception e)
            {
                filelist.Clear();          
                DirectoryInfo dir_info = new DirectoryInfo(dir);
                SearchManually(dir_info, filelist);
                CountBySize(filelist, sizes);
            }

        }

        private void SearchManually(DirectoryInfo dir_info, List<string> filelist)
        {
            try
            {
                foreach (DirectoryInfo subdir_info in dir_info.GetDirectories())
                {
                    SearchManually(subdir_info, filelist);
                }
            }
            catch
            {
            }
            try
            {
                foreach (FileInfo file_info in dir_info.GetFiles())
                {
                    filelist.Add(file_info.FullName);
                }
            }
            catch
            {
            }

        }

        private void CountBySize(IEnumerable<string> filelist, int[] sizes)
        {
            foreach (string file in filelist)
            {
                try
                {
                    var size = (new System.IO.FileInfo(file).Length) / 1048576; //

                    if (size > 100)
                    {
                        sizes[2]++;
                    }
                    else
                    {
                        if (size < 10)
                        {
                            sizes[0]++;
                        }
                        else
                        {
                            sizes[1]++;
                        }

                    }
                }
                catch
                { }
                
            }
        }


    }      
}
