﻿using SFA.DAS.FrameworkHelpers;
using System.Collections.Generic;

namespace SFA.DAS.EPAO.UITests.Project.Helpers.SqlHelpers;

internal static class EPAOCAInUseUlns
{
    private static readonly List<string> _ulns = ["1"];

    internal static string GetInUseUln() { lock (_ulns) { return _ulns.ToString(","); } }

    internal static void RemoveInUseUln(string uln) => _ulns.Remove(uln);

    internal static bool IsNotInUseUln(string uln)
    {
        lock (_ulns)
        {
            if (!(_ulns.Contains(uln)))
            {
                _ulns.Add(uln);
                return true;
            }

            return false;
        }
    }
}