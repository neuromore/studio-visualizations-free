﻿/* Copyright (c) 2020 ExT (V.Sigalkin) */

using extOSC.Core.Events;

namespace extOSC.Core
{
    public interface IOSCBind
    {
        #region Public Vars

        OSCEventMessage Callback { get; }

        string ReceiverAddress { get; }

        #endregion
    }
}