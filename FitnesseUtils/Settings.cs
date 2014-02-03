using System;

namespace FitnesseUtils
{
    public class Settings
    {
        public string[] Args { get; set; }

        public Settings(string[] args)
        {
            Args = args;
            ValidationError = "";
        }

        public bool Load()
        {
            return ProcessArgs();
        }

        private bool ProcessArgs()
        {
            bool ret = true;

            string flag = "";

            foreach (string arg in Args)
            {
                if (arg.StartsWith("-"))
                {
                    flag = arg;

                    switch (flag)
                    {
                        case "-c":
                            Clear = true;
                            break;
                        case "-g":
                            Get = true;
                            break;
                    }
                }
                else
                {
                    switch (flag)
                    {
                        case "-r":
                            RemoteDirectory = arg;
                            break;
                        case "-l":
                            LocalDirectory = arg;
                            break;
                        default:
                            if (arg.StartsWith("-"))
                            {
                                ret = false;
                                AddValidationError("Unrecognised flag '{0}'", arg);
                            }
                            break;
                    }
                }
            }

            return ret;
        }

        public bool Get { get; set; }

        public void AddValidationError(string format, params object[] args)
        {
            ValidationError += String.Format(format, args) + Environment.NewLine;
        }

        public string ValidationError { get; set; }

        public string RemoteDirectory { get; set; }

        public string LocalDirectory { get; set; }

        public bool Clear { get; set; }
    }
}