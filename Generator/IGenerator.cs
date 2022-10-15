﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib.Generator
{
    public interface IGenerator
    {
        //Generates values according to type of var
        internal T GenerateValue<T>();
    }
}