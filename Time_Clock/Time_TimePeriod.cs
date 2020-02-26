using System;
using System.Diagnostics.CodeAnalysis;

namespace Time_TimePeriod
{
    public struct Time : IComparable<Time>, IEquatable<Time>
    {
        //define variables and make them immutable
        private static int _Rest = 0;
        public byte _Minutes { get; }
        public byte _Seconds { get; }
        public byte _Hours { get; }
        public Time(byte hours = 0, byte minutes = 0, byte seconds = 0)
        {
            _Seconds = Check(seconds);
            _Minutes = Check(Convert.ToByte(minutes + _Rest)); _Rest = 0;
            _Hours = HourCheck(Convert.ToByte(hours + _Rest)); _Rest = 0;
        }
        //method to check if input is correct
        public static byte Check(byte input)
        {
            if (input < 0)
                throw new Exception("Incorrect input: negative values");
            else if (input > 255)
                throw new OverflowException();
            else if (input < 60)
                return input;
            else
            {
                while (input >= 60)
                {
                    _Rest = _Rest + 1;
                    input = Convert.ToByte(input - 60);
                }
                return input;
            }
        }
        public static byte HourCheck(byte input)
        {
            if (input < 0)
                throw new Exception("Incorrect input: negative values");
            else if (input >= 24)
                throw new Exception("Hours value can't be bigger than 24");
            else return input;
        }
        //print time as string
        public override string ToString() => $"{_Hours.ToString("D2")}:{_Minutes.ToString("D2")}:{_Seconds.ToString("D2")}";
        //implements Time.Equals
        public bool Equals(Time time)
        {
            if (time == null)
                return false;
            return (_Hours == time._Hours) && (_Minutes == time._Minutes) && (_Seconds == time._Seconds);
        }
        public override int GetHashCode() => _Hours.GetHashCode() ^ _Minutes.GetHashCode() ^ _Seconds.GetHashCode();
        //implement Time.CompareTo
        public int CompareTo(Time time)
        {
            if (time == null) return 1;
            Time time1 = time;
            if (time1 != null)
                return ToString().CompareTo(time1.ToString());
            else
                throw new Exception("Object is not a Time");
        }
        public static bool operator ==(Time time1, Time time2) => time1.Equals(time2);

        public static bool operator !=(Time time1, Time time2) => !(time1 == time2);
        public static bool operator <(Time time1, Time time2)
        {
            if (time1._Hours == time2._Hours)
            {
                if (time1._Minutes == time2._Minutes)
                    return time1._Seconds < time2._Seconds;
                else return time1._Minutes < time2._Minutes;
            }
            else return time1._Hours < time2._Hours;
        }
        public static bool operator <=(Time time1, Time time2)
        {
            if (time1._Hours == time2._Hours)
            {
                if (time1._Minutes == time2._Minutes)
                    return time1._Seconds <= time2._Seconds;
                else return time1._Minutes <= time2._Minutes;
            }
            else return time1._Hours <= time2._Hours;
        }
        public static bool operator >(Time time1, Time time2)
        {
            if (time1._Hours == time2._Hours)
            {
                if (time1._Minutes == time2._Minutes)
                    return time1._Seconds > time2._Seconds;
                else return time1._Minutes > time2._Minutes;
            }
            else return time1._Hours > time2._Hours;
        }
        public static bool operator >=(Time time1, Time time2)
        {
            if (time1._Hours == time2._Hours)
            {
                if (time1._Minutes == time2._Minutes)
                    return time1._Seconds >= time2._Seconds;
                else return time1._Minutes >= time2._Minutes;
            }
            else return time1._Hours >= time2._Hours;
        }
        //arithmetic
        public static Time operator +(Time time, TimePeriod tp) => new Time(Convert.ToByte(time._Hours + tp._Hours), Convert.ToByte(time._Minutes + tp._Minutes), Convert.ToByte(time._Seconds + tp._Seconds));
        public Time Plus(TimePeriod tp) => new Time(Convert.ToByte(_Hours + tp._Hours), Convert.ToByte(_Minutes + tp._Minutes), Convert.ToByte(_Seconds + tp._Seconds));
        public static Time Plus(Time time, TimePeriod tp) => new Time(Convert.ToByte(time._Hours + tp._Hours), Convert.ToByte(time._Minutes + tp._Minutes), Convert.ToByte(time._Seconds + tp._Seconds));

        public static Time operator -(Time time, TimePeriod tp) => new Time(Convert.ToByte(time._Hours - tp._Hours), Convert.ToByte(time._Minutes - tp._Minutes), Convert.ToByte(time._Seconds - tp._Seconds));
        public Time Minus(TimePeriod tp) => new Time(Convert.ToByte(_Hours - tp._Hours), Convert.ToByte(_Minutes - tp._Minutes), Convert.ToByte(_Seconds - tp._Seconds));
        public static Time Minus(Time time, TimePeriod tp) => new Time(Convert.ToByte(time._Hours - tp._Hours), Convert.ToByte(time._Minutes - tp._Minutes), Convert.ToByte(time._Seconds - tp._Seconds));

    }
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        //define variables and make them immutable
        private static int _Rest = 0;
        public new byte _Minutes { get; }
        public new byte _Seconds { get; }
        public new byte _Hours { get; }
        public TimePeriod(byte hours = 0, byte minutes = 0, byte seconds = 0)
        {
            _Seconds = Check(seconds);
            _Minutes = Check(Convert.ToByte(minutes + _Rest)); _Rest = 0;
            byte tempHours = Convert.ToByte(hours + _Rest); _Rest = 0;
            if (tempHours < 0)
                throw new Exception("Incorrect input: negative values");
            else if (tempHours > 255)
                throw new Exception("Incorrect input: max value is 255");
            _Hours = tempHours;
        }

