using System.Collections;
using System.Collections.Generic;

namespace Yaroyan.SproutWork.Domain.Model.SaveData
{
    public class Session
    {
        readonly SessionId _id;
        // 派生元のSession
        readonly SessionId _previousSessionId;
    }
}
