// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.IO
{
    // Thrown when trying to access a file that doesn't exist on disk.
    [Serializable]
    public class FileNotFoundException : IOException
    {
        private String _fileName;  // The name of the file that isn't found.
        private String _fusionLog;  // fusion log (when applicable)

        public FileNotFoundException()
            : base(SR.IO_FileNotFound)
        {
            HResult = __HResults.COR_E_FILENOTFOUND;
        }

        public FileNotFoundException(String message)
            : base(message)
        {
            HResult = __HResults.COR_E_FILENOTFOUND;
        }

        public FileNotFoundException(String message, Exception innerException)
            : base(message, innerException)
        {
            HResult = __HResults.COR_E_FILENOTFOUND;
        }

        public FileNotFoundException(String message, String fileName) : base(message)
        {
            HResult = __HResults.COR_E_FILENOTFOUND;
            _fileName = fileName;
        }

        public FileNotFoundException(String message, String fileName, Exception innerException)
            : base(message, innerException)
        {
            HResult = __HResults.COR_E_FILENOTFOUND;
            _fileName = fileName;
        }

        protected FileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            // Base class constructor will check info != null.

            _fileName = info.GetString("FileNotFound_FileName");
            try
            {
                _fusionLog = info.GetString("FileNotFound_FusionLog");
            }
            catch
            {
                _fusionLog = null;
            }
        }

        public override String Message
        {
            get
            {
                if (_message == null)
                {
                    if ((_fileName == null) &&
                        (HResult == __HResults.COR_E_EXCEPTION))
                    {
                        _message = SR.IO_FileNotFound;
                    }
                    else if (_fileName != null)
                    {
                        _message = SR.Format(SR.IO_FileNotFound_FileName, _fileName);
                    }
                }
                return _message;
            }
        }


        public String FileName
        {
            get { return _fileName; }
        }

        public String FusionLog
        {
            get { return _fusionLog; }
        }

        public override String ToString()
        {
            String s = GetType().ToString() + ": " + Message;

            if (_fileName != null && _fileName.Length != 0)
                s += Environment.NewLine + SR.Format(SR.IO_FileName_Name, _fileName);

            if (InnerException != null)
                s = s + " ---> " + InnerException.ToString();

            if (StackTrace != null)
                s += Environment.NewLine + StackTrace;

            return s;
        }
    }
}
