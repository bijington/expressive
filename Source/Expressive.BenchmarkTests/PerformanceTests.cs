using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Expressive;
using System;

namespace Expressive.BenchmarkTests
{
    [SimpleJob(RuntimeMoniker.Net48, baseline: true)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.Net60)]
    //[SimpleJob(RuntimeMoniker.Mono)]
    //[RPlotExporter]
    public class PerformanceTests
    {
        private Expression expression;

        [GlobalSetup]
        public void Setup()
        {
            this.expression = new Expression(@"[Reminders.RbEmailOn]==true
AND
[Reminders.TimeReminderAfternoon] == string(#Now#, 'HH:mm')
AND
((DaysBetween([Web RA Call 2 After package has arrived.DateSnoozeCall],#Today#)>=0 OR
[Web RA Call 2 After package has arrived.DateSnoozeCall]==Null)
OR
(DaysBetween([Web RA Call 2 After package has arrived.DateSnoozeCallStart],#Today#)<=0 OR
[Web RA Call 2 After package has arrived.DateSnoozeCallStart]==Null))
AND
((DayOf([PO.mxDisplayDate])!=DayOf(#Today#) OR
DayOf([PO.mxDisplayDate])==Null OR
(DayOf([PO.mxDisplayDate])==DayOf(#Today#) AND
MinutesBetween(SubString([PO.mxDisplayDate],11,8), #12:00:00#)>=0))
AND
(DayOf([Respiration3.mxDisplayDate])!=DayOf(#Today#) OR
DayOf([Respiration3.mxDisplayDate])==Null OR
(DayOf([Respiration3.mxDisplayDate])==DayOf(#Today#) AND
MinutesBetween(SubString([Respiration3.mxDisplayDate],11,8), #12:00:00#)>=0))
AND
(DayOf([TP.mxDisplayDate])!=DayOf(#Today#) OR
DayOf([TP.mxDisplayDate])==Null OR
(DayOf([TP.mxDisplayDate])==DayOf(#Today#) AND
MinutesBetween(SubString([TP.mxDisplayDate],11,8), #12:00:00#)>=0))
AND
(DayOf([BP.mxDisplayDate])!=DayOf(#Today#) OR
DayOf([BP.mxDisplayDate])==Null OR
(DayOf([BP.mxDisplayDate])==DayOf(#Today#) AND
MinutesBetween(SubString([BP.mxDisplayDate],11,8), #12:00:00#)>=0))
AND
(DayOf([PF2.mxDisplayDate])!=DayOf(#Today#) OR
DayOf([PF2.mxDisplayDate])==Null OR
(DayOf([PF2.mxDisplayDate])==DayOf(#Today#) AND
MinutesBetween(SubString([PF2.mxDisplayDate],11,8), #12:00:00#)>=0))
AND
(DayOf([UP.mxDisplayDate])!=DayOf(#Today#) OR
DayOf([UP.mxDisplayDate])==Null OR
(DayOf([UP.mxDisplayDate])==DayOf(#Today#) AND
MinutesBetween(SubString([UP.mxDisplayDate],11,8), #12:00:00#)>=0))
AND
(DayOf([DB.mxDisplayDate])!=DayOf(#Today#) OR
DayOf([DB.mxDisplayDate])==Null OR
(DayOf([DB.mxDisplayDate])==DayOf(#Today#) AND
MinutesBetween(SubString([DB.mxDisplayDate],11,8), #12:00:00#)>=0))
AND
(DayOf([KT.mxDisplayDate])!=DayOf(#Today#) OR
DayOf([KT.mxDisplayDate])==Null OR
(DayOf([KT.mxDisplayDate])==DayOf(#Today#) AND
MinutesBetween(SubString([KT.mxDisplayDate],11,8), #12:00:00#)>=0))
AND
(DayOf([ECG.mxDisplayDate])!=DayOf(#Today#) OR
DayOf([ECG.mxDisplayDate])==Null OR
(DayOf([ECG.mxDisplayDate])==DayOf(#Today#) AND
MinutesBetween(SubString([ECG.mxDisplayDate],11,8), #12:00:00#)>=0)))
AND
[Logistics.Chk_StudyComplete]!=true", ExpressiveOptions.IgnoreCaseForParsing);
        }

        [Benchmark]
        public object Evaluate() => this.expression.Evaluate(new Provider());
    }
}

public class Provider : IVariableProvider
{
    public bool TryGetValue(string variableName, out object value)
    {
        if (variableName == "Reminders.RbEmailOn")
        {
            value = false;
            return true;
        }

        throw new InvalidOperationException();
    }
}
