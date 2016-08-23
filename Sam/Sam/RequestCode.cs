using System;
using System.Collections.Generic;
using System.Text;

namespace Sam
{
    public sealed class RequestCode
    {
        private RequestCode() {}

        public static readonly string mcaStart = "mcaStart";
        public static readonly string mcaPause = "mcaPause";
        public static readonly string mcaClear = "mcaClear";
        public static readonly string mcaStop = "mcaStop";
        public static readonly string mcaSave = "mcaSave";
        public static readonly string mcaPREAL = "mcaPREAL";
        public static readonly string mcaPLIVE = "mcaPLIVE";

        public static readonly string setSample = "setSample";
        public static readonly string setPosition = "setPosition";
        public static readonly string setPathFile = "setPathFile";
        public static readonly string setPathFileBackup = "setPathFileBackup";
        public static readonly string setComments = "setComments";
        public static readonly string setIrrCode = "setIrrCode";
        public static readonly string setLockOn = "setLockOn";
        public static readonly string setLockOff = "setLockOff";
        public static readonly string setFileNaming = "setFileNaming";
        public static readonly string setSampleAll = "setSampleAll";



    }
}