        //method to check if input is correct
        public static byte Check(byte input)
        {
            if (input < 0)
                throw new Exception("Incorrect input: negative values");
            else if (input > 255)
                throw new Exception("Incorrect input: max value is 255");
            else if (input < 60)
                return input;
            else
            {
                while (input >= 60)
                {
                    _Rest = _Rest + 1;
                    input = Convert.ToByte(input - 60);
                }
                return input;
            }
        }
        public static byte HourCheck(byte input)
        {
            if (input < 0)
                throw new Exception("Incorrect input: negative values");
            else if (input >= 24)
                throw new Exception("Hours value can't be bigger than 24");
            else return input;
        }
        public override string ToString() => $"{_Hours.ToString("D2")}:{_Minutes.ToString("D2")}:{_Seconds.ToString("D2")}";

        //print time as number of seconds
        public long ToLong() => (_Hours * 3600) + (_Minutes * 60) + _Seconds;
        //implement TimePeriod.Equals
        public bool Equals(TimePeriod tp)
        {
            if (tp == null)
                return false;
            return (_Hours == tp._Hours) && (_Minutes == tp._Minutes) && (_Seconds == tp._Seconds);
        }
        public override int GetHashCode() => _Hours.GetHashCode() ^ _Minutes.GetHashCode() ^ _Seconds.GetHashCode();
        //implement TimePeriod.CompareTo
        public int CompareTo(TimePeriod tp)
        {
            if (tp == null) return 1;
            TimePeriod tp1 = tp;
            if (tp1 != null)
                return ToString().CompareTo(tp1.ToString());
            else
                throw new Exception("Object is not a TimePeriod");
        }
        public static bool operator ==(TimePeriod tp1, TimePeriod tp2)
        {
            if (tp1 == null)
                return tp2 == null;
            return tp1.Equals(tp2);
        }
        public static bool operator !=(TimePeriod tp1, TimePeriod tp2) => !(tp1 == tp2);
        public static bool operator <(TimePeriod tp1, TimePeriod tp2)
        {
            if (tp1._Hours == tp2._Hours)
            {
                if (tp1._Minutes == tp2._Minutes)
                    return tp1._Seconds < tp2._Seconds;
                else return tp1._Minutes < tp2._Minutes;
            }
            else return tp1._Hours < tp2._Hours;
        }
        public static bool operator <=(TimePeriod tp1, TimePeriod tp2)
        {
            if (tp1._Hours == tp2._Hours)
            {
                if (tp1._Minutes == tp2._Minutes)
                    return tp1._Seconds <= tp2._Seconds;
                else return tp1._Minutes <= tp2._Minutes;
            }
            else return tp1._Hours <= tp2._Hours;
        }
        public static bool operator >(TimePeriod tp1, TimePeriod tp2)
        {
            if (tp1._Hours == tp2._Hours)
            {
                if (tp1._Minutes == tp2._Minutes)
                    return tp1._Seconds > tp2._Seconds;
                else return tp1._Minutes > tp2._Minutes;
            }
            else return tp1._Hours > tp2._Hours;
        }
        public static bool operator >=(TimePeriod tp1, TimePeriod tp2)
        {
            if (tp1._Hours == tp2._Hours)
            {
                if (tp1._Minutes == tp2._Minutes)
                    return tp1._Seconds >= tp2._Seconds;
                else return tp1._Minutes >= tp2._Minutes;
            }
            else return tp1._Hours >= tp2._Hours;
        }
        //arithmetic
        public static TimePeriod operator +(TimePeriod tp1, TimePeriod tp2) => new TimePeriod(Convert.ToByte(tp1._Hours + tp2._Hours), Convert.ToByte(tp1._Minutes + tp2._Minutes), Convert.ToByte(tp1._Seconds + tp2._Seconds));
        public TimePeriod Plus(TimePeriod tp) => new TimePeriod(Convert.ToByte(_Hours + tp._Hours), Convert.ToByte(_Minutes + tp._Minutes), Convert.ToByte(_Seconds + tp._Seconds));
        public static TimePeriod Plus(TimePeriod tp1, TimePeriod tp2) => new TimePeriod(Convert.ToByte(tp1._Hours + tp2._Hours), Convert.ToByte(tp1._Minutes + tp2._Minutes), Convert.ToByte(tp1._Seconds + tp2._Seconds));

        public static TimePeriod operator -(TimePeriod tp1, TimePeriod tp2) => new TimePeriod(Convert.ToByte(tp1._Hours - tp2._Hours), Convert.ToByte(tp1._Minutes - tp2._Minutes), Convert.ToByte(tp1._Seconds - tp2._Seconds));
        public TimePeriod Minus(TimePeriod tp) => new TimePeriod(Convert.ToByte(_Hours - tp._Hours), Convert.ToByte(_Minutes - tp._Minutes), Convert.ToByte(_Seconds - tp._Seconds));
        public static TimePeriod Minus(TimePeriod tp1, TimePeriod tp2) => new TimePeriod(Convert.ToByte(tp1._Hours - tp2._Hours), Convert.ToByte(tp1._Minutes - tp2._Minutes), Convert.ToByte(tp1._Seconds - tp2._Seconds));

    }
}
