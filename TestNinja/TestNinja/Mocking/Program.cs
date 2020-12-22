using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    class Program
    {
        public static void Main() 
        {
            var videoService = new VideoService();
            var title = videoService.ReadVideoTitle();
        }
    }
}
