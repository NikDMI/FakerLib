using System;
using System.Collections.Generic;
using System.Text;

namespace TestFaker
{
    public class SimpleClassDTO
    {
        public int _int;
        public short _short;
        public byte _byte;
        public bool _bool;
        public float _float;
        public double _double;
        public string _string;
        public SimpleClassDTO _recursion;
        public int _field { get; set; }
        public int _readOnlyField { get; }
        public List<int> _listInt;
        public ComplexRecursionDTO _complexRecursion;
    }


    public class ComplexDTO
    {
        public SimpleClassDTO _simpleDTO;
    }


    public class ComplexRecursionDTO
    {
        public ComplexDTO _complexDTO;
    }


    public class NotDTO
    {
        private NotDTO() { }
    }
}
