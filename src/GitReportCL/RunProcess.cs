

namespace GitReportCL
{
    using System;
    using System.Diagnostics;
    using System.IO;

    class RunProcess
    {
        public Validation validate = new Validation();
        private string SetDirectory()
        {
            Console.WriteLine("Set directory for raport.");
            return validate.PathValidation(Console.Read().ToString());
        }
        private string ProcessInitialization(string arg)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("git");
            startInfo.WorkingDirectory = validate.setGitRep.gitArguments[2];
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.Arguments = arg;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            string processTemp = process.StandardOutput.ReadToEnd();
            process.CloseMainWindow();
            process.Dispose();
            return processTemp;
        }
        /*private string ProcessInitializationOutToBig(string arg)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("git");
            startInfo.WorkingDirectory = validate.setGitRep.gitArguments[2];
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.Arguments = arg;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            StreamReader reader = process.StandardOutput;
            byte[] byteArray = new byte[int.MaxValue];
            int count = 0;
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            encoding.GetBytes(ProcessDiff());
            while ( byteArray[count] = encoding.GetBytes(process.StandardOutput.ReadLine()))
            string processTemp = process.StandardOutput.ReadToEnd();
            //Console.WriteLine("Co proces wyrzuca: {0}",processTemp);
            process.CloseMainWindow();
            process.Dispose();
            return processTemp;
        }*/
        private string ArgumentsForBefore()
        {
            //Console.WriteLine("EndDate: {0}", validate.setGitRep.gitArguments[1].Substring(0,8));
            //Console.WriteLine("log --pretty=\"%H\" --before=\"24:00 " + validate.setGitRep.gitArguments[1].Substring(0, 8) + "\" -1");
            return "log --pretty=\"%H\" --before=\"24:00 " + validate.setGitRep.gitArguments[1].Substring(0, 8) + "\" -1";
        }
        private string ArgumentsForSince()
        {
            //Console.WriteLine("StartDate: {0}", validate.setGitRep.gitArguments[0].Substring(0, 8));
            // Console.WriteLine("log --pretty=\"%H\" --before=\"00:00 " + validate.setGitRep.gitArguments[0].Substring(0, 8) + "\" -1");
            return "log --pretty=\"%H\" --before=\"00:00 " + validate.setGitRep.gitArguments[0].Substring(0, 8) + "\" -1";
        }
        private string BuildArg(string commitSince, string commitBefore)
        {
            //Console.WriteLine("diff --numstat " + commitSince.Substring(0, 10) + ".." + commitBefore.Substring(0, 10));
            return "diff --numstat " + commitSince.Substring(0, 10) + ".." + commitBefore.Substring(0, 10);
        }
        public string ProcessDiff()
        {
            //Console.WriteLine("ComitSince 1:{0} i Comitbefore 2:{1}",ProcessInitialization(ArgumentsForSince()), ProcessInitialization(ArgumentsForBefore()));
            return ProcessInitialization(BuildArg(ProcessInitialization(ArgumentsForSince()), ProcessInitialization(ArgumentsForBefore())));
        }
        public char[] ConvertToChar(string arg)
        {
            return arg.ToCharArray();
        }
        public void Process()
        {
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;

            if (encoding.GetMaxByteCount(ConvertToChar(ProcessDiff()).Length) < int.MaxValue)
            {
                {
                    byte[] byteArray = encoding.GetBytes(ProcessDiff());

                    using (MemoryStream memStream = new MemoryStream(byteArray))
                    {
                        using (FileStream fileStream = new FileStream("C:\\Testy_C#\\test.1.txt", FileMode.Create))
                        {
                            memStream.CopyTo(fileStream);
                        }
                    }
                    string myString = new string(encoding.GetChars(byteArray));               //przepisanie bajtow do stringa
                    Console.WriteLine(myString);
                }
            }
        }
    }
}
