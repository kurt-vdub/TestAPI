using System;
using System.Text.RegularExpressions;

namespace SerkoTestAPI.Tests
{
    public static class TestData
    {

        private static readonly string TestValueRaw = @"Hi Yvaine,
            Please create an expense claim for the below. Relevant details are marked up as requested…
            <expense><cost_centre>DEV002</cost_centre> <total>890.55</total><payment_method>personal
            card</payment_method>
            </expense>
            From: Ivan Castle
            Sent: Friday, 16 February 2018 10:32 AM <Antoine.Lloyd@example.com>
            To: Antoine Lloyd 
            Subject: test
            Hi Antoine,
            Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our <description>development
            team’s project end celebration dinner</description> on <date>Tuesday 27 April 2017</date>. We expect to
            arrive around 7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.
            Regards,
            Ivan";

        public static readonly string TestValue = Regex.Replace(TestValueRaw, @"\s+", " ");
    }
}
